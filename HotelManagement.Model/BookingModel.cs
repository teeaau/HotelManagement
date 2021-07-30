using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Model
{
    public class Floor
    {
        public int floorId { get; set; }
        public string floorDisplay { get; set; }
        public ObservableCollection<Room> Rooms { get; set; }
    }
    public class Room
    {
        public int roomId { get; set; }
        public int price { get; set; }
        public bool isBooked { get; set; }
        public int floorId { get; set; }
        public Floor Floor { get; set; }
        public ObservableCollection<Booking> Bookings { get; set; }
    }
    public class Booking
    {
        public int Id { get; set; }
        public int roomId { get; set; }
        public string customerId { get; set; }
        public DateTime checkIn { get; set; }
        public DateTime? checkOut { get; set; }
        public int? total { get; set; }
        public Room Room { get; set; }
        public Customer Customer { get; set; }
    }
    public class Customer
    {
        public string customerId { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public ObservableCollection<Booking> Bookings { get; set; }
    }
    public class Income
    {
        public int Id { get; set; }
        public int bookingId { get; set; }
        public DateTime date { get; set; }
        public int total { get; set; }
    }
}
