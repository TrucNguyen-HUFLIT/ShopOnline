using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using ShopOnline.Business.Staff;
using ShopOnline.Core;
using ShopOnline.Core.Entities;
using ShopOnline.Core.Exceptions;
using ShopOnline.Core.Helpers;
using ShopOnline.Core.Models.Enum;
using ShopOnline.Core.Models.Staff;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace ShopOnline.Business.Logic.Staff
{
    public class StaffBusiness : IStaffBusiness
    {
        private readonly MyDbContext _context;
        private readonly IWebHostEnvironment hostEnvironment;
        public StaffBusiness(MyDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this.hostEnvironment = hostEnvironment;
        }

        public async Task CreateAsync(StaffCreate staffCreate)
        {
            var email = await _context.Staffs.Where(x => x.Email == staffCreate.Email && !x.IsDeleted).Select(x => x.Email).FirstOrDefaultAsync();
            if (email == null)
            {
                HashPasswordHelper.HashPasswordStrategy = new HashSHA256Strategy();
                var staff = new StaffEntity
                {
                    FullName = staffCreate.FullName,
                    Address = staffCreate.Address,
                    Salary = staffCreate.Salary,
                    Email = staffCreate.Email,
                    Password = HashPasswordHelper.DoHash(staffCreate.Password),
                    PhoneNumber = staffCreate.PhoneNumber,
                    TypeAcc = staffCreate.TypeAcc,
                };
                if (staffCreate.UploadAvt == null)
                {
                    staffCreate.Avatar = "avatar-icon-images-4.jpg";
                }
                else
                {
                    string wwwRootPath = hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(staffCreate.UploadAvt.FileName);
                    string extension = Path.GetExtension(staffCreate.UploadAvt.FileName);
                    staff.Avatar = "/img/Avatar/" + fileName + extension;
                    string path1 = Path.Combine(wwwRootPath + "/img/Avatar/", fileName + extension);
                    using (var fileStream = new FileStream(path1, FileMode.Create))
                    {
                        await staffCreate.UploadAvt.CopyToAsync(fileStream);
                    }
                }
                _context.Staffs.Add(staff);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new UserFriendlyException(ErrorCode.EmailExisted);
            }

        }

        public async Task<IPagedList<StaffInfor>> GetListStaffAsync(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var listStaff = new List<StaffInfor>();
            var staffs = await _context.Staffs.Where(x => !x.IsDeleted).ToListAsync();
            if (staffs != null)
            {
                foreach (var staff in staffs)
                {
                    var staffInforForList = new StaffInfor
                    {
                        Id = staff.Id,
                        Email = staff.Email,
                        FullName = staff.FullName,
                        Avatar = staff.Avatar,
                        TypeAcc = staff.TypeAcc,
                        Address = staff.Address,
                        PhoneNumber = staff.PhoneNumber
                    };
                    listStaff.Add(staffInforForList);
                }

                if (!String.IsNullOrEmpty(searchString))
                {
                    listStaff = listStaff.Where(s => s.FullName.Contains(searchString)
                                            || s.Email.Contains(searchString)).ToList();
                }
                listStaff = sortOrder switch
                {
                    "name_desc" => listStaff.OrderByDescending(x => x.FullName).ToList(),
                    "name" => listStaff.OrderBy(x => x.FullName).ToList(),
                    "id_desc" => listStaff.OrderByDescending(x => x.Id).ToList(),
                    _ => listStaff.OrderBy(x => x.Id).ToList(),
                };
                int pageSize = 10;
                int pageNumber = (page ?? 1);
                return listStaff.ToPagedList(pageNumber, pageSize);
            }

            return null;
        }

        public StaffEdit GetStaffById(int id)
        {
            var staff = _context.Staffs.Where(x => x.Id == id && !x.IsDeleted).Select(x => new StaffEdit
            {
                Id = x.Id,
                FullName = x.FullName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                Address = x.Address,
                Avatar = x.Avatar,
                TypeAcc = x.TypeAcc
            }).FirstOrDefault();
            return staff;

        }

        public async Task<bool> EditAsync(StaffEdit staffEdit)
        {
            var staffEntity = await _context.Staffs.Where(x => x.Id == staffEdit.Id && !x.IsDeleted).FirstOrDefaultAsync();

            staffEntity.FullName = staffEdit.FullName;
            staffEntity.Address = staffEdit.Address;
            staffEntity.TypeAcc = staffEdit.TypeAcc;
            staffEntity.PhoneNumber = staffEdit.PhoneNumber;

            if (staffEdit.UploadAvt != null)
            {
                string wwwRootPath = hostEnvironment.WebRootPath;
                string fileName1;
                string extension1;

                fileName1 = Path.GetFileNameWithoutExtension(staffEdit.UploadAvt.FileName);
                extension1 = Path.GetExtension(staffEdit.UploadAvt.FileName);
                staffEntity.Avatar = fileName1 += extension1;
                string path1 = Path.Combine(wwwRootPath + "/img/Avatar/", fileName1);
                using (var fileStream = new FileStream(path1, FileMode.Create))
                {
                    await staffEdit.UploadAvt.CopyToAsync(fileStream);
                }
            }
            _context.Staffs.Update(staffEntity);
            await _context.SaveChangesAsync();
            return true;
        }

        public StaffEdit GetStaffByEmail(string email)
        {
            var staff = _context.Staffs.Where(x => x.Email == email && !x.IsDeleted).Select(x => new StaffEdit
            {
                Id = x.Id,
                FullName = x.FullName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                Address = x.Address,
                Avatar = x.Avatar,
                TypeAcc = x.TypeAcc
            }).FirstOrDefault();
            return staff;
        }

        public async Task<bool> DeleteStaffAsync(StaffInfor staffInfor)
        {
            var staff = await _context.Staffs.Where(x => x.Id == staffInfor.Id && !x.IsDeleted).FirstOrDefaultAsync();

            if (staff != null)
            {
                staff.IsDeleted = true;
                _context.Staffs.Update(staff);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
