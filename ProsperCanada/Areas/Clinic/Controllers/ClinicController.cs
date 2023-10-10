using ProsperCanada.Models;
using ProsperCanada.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;

namespace ProsperCanada.Areas.Clinic.Controllers
{
    public class ClinicController : Controller
    {
        // GET: Clinic/Clinic
        public ActionResult Index()
        {
            return View();
        }

        public IList<Clinics> ClinicListing()
        {

            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var clinics = liDBq.Database.SqlQuery<Clinics>("EXEC spSelClinic").ToList();

                return clinics;
            }
        }

        public IList<Clinics> ClinicView(string ID)
        {
            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var clinicView = liDBq.Database.SqlQuery<Clinics>("EXEC spSelClinicView '" + ID + "'").ToList();

                return clinicView;
            }
        }

        public IList<ClinicTaxYear> ClinicTaxYearSelect(string ID)
        {
            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var clinicTaxView = liDBq.Database.SqlQuery<ClinicTaxYear>("EXEC spSelClinicTaxYear '" + ID + "'").ToList();

                return clinicTaxView;
            }
        }

        public IList<ClinicLanguage> ClinicLanguageSelect(string ID)
        {
            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var clinicLangView = liDBq.Database.SqlQuery<ClinicLanguage>("EXEC spSelClinicLanguage '" + ID + "'").ToList();

                return clinicLangView;
            }
        }

        public IList<ClinicMeetingType> ClinicMeetingTypeSelect(string ID)
        {
            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var clinicMeetView = liDBq.Database.SqlQuery<ClinicMeetingType>("EXEC spSelClinicMeetingType '" + ID + "'").ToList();

                return clinicMeetView;
            }
        }

        public IList<ClinicFamilyType> ClinicFamilyTypeSelect(string ID)
        {
            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var clinicFamView = liDBq.Database.SqlQuery<ClinicFamilyType>("EXEC spSelClinicFamilyType '" + ID + "'").ToList();

                return clinicFamView;
            }
        }

        public IList<ClinicMessage> ClinicViewEligibilities(string ID)
        {
            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var clinicViewEligibility = liDBq.Database.SqlQuery<ClinicMessage>("EXEC spSelClinicMessage '" + ID + "','1'").ToList();

                return clinicViewEligibility;
            }
        }

        public IList<ClinicMessage> ClinicViewMessageEligibilities(string ID)
        {
            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var clinicViewMessage = liDBq.Database.SqlQuery<ClinicMessage>("EXEC spSelClinicMessageView '" + ID + "'").ToList();

                return clinicViewMessage;
            }

        }

        public string ClinicUpdateMessageEligibility(string inputTitle, string inputDescription, string selectActive, string inputMessageId, string inputClinicId, string inputClinicType)
        {
            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var parameters = new[]
                {
                    new SqlParameter("@inputTitle", inputTitle),
                    new SqlParameter("@inputDescription", inputDescription),
                    new SqlParameter("@selectActive", selectActive),
                    new SqlParameter("@inputMessageId", inputMessageId),
                    new SqlParameter("@inputClinicId", inputClinicId),
                    new SqlParameter("@inputClinicType", inputClinicType),
                };
                // Call the stored procedure to insert data
                var result = liDBq.Database.ExecuteSqlCommand("EXEC spUpdClinicMessage @inputTitle, @inputDescription, @selectActive, @inputMessageId, @inputClinicId, @inputClinicType", parameters);

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


        public IList<ClinicMessage> ClinicViewInstructions(string ID)
        {
            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var clinicViewEligibility = liDBq.Database.SqlQuery<ClinicMessage>("EXEC spSelClinicMessage '" + ID + "','2'").ToList();

                return clinicViewEligibility;
            }
        }

        public IList<ClinicSchedule> ClinicViewAppointment(string ID)
        {
            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var clinicViewSchedule = liDBq.Database.SqlQuery<ClinicSchedule>("EXEC spSelClinicSchedule '" + ID + "'").ToList();

                return clinicViewSchedule;
            }
        }



        // VIEW APPOINTMENT //
        public IList<ClinicSchedule> ClinicAppointmentViewInfo(string ID)
        {
            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var clinicViewSchedule = liDBq.Database.SqlQuery<ClinicSchedule>("EXEC spSelClinicScheduleView '" + ID + "'").ToList();

                return clinicViewSchedule;
            }
        }

        public IList<ClinicScheduleTaxYear> ClinicAppointmentViewTaxYear(string ID)
        {
            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var clinicViewTaxYear = liDBq.Database.SqlQuery<ClinicScheduleTaxYear>("EXEC spSelClinicScheduleTaxYearView '" + ID + "'").ToList();

                return clinicViewTaxYear;
            }
        }

        public IList<ClinicScheduleLanguage> ClinicAppointmentViewLanguage(string ID)
        {
            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var clinicViewLanguage = liDBq.Database.SqlQuery<ClinicScheduleLanguage>("EXEC spSelClinicScheduleLanguageView '" + ID + "'").ToList();

                return clinicViewLanguage;
            }
        }

        public IList<ClinicScheduleMeetingType> ClinicAppointmentViewMeetingType(string ID)
        {
            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var clinicViewMeetingType = liDBq.Database.SqlQuery<ClinicScheduleMeetingType>("EXEC spSelClinicScheduleMeetingTypeView '" + ID + "'").ToList();

                return clinicViewMeetingType;
            }
        }

        public IList<ClinicScheduleFamilyType> ClinicAppointmentViewFamilyType(string ID)
        {
            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var clinicViewFamilyType = liDBq.Database.SqlQuery<ClinicScheduleFamilyType>("EXEC spSelClinicScheduleFamilyTypeView '" + ID + "'").ToList();

                return clinicViewFamilyType;
            }
        }

        public IList<ClinicSchedule> ClinicAppointmentSearch(string date, string ID)
        {
            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var ViewSearch = liDBq.Database.SqlQuery<ClinicSchedule>("EXEC spSelClinicAppointmentSearch '" + date + "','"+ID+"'").ToList();

                return ViewSearch;
            }
        }

        // count capacity
        public string ClinicAppointmentCapacity(string ID, string date)
        {
            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var Capacity = liDBq.Database.SqlQuery<ClinicSchedule>("EXEC spSelAppointmentCapacity '" + ID + "','" + date + "'").ToList();

                foreach (ClinicSchedule d in Capacity)
                {
                    ViewBag.clinicCapacity = d.clinicCapacity;
                }
            }
            return ViewBag.clinicCapacity.ToString();
        }

        // insert appointment
        public string ClinicAppointmentInsert(string clinicScheduleId
                                            , string clinicId, string clinicTaxYearId, string clinicLanguageId
                                            , string clinicFamilyTypeId, string clinicMeetingTypeId, string appointmentContactName
                                            , string appointmentContactNumber, string appointmentContactEmail, string appointmentContactDate
                                            , string appointmentContactTimeStart, string appointmentContactTimeEnd)
        {
            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var parameters = new[]
                {
                    new SqlParameter("@clinicScheduleId", clinicScheduleId),
                    new SqlParameter("@clinicId", clinicId),
                    new SqlParameter("@clinicTaxYearId", clinicTaxYearId),
                    new SqlParameter("@clinicLanguageId", clinicLanguageId),
                    new SqlParameter("@clinicFamilyTypeId", clinicFamilyTypeId),
                    new SqlParameter("@clinicMeetingTypeId", clinicMeetingTypeId),
                    new SqlParameter("@appointmentContactName", appointmentContactName),
                    new SqlParameter("@appointmentContactNumber", appointmentContactNumber),
                    new SqlParameter("@appointmentContactEmail", appointmentContactEmail),
                    new SqlParameter("@appointmentContactDate", appointmentContactDate),
                    new SqlParameter("@appointmentContactTimeStart", appointmentContactTimeStart),
                    new SqlParameter("@appointmentContactTimeEnd", appointmentContactTimeEnd)
                    
                };
                
                var result = liDBq.Database.ExecuteSqlCommand("EXEC spInsAppointment @clinicScheduleId, @clinicId, @clinicTaxYearId, @clinicLanguageId, @clinicFamilyTypeId, @clinicMeetingTypeId, @appointmentContactName, @appointmentContactNumber, @appointmentContactEmail, @appointmentContactDate, @appointmentContactTimeStart, @appointmentContactTimeEnd", parameters);

                // var Capacity = liDBq.Database.SqlQuery<ClinicSchedule>("EXEC spInsAppointment '" + ID + "','" + date + "'").ToList();

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

        public IList<ClinicAdmin> ClinicAdminAppointment(string username)
        {
            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var ViewSearch = liDBq.Database.SqlQuery<ClinicAdmin>("EXEC spSelClinicAdminAppointment '" + username + "'").ToList();

                return ViewSearch;
            }
            
        }

        public IList<ClinicAdmin> ClinicAdminAppointmentView(string ID)
        {
            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var ViewSearch = liDBq.Database.SqlQuery<ClinicAdmin>("EXEC spSelClinicAdminAppointmentView '" + ID + "'").ToList();

                return ViewSearch;
            }

        }

        public IList<ClinicAdmin> ClinicAppointmentAgentAdd(string ID, string appointmentId)
        {
            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var ViewSearch = liDBq.Database.SqlQuery<ClinicAdmin>("EXEC spInsAppointmentAgent '" + ID + "','" + appointmentId + "'").ToList();

                return ViewSearch;
            }

        }

        public string AppointmentCancelUpdate(string ID, string comments)
        {
            
            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var parameters = new[]
                {
                    new SqlParameter("@appointmentId", ID),
                    new SqlParameter("@comments", comments)
                };

                var result = liDBq.Database.ExecuteSqlCommand("EXEC spUptAppointmentCancel @appointmentId, @comments", parameters);

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

        public IList<ClinicAdmin> SelectAgentAppointment(string username)
        {
            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var ViewSearch = liDBq.Database.SqlQuery<ClinicAdmin>("EXEC spSelAgentAppointment '" + username + "'").ToList();

                return ViewSearch;
            }
        }


    }
}