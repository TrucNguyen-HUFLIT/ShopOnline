using Microsoft.AspNetCore.Mvc;
using ShopOnline.Business.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ShopOnline.Controllers.Customer
{
    public class ClientController : Controller
    {
        private readonly IClientBusiness _clientBusiness;
        
        public ClientController(IClientBusiness clientBusiness)
        {
            _clientBusiness = clientBusiness;
        }

        [HttpGet]
        public async Task<IActionResult> HomeAsync()
        {
            var model = await _clientBusiness.GetProductForHomePageAsync();
            return View(model);
        }

    }
}
