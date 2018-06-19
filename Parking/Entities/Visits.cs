using System;

namespace Parking.Entities
{
  public class Visit
  {
    /// <summary>
    /// ID des Besuchs.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Kennzeichen des Kunden.
    /// </summary>
    public string CustomerLicensePlate { get; set; }

    /// <summary>
    /// Ankunftszeit des Besuchs.
    /// </summary>
    public DateTime ArrivalTime { get; set; }

    /// <summary>
    /// Abfahrtszeit des Besuchs.
    /// </summary>
    public DateTime DepartureTime { get; set; }
  }
}