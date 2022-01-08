﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopOnline.Business.Staff;
using ShopOnline.Core.Models;
using ShopOnline.Core.Models.Review;
using System;
using System.Threading.Tasks;
using static ShopOnline.Core.Models.Enum.AppEnum;

namespace ShopOnline.Controllers.Staff
{
    public class ReviewController : Controller
    {
        private readonly IReviewBusiness _reviewBusiness;

        public ReviewController(IReviewBusiness reviewBusiness)
        {
            _reviewBusiness = reviewBusiness;
        }

        [Authorize(Roles = ROLE.STAFF)]
        public async Task<IActionResult> ListReviewAsync(string sortOrder, string currentFilter, int? reviewStatus, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) || sortOrder.Equals("name") ? "name_desc" : "name";

            ViewBag.IdSortParm = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";

            if (reviewStatus != null) page = 1;
            reviewStatus ??= 0;
            ViewBag.CurrentFilter = reviewStatus;

            var enumReviewStatus = (ReviewStatus)reviewStatus;

            var model = new ReviewModel
            {
                ListProductDetail = await _reviewBusiness.GetListProductDetail(),
                ListCustomer = await _reviewBusiness.GetListCustomer(),
                ListReview = await _reviewBusiness.GetListReviewAsync(sortOrder, currentFilter, enumReviewStatus, page)
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveReview(int id)
        {
            await _reviewBusiness.ApproveReview(id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> RejectReview(int id)
        {
            await _reviewBusiness.RejectReview(id);
            return Ok();
        }

    }
}
