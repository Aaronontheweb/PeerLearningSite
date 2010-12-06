using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PeerLearn.Web.Service;

namespace PeerLearn.Web.Controllers
{
    public class HomeController : Controller
    {
        private IEventService _eventService;

        public HomeController(IEventService eventService)
        {
            _eventService = eventService;   
        }

        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View(_eventService.GetUpcomingEvents(DateTime.Now));
        }

    }
}
