using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WallPaperManagement.Models
{
    public class CommonModel
    {
        public int IsEnable { get; set; }
    }
    
    public abstract  class CommonModel<T,TS> :CommonModel where T:CommonModel
    {
        public Func<T, bool> ListFilter;

        public abstract Func<T, bool> GetWhereCondition();
        
        public abstract Func<T, TS> GetSortCondition();

        public  virtual void  SetDefaultValue()
        {
        }
    }
}