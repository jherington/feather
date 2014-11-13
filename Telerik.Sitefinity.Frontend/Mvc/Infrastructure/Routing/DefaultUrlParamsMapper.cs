﻿using System.Web.Mvc;
using System.Web.Routing;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Mvc;

namespace Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Routing
{
    /// <summary>
    /// Instances of this class call the default Sitefinity logic for mapping URL parameters to route data.
    /// </summary>
    internal class DefaultUrlParamsMapper : UrlParamsMapperBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultUrlParamsMapper"/> class.
        /// </summary>
        /// <param name="controller">The controller.</param>
        public DefaultUrlParamsMapper(ControllerBase controller)
            : base(controller)
        {
        }

        /// <inheritdoc />
        protected override bool TryMatchUrl(string[] urlParams, RequestContext requestContext)
        {
            var selfRouting = this.Controller as ISelfRoutingController;
            if (urlParams != null && selfRouting != null && selfRouting.TryMapRouteParameters(urlParams, requestContext))
            {
                return true;
            }

            var controllerName = requestContext.RouteData.Values[DynamicUrlParamActionInvoker.ControllerNameKey] as string;
            string actionName = null;
            if (requestContext.RouteData.Values.ContainsKey("action"))
            {
                actionName = requestContext.RouteData.Values["action"] as string;
                requestContext.RouteData.Values.Remove("action");
            }

            try
            {
                requestContext.RouteData.Values.Remove(DynamicUrlParamActionInvoker.ControllerNameKey);
                MvcRequestContextBuilder.SetRouteParameters(urlParams, requestContext, this.Controller as Controller, controllerName);
            }
            finally
            {
                if (actionName != null)
                {
                    requestContext.RouteData.Values["action"] = actionName;
                }
            }

            return true;
        }
    }
}
