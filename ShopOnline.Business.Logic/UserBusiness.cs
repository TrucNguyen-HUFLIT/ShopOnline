using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using ShopOnline.Core;
using ShopOnline.Core.Entities;
using ShopOnline.Core.Exceptions;
using ShopOnline.Core.Helpers;
using ShopOnline.Core.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
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
            string password = AccountHelper.HashPassword(accountLogin.Password);
            bool isHaveCustomer = await _context.Customers
                .AnyAsync(x => x.Email == accountLogin.Email && x.Password == password && !x.IsDelete);

            var account = new 
            {
                FullName = "",
                Avatar = "",
                TypeAcc = TypeAcc.Customer,
            };

            if (isHaveCustomer)
            {
                account = await _context.Customers
                .Where(x => x.Email == accountLogin.Email && x.Password == password && !x.IsDelete)
                .Select(x=>new { x.FullName, x.Avatar, x.TypeAcc })
                .FirstOrDefaultAsync();
            }
            else
            {
                account = await _context.Staffs
                .Where(x => x.Email == accountLogin.Email && x.Password == password && !x.IsDelete)
                .Select(x => new { x.FullName, x.Avatar, x.TypeAcc })
                .FirstOrDefaultAsync();
            }

            if (account.TypeAcc != TypeAcc.Admin)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, accountLogin.Email),
                    new Claim(ClaimTypes.Name, account.FullName),
                    new Claim("Avatar", account.Avatar),
                    new Claim(ClaimTypes.Role, account.TypeAcc.ToString()),
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                return claimsPrincipal;
            }
            else
            {
                return null;
            }
        }
    
        public async Task<bool> RegisterAsync(AccountRegister accountRegister)
        {
            bool isExistingEmail = await _context.Customers.AnyAsync(x => x.Email == accountRegister.Email);
            
            if (isExistingEmail)
            {
                throw new UserFriendlyException($"Email '{accountRegister.Email}' already exists!");
            }

            var newAccountCustomer = new CustomerEntity()
            {
                FullName = accountRegister.FullName,
                Email = accountRegister.Email,
                Password = AccountHelper.HashPassword(accountRegister.Password),
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
            accountReset.Password = AccountHelper.HashPassword(newPassword);

            MimeMessage message = new();

            MailboxAddress from = new("H2T Moto", "h2t.moto.huflit@gmail.com");
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
            await client.AuthenticateAsync("h2t.moto.huflit@gmail.com", "H2tmotohuflit");
            
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
            client.Dispose();

            _context.Customers.Update(accountReset);
            await _context.SaveChangesAsync();
        }
    }
}
