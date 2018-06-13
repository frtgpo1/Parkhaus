using Parking.DatabaseConfiguration;
using Parking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Parking.Services
{
    public class ParkingService : IParkingService
    {
        private const int MaxVehicles = 180;
        private const int MaxLongTimeVehicles = 40;
        private const int MinFreePlacesToAllowShortParkers = 4;

        private List<ParkingEntity> _Parkers = DbMockup.InitDB();
        
        public int NumberOfShortParkers { get; set; } = 0;

        public ParkingEntity DriveIn(ParkingEntity driveInParker)
        {
            var isLongtimeparker = IsLongTimeParker(driveInParker.CarSign);
            if (isLongtimeparker)
            {
                driveInParker.IsLongTimeParker = isLongtimeparker;
                return driveInParker;
            }
            driveInParker.IsLongTimeParker = isLongtimeparker;
            driveInParker.CheckInTime = DateTime.UtcNow;

            var capacity = CapacityAvailable(isLongtimeparker);

            return driveInParker;
        }

        private bool IsLongTimeParker(string carSign)
        {
            return _Parkers.Any(x => x.CarSign == carSign);
        }
        
        public bool CapacityAvailable(bool isInParkHouse)
        {

            var parkingEntity = new ParkingEntity();

            var longerInParker = _Parkers.Where(l => l.IsLongTimeParker && l.IsInParkhouse).Count();
            if (longerInParker <= MaxLongTimeVehicles && parkingEntity.IsLongTimeParker)
            {
                _Parkers.Remove(_Parkers.First(parker => parker.Id == parkingEntity.Id));
                isInParkHouse = true;
                _Parkers.Add(parkingEntity);
                return true;
            }
            else if(longerInParker > MaxLongTimeVehicles && parkingEntity.IsLongTimeParker)
            {
                return false;
            }

            var shorterParkerIn = _Parkers.Where(s => !s.IsLongTimeParker).Count();
            var freePlaces = MaxVehicles - MaxLongTimeVehicles - MinFreePlacesToAllowShortParkers;
            var placesAvailable = freePlaces - shorterParkerIn > 0;

            if (placesAvailable)
            {
                _Parkers.Add(parkingEntity);
                return true;
            }

            return false;
        }

        public void CheckIn(ParkingEntity parkingEntity)
        {
            throw new NotImplementedException();
        }
    }
}