using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.ModelBinding;

using NuxtTemplate.BLL.Results;

namespace NuxtTemplate.Web.Controllers
{
    public static class ControllerExtensions
    {
        public static IEnumerable<ResultMessage> ResultMessages(this ModelStateDictionary modelState)
        {
            foreach (var modelStateEntry in modelState)
                foreach (var modelError in modelStateEntry.Value.Errors)
                    yield return new ResultMessage
                    {
                        Status = ResultStatus.Error,
                        Message = modelError.ErrorMessage,
                        Property = modelStateEntry.Key
                    };
            yield break;
        }
    }
}
