using ProsperCanada.Areas.Channel.Controllers;
using ProsperCanada.Areas.Clinic.Controllers;
//using ProsperCanada.Areas.MeetingType.Controllers;
//using ProsperCanada.Areas.TaxYear.Controllers;
//using ProsperCanada.Areas.Language.Controllers;
//using ProsperCanada.Areas.FamilyType.Controllers;
using ProsperCanada.Areas.Setup.Controllers;
using ProsperCanada.Areas.Users.Controllers;

using ProsperCanada.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Web;
using System.Web.Mvc;

namespace ProsperCanada.Controllers
{
    public class SettingsController : Controller
    {
        // GET: Settings
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
        ////////////////////////////
        /// Clinic Configuration ///
        ////////////////////////////
        public ActionResult Clinic()
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

        public ActionResult ClinicAdd()
        {
            var name = this.User.Identity.Name;
            ViewBag.name = name;


            if (name == "")
            {
                Response.Redirect("~/Account/Login");

            }

            var Channel = DependencyResolver.Current.GetService<ChannelController>();
            var Clinic = DependencyResolver.Current.GetService<ClinicController>();
            var Settings = DependencyResolver.Current.GetService<SetupController>();

            ViewBag.Controller = this.ControllerContext.RouteData.Values["controller"].ToString().ToUpper();

            var mixmodel = new ModelMix
            {
                ClinicList = Clinic.ClinicListing(),

                ChannelList = Channel.ChannelList(),
                ChannelListLogin = Channel.ChannelListLogin(),
                ChannelChildList = Channel.ChannelChildList(ViewBag.Controller),

                TaxYear = Settings.SetupList("TaxYear"),
                Language = Settings.SetupList("Language"),
                MeetingType = Settings.SetupList("MeetingType"),
                FamilyType = Settings.SetupList("FamilyType"),
                CountryList = Settings.SetupCountries()

            };

            return View(mixmodel);
        }

        [HttpPost]
        public ActionResult ClinicAdd(FormCollection formCollection)
        {
            var name = this.User.Identity.Name;
            ViewBag.name = name;


            if (name == "")
            {
                Response.Redirect("~/Account/Login");

            }

            var Settings = DependencyResolver.Current.GetService<SetupController>();

            string clinicName = formCollection["inputClinicName"].ToString();
            string clinicStreet = formCollection["inputStreet"].ToString();
            string clinicUnit = formCollection["inputUnit"].ToString();
            string clinicCity = formCollection["inputCity"].ToString();
            string clinicState = formCollection["inputState"].ToString();
            string clinicCountry = formCollection["inputCountry"].ToString();
            string clinicPostalCode = formCollection["inputPostalCode"].ToString();            

            var clinicId = Settings.SetupAddClinic(clinicName, clinicStreet, clinicUnit, clinicCity, clinicState, clinicCountry, clinicPostalCode);



            if (formCollection.AllKeys.Contains("myTaxYear"))
            {
                string[] selectedTax = formCollection["myTaxYear"].Split(',');

                foreach (var tax in selectedTax)
                {
                    Settings.SetupAddTaxYear(clinicId.ToString(), tax);                    
                }
            }            

            if (formCollection.AllKeys.Contains("myLanguage"))
            {
                string[] selectedLanguage = formCollection["myLanguage"].Split(',');

                foreach (var lang in selectedLanguage)
                {
                    Settings.SetupAddLanguage(clinicId.ToString(), lang);
                }
            }            

            if (formCollection.AllKeys.Contains("myMeetingType"))
            {
                string[] selectedMeeting = formCollection["myMeetingType"].Split(',');

                foreach (var meet in selectedMeeting)
                {
                    Settings.SetupAddMeetingType(clinicId.ToString(), meet);
                }
            }

            

            if (formCollection.AllKeys.Contains("myFamilyType"))
            {
                string[] selectedfamily = formCollection["myFamilyType"].Split(',');

                foreach (var fam in selectedfamily)
                {
                    Settings.SetupAddFamilyType(clinicId.ToString(), fam);
                }
            }

            return RedirectToAction("Clinic");
        }

        public ActionResult ClinicEdit(string ID)
        {
            var name = this.User.Identity.Name;
            ViewBag.name = name;


            if (name == "")
            {
                Response.Redirect("~/Account/Login");

            }
            var Channel = DependencyResolver.Current.GetService<ChannelController>();
            var Clinic = DependencyResolver.Current.GetService<ClinicController>();
            var Settings = DependencyResolver.Current.GetService<SetupController>();

            ViewBag.Controller = this.ControllerContext.RouteData.Values["controller"].ToString().ToUpper();
            ViewBag.Id = ID;

            var mixmodel = new ModelMix
            {
                ClinicList = Clinic.ClinicView(ID),
                ChannelList = Channel.ChannelList(),
                ChannelListLogin = Channel.ChannelListLogin(),
                ChannelChildList = Channel.ChannelChildList(ViewBag.Controller),
                TaxYear = Settings.SetupList("TaxYear"),
                Language = Settings.SetupList("Language"),
                MeetingType = Settings.SetupList("MeetingType"),
                FamilyType = Settings.SetupList("FamilyType"),
                CountryList = Settings.SetupCountries(),

                ClinicTaxYears = Clinic.ClinicTaxYearSelect(ID),
                ClinicLanguages = Clinic.ClinicLanguageSelect(ID),
                ClinicMeetingTypes = Clinic.ClinicMeetingTypeSelect(ID),
                clinicFamilyTypes = Clinic.ClinicFamilyTypeSelect(ID)

            };

            return View(mixmodel);
        }

        [HttpPost]
        public ActionResult ClinicEdit(FormCollection formCollection)
        {
            var name = this.User.Identity.Name;
            ViewBag.name = name;


            if (name == "")
            {
                Response.Redirect("~/Account/Login");

            }

            var Settings = DependencyResolver.Current.GetService<SetupController>();

            // Variable Declaration
            string clinicName = formCollection["inputClinicName"].ToString();
            string clinicStreet = formCollection["inputStreet"].ToString();
            string clinicUnit = formCollection["inputUnit"].ToString();
            string clinicCity = formCollection["inputCity"].ToString();
            string clinicState = formCollection["inputState"].ToString();
            string clinicCountry = formCollection["inputCountry"].ToString();
            string clinicPostalCode = formCollection["inputPostalCode"].ToString();

            string clinicId = formCollection["inputSettingId"].ToString();

            // Update Clinic
            Settings.SetupUpdateClinic(clinicName, clinicStreet, clinicUnit, clinicCity, clinicState, clinicCountry, clinicPostalCode, clinicId);

            // Delete Setup
            Settings.SetupDeleteTaxYear(clinicId.ToString());
            Settings.SetupDeleteLanguage(clinicId.ToString());
            Settings.SetupDeleteMeetingType(clinicId.ToString());
            Settings.SetupDeleteFamilyType(clinicId.ToString());

            // Insert Setup
            if (formCollection.AllKeys.Contains("myTaxYear"))
            {
                string[] selectedTax = formCollection["myTaxYear"].Split(',');

                foreach (var tax in selectedTax)
                {
                    Settings.SetupAddTaxYear(clinicId.ToString(), tax);                    
                }
            }            

            if (formCollection.AllKeys.Contains("myLanguage"))
            {
                string[] selectedLanguage = formCollection["myLanguage"].Split(',');

                foreach (var lang in selectedLanguage)
                {
                    Settings.SetupAddLanguage(clinicId.ToString(), lang);
                }
            }            

            if (formCollection.AllKeys.Contains("myMeetingType"))
            {
                string[] selectedMeeting = formCollection["myMeetingType"].Split(',');

                foreach (var meet in selectedMeeting)
                {
                    Settings.SetupAddMeetingType(clinicId.ToString(), meet);
                }
            }            

            if (formCollection.AllKeys.Contains("myFamilyType"))
            {
                string[] selectedfamily = formCollection["myFamilyType"].Split(',');

                foreach (var fam in selectedfamily)
                {
                    Settings.SetupAddFamilyType(clinicId.ToString(), fam);
                }
            }

            // Redirection
            return RedirectToAction("Clinic");
        }

        public ActionResult ClinicViewEligibility(string ID)
        {
            var name = this.User.Identity.Name;
            ViewBag.name = name;


            if (name == "")
            {
                Response.Redirect("~/Account/Login");

            }

            var Channel = DependencyResolver.Current.GetService<ChannelController>();
            var Clinic = DependencyResolver.Current.GetService<ClinicController>();
            var Settings = DependencyResolver.Current.GetService<SetupController>();

            ViewBag.Controller = this.ControllerContext.RouteData.Values["controller"].ToString().ToUpper();
            ViewBag.ID = ID;
            var mixmodel = new ModelMix
            {
                MessageList = Clinic.ClinicViewEligibilities(ID),
                ChannelList = Channel.ChannelList(),
                ChannelListLogin = Channel.ChannelListLogin(),
                ChannelChildList = Channel.ChannelChildList(ViewBag.Controller)
            };

            return View(mixmodel);
        }

        public ActionResult ClinicEditMessage(string ID)
        {
            var name = this.User.Identity.Name;
            ViewBag.name = name;


            if (name == "")
            {
                Response.Redirect("~/Account/Login");

            }

            var Channel = DependencyResolver.Current.GetService<ChannelController>();
            var Clinic = DependencyResolver.Current.GetService<ClinicController>();
            var Settings = DependencyResolver.Current.GetService<SetupController>();

            ViewBag.Controller = this.ControllerContext.RouteData.Values["controller"].ToString().ToUpper();

            var mixmodel = new ModelMix
            {
                MessageList = Clinic.ClinicViewMessageEligibilities(ID),
                ChannelList = Channel.ChannelList(),
                ChannelListLogin = Channel.ChannelListLogin(),
                ChannelChildList = Channel.ChannelChildList(ViewBag.Controller)
            };

            return View(mixmodel);

        }

        [HttpPost]
        public ActionResult ClinicEditMessage(FormCollection formCollection)
        {
            var name = this.User.Identity.Name;
            ViewBag.name = name;


            if (name == "")
            {
                Response.Redirect("~/Account/Login");

            }

            var Channel = DependencyResolver.Current.GetService<ChannelController>();
            var Clinic = DependencyResolver.Current.GetService<ClinicController>();
            var Settings = DependencyResolver.Current.GetService<SetupController>();

            string inputTitle = formCollection["inputTitle"].ToString();
            string inputDescription = formCollection["inputDescription"].ToString();
            string selectActive = formCollection["selectActive"].ToString();
            string inputMessageId = formCollection["inputMessageId"].ToString();
            string inputClinicId = formCollection["inputClinicId"].ToString();
            string inputClinicType = formCollection["inputClinicType"].ToString();

            var mixmodel = new ModelMix
            {
                ClinicList = Clinic.ClinicListing(),
                ChannelList = Channel.ChannelList(),
                ChannelListLogin = Channel.ChannelListLogin(),
                ChannelChildList = Channel.ChannelChildList(ViewBag.Controller),
                
                Message = Clinic.ClinicUpdateMessageEligibility(inputTitle, inputDescription, selectActive, inputMessageId, inputClinicId, inputClinicType)
            };


            return RedirectToAction("ClinicViewEligibility", new {id = inputClinicId });
        }

        public ActionResult ClinicAgent(string ID)
        {
            var name = this.User.Identity.Name;
            ViewBag.name = name;

            if (name == "")
            {
                Response.Redirect("~/Account/Login");

            }

            var Channel = DependencyResolver.Current.GetService<ChannelController>();
            var Clinic = DependencyResolver.Current.GetService<ClinicController>();
            var Settings = DependencyResolver.Current.GetService<SetupController>();
            var Users = DependencyResolver.Current.GetService<UsersController>();

            ViewBag.Controller = this.ControllerContext.RouteData.Values["controller"].ToString().ToUpper();
            ViewBag.ID = ID;

            var mixmodel = new ModelMix
            {                
                ChannelList = Channel.ChannelList(),
                ChannelListLogin = Channel.ChannelListLogin(),
                ChannelChildList = Channel.ChannelChildList(ViewBag.Controller),
                UserList = Users.SelectUserAgent(ID)

            };

            return View(mixmodel);
        }

        public ActionResult AgentAdd(string ID)
        {
            var name = this.User.Identity.Name;
            ViewBag.name = name;

            if (name == "")
            {
                Response.Redirect("~/Account/Login");

            }

            var Channel = DependencyResolver.Current.GetService<ChannelController>();
            var Clinic = DependencyResolver.Current.GetService<ClinicController>();
            var Settings = DependencyResolver.Current.GetService<SetupController>();
            var Users = DependencyResolver.Current.GetService<UsersController>();

            ViewBag.Controller = this.ControllerContext.RouteData.Values["controller"].ToString().ToUpper();
            ViewBag.ID = ID;

            var mixmodel = new ModelMix
            {
                ChannelList = Channel.ChannelList(),
                ChannelListLogin = Channel.ChannelListLogin(),
                ChannelChildList = Channel.ChannelChildList(ViewBag.Controller),
                UserList = Users.SelectUser()

            };

            return View(mixmodel);

        }

        [HttpPost]
        public ActionResult AgentAdd(FormCollection formCollection)
        {
            

            var name = this.User.Identity.Name;
            ViewBag.name = name;

            if (name == "")
            {
                Response.Redirect("~/Account/Login");

            }

            var Channel = DependencyResolver.Current.GetService<ChannelController>();
            var Clinic = DependencyResolver.Current.GetService<ClinicController>();
            var Settings = DependencyResolver.Current.GetService<SetupController>();
            var Users = DependencyResolver.Current.GetService<UsersController>();

            ViewBag.Controller = this.ControllerContext.RouteData.Values["controller"].ToString().ToUpper();
            
            string selectUser = formCollection["selectUser"].ToString();
            string selectRole = formCollection["selectRole"].ToString();
            string clinicId = formCollection["clinicId"].ToString();

            var userRole = Users.UserRoleAdd(clinicId, selectUser, selectRole);

            var mixmodel = new ModelMix
            {
                ChannelList = Channel.ChannelList(),
                ChannelListLogin = Channel.ChannelListLogin(),
                ChannelChildList = Channel.ChannelChildList(ViewBag.Controller)
                
            };

            return RedirectToAction("ClinicAgent", new { id = clinicId });
        }

        //////////////////////////////////
        /// Meeting Type Configuration ///
        //////////////////////////////////

        public ActionResult MeetingType()
        {
            var name = this.User.Identity.Name;
            ViewBag.name = name;


            if (name == "")
            {
                Response.Redirect("~/Account/Login");

            }

            var Channel = DependencyResolver.Current.GetService<ChannelController>();
            var Clinic = DependencyResolver.Current.GetService<ClinicController>();
            var Settings = DependencyResolver.Current.GetService<SetupController>();

            ViewBag.Controller = this.ControllerContext.RouteData.Values["controller"].ToString().ToUpper();

            var mixmodel = new ModelMix
            {
                ClinicList = Clinic.ClinicListing(),
                ChannelList = Channel.ChannelList(),
                ChannelListLogin = Channel.ChannelListLogin(),
                ChannelChildList = Channel.ChannelChildList(ViewBag.Controller),
                SettingsList = Settings.SetupList("MeetingType")

            };

            return View(mixmodel);
        }

        public ActionResult MeetingTypeAdd()
        {
            var name = this.User.Identity.Name;
            ViewBag.name = name;


            if (name == "")
            {
                Response.Redirect("~/Account/Login");

            }
            var Channel = DependencyResolver.Current.GetService<ChannelController>();
            var Clinic = DependencyResolver.Current.GetService<ClinicController>();
            var Settings = DependencyResolver.Current.GetService<SetupController>();

            ViewBag.Controller = this.ControllerContext.RouteData.Values["controller"].ToString().ToUpper();

            var mixmodel = new ModelMix
            {
                ClinicList = Clinic.ClinicListing(),
                ChannelList = Channel.ChannelList(),
                ChannelListLogin = Channel.ChannelListLogin(),
                ChannelChildList = Channel.ChannelChildList(ViewBag.Controller),
                SettingsList = Settings.SetupList("MeetingType")

            };

            return View(mixmodel);
        }

        [HttpPost]
        public ActionResult MeetingTypeAdd(string inputMeetingType, string inputDescription, string selectActive)
        {
            var name = this.User.Identity.Name;
            ViewBag.name = name;


            if (name == "")
            {
                Response.Redirect("~/Account/Login");

            }
            var Channel = DependencyResolver.Current.GetService<ChannelController>();
            var Clinic = DependencyResolver.Current.GetService<ClinicController>();
            var Settings = DependencyResolver.Current.GetService<SetupController>();

            ViewBag.Controller = this.ControllerContext.RouteData.Values["controller"].ToString().ToUpper();

            var mixmodel = new ModelMix
            {
                ClinicList = Clinic.ClinicListing(),
                ChannelList = Channel.ChannelList(),
                ChannelListLogin = Channel.ChannelListLogin(),
                ChannelChildList = Channel.ChannelChildList(ViewBag.Controller),
                SettingsList = Settings.SetupList("MeetingType"),
                Message = Settings.SetupAdd(inputMeetingType, inputDescription, selectActive, "MeetingType")
            };


            return View(mixmodel);
        }

        public ActionResult MeetingTypeEdit(string ID)
        {
            var name = this.User.Identity.Name;
            ViewBag.name = name;


            if (name == "")
            {
                Response.Redirect("~/Account/Login");

            }
            var Channel = DependencyResolver.Current.GetService<ChannelController>();
            var Clinic = DependencyResolver.Current.GetService<ClinicController>();
            var Settings = DependencyResolver.Current.GetService<SetupController>();

            ViewBag.Controller = this.ControllerContext.RouteData.Values["controller"].ToString().ToUpper();
            ViewBag.Id = ID;

            var mixmodel = new ModelMix
            {
                ClinicList = Clinic.ClinicListing(),
                ChannelList = Channel.ChannelList(),
                ChannelListLogin = Channel.ChannelListLogin(),
                ChannelChildList = Channel.ChannelChildList(ViewBag.Controller),
                SettingsList = Settings.SetupView(ID,"MeetingType")
               
            };

            return View(mixmodel);
        }

        [HttpPost]
        public ActionResult MeetingTypeEdit(string inputMeetingType, string inputDescription, string selectActive, string inputSettingId)
        {
            var name = this.User.Identity.Name;
            ViewBag.name = name;


            if (name == "")
            {
                Response.Redirect("~/Account/Login");

            }
            var Channel = DependencyResolver.Current.GetService<ChannelController>();
            var Clinic = DependencyResolver.Current.GetService<ClinicController>();
            var Settings = DependencyResolver.Current.GetService<SetupController>();

            ViewBag.Controller = this.ControllerContext.RouteData.Values["controller"].ToString().ToUpper();

            var mixmodel = new ModelMix
            {
                ClinicList = Clinic.ClinicListing(),
                ChannelList = Channel.ChannelList(),
                ChannelListLogin = Channel.ChannelListLogin(),
                ChannelChildList = Channel.ChannelChildList(ViewBag.Controller),
                SettingsList = Settings.SetupList("MeetingType"),
                Message = Settings.SetupUpdate(inputSettingId, inputMeetingType, inputDescription, selectActive, "MeetingType")
            };

            Response.Redirect("../MeetingType");
            return View(mixmodel);
        }


        //////////////////////////////
        /// Tax Year Configuration ///
        //////////////////////////////

        public ActionResult TaxYear()
        {
            var name = this.User.Identity.Name;
            ViewBag.name = name;


            if (name == "")
            {
                Response.Redirect("~/Account/Login");

            }
            var Channel = DependencyResolver.Current.GetService<ChannelController>();
            var Clinic = DependencyResolver.Current.GetService<ClinicController>();
            var Settings = DependencyResolver.Current.GetService<SetupController>();

            ViewBag.Controller = this.ControllerContext.RouteData.Values["controller"].ToString().ToUpper();

            var mixmodel = new ModelMix
            {
                ClinicList = Clinic.ClinicListing(),
                ChannelList = Channel.ChannelList(),
                ChannelListLogin = Channel.ChannelListLogin(),
                ChannelChildList = Channel.ChannelChildList(ViewBag.Controller),
                SettingsList = Settings.SetupList("TaxYear")

            };

            return View(mixmodel);
        }

        public ActionResult TaxYearAdd()
        {
            var name = this.User.Identity.Name;
            ViewBag.name = name;


            if (name == "")
            {
                Response.Redirect("~/Account/Login");

            }
            var Channel = DependencyResolver.Current.GetService<ChannelController>();
            var Clinic = DependencyResolver.Current.GetService<ClinicController>();
            var Settings = DependencyResolver.Current.GetService<SetupController>();

            ViewBag.Controller = this.ControllerContext.RouteData.Values["controller"].ToString().ToUpper();

            var mixmodel = new ModelMix
            {
                ClinicList = Clinic.ClinicListing(),
                ChannelList = Channel.ChannelList(),
                ChannelListLogin = Channel.ChannelListLogin(),
                ChannelChildList = Channel.ChannelChildList(ViewBag.Controller),
                SettingsList = Settings.SetupList("TaxYear")

            };

            return View(mixmodel);
        }

        [HttpPost]
        public ActionResult TaxYearAdd(string inputTaxYear, string inputDescription, string selectActive)
        {
            var name = this.User.Identity.Name;
            ViewBag.name = name;


            if (name == "")
            {
                Response.Redirect("~/Account/Login");

            }
            var Channel = DependencyResolver.Current.GetService<ChannelController>();
            var Clinic = DependencyResolver.Current.GetService<ClinicController>();
            var Settings = DependencyResolver.Current.GetService<SetupController>();

            ViewBag.Controller = this.ControllerContext.RouteData.Values["controller"].ToString().ToUpper();

            var mixmodel = new ModelMix
            {
                ClinicList = Clinic.ClinicListing(),
                ChannelList = Channel.ChannelList(),
                ChannelListLogin = Channel.ChannelListLogin(),
                ChannelChildList = Channel.ChannelChildList(ViewBag.Controller),
                SettingsList = Settings.SetupList("TaxYear"),
                Message = Settings.SetupAdd(inputTaxYear, inputDescription, selectActive, "TaxYear")
            };


            return View(mixmodel);
        }

        public ActionResult TaxYearEdit(string ID)
        {
            var name = this.User.Identity.Name;
            ViewBag.name = name;


            if (name == "")
            {
                Response.Redirect("~/Account/Login");

            }
            var Channel = DependencyResolver.Current.GetService<ChannelController>();
            var Clinic = DependencyResolver.Current.GetService<ClinicController>();
            var Settings = DependencyResolver.Current.GetService<SetupController>();

            ViewBag.Controller = this.ControllerContext.RouteData.Values["controller"].ToString().ToUpper();
            ViewBag.Id = ID;

            var mixmodel = new ModelMix
            {
                ClinicList = Clinic.ClinicListing(),
                ChannelList = Channel.ChannelList(),
                ChannelListLogin = Channel.ChannelListLogin(),
                ChannelChildList = Channel.ChannelChildList(ViewBag.Controller),
                SettingsList = Settings.SetupView(ID, "TaxYear")

            };

            return View(mixmodel);
        }

        [HttpPost]
        public ActionResult TaxYearEdit(string inputMeetingType, string inputDescription, string selectActive, string inputSettingId)
        {
            var name = this.User.Identity.Name;
            ViewBag.name = name;


            if (name == "")
            {
                Response.Redirect("~/Account/Login");

            }
            var Channel = DependencyResolver.Current.GetService<ChannelController>();
            var Clinic = DependencyResolver.Current.GetService<ClinicController>();
            var Settings = DependencyResolver.Current.GetService<SetupController>();

            ViewBag.Controller = this.ControllerContext.RouteData.Values["controller"].ToString().ToUpper();

            var mixmodel = new ModelMix
            {
                ClinicList = Clinic.ClinicListing(),
                ChannelList = Channel.ChannelList(),
                ChannelListLogin = Channel.ChannelListLogin(),
                ChannelChildList = Channel.ChannelChildList(ViewBag.Controller),
                SettingsList = Settings.SetupList("TaxYear"),
                Message = Settings.SetupUpdate(inputSettingId, inputMeetingType, inputDescription, selectActive, "TaxYear")
            };

            Response.Redirect("../TaxYear");
            return View(mixmodel);
        }

        //////////////////////////////
        /// Language Configuration ///
        //////////////////////////////
        public ActionResult Language()
        {
            var name = this.User.Identity.Name;
            ViewBag.name = name;


            if (name == "")
            {
                Response.Redirect("~/Account/Login");

            }
            var Channel = DependencyResolver.Current.GetService<ChannelController>();
            var Clinic = DependencyResolver.Current.GetService<ClinicController>();
            var Settings = DependencyResolver.Current.GetService<SetupController>();

            ViewBag.Controller = this.ControllerContext.RouteData.Values["controller"].ToString().ToUpper();

            var mixmodel = new ModelMix
            {
                ClinicList = Clinic.ClinicListing(),
                ChannelList = Channel.ChannelList(),
                ChannelListLogin = Channel.ChannelListLogin(),
                ChannelChildList = Channel.ChannelChildList(ViewBag.Controller),
                SettingsList = Settings.SetupList("Language")

            };

            return View(mixmodel);
        }

        public ActionResult LanguageAdd()
        {
            var name = this.User.Identity.Name;
            ViewBag.name = name;


            if (name == "")
            {
                Response.Redirect("~/Account/Login");

            }
            var Channel = DependencyResolver.Current.GetService<ChannelController>();
            var Clinic = DependencyResolver.Current.GetService<ClinicController>();
            var Settings = DependencyResolver.Current.GetService<SetupController>();

            ViewBag.Controller = this.ControllerContext.RouteData.Values["controller"].ToString().ToUpper();

            var mixmodel = new ModelMix
            {
                ClinicList = Clinic.ClinicListing(),
                ChannelList = Channel.ChannelList(),
                ChannelListLogin = Channel.ChannelListLogin(),
                ChannelChildList = Channel.ChannelChildList(ViewBag.Controller),
                SettingsList = Settings.SetupList("Language")

            };

            return View(mixmodel);
        }

        [HttpPost]
        public ActionResult LanguageAdd(string inputLanguage, string inputDescription, string selectActive)
        {
            var name = this.User.Identity.Name;
            ViewBag.name = name;


            if (name == "")
            {
                Response.Redirect("~/Account/Login");

            }
            var Channel = DependencyResolver.Current.GetService<ChannelController>();
            var Clinic = DependencyResolver.Current.GetService<ClinicController>();
            var Settings = DependencyResolver.Current.GetService<SetupController>();

            ViewBag.Controller = this.ControllerContext.RouteData.Values["controller"].ToString().ToUpper();

            var mixmodel = new ModelMix
            {
                ClinicList = Clinic.ClinicListing(),
                ChannelList = Channel.ChannelList(),
                ChannelListLogin = Channel.ChannelListLogin(),
                ChannelChildList = Channel.ChannelChildList(ViewBag.Controller),
                SettingsList = Settings.SetupList("Language"),
                Message = Settings.SetupAdd(inputLanguage, inputDescription, selectActive, "Language")
            };


            return View(mixmodel);
        }

        public ActionResult LanguageEdit(string ID)
        {
            var name = this.User.Identity.Name;
            ViewBag.name = name;


            if (name == "")
            {
                Response.Redirect("~/Account/Login");

            }
            var Channel = DependencyResolver.Current.GetService<ChannelController>();
            var Clinic = DependencyResolver.Current.GetService<ClinicController>();
            var Settings = DependencyResolver.Current.GetService<SetupController>();

            ViewBag.Controller = this.ControllerContext.RouteData.Values["controller"].ToString().ToUpper();
            ViewBag.Id = ID;

            var mixmodel = new ModelMix
            {
                ClinicList = Clinic.ClinicListing(),
                ChannelList = Channel.ChannelList(),
                ChannelListLogin = Channel.ChannelListLogin(),
                ChannelChildList = Channel.ChannelChildList(ViewBag.Controller),
                SettingsList = Settings.SetupView(ID, "Language")

            };

            return View(mixmodel);
        }

        [HttpPost]
        public ActionResult LanguageEdit(string inputMeetingType, string inputDescription, string selectActive, string inputSettingId)
        {
            var name = this.User.Identity.Name;
            ViewBag.name = name;


            if (name == "")
            {
                Response.Redirect("~/Account/Login");

            }
            var Channel = DependencyResolver.Current.GetService<ChannelController>();
            var Clinic = DependencyResolver.Current.GetService<ClinicController>();
            var Settings = DependencyResolver.Current.GetService<SetupController>();

            ViewBag.Controller = this.ControllerContext.RouteData.Values["controller"].ToString().ToUpper();

            var mixmodel = new ModelMix
            {
                ClinicList = Clinic.ClinicListing(),
                ChannelList = Channel.ChannelList(),
                ChannelListLogin = Channel.ChannelListLogin(),
                ChannelChildList = Channel.ChannelChildList(ViewBag.Controller),
                SettingsList = Settings.SetupList("Language"),
                Message = Settings.SetupUpdate(inputSettingId, inputMeetingType, inputDescription, selectActive, "Language")
            };

            Response.Redirect("../Language");
            return View(mixmodel);
        }
        /////////////////////////////////
        /// Family Type Configuration ///
        /////////////////////////////////

        public ActionResult FamilyType()
        {
            var name = this.User.Identity.Name;
            ViewBag.name = name;


            if (name == "")
            {
                Response.Redirect("~/Account/Login");

            }
            var Channel = DependencyResolver.Current.GetService<ChannelController>();
            var Clinic = DependencyResolver.Current.GetService<ClinicController>();
            var Settings = DependencyResolver.Current.GetService<SetupController>();

            ViewBag.Controller = this.ControllerContext.RouteData.Values["controller"].ToString().ToUpper();

            var mixmodel = new ModelMix
            {
                ClinicList = Clinic.ClinicListing(),
                ChannelList = Channel.ChannelList(),
                ChannelListLogin = Channel.ChannelListLogin(),
                ChannelChildList = Channel.ChannelChildList(ViewBag.Controller),
                SettingsList = Settings.SetupList("FamilyType")

            };

            return View(mixmodel);
        }

        public ActionResult FamilyTypeAdd()
        {
            var name = this.User.Identity.Name;
            ViewBag.name = name;


            if (name == "")
            {
                Response.Redirect("~/Account/Login");

            }
            var Channel = DependencyResolver.Current.GetService<ChannelController>();
            var Clinic = DependencyResolver.Current.GetService<ClinicController>();
            var Settings = DependencyResolver.Current.GetService<SetupController>();

            ViewBag.Controller = this.ControllerContext.RouteData.Values["controller"].ToString().ToUpper();

            var mixmodel = new ModelMix
            {
                ClinicList = Clinic.ClinicListing(),
                ChannelList = Channel.ChannelList(),
                ChannelListLogin = Channel.ChannelListLogin(),
                ChannelChildList = Channel.ChannelChildList(ViewBag.Controller),
                SettingsList = Settings.SetupList("FamilyType")

            };

            return View(mixmodel);
        }

        [HttpPost]
        public ActionResult FamilyTypeAdd(string inputFamilyType, string inputDescription, string selectActive)
        {
            var name = this.User.Identity.Name;
            ViewBag.name = name;


            if (name == "")
            {
                Response.Redirect("~/Account/Login");

            }
            var Channel = DependencyResolver.Current.GetService<ChannelController>();
            var Clinic = DependencyResolver.Current.GetService<ClinicController>();
            var Settings = DependencyResolver.Current.GetService<SetupController>();

            ViewBag.Controller = this.ControllerContext.RouteData.Values["controller"].ToString().ToUpper();

            var mixmodel = new ModelMix
            {
                ClinicList = Clinic.ClinicListing(),
                ChannelList = Channel.ChannelList(),
                ChannelListLogin = Channel.ChannelListLogin(),
                ChannelChildList = Channel.ChannelChildList(ViewBag.Controller),
                SettingsList = Settings.SetupList("FamilyType"),
                Message = Settings.SetupAdd(inputFamilyType, inputDescription, selectActive, "FamilyType")
            };


            return View(mixmodel);
        }

        public ActionResult FamilyTypeEdit(string ID)
        {
            var name = this.User.Identity.Name;
            ViewBag.name = name;


            if (name == "")
            {
                Response.Redirect("~/Account/Login");

            }
            var Channel = DependencyResolver.Current.GetService<ChannelController>();
            var Clinic = DependencyResolver.Current.GetService<ClinicController>();
            var Settings = DependencyResolver.Current.GetService<SetupController>();

            ViewBag.Controller = this.ControllerContext.RouteData.Values["controller"].ToString().ToUpper();
            ViewBag.Id = ID;

            var mixmodel = new ModelMix
            {
                ClinicList = Clinic.ClinicListing(),
                ChannelList = Channel.ChannelList(),
                ChannelListLogin = Channel.ChannelListLogin(),
                ChannelChildList = Channel.ChannelChildList(ViewBag.Controller),
                SettingsList = Settings.SetupView(ID, "FamilyType")

            };

            return View(mixmodel);
        }

        [HttpPost]
        public ActionResult FamilyTypeEdit(string inputMeetingType, string inputDescription, string selectActive, string inputSettingId)
        {
            var name = this.User.Identity.Name;
            ViewBag.name = name;


            if (name == "")
            {
                Response.Redirect("~/Account/Login");

            }
            var Channel = DependencyResolver.Current.GetService<ChannelController>();
            var Clinic = DependencyResolver.Current.GetService<ClinicController>();
            var Settings = DependencyResolver.Current.GetService<SetupController>();

            ViewBag.Controller = this.ControllerContext.RouteData.Values["controller"].ToString().ToUpper();

            var mixmodel = new ModelMix
            {
                ClinicList = Clinic.ClinicListing(),
                ChannelList = Channel.ChannelList(),
                ChannelListLogin = Channel.ChannelListLogin(),
                ChannelChildList = Channel.ChannelChildList(ViewBag.Controller),
                SettingsList = Settings.SetupList("FamilyType"),
                Message = Settings.SetupUpdate(inputSettingId, inputMeetingType, inputDescription, selectActive, "FamilyType")
            };

            Response.Redirect("../FamilyType");
            return View(mixmodel);
        }



        /////////////////////////////////
        /// Eligibility Configuration ///
        /////////////////////////////////
        
        public ActionResult EligibilityAdd(string ID)
        {
            var name = this.User.Identity.Name;
            ViewBag.name = name;


            if (name == "")
            {
                Response.Redirect("~/Account/Login");

            }
            var Channel = DependencyResolver.Current.GetService<ChannelController>();
            var Clinic = DependencyResolver.Current.GetService<ClinicController>();
            var Settings = DependencyResolver.Current.GetService<SetupController>();

            ViewBag.Controller = this.ControllerContext.RouteData.Values["controller"].ToString().ToUpper();
            ViewBag.Id = ID;

            var mixmodel = new ModelMix
            {
               
                ChannelList = Channel.ChannelList(),
                ChannelListLogin = Channel.ChannelListLogin(),
                ChannelChildList = Channel.ChannelChildList(ViewBag.Controller),
               

            };

            return View(mixmodel);
        }

        [HttpPost]
        public ActionResult EligibilityAdd(string inputTitle, string inputDescription, string selectActive, string inputClinicId)
        {
            var name = this.User.Identity.Name;
            ViewBag.name = name;


            if (name == "")
            {
                Response.Redirect("~/Account/Login");

            }
            var Channel = DependencyResolver.Current.GetService<ChannelController>();
            var Clinic = DependencyResolver.Current.GetService<ClinicController>();
            var Settings = DependencyResolver.Current.GetService<SetupController>();

            ViewBag.Controller = this.ControllerContext.RouteData.Values["controller"].ToString().ToUpper();
            

            var mixmodel = new ModelMix
            {

                ChannelList = Channel.ChannelList(),
                ChannelListLogin = Channel.ChannelListLogin(),
                ChannelChildList = Channel.ChannelChildList(ViewBag.Controller),
                Message = Settings.SetupMessageAdd(inputTitle, inputDescription, selectActive, inputClinicId,"1")

            };

            return RedirectToAction("ClinicViewEligibility", new { id = inputClinicId });
        }



        /////////////////////////////////
        /// Instruction Configuration ///
        /////////////////////////////////

        public ActionResult ClinicViewInstruction(string ID)
        {
            var name = this.User.Identity.Name;
            ViewBag.name = name;


            if (name == "")
            {
                Response.Redirect("~/Account/Login");

            }
            var Channel = DependencyResolver.Current.GetService<ChannelController>();
            var Clinic = DependencyResolver.Current.GetService<ClinicController>();
            var Settings = DependencyResolver.Current.GetService<SetupController>();

            ViewBag.Controller = this.ControllerContext.RouteData.Values["controller"].ToString().ToUpper();
            ViewBag.ID = ID;
            var mixmodel = new ModelMix
            {
                MessageList = Clinic.ClinicViewInstructions(ID),
                ChannelList = Channel.ChannelList(),
                ChannelListLogin = Channel.ChannelListLogin(),
                ChannelChildList = Channel.ChannelChildList(ViewBag.Controller)
            };

            return View(mixmodel);
        }

        public ActionResult InstructionAdd(string ID)
        {
            var name = this.User.Identity.Name;
            ViewBag.name = name;


            if (name == "")
            {
                Response.Redirect("~/Account/Login");

            }
            var Channel = DependencyResolver.Current.GetService<ChannelController>();
            var Clinic = DependencyResolver.Current.GetService<ClinicController>();
            var Settings = DependencyResolver.Current.GetService<SetupController>();

            ViewBag.Controller = this.ControllerContext.RouteData.Values["controller"].ToString().ToUpper();
            ViewBag.Id = ID;

            var mixmodel = new ModelMix
            {

                ChannelList = Channel.ChannelList(),
                ChannelListLogin = Channel.ChannelListLogin(),
                ChannelChildList = Channel.ChannelChildList(ViewBag.Controller),


            };

            return View(mixmodel);
        }

        [HttpPost]
        public ActionResult InstructionAdd(string inputTitle, string inputDescription, string selectActive, string inputClinicId)
        {
            var name = this.User.Identity.Name;
            ViewBag.name = name;


            if (name == "")
            {
                Response.Redirect("~/Account/Login");

            }
            var Channel = DependencyResolver.Current.GetService<ChannelController>();
            var Clinic = DependencyResolver.Current.GetService<ClinicController>();
            var Settings = DependencyResolver.Current.GetService<SetupController>();

            ViewBag.Controller = this.ControllerContext.RouteData.Values["controller"].ToString().ToUpper();


            var mixmodel = new ModelMix
            {

                ChannelList = Channel.ChannelList(),
                ChannelListLogin = Channel.ChannelListLogin(),
                ChannelChildList = Channel.ChannelChildList(ViewBag.Controller),
                Message = Settings.SetupMessageAdd(inputTitle, inputDescription, selectActive, inputClinicId, "2")

            };

            return RedirectToAction("ClinicViewInstruction", new { id = inputClinicId });
        }

        public ActionResult ClinicEditMessageInstruction(string ID)
        {
            var name = this.User.Identity.Name;
            ViewBag.name = name;


            if (name == "")
            {
                Response.Redirect("~/Account/Login");

            }
            var Channel = DependencyResolver.Current.GetService<ChannelController>();
            var Clinic = DependencyResolver.Current.GetService<ClinicController>();
            var Settings = DependencyResolver.Current.GetService<SetupController>();

            ViewBag.Controller = this.ControllerContext.RouteData.Values["controller"].ToString().ToUpper();

            var mixmodel = new ModelMix
            {
                MessageList = Clinic.ClinicViewMessageEligibilities(ID),
                ChannelList = Channel.ChannelList(),
                ChannelListLogin = Channel.ChannelListLogin(),
                ChannelChildList = Channel.ChannelChildList(ViewBag.Controller)
            };

            return View(mixmodel);

        }

        [HttpPost]
        public ActionResult ClinicEditMessageInstruction(FormCollection formCollection)
        {
            var name = this.User.Identity.Name;
            ViewBag.name = name;


            if (name == "")
            {
                Response.Redirect("~/Account/Login");

            }
            var Channel = DependencyResolver.Current.GetService<ChannelController>();
            var Clinic = DependencyResolver.Current.GetService<ClinicController>();
            var Settings = DependencyResolver.Current.GetService<SetupController>();

            string inputTitle = formCollection["inputTitle"].ToString();
            string inputDescription = formCollection["inputDescription"].ToString();
            string selectActive = formCollection["selectActive"].ToString();
            string inputMessageId = formCollection["inputMessageId"].ToString();
            string inputClinicId = formCollection["inputClinicId"].ToString();
            string inputClinicType = formCollection["inputClinicType"].ToString();

            var mixmodel = new ModelMix
            {
                ClinicList = Clinic.ClinicListing(),
                ChannelList = Channel.ChannelList(),
                ChannelListLogin = Channel.ChannelListLogin(),
                ChannelChildList = Channel.ChannelChildList(ViewBag.Controller),

                Message = Clinic.ClinicUpdateMessageEligibility(inputTitle, inputDescription, selectActive, inputMessageId, inputClinicId, inputClinicType)
            };


            return RedirectToAction("ClinicViewInstruction", new { id = inputClinicId });
        }

        /////////////////////////////////
        /// Appointment Configuration ///
        /////////////////////////////////

        public ActionResult ClinicAppointment(string ID)
        {
            var name = this.User.Identity.Name;
            ViewBag.name = name;


            if (name == "")
            {
                Response.Redirect("~/Account/Login");

            }
            var Channel = DependencyResolver.Current.GetService<ChannelController>();
            var Clinic = DependencyResolver.Current.GetService<ClinicController>();
            var Settings = DependencyResolver.Current.GetService<SetupController>();

            ViewBag.Controller = this.ControllerContext.RouteData.Values["controller"].ToString().ToUpper();
            ViewBag.ID = ID;

            var mixmodel = new ModelMix
            {
                ClinicSchedules = Clinic.ClinicViewAppointment(ID),
                ChannelList = Channel.ChannelList(),
                ChannelListLogin = Channel.ChannelListLogin(),
                ChannelChildList = Channel.ChannelChildList(ViewBag.Controller)
            };

            return View(mixmodel);
        }

        public ActionResult AppointmentAdd(string ID)
        {
            var name = this.User.Identity.Name;
            ViewBag.name = name;


            if (name == "")
            {
                Response.Redirect("~/Account/Login");

            }
            var Channel = DependencyResolver.Current.GetService<ChannelController>();
            var Clinic = DependencyResolver.Current.GetService<ClinicController>();
            var Settings = DependencyResolver.Current.GetService<SetupController>();

            ViewBag.Controller = this.ControllerContext.RouteData.Values["controller"].ToString().ToUpper();
            ViewBag.ID = ID;

            var mixmodel = new ModelMix
            {
                ClinicSchedules = Clinic.ClinicViewAppointment(ID),
                ChannelList = Channel.ChannelList(),
                ChannelListLogin = Channel.ChannelListLogin(),
                ChannelChildList = Channel.ChannelChildList(ViewBag.Controller),

                ClinicTaxYears = Clinic.ClinicTaxYearSelect(ID),
                ClinicLanguages = Clinic.ClinicLanguageSelect(ID),
                ClinicMeetingTypes = Clinic.ClinicMeetingTypeSelect(ID),
                clinicFamilyTypes = Clinic.ClinicFamilyTypeSelect(ID)

            };

            return View(mixmodel);
        }

        [HttpPost]
        public ActionResult AppointmentAdd(FormCollection formCollection)
        {
            var name = this.User.Identity.Name;
            ViewBag.name = name;


            if (name == "")
            {
                Response.Redirect("~/Account/Login");

            }
            var Settings = DependencyResolver.Current.GetService<SetupController>();

            string inputCode = formCollection["inputCode"].ToString();
            string myDay = formCollection["myDay"].ToString();
            string inputDateStart = formCollection["inputDateStart"].ToString();
            string inputDateEnd = formCollection["inputDateEnd"].ToString();
            string inputTimeStart = formCollection["inputTimeStart"].ToString();
            string inputTimeEnd = formCollection["inputTimeEnd"].ToString();
            string inputCapacity = formCollection["inputCapacity"].ToString();
            string inputClinicId = formCollection["inputClinicId"].ToString();

            var clinicScheduleId = Settings.SetupAddClinicSchedule(inputCode, myDay, inputDateStart, inputDateEnd, inputTimeStart, inputTimeEnd, inputCapacity, inputClinicId);

            // Insert Setup
            if (formCollection.AllKeys.Contains("myTaxYear"))
            {
                string[] selectedTax = formCollection["myTaxYear"].Split(',');

                foreach (var tax in selectedTax)
                {
                    Settings.SetupClinicScheduleAddTaxYear(inputClinicId.ToString(), clinicScheduleId.ToString(), tax);
                }
            }

            if (formCollection.AllKeys.Contains("myLanguage"))
            {
                string[] selectedLanguage = formCollection["myLanguage"].Split(',');

                foreach (var lang in selectedLanguage)
                {
                    Settings.SetupClinicScheduleAddLanguage(inputClinicId.ToString(), clinicScheduleId.ToString(), lang);
                }
            }

            if (formCollection.AllKeys.Contains("myMeetingTypes"))
            {
                string[] selectedMeeting = formCollection["myMeetingTypes"].Split(',');

                foreach (var meet in selectedMeeting)
                {
                    Settings.SetupClinicScheduleAddMeetingType(inputClinicId.ToString(), clinicScheduleId.ToString(), meet);
                }
            }

            if (formCollection.AllKeys.Contains("myFamilyTypes"))
            {
                string[] selectedfamily = formCollection["myFamilyTypes"].Split(',');

                foreach (var fam in selectedfamily)
                {
                    Settings.SetupClinicScheduleAddFamilyType(inputClinicId.ToString(), clinicScheduleId.ToString(), fam);
                }
            }

            return RedirectToAction("ClinicAppointment", new { id = inputClinicId });
        }

        public ActionResult ClinicAppointmentView(string ID)
        {
            var name = this.User.Identity.Name;
            ViewBag.name = name;


            if (name == "")
            {
                Response.Redirect("~/Account/Login");

            }
            var Clinic = DependencyResolver.Current.GetService<ClinicController>();
            ViewBag.Id = ID;
            var mixmodel = new ModelMix
            {
                ClinicSchedules = Clinic.ClinicAppointmentViewInfo(ID),
                ClinicScheduleTaxYears = Clinic.ClinicAppointmentViewTaxYear(ID),
                ClinicScheduleLanguages = Clinic.ClinicAppointmentViewLanguage(ID),
                ClinicScheduleMeetingTypes = Clinic.ClinicAppointmentViewMeetingType(ID),
                clinicScheduleFamilyTypes = Clinic.ClinicAppointmentViewFamilyType(ID)
            };           
            return View(mixmodel);
        }

        // end //
    }
}