using System.Linq;
using System.Web.Mvc;

namespace NCas.Web.Extensions
{
    public static class ModelStateDictionaryExtensions
    {
        public static string GetFirstError(this ModelStateDictionary modelState)
        {

            return
                // ReSharper disable once PossibleNullReferenceException
                modelState.FirstOrDefault(x => x.Value.Errors.Any()).Value.Errors.FirstOrDefault().ErrorMessage;
        }
    }
}