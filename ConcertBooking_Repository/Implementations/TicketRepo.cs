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
    public class TicketRepo : ITicketRepo
    {
        private readonly ApplicationDbContext _context;

        public TicketRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<int>> GetBookedTickets(int id)
        {
            var bookedTickets = await _context.Tickets.Where(t => t.ConcertId == id && t.IsBooked)
                .Select(t=>t.SeatNumber).ToListAsync();
            return bookedTickets;
        }
    }
}
