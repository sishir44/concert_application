﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBooking_Repository.Interfaces
{
    public interface IDbInitial
    {
        Task Seed();
    }
}
