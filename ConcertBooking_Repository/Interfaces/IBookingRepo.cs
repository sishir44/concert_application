using ConcertBooking_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBooking_Repository.Interfaces
{
    public interface IBookingRepo
    {
        Task AddBooking(Booking booking);
        Task <IEnumerable<Booking>> GetAll(int ConcertId);
    }
}
