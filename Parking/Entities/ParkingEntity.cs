using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Parking.Entities
{
    public class ParkingEntity
    {
        /// <summary>
        /// Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Kennzeichen des Autos.
        /// </summary>
        public string CarSign { get; set; }

        /// <summary>
        /// Ankunft.
        /// </summary>
        public DateTime CheckInTime { get; set; }

        /// <summary>
        /// Abfahrt.
        /// </summary>
        public DateTime CheckOutTime { get; set; }

        /// <summary>
        /// Langzeit-Parker.
        /// </summary>
        public bool IsLongTimeParker { get; set; }

        /// <summary>
        /// Ist der Parkende im Parkhaus.   
        /// </summary>
        public bool IsInParkhouse { get; set; }
    }
}