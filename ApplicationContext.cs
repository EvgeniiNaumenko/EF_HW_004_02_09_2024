using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace HW_005_04_09_2024
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<GuestRole> Roles { get; set; }
        public DbSet<EventGuest> EventGuests { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-C317JNM;Database=HWEvent;Trusted_Connection=True;TrustServerCertificate=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<EventGuest>()
                .HasKey(eg => new { eg.EventId, eg.GuestId});

            modelBuilder.Entity<EventGuest>()
                .HasOne(eg => eg.Event)
                .WithMany(eg => eg.Relations)
                .HasForeignKey(eg => eg.EventId);

            modelBuilder.Entity<EventGuest>()
                .HasOne(eg => eg.Guest)
                .WithMany(eg => eg.Relations)
                .HasForeignKey(eg => eg.GuestId);

            modelBuilder.Entity<EventGuest>()
                .HasOne(eg => eg.Role)
                .WithMany(eg => eg.Relations)
                .HasForeignKey(eg => eg.GuestRoleId);

            modelBuilder.Entity<Event>().HasData(
            new Event { Id = 1, Name = "Conference 2024" },
            new Event { Id = 2, Name = "Workshop AI" }
            );

            
            modelBuilder.Entity<Guest>().HasData(
                new Guest { Id = 1, Name = "John", Surname = "Doe" },
                new Guest { Id = 2, Name = "Jane", Surname = "Smith" },
                new Guest { Id = 3, Name = "Bill", Surname = "Gates" },
                new Guest { Id = 4, Name = "Elon", Surname = "Musk" },
                new Guest { Id = 5, Name = "Sara", Surname = "Connor" },
                new Guest { Id = 6, Name = "Alan", Surname = "Turing" },
                new Guest { Id = 7, Name = "Ada", Surname = "Lovelace" },
                new Guest { Id = 8, Name = "Grace", Surname = "Hopper" },
                new Guest { Id = 9, Name = "Mark", Surname = "Zuckerberg" },
                new Guest { Id = 10, Name = "Jeff", Surname = "Bezos" }
            );

            
            modelBuilder.Entity<GuestRole>().HasData(
                new GuestRole { Id = 1, RoleName = "Speaker" },
                new GuestRole { Id = 2, RoleName = "Attendee" },
                new GuestRole { Id = 3, RoleName = "Organizer" }
            );

            
            modelBuilder.Entity<EventGuest>().HasData(
                new EventGuest { EventId = 1, GuestId = 1, GuestRoleId = 1 },  
                new EventGuest { EventId = 1, GuestId = 2, GuestRoleId = 2 },  
                new EventGuest { EventId = 2, GuestId = 3, GuestRoleId = 3 },  
                new EventGuest { EventId = 2, GuestId = 4, GuestRoleId = 1 },  
                new EventGuest { EventId = 2, GuestId = 5, GuestRoleId = 2 },  
                new EventGuest { EventId = 1, GuestId = 6, GuestRoleId = 3 }, 
                new EventGuest { EventId = 1, GuestId = 7, GuestRoleId = 2 },  
                new EventGuest { EventId = 2, GuestId = 8, GuestRoleId = 2 },  
                new EventGuest { EventId = 1, GuestId = 9, GuestRoleId = 2 },  
                new EventGuest { EventId = 2, GuestId = 10, GuestRoleId = 2 }
            );
        }
    }
}
