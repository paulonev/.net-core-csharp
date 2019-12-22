using Microsoft.EntityFrameworkCore;

namespace SportsStore.Models
{
    /// <summary>
    /// Context of database class
    /// Any instance of DbContext represents a session with the database which
    /// can be used to query and save instances of your entities to a database
    /// What can DbContext do:
    /// 1) Manage db connection
    /// 2) Configure model & relationship
    /// 3) Querying database
    /// 4) Saving data to the database
    /// 5) Configure change tracking
    /// 6) Caching
    /// 7) Transaction management
    /// Typically contains DbSet<TEntity> property for each entity in the model 
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base()
        {}
        
        public ApplicationDbContext()
        {
            Database.EnsureCreated();
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=SportsStore.db");
            //optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFProviders.InMemory;Trusted_Connection=True;ConnectRetryCount=0");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //allows to configure the model using ModelBuilder Fluent API
            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Provide read/write access to Product object in database
        /// </summary>
        public DbSet<Product> Products { get; set; }
        
    }
}