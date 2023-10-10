using ProsperCanada.Areas.Channel.Controllers;
using ProsperCanada.Areas.Clinic.Controllers;
using ProsperCanada.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace ProsperCanada.Controllers
{
    public class AppointmentController : Controller
    {
        // GET: Appointment
        public ActionResult Index()
        {
            var name = this.User.Identity.Name;
            ViewBag.name = name;


            if (name == "")
            {
                Response.Redirect("~/Account/Login");

            }
            var Channel = DependencyResolver.Current.GetService<ChannelController>();
            var Clinic = DependencyResolver.Current.GetService<ClinicController>();

            ViewBag.Controller = this.ControllerContext.RouteData.Values["controller"].ToString().ToUpper();

            var mixmodel = new ModelMix
            {
                ClinicList = Clinic.ClinicListing(),
                ChannelList = Channel.ChannelList(),
                ChannelListLogin = Channel.ChannelListLogin(),
                ChannelChildList = Channel.ChannelChildList(ViewBag.Controller)

            };

            return View(mixmodel);
        }

        

        public ActionResult ClinicList()
        {
            var name = this.User.Identity.Name;
            ViewBag.name = name;


            if (name == "")
            {
                Response.Redirect("~/Account/Login");

            }

            var Channel = DependencyResolver.Current.GetService<ChannelController>();
            var Clinic = DependencyResolver.Current.GetService<ClinicController>();

            ViewBag.Controller = this.ControllerContext.RouteData.Values["controller"].ToString().ToUpper();

            var mixmodel = new ModelMix
            {
                ClinicList = Clinic.ClinicListing(),
                ChannelList = Channel.ChannelList(),
                ChannelListLogin = Channel.ChannelListLogin(),
                ChannelChildList = Channel.ChannelChildList(ViewBag.Controller)

            };

            return View(mixmodel);
        }

        public ActionResult ClinicBookedAppointment(string id)
        {
            var name = this.User.Identity.Name;
            ViewBag.name = name;


            if (name == "")
            {
                Response.Redirect("~/Account/Login");

            }
            ViewBag.Id = id;

            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {

                var clinic = liDBq.Database.SqlQuery<Clinics>("EXEC spSelClinicBooking '" + id + "'").ToList();
                foreach (var item in clinic)
                {
                    ViewBag.ClinicName = item.clinicName;
                }

                var eligibility = liDBq.Database.SqlQuery<ClinicMessage>("EXEC spSelClinicMessage '" + id + "','1'").ToList();
                foreach (var item in eligibility)
                {
                    ViewBag.eligibilityDesc = item.clinicDescription;
                }

                var instruction = liDBq.Database.SqlQuery<ClinicMessage>("EXEC spSelClinicMessage '" + id + "','2'").ToList();
                foreach (var items in instruction)
                {
                    ViewBag.instructionDesc = items.clinicDescription;
                }

                return View();
            }

            //return View();
        }

        public ActionResult AppointmentBooking(string id)
        {
            var name = this.User.Identity.Name;
            ViewBag.name = name;


            if (name == "")
            {
                Response.Redirect("~/Account/Login");

            }
            var Channel = DependencyResolver.Current.GetService<ChannelController>();
            var Clinic = DependencyResolver.Current.GetService<ClinicController>();

            ViewBag.Controller = this.ControllerContext.RouteData.Values["controller"].ToString().ToUpper();

            var mixmodel = new ModelMix
            {
                ChannelList = Channel.ChannelList(),
                ChannelListLogin = Channel.ChannelListLogin(),
                ChannelChildList = Channel.ChannelChildList(ViewBag.Controller),
                ClinicSchedules = Clinic.ClinicAppointmentSearch(null,null)
            };

            return View(mixmodel);
        }

        [HttpPost]
        public ActionResult AppointmentBooking(FormCollection formCollection)
        {
            var name = this.User.Identity.Name;
            ViewBag.name = name;


            if (name == "")
            {
                Response.Redirect("~/Account/Login");

            }
            var Channel = DependencyResolver.Current.GetService<ChannelController>();
            var Clinic = DependencyResolver.Current.GetService<ClinicController>();

            string searchQuery = formCollection["searchQuery"].ToString();

            ViewBag.Controller = this.ControllerContext.RouteData.Values["controller"].ToString().ToUpper();
            string ID = this.ControllerContext.RouteData.Values["id"].ToString().ToUpper();
            ViewBag.DateSearch = searchQuery;

            ViewBag.Capacity = Clinic.ClinicAppointmentCapacity(ID, searchQuery);

            var mixmodel = new ModelMix
            {
                ChannelList = Channel.ChannelList(),
                ChannelListLogin = Channel.ChannelListLogin(),
                ChannelChildList = Channel.ChannelChildList(ViewBag.Controller),
                ClinicSchedules = Clinic.ClinicAppointmentSearch(searchQuery,ID)
            };

            return View(mixmodel);
        }

        public ActionResult AppointmentMake(string ID, string date)
        {
            var name = this.User.Identity.Name;
            ViewBag.name = name;


            if (name == "")
            {
                Response.Redirect("~/Account/Login");

            }
            var Channel = DependencyResolver.Current.GetService<ChannelController>();
            var Clinic = DependencyResolver.Current.GetService<ClinicController>();

            //string searchQuery = formCollection["searchQuery"].ToString();

            ViewBag.Controller = this.ControllerContext.RouteData.Values["controller"].ToString().ToUpper();
            ViewBag.Id = ID;
            ViewBag.Date = date;
            ViewBag.Capacity = Clinic.ClinicAppointmentCapacity(ID, date);

            var mixmodel = new ModelMix
            {
                ChannelList = Channel.ChannelList(),
                ChannelListLogin = Channel.ChannelListLogin(),
                ChannelChildList = Channel.ChannelChildList(ViewBag.Controller),

                ClinicSchedules = Clinic.ClinicAppointmentViewInfo(ID),
                ClinicScheduleTaxYears = Clinic.ClinicAppointmentViewTaxYear(ID),
                ClinicScheduleLanguages = Clinic.ClinicAppointmentViewLanguage(ID),
                ClinicScheduleMeetingTypes = Clinic.ClinicAppointmentViewMeetingType(ID),
                clinicScheduleFamilyTypes = Clinic.ClinicAppointmentViewFamilyType(ID)
            };

            return View(mixmodel);
        }

        [HttpPost]
        public ActionResult AppointmentMake(FormCollection formCollection)
        {
            var name = this.User.Identity.Name;
            ViewBag.name = name;


            if (name == "")
            {
                Response.Redirect("~/Account/Login");

            }
            var Clinic = DependencyResolver.Current.GetService<ClinicController>();
            var Channel = DependencyResolver.Current.GetService<ChannelController>();


            string clinicScheduleId = formCollection["appointmentId"].ToString();
            string clinicId = formCollection["clinicId"].ToString();
            string clinicTaxYearId = formCollection["taxYear"].ToString();
            string clinicLanguageId = formCollection["language"].ToString();
            string clinicFamilyTypeId = formCollection["familyType"].ToString();
            string clinicMeetingTypeId = formCollection["meetingType"].ToString();
            string appointmentContactName = formCollection["inputName"].ToString();
            string appointmentContactNumber = formCollection["inputContactNumber"].ToString();
            string appointmentContactEmail = formCollection["inputContactEmail"].ToString();
            string appointmentContactDate = formCollection["searchDate"].ToString();
            string appointmentContactTimeStart = formCollection["clinicTimeStart"].ToString();
            string appointmentContactTimeEnd = formCollection["clinicTimeEnd"].ToString();



            Clinic.ClinicAppointmentInsert(clinicScheduleId, clinicId, clinicTaxYearId, clinicLanguageId, clinicFamilyTypeId, clinicMeetingTypeId, appointmentContactName, appointmentContactNumber, appointmentContactEmail, appointmentContactDate, appointmentContactTimeStart, appointmentContactTimeEnd);
            ViewBag.CloseJs = "test()";

            //Response.Redirect("AppointmentBooking/" + clinicId + "");


            //string searchQuery = formCollection["searchQuery"].ToString();

            ViewBag.Controller = this.ControllerContext.RouteData.Values["controller"].ToString().ToUpper();
            ViewBag.Id = clinicScheduleId;
            ViewBag.Date = appointmentContactDate;
            ViewBag.Capacity = Clinic.ClinicAppointmentCapacity(clinicScheduleId, appointmentContactDate);

            var mixmodel = new ModelMix
            {
                ChannelList = Channel.ChannelList(),
                ChannelListLogin = Channel.ChannelListLogin(),
                ChannelChildList = Channel.ChannelChildList(ViewBag.Controller),

                ClinicSchedules = Clinic.ClinicAppointmentViewInfo(clinicScheduleId),
                ClinicScheduleTaxYears = Clinic.ClinicAppointmentViewTaxYear(clinicScheduleId),
                ClinicScheduleLanguages = Clinic.ClinicAppointmentViewLanguage(clinicScheduleId),
                ClinicScheduleMeetingTypes = Clinic.ClinicAppointmentViewMeetingType(clinicScheduleId),
                clinicScheduleFamilyTypes = Clinic.ClinicAppointmentViewFamilyType(clinicScheduleId)
            };

            return View(mixmodel);
        }
    }
}