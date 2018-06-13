using Parking.Models;
using Parking.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Parking.Controllers
{
    public class HomeController : Controller
    {
        private readonly IParkingService _parkingService;
        
        public HomeController()
        {
            _parkingService = new ParkingService();
        }
        
        public ViewResult CarSign()
        {
            return View("CarSign");
        }

        public ViewResult CarSign(CarSignModel model)
        {
            //List<>

            return View("DriveIn");
        }

        public ViewResult DriveIn()
        {
            return View("DriveIn");
        }


        public ActionResult ClickOnShortParking (DriveInModel driveInModel)
        {
             //_parkingService.CheckIn();

            return View("DriveIn");
        }

        public ActionResult ClickOnLongParking(DriveInModel driveInModel)
        {
            //_parkingService.CheckIn();

            return View("DriveIn");
        }
    }
}