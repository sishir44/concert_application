using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBooking_Entities
{
    public class Ticket
    {
        [Key]
        public int TickedId { get; set; }
        public int SeatNumber { get; set; }
        public bool IsBooked { get; set; }
        //public int ConcertId { get; set; }
        //public Concert Concert { get; set; }
        public int? BookingId { get; set; } 
        public Booking Booking { get; set; }

    }
}
