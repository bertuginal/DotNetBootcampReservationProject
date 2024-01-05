using Microsoft.EntityFrameworkCore;

namespace NetBootcampProject.Models.ORM
{
    public class NetBootcampDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //connection string
            optionsBuilder.UseSqlServer("Server=Siccin; Database=CompanyReservationDb; trusted_connection=true");
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}
