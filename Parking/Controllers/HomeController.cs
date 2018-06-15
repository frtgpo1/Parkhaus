using Parking.Entities;
using Parking.Models;
using Parking.Services;
using System.Web.Mvc;

namespace Parking.Controllers
{
	public class HomeController : Controller
	{
		public ViewResult Index()
		{
			return View("Index");
		}

		public ViewResult Entry()
		{
			return View("Entry");
		}

		public ViewResult EntrySubmit(EntryModel model)
		{
			if ( /* Wenn Kennzeichen Dauerparker ist */ false)
			{
				ViewBag.Message = "Herzlich willkommen, Herr Dauerparker ;)";
				return View("Index");
			}
			else if ( /* Wenn Kennzeichen neu ist */ true)
			{
				return View("ChooseParkingStyle", model);
			}
		}

		public ViewResult NewShortTerm(EntryModel model)
		{
			ViewBag.Message = "Herzlich Willkommen, neuer Kurzzeitparker";
			return View("Index", model);
		}

		[HttpPost]
		public ViewResult Register(EntryModel model)
		{
			return View("Register", model);
		}

		public ViewResult RegisterSubmit(EntryModel model)
		{
			// Füge neuen Kunden hinzu
			using (var context = new ParkingContext())
			{
				// TODO: Hier weiter denken und sowas.
				var customer = new Customer();
				customer.LicensePlate = model.Kennzeichen;
				customer.IsInParkhouse = true;
				customer.IsLongTimeParker = true;
				context.Customers.Add(customer);
				context.SaveChanges();
			}
			ViewBag.Message = "Erfolgreich als Dauerparker angemeldet";
			return View("Index");
		}

		public ViewResult Out()
		{
			var model = new OutModel();
			return View("Out", model);
		}

		public ViewResult OutSubmit(OutModel model)
		{
			ViewBag.Message = "Erfolgreich hinausgefahren, beehren Sie uns bald wieder.";
			return View("Index");
		}
	}
}