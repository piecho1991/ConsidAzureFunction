using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ConsidAzureFunction.Other
{
    public class InternalServerErrorMessageResult : ObjectResult
    {
        public string Message { get; }

        public InternalServerErrorMessageResult(string message) : base(new HttpError(message))
        {
            this.Message = message ?? throw new ArgumentNullException(nameof(message));
        }

        public override Task ExecuteResultAsync(ActionContext context)
        {
            context.HttpContext.Response.StatusCode = 500;
            return base.ExecuteResultAsync(context);
        }
    }
}
