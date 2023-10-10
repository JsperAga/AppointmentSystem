using ProsperCanada.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ProsperCanada.Areas.Setup.Controllers
{
    public class SetupController : Controller
    {
        // GET: Setup/Setup
        public ActionResult Index()
        {
            return View();
        }
        public IList<Settings> SetupList(string settingType)
        {

            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var SetupType = liDBq.Database.SqlQuery<Settings>("EXEC spSelSettings '" + settingType + "'").ToList();

                return SetupType;
            }
        }

        public string SetupAdd(string inputName, string inputDescription, string selectActive, string setupType)
        {

            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var parameters = new[]
                {
                    new SqlParameter("@settingName ", inputName),
                    new SqlParameter("@settingDesc", inputDescription),
                    new SqlParameter("@settingType",setupType),
                    new SqlParameter("@selectActive", selectActive)
                    
                };
                // Call the stored procedure to insert data
                var result = liDBq.Database.ExecuteSqlCommand("EXEC spInsSettings @settingName , @settingDesc, @settingType, @selectActive", parameters);

                if (result >= 0)
                {
                    // Data inserted successfully, you can redirect or return a success message
                    return "Success";
                }
                else
                {
                    // Handle the error scenario
                    return "Error";
                }
            }

        }

        // select setup for EDIT
        public IList<Settings> SetupView(string ID, string settingType)
        {
            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var SetupView = liDBq.Database.SqlQuery<Settings>("EXEC spSelSettingView '" + ID + "', '"+settingType+"'").ToList();

                return SetupView;
            }
        }

        public string SetupUpdate(string settingId, string inputName, string inputDescription, string selectActive, string setupType)
        {

            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var parameters = new[]
                {
                    new SqlParameter("@settingId", settingId),
                    new SqlParameter("@settingName", inputName),
                    new SqlParameter("@settingDesc", inputDescription),
                    new SqlParameter("@settingType",setupType),
                    new SqlParameter("@selectActive", selectActive)

                };
                // Call the stored procedure to insert data
                //var result = liDBq.Database.ExecuteSqlCommand("EXEC spUpdSettings @settingId, @settingName , @settingDesc, @settingType, @selectActive", parameters).ToList();
                var result = liDBq.Database.SqlQuery<Settings>("EXEC spUpdSettings '" + settingId + "','"+ inputName + "','"+ inputDescription + "','"+ setupType + "','"+ selectActive + "'").ToList();


                foreach (Settings d in result)
                {
                    ViewBag.Active = d.updatecount.ToString();
                }

               
                return ViewBag.Active;


                //if (result >= 0)
                //{
                //    // Data inserted successfully, you can redirect or return a success message
                //    return "Success";
                //}
                //else
                //{
                //    // Handle the error scenario
                //    return "Error";
                //}
            }

        }

        [HttpGet]
        public int SetupUpdateActive(string settingId)
        {

            //var result = "This is the response from the controller action."; // Replace with your data

            //return Content(result);
            string res;
            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
             
                var parameters = new[]
                {
                    new SqlParameter("@settingId", settingId),


                };
                // Call the stored procedure to insert data
                //var result = liDBq.Database.ExecuteSqlCommand("EXEC spUpdSettingActive @settingId", parameters);
                var SetupView = liDBq.Database.SqlQuery<Settings>("EXEC spUpdSettingActive '" + settingId + "'").ToList();
                
                foreach (Settings d in SetupView)
                {
                    ViewBag.Active = d.settingsActive;
                }
               
            }
            return ViewBag.Active;
        }

        [HttpGet]
        public int SetupActiveDelete(string settingId)
        {

            //var result = "This is the response from the controller action."; // Replace with your data

            //return Content(result);
            string res;
            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {

                var parameters = new[]
                {
                    new SqlParameter("@settingId", settingId)
                };
                // Call the stored procedure to insert data
                //var result = liDBq.Database.ExecuteSqlCommand("EXEC spUpdSettingActive @settingId", parameters);
                var SetupView = liDBq.Database.SqlQuery<Settings>("EXEC spUpdSettingsDelete '" + settingId + "'").ToList();

                foreach (Settings d in SetupView)
                {
                    ViewBag.Active = d.settingsActive;
                }

            }
            return ViewBag.Active;
        }

        //// ADD CLINIC ////
        public Guid SetupAddClinic(string clinicName, string clinicStreetName, string clinicUnit, string clinicCity, string clinicState, string clinicCountry, string clinicPostalCode)
        {

            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var parameters = new[]
                {
                    new SqlParameter("@clinicName ", clinicName),
                    new SqlParameter("@clinicStreetName", clinicStreetName),
                    new SqlParameter("@clinicUnit",clinicUnit),
                    new SqlParameter("@clinicCity", clinicCity),
                    new SqlParameter("@clinicState", clinicState),
                    new SqlParameter("@clinicCountry", clinicCountry),
                    new SqlParameter("@clinicPostalCode", clinicPostalCode)

                };
                // Call the stored procedure to insert data
                var SetupView = liDBq.Database.SqlQuery<Clinics>("EXEC spInsClinicInfo '" + clinicName + "','" + clinicStreetName + "','" + clinicUnit + "','" + clinicCity + "','" + clinicState + "','" + clinicCountry + "','" + clinicPostalCode + "'").ToList();

                foreach (Clinics d in SetupView)
                {
                    ViewBag.clinicId = d.clinicId;
                }
            }
            return ViewBag.clinicId;

        }

        // Update Clinic
        public string SetupUpdateClinic(string clinicName, string clinicStreetName, string clinicUnit, string clinicCity, string clinicState, string clinicCountry, string clinicPostalCode, string clinicId)
        {
            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var parameters = new[]
                {
                    new SqlParameter("@clinicName ", clinicName),
                    new SqlParameter("@clinicStreetName", clinicStreetName),
                    new SqlParameter("@clinicUnit",clinicUnit),
                    new SqlParameter("@clinicCity", clinicCity),
                    new SqlParameter("@clinicState", clinicState),
                    new SqlParameter("@clinicCountry", clinicCountry),
                    new SqlParameter("@clinicPostalCode", clinicPostalCode)

                };
                // Call the stored procedure to insert data
                var SetupUdateClinic = liDBq.Database.SqlQuery<Clinics>("EXEC spUpdClinicInfo '" + clinicName + "','" + clinicStreetName + "','" + clinicUnit + "','" + clinicCity + "','" + clinicState + "','" + clinicCountry + "','" + clinicPostalCode + "','"+clinicId+"'").ToList();

                foreach (Clinics d in SetupUdateClinic)
                {
                    ViewBag.clinicId = d.clinicId;
                }
            }
            return ViewBag.clinicId.ToString();
        }


        // ADD TAX YEAR //

        public string SetupAddTaxYear(string clinicId, string taxYear)
        {
            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var parameters = new[]
                {
                    new SqlParameter("@clinicId ", clinicId),
                    new SqlParameter("@taxYear", taxYear)
                };
                // Call the stored procedure to insert data
                var result = liDBq.Database.ExecuteSqlCommand("EXEC spInsClinicTaxYear @clinicId , @taxYear", parameters);

                if (result >= 0)
                {
                    // Data inserted successfully, you can redirect or return a success message
                    return "Success";
                }
                else
                {
                    // Handle the error scenario
                    return "Error";
                }
            }
        }

        // Delete Tax Year //
        public string SetupDeleteTaxYear(string clinicId)
        {
            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var parameters = new[]
                {
                    new SqlParameter("@clinicId ", clinicId)                    
                };
                // Call the stored procedure to insert data
                var result = liDBq.Database.ExecuteSqlCommand("EXEC spDelClinicTaxYear @clinicId", parameters);

                if (result >= 0)
                {
                    // Data inserted successfully, you can redirect or return a success message
                    return "Success";
                }
                else
                {
                    // Handle the error scenario
                    return "Error";
                }
            }
        }


        // ADD LANGUAGE //
        public string SetupAddLanguage(string clinicId, string language)
        {
            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var parameters = new[]
                {
                    new SqlParameter("@clinicId ", clinicId),
                    new SqlParameter("@language", language)
                };
                // Call the stored procedure to insert data
                var result = liDBq.Database.ExecuteSqlCommand("EXEC spInsClinicLanguage @clinicId , @language", parameters);

                if (result >= 0)
                {
                    // Data inserted successfully, you can redirect or return a success message
                    return "Success";
                }
                else
                {
                    // Handle the error scenario
                    return "Error";
                }
            }
        }

        // Delete Language //
        public string SetupDeleteLanguage(string clinicId)
        {
            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var parameters = new[]
                {
                    new SqlParameter("@clinicId ", clinicId)
                };
                // Call the stored procedure to insert data
                var result = liDBq.Database.ExecuteSqlCommand("EXEC spDelClinicLanguage @clinicId", parameters);

                if (result >= 0)
                {
                    // Data inserted successfully, you can redirect or return a success message
                    return "Success";
                }
                else
                {
                    // Handle the error scenario
                    return "Error";
                }
            }
        }

        // ADD Meeting Type //
        public string SetupAddMeetingType(string clinicId, string meetingtype)
        {
            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var parameters = new[]
                {
                    new SqlParameter("@clinicId ", clinicId),
                    new SqlParameter("@meetingtype", meetingtype)
                };
                // Call the stored procedure to insert data
                var result = liDBq.Database.ExecuteSqlCommand("EXEC spInsClinicMeetingType @clinicId , @meetingtype", parameters);

                if (result >= 0)
                {
                    // Data inserted successfully, you can redirect or return a success message
                    return "Success";
                }
                else
                {
                    // Handle the error scenario
                    return "Error";
                }
            }
        }

        // Delete Meeting Type //
        public string SetupDeleteMeetingType(string clinicId)
        {
            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var parameters = new[]
                {
                    new SqlParameter("@clinicId ", clinicId)
                };
                // Call the stored procedure to insert data
                var result = liDBq.Database.ExecuteSqlCommand("EXEC spDelClinicMeetingType @clinicId", parameters);

                if (result >= 0)
                {
                    // Data inserted successfully, you can redirect or return a success message
                    return "Success";
                }
                else
                {
                    // Handle the error scenario
                    return "Error";
                }
            }
        }

        // ADD Family Type //
        public string SetupAddFamilyType(string clinicId, string familytype)
        {
            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var parameters = new[]
                {
                    new SqlParameter("@clinicId ", clinicId),
                    new SqlParameter("@familytype", familytype)
                };
                // Call the stored procedure to insert data
                var result = liDBq.Database.ExecuteSqlCommand("EXEC spInsClinicFamilyType @clinicId , @familytype", parameters);

                if (result >= 0)
                {
                    // Data inserted successfully, you can redirect or return a success message
                    return "Success";
                }
                else
                {
                    // Handle the error scenario
                    return "Error";
                }
            }
        }

        // Delete Family Type //
        public string SetupDeleteFamilyType(string clinicId)
        {
            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var parameters = new[]
                {
                    new SqlParameter("@clinicId ", clinicId)
                };
                // Call the stored procedure to insert data
                var result = liDBq.Database.ExecuteSqlCommand("EXEC spDelClinicFamilyType @clinicId", parameters);

                if (result >= 0)
                {
                    // Data inserted successfully, you can redirect or return a success message
                    return "Success";
                }
                else
                {
                    // Handle the error scenario
                    return "Error";
                }
            }
        }

        // Select Countries //
        public IList<Country> SetupCountries()
        {
            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var SetupCountry = liDBq.Database.SqlQuery<Country>("EXEC spSelClinicCountries").ToList();

                return SetupCountry;
            }
        }


        // Message Activate
        [HttpGet]
        public int SetupMessageUpdateActive(string clinicMessageId)
        {

            //var result = "This is the response from the controller action."; // Replace with your data

            //return Content(result);
            string res;
            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {

               
                // Call the stored procedure to insert data
                //var result = liDBq.Database.ExecuteSqlCommand("EXEC spUpdSettingActive @settingId", parameters);
                var SetupView = liDBq.Database.SqlQuery<ClinicMessage>("EXEC spUpdClinicMessageActive '" + clinicMessageId + "'").ToList();

                foreach (ClinicMessage d in SetupView)
                {
                    ViewBag.Active = d.clinicMessageActive;
                }

            }
            return ViewBag.Active;
        }

        [HttpPost]
        public string SetupMessageAdd(string inputTitle, string inputDescription, string selectActive, string inputClinicId, string clinicType)
        {

            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var parameters = new[]
                {
                    new SqlParameter("@inputTitle", inputTitle),
                    new SqlParameter("@inputDescription", inputDescription),
                    new SqlParameter("@selectActive",selectActive),
                    new SqlParameter("@inputClinicIde", inputClinicId),
                    new SqlParameter("@clinicType", clinicType)

                };
                // Call the stored procedure to insert data
                var result = liDBq.Database.ExecuteSqlCommand("EXEC spInsClinicMessage @inputTitle , @inputDescription, @selectActive, @inputClinicIde, @clinicType", parameters);

                if (result >= 0)
                {
                    // Data inserted successfully, you can redirect or return a success message
                    return "Success";
                }
                else
                {
                    // Handle the error scenario
                    return "Error";
                }
            }

        }

        // add clinic schedule

        public Guid SetupAddClinicSchedule(string inputCode, string myDay, string inputDateStart, string inputDateEnd, string inputTimeStart, string inputTimeEnd, string inputCapacity, string inputClinicId)
        {

            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var parameters = new[]
                {
                    new SqlParameter("@inputCode ", inputCode),
                    new SqlParameter("@myDay", myDay),
                    new SqlParameter("@inputDateStart",inputDateStart),
                    new SqlParameter("@inputDateEnd", inputDateEnd),
                    new SqlParameter("@inputTimeStart", inputTimeStart),
                    new SqlParameter("@inputTimeEnd", inputTimeEnd),
                    new SqlParameter("@inputCapacity", inputCapacity),
                    new SqlParameter("@inputClinicId", inputClinicId)
                    

                };
                // Call the stored procedure to insert data
                var SetupView = liDBq.Database.SqlQuery<ClinicSchedule>("EXEC spInsClinicSchedule '" + inputCode + "','" + myDay + "','" + inputDateStart + "','" + inputDateEnd + "','" + inputTimeStart + "','" + inputTimeEnd + "','" + inputCapacity + "','"+ inputClinicId + "'").ToList();

                foreach (ClinicSchedule d in SetupView)
                {
                    ViewBag.clinicScheduleId = d.clinicScheduleId;
                }
            }
            return ViewBag.clinicScheduleId;

        }

        // ADD Clinic Schedule TAX YEAR //

        public string SetupClinicScheduleAddTaxYear(string clinicId, string clinicScheduleId, string clinicTaxYear)
        {
            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var parameters = new[]
                {
                    new SqlParameter("@clinicId ", clinicId),
                    new SqlParameter("@clinicScheduleId", clinicScheduleId),
                    new SqlParameter("@clinicTaxYear", clinicTaxYear)
                };
                // Call the stored procedure to insert data
                var result = liDBq.Database.ExecuteSqlCommand("EXEC spInsClinicScheduleTaxYear @clinicId, @clinicScheduleId, @clinicTaxYear", parameters);

                if (result >= 0)
                {
                    // Data inserted successfully, you can redirect or return a success message
                    return "Success";
                }
                else
                {
                    // Handle the error scenario
                    return "Error";
                }
            }
        }
        
        // ADD Clinic Schedule Language //
        public string SetupClinicScheduleAddLanguage(string clinicId, string clinicScheduleId, string clinicLanguage)
        {
            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var parameters = new[]
                {
                    new SqlParameter("@clinicId ", clinicId),
                    new SqlParameter("@clinicScheduleId", clinicScheduleId),
                    new SqlParameter("@clinicLanguage", clinicLanguage)
                };
                // Call the stored procedure to insert data
                var result = liDBq.Database.ExecuteSqlCommand("EXEC spInsClinicScheduleLanguage @clinicId, @clinicScheduleId, @clinicLanguage", parameters);

                if (result >= 0)
                {
                    // Data inserted successfully, you can redirect or return a success message
                    return "Success";
                }
                else
                {
                    // Handle the error scenario
                    return "Error";
                }
            }
        }

        // ADD Clinic Schedule Meeting Type //
        public string SetupClinicScheduleAddMeetingType(string clinicId, string clinicScheduleId, string clinicMeetingType)
        {
            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var parameters = new[]
                {
                    new SqlParameter("@clinicId ", clinicId),
                    new SqlParameter("@clinicScheduleId", clinicScheduleId),
                    new SqlParameter("@clinicMeetingType", clinicMeetingType)
                };
                // Call the stored procedure to insert data
                var result = liDBq.Database.ExecuteSqlCommand("EXEC spInsClinicScheduleMeetingType @clinicId, @clinicScheduleId, @clinicMeetingType", parameters);

                if (result >= 0)
                {
                    // Data inserted successfully, you can redirect or return a success message
                    return "Success";
                }
                else
                {
                    // Handle the error scenario
                    return "Error";
                }
            }
        }

        // ADD Clinic Schedule Family Type //
        public string SetupClinicScheduleAddFamilyType(string clinicId, string clinicScheduleId, string clinicFamilyType)
        {
            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var parameters = new[]
                {
                    new SqlParameter("@clinicId ", clinicId),
                    new SqlParameter("@clinicScheduleId", clinicScheduleId),
                    new SqlParameter("@clinicFamilyType", clinicFamilyType)
                };
                // Call the stored procedure to insert data
                var result = liDBq.Database.ExecuteSqlCommand("EXEC spInsClinicScheduleFamilyType @clinicId, @clinicScheduleId, @clinicFamilyType", parameters);

                if (result >= 0)
                {
                    // Data inserted successfully, you can redirect or return a success message
                    return "Success";
                }
                else
                {
                    // Handle the error scenario
                    return "Error";
                }
            }
        }
    }
}