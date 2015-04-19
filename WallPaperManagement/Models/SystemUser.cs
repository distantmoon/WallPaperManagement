using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WallPaperManagement.Models
{
    public class SystemUser : CommonModel<SystemUser,long>
    {
        [Key]
        [Column("SystemUserId")]
        public int Id { get; set; }
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "用户角色")]
        public string UserRole { get; set; }

        public override Func<SystemUser, bool> GetWhereCondition()
        {
            throw new NotImplementedException();
        }

        public override Func<SystemUser, long> GetSortCondition()
        {
            throw new NotImplementedException();
        }
    }
}