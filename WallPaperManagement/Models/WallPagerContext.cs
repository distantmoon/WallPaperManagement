using System.Data.Entity;

namespace WallPaperManagement.Models
{
    public class WallPagerContext : DbContext
    {

        public WallPagerContext()
        {
            Database.SetInitializer<WallPagerContext>(null);
        }

        public DbSet<WallPaper> WallPapgers { get; set; }
        public DbSet<SystemUser> SystemUsers { get; set; }
        public DbSet<OrderInfo> OrderInfos { get; set; }
        public DbSet<ClientInfo> ClientInfos { get; set; }
    }
}