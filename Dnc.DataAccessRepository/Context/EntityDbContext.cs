using Dnc.Entities.Application;
using Dnc.Entities.Article;
using Microsoft.EntityFrameworkCore;

namespace Dnc.DataAccessRepository.Context
{
    public class EntityDbContext:DbContext
    {
        public EntityDbContext(DbContextOptions<EntityDbContext> options)
            : base(options){}

        public DbSet<ImageInfo> ImageInfo { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<CollectionInfo> CollectionInfo { get; set; }
        public DbSet<Evaluate> Evaluate { get; set; }
        public DbSet<OrderProductItems> OrderProductItems { get; set; }
        public DbSet<OrderProduct> OrderProduct { get; set; }
        public DbSet<ProductsInfo> ProductsInfo { get; set; }
        public DbSet<ProductsCategory> ProductsCategory { get; set; }
        public DbSet<SearchHistories> SearchHistories { get; set; }
        public DbSet<ShipAddresses> ShipAddresses { get; set; }
         public DbSet<Coupons> Coupons { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            {
               /* optionsBuilder.UseSqlServer(
                    @"Server=SC-201804102054;Initial Catalog=CPMD_Team20140208; uid=sa;pwd=123;MultipleActiveResultSets=True");
                base.OnConfiguring(optionsBuilder);*/
                optionsBuilder.UseMySQL(@"Server=47.98.212.255;port=3306;database=cpmd_team2018;uid=zjg;pwd=123456;SslMode=None");
                base.OnConfiguring(optionsBuilder);

            }
        }
    }
}
