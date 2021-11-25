using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using ShopOnline.Core;
using ShopOnline.Core.Entities;
using ShopOnline.Core.Exceptions;
using ShopOnline.Core.Helpers;
using ShopOnline.Core.Models.Account;
using ShopOnline.Core.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using static ShopOnline.Core.Models.Enum.AppEnum;

namespace ShopOnline.Business.Logic
{
    public class UserBusiness : IUserBusiness
    {
        private readonly MyDbContext _context;

        public UserBusiness(MyDbContext context)
        {
            _context = context;
        }

        public async Task<ClaimsPrincipal> LoginAsync(AccountLogin accountLogin)
        {
            Expression<Func<IBaseUserEntity, BaseInforAccount>> selectBaseInforAccount = x => new BaseInforAccount
            {
                Email = x.Email,
                Password = x.Password,
                FullName = x.FullName,
                Avatar = x.Avatar,
                TypeAcc = x.TypeAcc,
            };

            var inforAccount = await _context.Customers.Where(x => x.Email == accountLogin.Email && !x.IsDelete)
                                        .Select(selectBaseInforAccount)
                                        .FirstOrDefaultAsync();
            if (inforAccount == null)
                inforAccount = await _context.Staffs.Where(x => x.Email == accountLogin.Email && !x.IsDelete)
                                        .Select(selectBaseInforAccount)
                                        .FirstOrDefaultAsync();
            if (inforAccount == null)
                inforAccount = await _context.Shippers.Where(x => x.Email == accountLogin.Email && !x.IsDelete)
                                        .Select(selectBaseInforAccount)
                                        .FirstOrDefaultAsync();
            if (inforAccount == null)
                throw new UserFriendlyException(ErrorCode.WrongEmail);

            string password;
            bool loginSuccess = false;

            switch (inforAccount.TypeAcc)
            {
                case TypeAcc.Customer:
                    HashPasswordHelper.HashPasswordStrategy = new HashMD5Strategy();
                    password = HashPasswordHelper.DoHash(accountLogin.Password);
                    if (inforAccount.Password == password)
                        loginSuccess = true;
                    break;
                case TypeAcc.Shipper:
                    HashPasswordHelper.HashPasswordStrategy = new HashSHA1Strategy();
                    password = HashPasswordHelper.DoHash(accountLogin.Password);
                    if (inforAccount.Password == password)
                        loginSuccess = true;
                    break;
                default: // Admin || Staff
                    HashPasswordHelper.HashPasswordStrategy = new HashSHA256Strategy();
                    password = HashPasswordHelper.DoHash(accountLogin.Password);
                    if (inforAccount.Password == password)
                        loginSuccess = true;
                    break;
            }

            if (!loginSuccess)
                throw new UserFriendlyException(ErrorCode.WrongPassword);

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, inforAccount.Email),
                    new Claim(ClaimTypes.Name, inforAccount.FullName),
                    new Claim("Avatar", inforAccount.Avatar),
                    new Claim(ClaimTypes.Role, inforAccount.TypeAcc.ToString()),
                };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            return claimsPrincipal;
        }

        public async Task<bool> RegisterAsync(AccountRegister accountRegister)
        {
            bool isExistingEmail = await _context.Customers.AnyAsync(x => x.Email == accountRegister.Email);

            if (isExistingEmail)
            {
                throw new UserFriendlyException($"Email '{accountRegister.Email}' already exists!");
            }

            HashPasswordHelper.HashPasswordStrategy = new HashMD5Strategy();
            var newAccountCustomer = new CustomerEntity()
            {
                FullName = accountRegister.FullName,
                Email = accountRegister.Email,
                Password = HashPasswordHelper.DoHash(accountRegister.Password),
                TypeAcc = TypeAcc.Customer
            };

            _context.Customers.Add(newAccountCustomer);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task ResetPasswordAsync(string email)
        {
            bool isExistingEmail = await _context.Customers.AnyAsync(x => x.Email == email);

            if (!isExistingEmail)
            {
                throw new UserFriendlyException($"Email '{email}' does not exist!");
            }

            var accountReset = await _context.Customers.Where(x => x.Email == email).FirstOrDefaultAsync();
            string newPassword = AccountHelper.GetNewRandomPassword();

            HashPasswordHelper.HashPasswordStrategy = new HashMD5Strategy();
            accountReset.Password = HashPasswordHelper.DoHash(newPassword);

            MimeMessage message = new();

            MailboxAddress from = new("SHOES SHOP", "shop.online.huflit@gmail.com");
            message.From.Add(from);

            MailboxAddress to = new(accountReset.FullName, accountReset.Email);
            message.To.Add(to);

            message.Subject = "Cấp lại mật khẩu thành công";
            BodyBuilder bodyBuilder = new()
            {
                HtmlBody = $"<h1>Mật khẩu của bạn đã được reset, mật khẩu mới: {newPassword}  </h1>",
                TextBody = "Mật Khẩu của bạn đã được thay đổi "
            };
            message.Body = bodyBuilder.ToMessageBody();

            SmtpClient client = new();
            //connect (smtp address, port , true)
            await client.ConnectAsync("smtp.gmail.com", 465, true);
            await client.AuthenticateAsync("shop.online.huflit@gmail.com", "ShopOnlineHuflit");

            await client.SendAsync(message);
            await client.DisconnectAsync(true);
            client.Dispose();

            _context.Customers.Update(accountReset);
            await _context.SaveChangesAsync();
        }
    }
}
