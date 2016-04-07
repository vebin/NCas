using ECommon.IO;
using ENode.Commanding;

namespace NCas.Web.Extensions
{

    public static class AsyncTaskResultExtensions
    {
        public static bool IsSuccess(this AsyncTaskResult<CommandResult> result)
        {
            if (result.Status != AsyncTaskStatus.Success || result.Data.Status == CommandStatus.Failed)
            {
                return false;
            }
            return true;
        }

        public static string GetErrorMessage(this AsyncTaskResult<CommandResult> result)
        {
            if (result.Status != AsyncTaskStatus.Success || result.Data.Status == CommandStatus.Failed)
            {
                if (result.ErrorMessage != null)
                {
                    return result.ErrorMessage;
                }
                return result.Data.Result;
            }
            return null;
        }
    }
}