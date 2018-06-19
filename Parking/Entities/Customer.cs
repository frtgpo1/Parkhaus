using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Parking.Entities
{
  public class Customer
	{
		/// <summary>
		/// Kennzeichen des Autos des Kunden.
		/// </summary>
    [Key]
		public string LicensePlate { get; set; }

		/// <summary>
		/// Gibt an, ob der Kunden ein Langzeit-Parker ist oder nicht.
		/// </summary>
		public bool IsLongTimeParker { get; set; }

		/// <summary>
		/// Gibt an, ob der Kunden momentan im Parkhaus parkt.
		/// </summary>
		public bool IsInCarPark { get; set; }

    /// <summary>
    /// Vorname des Kunden.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Nachname des Kunden.
    /// </summary>
    public string Lastname { get; set; }

    /// <summary>
    /// E-Mail Adresse des Kunden.
    /// </summary>
    public string EMail { get; set; }

    /// <summary>
    /// Besuche des Kunden.
    /// </summary>
    public ICollection<Visit> Visits { get; set; }
  }
}