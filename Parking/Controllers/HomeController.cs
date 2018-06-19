using Parking.Entities;
using Parking.Models;
using Parking.Services;
using System.Web.Mvc;
using System.Linq;

namespace Parking.Controllers
{
  public class HomeController : Controller
	{
    private const int maxCars = 180;
    private const int maxShortTermCars = 140;
    private const int maxLongTermCars = 40;

    public HomeController()
    {
      
    }

		public ViewResult Index()
		{
      ViewBag.NumberOfCarsInCarPark = AvailableParkingLotsOverall();
      ViewBag.Message = TempData["notification"]?.ToString();
			return View("Index");
		}

		public ViewResult Entry()
		{
			return View("Entry");
		}

		public ActionResult EntrySubmit(EntryModel model)
		{
      if ( IsCarKnown( model.LicensePlate ) )
      {
        if ( IsCarInPark(model.LicensePlate ) )
        {
          TempData[ "notification" ] = "Digger, du BIST bereits im Autohaus.";
          return RedirectToAction( "Index" );
        }
        else
        {
          // Kann nur ein Dauerparker sein
          if ( CalculateAvailableLongTermParkingSpots() > 0 || CalculateAvailableShortTermParkingSpots() > 4 )
          {
            ChangeIsInCarParkStatus( model.LicensePlate, true );
            TempData[ "notification" ] = "Herzlich Willkommen zurück im Parkhaus, " + model.FirstName + " " + model.Lastname + "!";
            return RedirectToAction( "Index" );
          }
          else
          {
            TempData[ "notification" ] = "Das Parkhaus ist leider voll, " + model.FirstName + " " + model.Lastname + "!";
            return RedirectToAction( "Index" );
          }
        }
      }
      else
      {
        if ( CalculateAvailableShortTermParkingSpots() < 4 )
        {
          TempData["notification"] = "Das Parkhaus ist voll, tut uns sehr leid!";
          return RedirectToAction( "Index" );
        }
        else
        {
				  return View("ChooseParkingStyle", model);
        }
      }
		}

    [HttpPost]
    public ActionResult NewShortTerm(EntryModel model)
		{
      RegisterNewShortTermParker( model.LicensePlate );
      ChangeIsInCarParkStatus( model.LicensePlate, true );
			TempData["notification"] = "Herzlich Willkommen, neuer Kurzzeitparker";
			return RedirectToAction("Index");
		}

    [HttpPost]
		public ViewResult Register(EntryModel model)
		{
			return View("Register", model);
		}

		public ActionResult RegisterSubmit(EntryModel model)
		{
      RegisterNewLongTimeParker( model );
      ChangeIsInCarParkStatus( model.LicensePlate, true );
			TempData["notification"] = "Herzlich Willkommen, Sie haben sich erfolgreich als Dauerparker angemeldet.";
      return RedirectToAction("Index");
		}

    public ViewResult Out()
		{
			var model = new OutModel();
			return View("Out", model);
		}

    [HttpPost]
		public ActionResult OutSubmit(OutModel model)
		{
      if ( IsCarInPark(model.LicensePlate) )
      {
        SignOut( model.LicensePlate );
        TempData[ "notification" ] = "Erfolgreich hinausgefahren, beehren Sie uns bald wieder.";
        return RedirectToAction( "Index" );
      }
      else
      {
        TempData[ "notification" ] = "Du kannst nicht noch mehr draußen sein!";
        return RedirectToAction( "Index" );
      }
		}

    private void RegisterNewLongTimeParker( EntryModel model )
    {
      using ( var context = new ParkingContext() )
      {
        context.Customers.Add( new Customer()
        {
          LicensePlate = model.LicensePlate.ToUpper(),
          IsLongTimeParker = true,
          IsInCarPark = false,
          FirstName = model.FirstName,
          Lastname = model.Lastname,
          EMail = model.EMail
        } );
        context.SaveChanges();
      }
    }

    private void RegisterNewShortTermParker( string licensePlate )
    {
      using ( var context = new ParkingContext())
      {
        context.Customers.Add( new Customer()
        {
          LicensePlate = licensePlate.ToUpper(),
          IsLongTimeParker = false,
          IsInCarPark = false,
          FirstName = string.Empty,
          Lastname = string.Empty,
          EMail = string.Empty
        } );
        context.SaveChanges();
      }
    }

    private bool IsCarInPark( string licensePlate )
    {
      using(var context = new ParkingContext())
      {
        return context.Customers.Where(c => c.LicensePlate == licensePlate).Select( c => c.IsInCarPark ).Single();
      }  
    }

    private bool IsCarKnown( string licensePlate )
    {
      using ( var context = new ParkingContext() )
      {
        return context.Customers.Any( c => c.LicensePlate == licensePlate );
      }
    }

    private bool IsLongTimeParker( string carSign )
    {
      using ( var context = new ParkingContext() )
      {
        return context.Customers.Find(carSign).IsLongTimeParker;
      }
    }

    private int CalculateCarsInCarParkOverall()
    {
      using (var context = new ParkingContext())
      {
        return context.Customers.Where(c => c.IsInCarPark == true).Count();
      }
    }

    private int CalculateCarsOfLongTermParkersInCarPark()
    {
      using (var context = new ParkingContext())
      {
        return context.Customers.Where(c => c.IsInCarPark == true && c.IsLongTimeParker == true).Count();
      }
    }

    private int CalculateCarsOfShortTermParkersInCarPark()
    {
      using (var context = new ParkingContext())
      {
        return context.Customers.Where(c => c.IsInCarPark == true && c.IsLongTimeParker == false).Count();
      }
    }

    private int CalculateAvailableLongTermParkingSpots()
    {
      using (var context = new ParkingContext())
      {
        return maxLongTermCars - CalculateCarsOfLongTermParkersInCarPark();
      }
    }

    private int CalculateAvailableShortTermParkingSpots()
    {
      using (var context = new ParkingContext())
      {
        return maxLongTermCars - CalculateCarsOfShortTermParkersInCarPark();
      }
    }

    private int AvailableParkingLotsOverall()

    {
      return maxCars - CalculateCarsInCarParkOverall();
    }

    private void ChangeIsInCarParkStatus(string licensePlate, bool isCarInCarPark)
    {
      using (var context = new ParkingContext())
      {
        if ( context.Customers.Select( c => c.LicensePlate == licensePlate ).Any() )
        {
          var customer = context.Customers.Where(c => c.LicensePlate == licensePlate).FirstOrDefault();
          customer.IsInCarPark = isCarInCarPark;
          context.SaveChanges();
        }
      }
    }

    private void SignOut(string licensePlate)
    {
      using (var context = new ParkingContext())
      {
        var customer = context.Customers.Where( c => c.LicensePlate == licensePlate ).Single();
        if ( customer.IsLongTimeParker )
        {
          ChangeIsInCarParkStatus( licensePlate, false );
        }
        else
        {
          context.Customers.Remove(customer);
        }
        context.SaveChanges();
      }
    }
	}
}