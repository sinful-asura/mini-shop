using Microsoft.EntityFrameworkCore;

namespace Models {
    public class StoreContext : DbContext {
        public DbSet<Store>? Store { get; set; }
        public DbSet<Item>? Item { get; set; }
        public DbSet<User>? User { get; set; }
        public DbSet<Section>? Section { get; set; }
        public DbSet<Sale>? Sale { get; set; }

        // public StoreContext(
        //     DbSet<Store> Stores,
        //     DbSet<Item> Items,
        //     DbSet<User> Users,
        //     DbSet<Section> Sections,
        //     DbSet<Sale> Sales
        // ) {
        //     this.Stores = Stores;
        //     this.Items = Items;
        //     this.Users = Users;
        //     this.Sections = Sections;
        //     this.Sales = Sales;
        // }

        public StoreContext(DbContextOptions options): base(options){

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder
            .Entity<List<Item>>()
            .HasMany<Sale>()
            .WithOne(s => s.SoldItems);
        }
    }
}