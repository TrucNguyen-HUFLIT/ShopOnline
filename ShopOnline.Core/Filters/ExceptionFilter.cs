using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.Core.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            //string actionName = context.ActionDescriptor.RouteValues.Values.ToArray()[0];
            //string controllerName = context.ActionDescriptor.RouteValues.Values.ToArray()[1];
            context.Result = new BadRequestObjectResult(context.Exception.Message);
        }
    }
}
