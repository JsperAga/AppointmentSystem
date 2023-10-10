using ProsperCanada.Areas.Channel.Controllers;
using ProsperCanada.Areas.Clinic.Controllers;
using ProsperCanada.Areas.Users.Controllers;

using ProsperCanada.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProsperCanada.Controllers
{
    public class ClinicAdminController : Controller
    {
        // GET: ClinicAdmin
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
                
                ChannelList = Channel.ChannelList(),
                ChannelListLogin = Channel.ChannelListLogin(),
                ChannelChildList = Channel.ChannelChildList(ViewBag.Controller)

            };

            return View(mixmodel);
        }

        public ActionResult ClinicAppointment()
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
                ClinicAdmin = Clinic.ClinicAdminAppointment(name)

            };

            return View(mixmodel);
        }

        public ActionResult AppointmentAssign(string ID, string clinicId)
        {
            var name = this.User.Identity.Name;
            ViewBag.name = name;


            if (name == "")
            {
                Response.Redirect("~/Account/Login");

            }
            var Channel = DependencyResolver.Current.GetService<ChannelController>();
            var Clinic = DependencyResolver.Current.GetService<ClinicController>();
            var Users = DependencyResolver.Current.GetService<UsersController>();

            ViewBag.Controller = this.ControllerContext.RouteData.Values["controller"].ToString().ToUpper();

            var mixmodel = new ModelMix
            {

                ChannelList = Channel.ChannelList(),
                ChannelListLogin = Channel.ChannelListLogin(),
                ChannelChildList = Channel.ChannelChildList(ViewBag.Controller),
                ClinicAdmin = Clinic.ClinicAdminAppointmentView(ID),
                UserList = Users.SelectUserAgent(clinicId)

            };

            return View(mixmodel);
        }

        [HttpPost]
        public ActionResult AppointmentAssign(FormCollection formCollection)
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

            string myUsers = formCollection["myUsers"].ToString();
            string appointmentId = formCollection["appointmentId"].ToString();
      
            var mixmodel = new ModelMix
            {
                
                ChannelList = Channel.ChannelList(),
                ChannelListLogin = Channel.ChannelListLogin(),
                ChannelChildList = Channel.ChannelChildList(ViewBag.Controller),
                //ClinicAdmin = Clinic.ClinicAdminAppointmentView(ID),
                ClinicAdmin = Clinic.ClinicAppointmentAgentAdd(myUsers, appointmentId)
                
          
            };
            return RedirectToAction("ClinicAppointment");
        }
    }
}