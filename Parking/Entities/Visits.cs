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
    /// Ankunftszeit des Besuchs.
    /// </summary>
    public DateTime? ArrivalTime { get; set; }

    /// <summary>
    /// Abfahrtszeit des Besuchs.
    /// </summary>
    public DateTime? DepartureTime { get; set; }

    /// <summary>
    /// Kunde des Besuchs.
    /// </summary>
    public Customer Customer { get; set; }
  }
}