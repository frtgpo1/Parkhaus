using System;

namespace Parking.Entities
{
  public class Customer
    {
        /// <summary>
        /// ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Kennzeichen des Autos.
        /// </summary>
        public string LicensePlate { get; set; }

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
        /// Parkt aktuell im Parkhaus.   
        /// </summary>
        public bool IsInParkhouse { get; set; }
    }
}