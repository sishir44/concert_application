﻿using ConcertBooking_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBooking_Repository.Interfaces
{
    public interface ITicketRepo
    {
        Task<IEnumerable<int>> GetBookedTickets(int concertId);
        Task<IEnumerable<Booking>> GetBookings(string userId);
    }
}
