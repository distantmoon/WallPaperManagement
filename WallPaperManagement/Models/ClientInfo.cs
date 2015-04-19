using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WallPaperManagement.Models
{
    public class ClientInfo : CommonModel<ClientInfo,long>
    {
        [Key]
        [Column("ClientInfoId")]
        public int Id { get; set; }
        [Display(Name = "客户名")]
        public string Name { get; set; }

        [Display(Name = "固定电话")]
        public string Telephone { get; set; }

        [Display(Name = "移动电话")]
        public string Mobile { get; set; }

        [Display(Name = "客户地址")]
        public string Address { get; set; }
        public DateTime AddDate { get; set; }

        virtual public List<OrderInfo> OrderInfos { get; set; }
        public override Func<ClientInfo, bool> GetWhereCondition()
        {
            throw new NotImplementedException();
        }

        public override Func<ClientInfo, long> GetSortCondition()
        {
            throw new NotImplementedException();
        }
    }
}