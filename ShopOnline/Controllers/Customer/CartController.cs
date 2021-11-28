using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopOnline.Controllers.Customer
{
    public class CartController : Controller
    {

        [HttpPost]
        public async Task<IActionResult> AddProductToCardAsync()
        {

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddProductQuantityAsync()
        {

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> ReduceProductQuantityAsync()
        {

            return Ok();
        }




        public IActionResult Index()
        {
            return View();
        }

    }
}
