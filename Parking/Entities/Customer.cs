using System.ComponentModel.DataAnnotations;

namespace Parking.Entities
{
  public class Customer
	{
		/// <summary>
		/// Kennzeichen des Autos.
		/// </summary>
    [Key]
		public string LicensePlate { get; set; }

		/// <summary>
		/// Langzeit-Parker.
		/// </summary>
		public bool IsLongTimeParker { get; set; }

		/// <summary>
		/// Parkt momentan im Parkhaus.
		/// </summary>
		public bool IsInCarPark { get; set; }

    /// <summary>
    /// Vorname.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Nachname.
    /// </summary>
    public string Lastname { get; set; }

    /// <summary>
    /// E-Mail Adresse.
    /// </summary>
    public string EMail { get; set; }
  }
}