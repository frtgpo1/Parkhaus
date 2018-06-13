using Parking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Parking.DatabaseConfiguration
{
    public static class DbMockup
    {
        public static List<ParkingEntity> InitDB()
        {
            List<ParkingEntity> parkerList = new List<ParkingEntity>();


            for (int i = 0; i < 35; i++)
            {
                Random rad = new Random();
                ParkingEntity parker = new ParkingEntity()
                {
                    Id = i + 1,
                    CheckInTime = DateTime.Now - TimeSpan.FromMinutes(rad.Next(1, 1000)),
                    IsLongTimeParker = true,
                    CarSign = "OS-AB-" + i,
                    IsInParkhouse = rad.Next(0, 1) > 0
                };
                parkerList.Add(parker);
            };

            for (int i = 36; i < 71; i++)
            {
                Random rad = new Random();
                ParkingEntity parker = new ParkingEntity()
                {
                    Id = i + 1,
                    CheckInTime = DateTime.Now - TimeSpan.FromMinutes(rad.Next(1, 1000)),
                    IsLongTimeParker = false,
                    CarSign = "OS-AB-" + i,
                    IsInParkhouse = true
                };
                parkerList.Add(parker);
            }

            return parkerList;
        }
    }
}