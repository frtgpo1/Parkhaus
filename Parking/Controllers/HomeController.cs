using Parking.Entities;
using Parking.Models;
using Parking.Services;
using System.Web.Mvc;
using System.Linq;
using System;

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
      if ( IsCarInPark(model.LicensePlate ) )
      {
        TempData[ "notification" ] = "Digger, du BIST bereits im Autohaus.";
        return RedirectToAction( "Index" );
      }
      else
      {
        if ( IsCarKnown( model.LicensePlate ) )
        {
          if ( IsLongTermParker(model.LicensePlate) )
          {
            if ( CalculateAvailableLongTermParkingLots() > 0 || CalculateAvailableShortTermParkingLots() > 4 )
            {
              ChangeIsInCarParkStatus( model.LicensePlate, true );
              RegisterNewVisitEntry( model.LicensePlate );
              var customer = GetCustomer( model.LicensePlate );
              TempData[ "notification" ] = "Herzlich Willkommen zurück im Parkhaus, " + customer.FirstName + " " + customer.Lastname + "!";
              return RedirectToAction( "Index" );
            }
            else
            {
              TempData[ "notification" ] = "Das Parkhaus ist leider voll, " + model.FirstName + " " + model.Lastname + "!";
              return RedirectToAction( "Index" );
            }
          }
          else
          {
            if ( CalculateAvailableLongTermParkingLots() > 0 || CalculateAvailableShortTermParkingLots() < 4 )
            {
              ViewBag.FreeShortTermParkingLots = CalculateAvailableShortTermParkingLots();
				      return View("ChooseParkingStyle", model);
            }
            else
            {
              TempData[ "notification" ] = "Das Parkhaus ist voll, tut uns sehr leid!";
              return RedirectToAction( "Index" );
            }
          }
        }
        else
        {
          if ( CalculateAvailableLongTermParkingLots() > 0 || CalculateAvailableShortTermParkingLots() < 4 )
          {
            ViewBag.FreeShortTermParkingLots = CalculateAvailableShortTermParkingLots();
				    return View("ChooseParkingStyle", model);
          }
          else
          {
            TempData[ "notification" ] = "Das Parkhaus ist voll, tut uns sehr leid!";
            return RedirectToAction( "Index" );
          }
        }
      }
		}

    [HttpPost]
    public ActionResult NewShortTerm(EntryModel model)
		{
      if ( !(CalculateAvailableShortTermParkingLots() > 4) )
      {
        TempData[ "notification" ] = "Der Parkhaus ist belegt, versuchen Sie es später noch einmal.";
        return RedirectToAction( "Index" );
      }

      if ( !IsCarKnown(model.LicensePlate) )
      {
        RegisterNewShortTermParker( model.LicensePlate );
      }

      RegisterNewVisitEntry( model.LicensePlate );
      ChangeIsInCarParkStatus( model.LicensePlate, true );
			TempData["notification"] = "Herzlich Willkommen, lieber Kurzzeitparker";
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
      RegisterNewVisitEntry(model.LicensePlate);
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
      if ( !IsLongTermParker(model.LicensePlate) )
      {
        return View( "PayForShortTerm", model );  
      }
      else
      {
        return View( "PayForLongTerm", model );
      }
		}

    public ActionResult DriveOut(OutModel model)
    {
    if ( IsCarInPark(model.LicensePlate) )
      {
        SignOutOfCarPark( model.LicensePlate );
        RegisterNewVisitOut( model.LicensePlate );
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
        return context.Customers.Where(c => c.LicensePlate == licensePlate).Any( c => c.IsInCarPark );
      }  
    }

    private bool IsCarKnown( string licensePlate )
    {
      using ( var context = new ParkingContext() )
      {
        return context.Customers.Any( c => c.LicensePlate == licensePlate );
      }
    }

    private bool IsLongTermParker( string carSign )
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

    private int CalculateAvailableLongTermParkingLots()
    {
      using (var context = new ParkingContext())
      {
        return maxLongTermCars - CalculateCarsOfLongTermParkersInCarPark();
      }
    }

    private int CalculateAvailableShortTermParkingLots()
    {
      using (var context = new ParkingContext())
      {
    return maxShortTermCars - CalculateCarsOfShortTermParkersInCarPark();
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

    private void SignOutOfCarPark(string licensePlate)
    {
      using (var context = new ParkingContext())
      {
        try
        {
          var customer = context.Customers.Where( c => c.LicensePlate == licensePlate ).Single();
          ChangeIsInCarParkStatus( licensePlate, false );
          context.SaveChanges();
        }
        catch ( Exception ex )
        {
          TempData[ "notification" ] = ex.Message;
          RedirectToAction( "Index" );  
        }
      }
    }

    private void RegisterNewVisitEntry(string licensePlate)
    {
      try
      {
        using (var context = new ParkingContext())
        {
          context.Visits.Add( new Visit()
          {
            ArrivalTime = DateTime.Now,
            DepartureTime = null,
            Customer = context.Customers.Where( c => c.LicensePlate == licensePlate ).Single()
          });
          context.SaveChanges();
        }
      }
      catch ( Exception ex)
      {
        TempData[ "notification" ] = ex.Message;
        RedirectToAction( "Index" );
      }
    }

    private void RegisterNewVisitOut(string licensePlate)
    {
      using (var context = new ParkingContext())
      {
        try
        {
          var visit = context.Visits.Where( v => v.Customer.LicensePlate == licensePlate && v.DepartureTime == null ).Single();
          visit.DepartureTime = DateTime.Now;
          context.SaveChanges();
        }
        catch ( Exception ex )
        {
          TempData[ "notification" ] = ex.Message;
          RedirectToAction( "Index" );
        }
      }
    }

    private Customer GetCustomer( string licensePlate )
    {
      using(var context = new ParkingContext())
      {
        return context.Customers.Where( c => c.LicensePlate == licensePlate ).Single();
      }
    }
  }
}