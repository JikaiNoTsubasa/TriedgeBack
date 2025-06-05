using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SBIDotnetUtils.Extensions;
using triedge_api.Global;

namespace triedge_api.JobControllers;

[ApiController]
public class TriController : Controller
{
    protected long _loggedUserId = -1;
    protected QueryableEx.Pagination? _pagination = null;
    protected QueryableEx.SearchQuery? _search = null;
    protected QueryableEx.OrderQuery? _orderby = null;

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var user = context.HttpContext.User;
        if (user is not null){
            var ident = user.Identity;
            if (ident is not null && ident.IsAuthenticated){
                var nameid = user.Claims.FirstOrDefault(c => c.Type.Equals("userid"));
                if (nameid != null){
                    _loggedUserId = long.Parse(nameid.Value);
                }
            }
        }

        // Check if page and limit parameters are provided, then fill the pagination object
        if(context.HttpContext.Request.Query.ContainsKey("page") && context.HttpContext.Request.Query.ContainsKey("limit"))
        {
            _pagination = new()
            {
                Page = int.Parse(context.HttpContext.Request.Query["page"]!),
                Limit = int.Parse(context.HttpContext.Request.Query["limit"]!)
            };
        }

        // Check if search parameters is provided, then fill the search object
        if(context.HttpContext.Request.Query.ContainsKey("search"))
        {
            _search = new()
            {
                Content = context.HttpContext.Request.Query["search"]
            };
        }

        // Check if orderby parameter is provided, then fill the orderby object
        if(context.HttpContext.Request.Query.ContainsKey("orderby"))
        {
            _orderby = new()
            {
                OrderBy = context.HttpContext.Request.Query["orderby"],
                Order = context.HttpContext.Request.Query["order"]
            };
        }

        base.OnActionExecuting(context);
    }

    [NonAction]
    public virtual ObjectResult Return(ApiResult result){
        if (result.Meta is not null){
            var dict = result.Meta.GenerateDictionary();
            foreach (var item in dict){
                HttpContext.Response.Headers.Append(item.Key, item.Value);
            }
        }
        return StatusCode(result.HttpCode, result.Content);
    }
}
