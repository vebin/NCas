using System.ComponentModel.DataAnnotations;

namespace NCas.Web.ViewModels
{
    /// <summary>账号登陆Dto
    /// </summary>
    public class AccountLoginDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "账号不能为空")]
        public string AccountName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "密码不能为空")]
        public string Password { get; set; }

        [Required]
        public string CacheKey { get; set; }

        [Required]
        public string CallBackUrl { get; set; }
    }
}