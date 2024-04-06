using ConcertBooking_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBooking_Repository.Interfaces
{
    public interface IVenueRepo
    {
        Task<IEnumerable<Venue>> GetAll();
        Task<Venue> GetById(int id);
        Task Save(Venue venue);
        Task Edit(Venue venue);
        Task RemoveData(Venue venue);
    }
}
