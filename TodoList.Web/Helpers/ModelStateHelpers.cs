using System.Collections.Generic;
using System.Linq;
using System.Web.Http.ModelBinding;

namespace TodoList.Web.Helpers
{
    public static class ModelStateHelpers
    {
        public static IEnumerable<string> GetErrorsFromModelState(ModelStateDictionary modelState)
        {
            return modelState.SelectMany(x => x.Value.Errors.Select(error => error.ErrorMessage));
        }
    }
}