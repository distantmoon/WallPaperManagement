using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WallPaperManagement.Models
{
    public class WallPaper : CommonModel<WallPaper,long>
    {
        [Key]
        [Column("WallPaperId")]
        public int Id { get; set; }

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

        public override Func<WallPaper, bool> GetWhereCondition()
        {
            return p => p.IsEnable == 1 && p.Amount > 0;
        }

        public override Func<WallPaper, long> GetSortCondition()
        {
            return p => p.Id;
        }

        public override void SetDefaultValue()
        {
            this.AddDate = DateTime.Now;
            this.IsEnable = 1;
            this.Id = 1;
        }
    }
}