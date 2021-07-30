using HotelManagement.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Database.Configurations
{

    public class FloorConfiguration : EntityTypeConfiguration<Floor>
    {
        public FloorConfiguration()
        {
            this.ToTable("Floor")
                .HasKey(k => k.floorId)
                .Property(k => k.floorId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
    public class RoomConfiguration : EntityTypeConfiguration<Room>
    {
        public RoomConfiguration()
        {
            this.ToTable("Room")
                .HasKey(k => k.roomId)
                .Property(k => k.roomId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            this.HasRequired(r => r.Floor)
                .WithMany(f => f.Rooms)
                .HasForeignKey(f => f.floorId)
                .WillCascadeOnDelete(true);
        }
    }
    public class BookingConfiguration : EntityTypeConfiguration<Booking>
    {
        public BookingConfiguration()
        {
            this.ToTable("Booking")
                .HasKey(k => k.Id);
            this.Property(fk => fk.customerId)
                .IsRequired()
                .HasMaxLength(128);
            this.HasRequired(b => b.Room)
                .WithMany(r => r.Bookings)
                .HasForeignKey(r => r.roomId);                
            this.HasRequired(b => b.Customer)
                .WithMany(c => c.Bookings)
                .HasForeignKey(c => c.customerId);
        }
    }
    public class CustomerConfiguration : EntityTypeConfiguration<Customer>
    {
        public CustomerConfiguration()
        {
            this.ToTable("Customer")
                .HasKey(k => k.customerId);
        }
    }

}
