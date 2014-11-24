using System;
using System.ComponentModel.DataAnnotations;

namespace WallPaperManagement.Models
{
    public class OrderInfo
    {
        public int OrderInfoId { get; set; }

        [Display(Name = "订单日期")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "订单价格")]
        public decimal PriceDecimal { get; set; }

        [Display(Name = "数量")]
        public int OrderCount { get; set; }

        public int ClientInfoId { get; set; }
        public int WallPaperId { get; set; }
        public int SystemUserId { get; set; }

        public int PayStatus { get; set; }

        public string JustForTest { get; set; }

        public virtual ClientInfo ClientInfo { get; set; }
        public virtual WallPaper WallPaper { get; set; }
        public virtual SystemUser SystemUser { get; set; }


    }
}