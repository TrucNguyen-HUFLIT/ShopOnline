using ReflectionIT.Mvc.Paging;
using ShopOnline.Core.Entities;
using ShopOnline.Core.Models.Staff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace ShopOnline.Business.Staff
{
    public interface IStaffBusiness
    {
        Task<IPagedList<StaffInfor>> GetListStaffAsync(string sortOrder, string currentFilter, string searchString, int? page);
        Task CreateAsync(StaffCreate staffCreate);
        StaffEdit GetStaffById(int id);
        Task<bool> EditAsync(StaffEdit staffEdit);
    }
}
