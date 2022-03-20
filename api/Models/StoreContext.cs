using Microsoft.EntityFrameworkCore;

namespace Models {
    public class StoreContext : DbContext {
        public DbSet<Store> Store { get; set; } = null!;
        public DbSet<Item> Item { get; set; } = null!;
        public DbSet<User> User { get; set; } = null!;
        public DbSet<Section> Section { get; set; } = null!;
        public DbSet<Sale> Sale { get; set; } = null!;

        public StoreContext(DbContextOptions options): base(options){

        }

        // protected override void OnModelCreating(ModelBuilder modelBuilder) {
        //     base.OnModelCreating(modelBuilder);

        //     modelBuilder
        //     .Entity<Item>()
        //     .HasMany<Sale>()
        //     .WithOne(s => s.SoldItems);
        // }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("MiniShopCS");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}