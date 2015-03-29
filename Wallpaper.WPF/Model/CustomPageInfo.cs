using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WallPaper.DAL;
using Wallpaper.WPF.Annotations;
using Microsoft.Practices.Composite.Presentation.Commands;
using Wallpaper.WPF.BLL;

namespace Wallpaper.WPF.Model
{
    public class CustomPageInfo:PageInfoBase<Custom>
    {
        public CustomPageInfo() : base(new CustomBLL())
        {
        }
    }
}