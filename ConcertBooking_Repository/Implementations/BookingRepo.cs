using ConcertBooking_Entities;
using ConcertBooking_Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBooking_Repository.Implementations
{
    public class BookingRepo : IBookingRepo
    {
        private readonly ApplicationDbContext _context;

        public BookingRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddBooking(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Booking>> GetAll(int ConcertId)
        {
            var bookings = await _context.Bookings.Include(x=>x.Tickets)
                .Include(w=>w.Concert)
                .Include(y=>y.User)
                .Where(z=>z.ConcertId == ConcertId).ToListAsync();
            return bookings;
        }
    }
}
