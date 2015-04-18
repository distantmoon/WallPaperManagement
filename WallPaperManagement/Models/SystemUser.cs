using System.ComponentModel.DataAnnotations;

namespace WallPaperManagement.Models
{
    public class SystemUser : CommonModel
    {

        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "用户角色")]
        public string UserRole { get; set; }
    }
}