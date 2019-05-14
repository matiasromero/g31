using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HomeSwitchHome.API.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            if (actionContext.ModelState.IsValid == false)
            {
                //throw new ComponentException(System.Net.HttpStatusCode.BadRequest, actionContext.ModelState);
                actionContext.Result = new BadRequestObjectResult(actionContext.ModelState);
            }
        }
    }
}