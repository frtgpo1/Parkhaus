using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Parking.Models
{
    public class DriveInModel
    {
        /// <summary>
        /// Kapazität.
        /// </summary>
        public int Capacity { get; set; }

        /// <summary>
        /// Nummernschild.
        /// </summary>
        public string CarSign { get; set; }

    }
}