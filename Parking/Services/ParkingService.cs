using Parking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Parking.Services
{ // : IParkingService
  public class ParkingService
    {
        private const int MaxVehicles = 180;
        private const int MaxLongTimeVehicles = 40;
        private const int MinFreePlacesToAllowShortParkers = 4;

        private List<Customer> _longtimeParkers = new List<Customer>();
        
        public int NumberOfShortParkers { get; set; } = 0;

        public Customer DriveIn(Customer driveInParker)
        {
            if (IsLongTimeParker(driveInParker.LicensePlate))
            {
                driveInParker.IsLongTimeParker = true;
                return driveInParker;
            }

            driveInParker.CheckInTime = DateTime.UtcNow;


            return driveInParker;
        }

        
        public bool IsLongTimeParker(string carSign)
        {

            return _longtimeParkers.Where(parker => parker.LicensePlate == carSign).Any();
        }
        
        public int checkCapacity()
        {
            return 0;
        }

        public bool FirstVisite()
        {
            return false;
        }

        public bool NoCapacity( bool ShortParker = false)
        {
            // TODO: Datenbak auswertung.
            return true;
        }
    }
}