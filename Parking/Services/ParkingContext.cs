using System.Data.Entity;
using Parking.Entities;

namespace Parking.Services
{
  public class ParkingContext : DbContext
  {
    public DbSet<Customer> Customers { get; set; }

    public DbSet<Visit> Visits { get; set; }
  }
}