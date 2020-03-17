using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Models;
using Newtonsoft.Json;

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

            _db.Dispose();
        }

        #region Cookie Extension


        /// <summary>  
        /// Returns if the cookie is present
        /// </summary>  
        /// <param name="key">Key </param>  
        /// <returns>string value</returns>
        public bool Present(string key)
        {
            return Request.Cookies.ContainsKey(key);
        }
        
        /// <summary>  
        /// Get the cookie  
        /// </summary>  
        /// <param name="key">Key </param>  
        /// <returns>string value</returns>  
        public string Get(string key)  
        {  
            return Request.Cookies[key];  
        }  
        /// <summary>  
        /// set the cookie  
        /// </summary>  
        /// <param name="key">key (unique indentifier)</param>  
        /// <param name="value">value to store in cookie object</param>  
        /// <param name="expireTime">expiration time</param>  
        public void Set(string key, string value, int? expireTime = null)  
        {
            var option = new CookieOptions
            {
                Expires = expireTime.HasValue
                    ? DateTime.Now.AddMinutes(expireTime.Value)
                    : DateTime.MaxValue
            };

            Response.Cookies.Append(key, value, option);  
        }  
        /// <summary>  
        /// Delete the key  
        /// </summary>  
        /// <param name="key">Key</param>  
        public void Remove(string key)  
        {  
            Response.Cookies.Delete(key);  
        } 
        

        #endregion
        
    }
}