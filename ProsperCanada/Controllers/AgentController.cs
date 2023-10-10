using ProsperCanada.Areas.Channel.Controllers;
using ProsperCanada.Areas.Clinic.Controllers;
using ProsperCanada.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProsperCanada.Controllers
{
    public class AgentController : Controller
    {
        // GET: Agent
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

        public ActionResult AssignedAppointment()
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
                ChannelChildList = Channel.ChannelChildList(ViewBag.Controller),
                ClinicAdmin = Clinic.SelectAgentAppointment(name)

            };

            return View(mixmodel);
        }
    }
}