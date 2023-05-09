using Azure.Core;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net.NetworkInformation;
using System.Text;
using GameStoreApp.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http.Features;

namespace GameStoreApp.Filters
{
    public class LogRequestFilter : ActionFilterAttribute
    {
        /// <summary>
        /// Called by the ASP.NET MVC framework before the action method executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Get the HTTP request object from the filter context.
            var request = filterContext.HttpContext.Request;

            // Create a new anonymous object called 'log' that contains information about the request.
            var log = new
            {
                // Get the name of the controller that contains the action being executed.
                Controller = filterContext.ActionDescriptor.RouteValues["controller"],
                // Get the name of the action being executed.
                Action = filterContext.ActionDescriptor.RouteValues["action"],
                // Get the IP address of the client making the request as a string.
                IP = filterContext.HttpContext.Connection.RemoteIpAddress?.ToString(),
                // Get the current date and time in UTC format.
                DateTime = DateTime.UtcNow,
                // Get the name of the user making the request, if this is null, then assing "Guest"
                User = filterContext.HttpContext.User.Identity?.Name ?? "Guest" 
            };

            // Write the JSON representation of 'log' to the debug output window.
            Debug.WriteLine(JsonConvert.SerializeObject(log));
        }
    }
}
