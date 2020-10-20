using crmall.Domain.Commands.BaseResponse;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace crmall.Api.Filters
{

    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errorsInModelState = context.ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage)).ToArray();

                var errorResponse = new BaseResponse<string>();
                var errorModelList = new List<ErrorModel>();

                foreach (var error in errorsInModelState)
                {
                    var errorModel = new ErrorModel
                    {
                        Property = error.Key,
                        Message = error.Value
                    };
                    errorModelList.Add(errorModel);
                }
                errorResponse.Errors = errorModelList;
                errorResponse.Success = false;
                errorResponse.Message = "Campos inválidos.";
                context.Result = new BadRequestObjectResult(errorResponse);
                return;
            }

            await next();

            // after controller
        }
    }
}
