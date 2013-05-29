using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


    public class FillQueryStringToRouteDataValuesAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            //store querystring to routedata
            var querystring = filterContext.HttpContext.Request.QueryString;
            var routeValueDictionary = filterContext.Controller.ControllerContext.RouteData.Values;

            for (int i = 0; i < querystring.Count; i++)
            {
                var value = querystring[i];
                var key = querystring.Keys[i];
                if (string.IsNullOrEmpty(value))
                {
                    routeValueDictionary.Remove(key);
                    continue;
                }

                routeValueDictionary[querystring.Keys[i]] = querystring[i];
            }
        }
    }

