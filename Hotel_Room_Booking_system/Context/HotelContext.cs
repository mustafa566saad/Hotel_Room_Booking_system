namespace Hotel_Room_Booking_system.Context
{
    public class HotelContext : IdentityDbContext<ApplicationUser>
    {
        public HotelContext(DbContextOptions<HotelContext> options)
            : base(options) { }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<SeasonalPrice> SeasonalPrices { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .HasIndex(u => u.Email)
                .IsUnique();
            modelBuilder.Entity<SeasonalPrice>()
                .HasData(
                    new SeasonalPrice
                    {
                        Id = 1,
                        StartDate = new DateTime(DateTime.Now.Year, 6, 1),
                        EndDate = new DateTime(DateTime.Now.Year, 8, 31),
                    },
                    new SeasonalPrice
                    {
                        Id = 2,
                        StartDate = new DateTime(DateTime.Now.Year, 12, 15),
                        EndDate = new DateTime(DateTime.Now.Year + 1, 1, 5),
                        IncreasePercentage = 0.30m
                    }
                );
        }
    }
}
