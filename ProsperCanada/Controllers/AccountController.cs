using ProsperCanada.Areas.Channel.Controllers;
using ProsperCanada.Areas.Clinic.Controllers;
using ProsperCanada.Areas.Users.Controllers;
using ProsperCanada.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProsperCanada.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
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

        [HttpPost]
        public ActionResult Login(FormCollection formCollection)
        {
            Session.Contents.Abandon();
            FormsAuthentication.SignOut();

            var name = this.User.Identity.Name;
            ViewBag.name = name;

            var Channel = DependencyResolver.Current.GetService<ChannelController>();
            var Clinic = DependencyResolver.Current.GetService<ClinicController>();
            var User = DependencyResolver.Current.GetService<UsersController>();

            string username = formCollection["username"].ToString();
            string password = formCollection["password"].ToString();


            var mixmodel = new ModelMix
            {
                ClinicList = Clinic.ClinicListing(),
                ChannelList = Channel.ChannelList(),
                ChannelListLogin = Channel.ChannelListLogin(),
                ChannelChildList = Channel.ChannelChildList(ViewBag.Controller)

            };         

            IList<User> users = User.UserLogin(username, password);
            foreach (var u in users)
            {
                string uName = u.userName;
                
                FormsAuthentication.SetAuthCookie(uName.ToUpper(), false);
                Response.Redirect("~/Home");
            }


            return View(mixmodel);



        }

        public ActionResult Logout()
        {
            Session.Contents.Abandon();
            FormsAuthentication.SignOut();

            return RedirectToAction("Login");
        }

        public ActionResult ManageAppointment()
        {
            var name = this.User.Identity.Name;
            ViewBag.name = name;


            if (name == "")
            {
                Response.Redirect("~/Account/Login");

            }
            var Channel = DependencyResolver.Current.GetService<ChannelController>();
            var Clinic = DependencyResolver.Current.GetService<ClinicController>();
            var User = DependencyResolver.Current.GetService<UsersController>();

            ViewBag.Controller = this.ControllerContext.RouteData.Values["controller"].ToString().ToUpper();

            var mixmodel = new ModelMix
            {
                ClinicList = Clinic.ClinicListing(),
                ChannelList = Channel.ChannelList(),
                ChannelListLogin = Channel.ChannelListLogin(),
                ChannelChildList = Channel.ChannelChildList(ViewBag.Controller),

                Appointments = User.SelectManageAppointment(name)
            };

            return View(mixmodel);
        }

        public ActionResult AppointmentCancel(string ID)
        {
            var name = this.User.Identity.Name;
            ViewBag.name = name;


            if (name == "")
            {
                Response.Redirect("~/Account/Login");

            }
            var Channel = DependencyResolver.Current.GetService<ChannelController>();
            var Clinic = DependencyResolver.Current.GetService<ClinicController>();
            var User = DependencyResolver.Current.GetService<UsersController>();

            ViewBag.Controller = this.ControllerContext.RouteData.Values["controller"].ToString().ToUpper();

            var mixmodel = new ModelMix
            {
                ClinicList = Clinic.ClinicListing(),
                ChannelList = Channel.ChannelList(),
                ChannelListLogin = Channel.ChannelListLogin(),
                ChannelChildList = Channel.ChannelChildList(ViewBag.Controller),
                ClinicAdmin = Clinic.ClinicAdminAppointmentView(ID),
                //Appointments = User.SelectManageAppointment(name)
            };

            return View(mixmodel);
        }

        [HttpPost]
        public ActionResult AppointmentCancel(FormCollection formCollection)
        {

            var name = this.User.Identity.Name;
            ViewBag.name = name;


            if (name == "")
            {
                Response.Redirect("~/Account/Login");

            }
            var Channel = DependencyResolver.Current.GetService<ChannelController>();
            var Clinic = DependencyResolver.Current.GetService<ClinicController>();
            var User = DependencyResolver.Current.GetService<UsersController>();

            ViewBag.Controller = this.ControllerContext.RouteData.Values["controller"].ToString().ToUpper();

            string appointmentId = formCollection["appointmentId"].ToString();
            string cancelReason = formCollection["cancelReason"].ToString();

            var update = Clinic.AppointmentCancelUpdate(appointmentId, cancelReason);

            var mixmodel = new ModelMix
            {
                ClinicList = Clinic.ClinicListing(),
                ChannelList = Channel.ChannelList(),
                ChannelListLogin = Channel.ChannelListLogin(),
                ChannelChildList = Channel.ChannelChildList(ViewBag.Controller),
                //ClinicAdmin = Clinic.ClinicAdminAppointmentView(ID),
                //Appointments = User.SelectManageAppointment(name)
            };

            return RedirectToAction("ManageAppointment");
        }
    }
}