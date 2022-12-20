using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FinalProjectDB
{
    public class PetCareContext : DbContext
    {
        public PetCareContext(DbContextOptions<PetCareContext> options) : base(options) { }

        public DbSet<Bill> Bill { get; set; }
        public DbSet<BillDetail> BillDetail { get; set; }
        public DbSet<BranchOffice> BranchOffice { get; set; }
        public DbSet<CarePrice> CarePrice { get; set; }
        public DbSet<CareType> CareType { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<Gender> Gender { get; set; }
        public DbSet<Pet> Pet { get; set; }
        public DbSet<PetType> PetType { get; set; }
        public DbSet<Province> Province { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<TransactionDetails> TransactionDetails { get; set; }
        public DbSet<ItemDetail> ItemDetail { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<BillDetail>()
                .Property(e => e.BillDetailId)
                .HasDefaultValueSql("gen_random_uuid()");
            modelBuilder
                .Entity<Customer>()
                .Property(e => e.CustomerId)
                .HasDefaultValueSql("gen_random_uuid()");
            modelBuilder
                .Entity<Customer>()
                .Property(lu => lu.CreatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("now()");
            modelBuilder
                .Entity<Bill>()
                .Property(lu => lu.BillDate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("now()");
            modelBuilder
                .Entity<Doctor>()
                .Property(lu => lu.CreatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("now()");
        }
    }
}
