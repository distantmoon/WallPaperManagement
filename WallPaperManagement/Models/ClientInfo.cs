using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WallPaperManagement.Models
{
    public class ClientInfo
    {
        public int ClientInfoId { get; set; }

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
    }
}