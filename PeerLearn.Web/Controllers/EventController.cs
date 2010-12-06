using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PeerLearn.Data.Entities;
using PeerLearn.Web.Models;
using PeerLearn.Web.Service;

namespace PeerLearn.Web.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        //
        // GET: /Event/

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Event/Details/5

        public ActionResult Details(int id)
        {
            var ev = _eventService.GetEvent(id);

            if (ev == null)
                return RedirectToAction("Unknown");

            return View(ev);
        }

        public ActionResult Unknown()
        {
            return View();
        }

        //
        // GET: /Event/Create

        [Authorize]
        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Event/Create
        [Authorize]
        [HttpPost]
        public ActionResult Create(NewEvent newEvent)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    //Should probably refactor this into a static method in IEventService /shrug
                    var insertedEvent = new Event
                                            {
                                                EventName = Server.HtmlEncode(newEvent.EventName),
                                                EventDescription = Server.HtmlEncode(newEvent.EventDescription),
                                                EventBeginDate = newEvent.EventBeginTime,
                                                EventEndDate = newEvent.EventEndTime,
                                                RegistrationOpenDate = newEvent.RegistrationBegins,
                                                Address = new Address
                                                              {
                                                                  StreetAddress =
                                                                      Server.HtmlEncode(newEvent.StreetAddress),
                                                                  StreetAddress2 =
                                                                      Server.HtmlEncode(newEvent.StreetAddress2 ??
                                                                                        string.Empty),
                                                                  City = Server.HtmlEncode(newEvent.City),
                                                                  PostalCode = Server.HtmlEncode(newEvent.PostalCode),
                                                                  State = Server.HtmlEncode(newEvent.State)
                                                              }
                                            };

                    var user = _eventService.Repository.GetUserByName(HttpContext.User.Identity.Name);

                    insertedEvent.AddAttendee(user);
                    insertedEvent.AddOrganizer(user);

                    var result = _eventService.Repository.CreateEvent(insertedEvent);

                    if(result)
                    {
                        return RedirectToAction("Details", new {id = insertedEvent.EventId});
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unable to add event to calendar.");
                        return View(newEvent);
                    }
                }

                ModelState.AddModelError("", "Invalid event - please correct errors before re-submitting.");
                return View(newEvent);
            }
            catch
            {
                ModelState.AddModelError("", "Unable to add event to calendar.");
                return View();
            }
        }
        
        

        #region Validation Actions

        public JsonResult EventNameAvailable(string eventname)
        {
            var existingEvent = _eventService.GetEvent(eventname);
            if (existingEvent == null)
                return Json(true, JsonRequestBehavior.AllowGet);
            else
            {
                return Json(string.Format("An event already exists with name {0}", eventname),
                            JsonRequestBehavior.AllowGet);
            }
        }

        #endregion
    }
}
