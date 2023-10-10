using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using ProsperCanada.Models;

using System.Data.Entity;


using ProsperCanada.Areas.Channel.Controllers;
using ProsperCanada.Areas.Clinic.Controllers;



using System.Net;
using System.Net.Mail;
using System.Configuration;

using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

using System.Threading;
using System.Globalization;
using System.Data.Common;
using System.Web.Configuration;
using System.Net.Configuration;
using System.Web.Security;



namespace ProsperCanada.Controllers
{
    public class HomeController : Controller
    {
        SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBConnect"]);
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
            ViewBag.Title = "Index";

            var mixmodel = new ModelMix
            {
                ClinicList = Clinic.ClinicListing(),

                ChannelList = Channel.ChannelList(),
                ChannelListLogin = Channel.ChannelListLogin(),
                ChannelChildList = Channel.ChannelChildList(ViewBag.Controller)

            };

            return View(mixmodel);
        }

        public ActionResult About()
        {
            var Channel = DependencyResolver.Current.GetService<ChannelController>();
            var Clinic  = DependencyResolver.Current.GetService<ClinicController>();

            var mixmodel = new ModelMix
            {
                ClinicList = Clinic.ClinicListing(),
                ChannelList = Channel.ChannelList()

            };


            return View(mixmodel);

        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        

        //public IList<Channel> ChannelListing()
        //{
        //    using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
        //    {
        //        var channel = liDBq.Database.SqlQuery<Channel>("EXEC spSelChannel").ToList();

        //        return channel;
        //    }
        //}

        //public ActionResult ClinicList()
        //{

        //    var Channel = DependencyResolver.Current.GetService<ChannelController>();
        //    ViewBag.Controller = this.ControllerContext.RouteData.Values["controller"].ToString().ToUpper();

        //    var mixmodel = new ModelMix
        //    {
        //        ClinicList = ClinicListing(),
        //        ChannelList = Channel.ChannelList(),
        //        ChannelChildList = Channel.ChannelChildList(ViewBag.Controller)

        //    };

        //    return View(mixmodel);
        //}






        
        

    }
}