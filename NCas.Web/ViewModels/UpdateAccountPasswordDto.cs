using System.ComponentModel.DataAnnotations;

namespace NCas.Web.ViewModels
{
    /// <summary>修改
    /// </summary>
    public class UpdateAccountPasswordDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Code不能为空")]
        [StringLength(50, ErrorMessage = "Code应在50字符以内")]
        public string Code { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "账号密码不能为空")]
        public string Password { get; set; }
    }
}