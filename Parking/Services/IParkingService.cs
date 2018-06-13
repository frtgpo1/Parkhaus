using Parking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Parking.Services
{   
    public interface IParkingService
    {
        void CheckIn(ParkingEntity parkingEntity);
    }
}