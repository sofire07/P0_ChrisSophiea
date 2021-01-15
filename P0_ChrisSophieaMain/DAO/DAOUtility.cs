using Microsoft.EntityFrameworkCore;

namespace P0_ChrisSophiea
{
    public class DAOUtility : DbContext
    {
        public DbSet<Customer> customers { get; set; }
        public DbSet<Store> stores { get; set; }
        public DbSet<Item> items { get; set; }
        public DbSet<Inventory> inventories { get; set; }
        public DbSet<Purchase> purchases { get; set; }

        public DAOUtility() { }

        public DAOUtility(DbContextOptions<DAOUtility> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Purchases)
                .WithOne(e => e.Customer1)
                .IsRequired();

            modelBuilder.Entity<Purchase>()
                .HasMany(c => c.Item1)
                .WithMany(e => e.Purchases);

            modelBuilder.Entity<Item>()
                .HasMany(c => c.Inventories)
                .WithOne(e => e.Item1)
                .IsRequired();

            modelBuilder.Entity<Store>()
                .HasMany(c => c.Purchases)
                .WithOne(e => e.Store1)
                .IsRequired();

            modelBuilder.Entity<Store>()
                .HasMany(c => c.Inventories)
                .WithOne(e => e.Store1)
                .IsRequired();
        }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.EnableSensitiveDataLogging();
                options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=MyStore;Trusted_Connection=True;");
            }
        }

    }
}