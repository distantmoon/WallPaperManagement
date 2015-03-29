using WallPaper.DAL;

namespace Wallpaper.WPF.BLL
{
    public class CustomBLL:BLLBase<Custom>
    {
        public override string GetCountUrl()
        {
            return "http://localhost:8099/Custom/GetCount";
        }

        public override string GetPageUrl()
        {
            return "http://localhost:8099/Custom/GetPage";
        }
    }
}