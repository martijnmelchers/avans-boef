using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Models;

namespace Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly ApplicationDbContext _db;

        protected BaseController(ApplicationDbContext db)
        {
            _db = db;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if ((await next.Invoke()).Exception == null)
                await _db.SaveChangesAsync();

            TempData.Keep();
            _db.Dispose();
        }

        #region Access Token Management

        public bool HasAccessToken()
        {
            return TempData["AccessToken"] != null;
        }

        public void SetAccessToken(string accessToken)
        {
            TempData["AccessToken"] = accessToken;
        }

        public string GetAccessToken()
        {
            return TempData["AccessToken"].ToString();
        }

        #endregion
    }
}