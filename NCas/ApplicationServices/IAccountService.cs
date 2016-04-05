using NCas.QueryServices.Dtos;

namespace NCas.ApplicationServices
{
    public interface IAccountService
    {
        /// <summary>账号与密码是否匹配
        /// </summary>
        bool AccountMatch(string accountName, string password);
    }
}
