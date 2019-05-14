using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace HomeSwitchHome.API.Infrastructure
{
    public class NhUnitOfWorkFilter : IActionFilter
    {
        public IUnitOfWork UnitOfWork { get; set; }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            UnitOfWork = context.HttpContext.RequestServices.GetService<IUnitOfWork>();
            UnitOfWork.BeginTransaction();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            UnitOfWork = context.HttpContext.RequestServices.GetService<IUnitOfWork>();
            if (context.Exception == null)
            {
                UnitOfWork.Commit();
            }
            else
            {
                UnitOfWork.Rollback();
            }

            UnitOfWork.Dispose();
        }
    }
}