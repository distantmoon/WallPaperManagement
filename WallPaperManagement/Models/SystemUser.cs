using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace WallPaperManagement.Models
{
    public class SystemUser
    {
        public int SystemUserId { get; set; }

        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "用户角色")]
        public string UserRole { get; set; }
    }

   
}