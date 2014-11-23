using System;
using System.ComponentModel.DataAnnotations;

namespace WallPaperManagement.Models
{
    public class WallPaper
    {
        public int WallPaperId { get; set; }

        [Display(Name = "系列")]
        public string Name   { get; set; }

         [Display(Name = "型号")]
        public string  SeriesName { set; get; }

         [Display(Name = "批次")]
        public string   No { get; set; }

        [Display(Name = "添加日期")]
        public DateTime AddDate { get; set; }

         [Display(Name = "库存数量")]
        public int Amount { get; set; }

    }
}