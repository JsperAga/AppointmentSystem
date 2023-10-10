using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace ProsperCanada.Models
{
    public class Clinics
    {
        public Guid clinicId { get; set; }
        public string clinicName { get; set; }
        public string clinicStreetName { get; set; }
        public string clinicUnit { get; set; }
        public string clinicCity { get; set; }
        public string clinicState { get; set; }
        public string clinicCountry { get; set; }
        public string clinicPostalCode { get; set; }
    }

    public class ClinicMessage
    {
        public Guid clinicMessageId { get; set; } 
        public string clinicId { get; set; }
        public string clinicDescription { get; set; }
        public string clinicType { get; set; }
        public int clinicMessageActive { get; set; }   
        public string clinicTypeWords { get; set; }
        public string clinicMessageTitle { get; set; }  

    }

    public class ClinicSchedule
    {
        public Guid clinicScheduleId { get; set; }
        public string clinicId { get; set; }
        public string clinicScheduleCode { get; set; }  
        public string clinicDay { get; set; }
        public string clinicDateStart { get; set; }
        public string clinicDateEnd { get; set; }
        public string clinicTimeStart { get; set; }
        public string clinicTimeEnd { get; set; }
        public int clinicCapacity { get; set; }
        public int clinicScheduleActive { get; set; }
        public string TaxYear { get; set; }
        public string Language { get; set; }
        public string MeetingType { get; set; }
        public string FamilyType { get; set; }
        public string clinicName { get; set; }
        public int counter { get; set; }

    }

    public class ClinicScheduleTaxYear
    {
        public Guid clinicScheduleTaxId { get; set; }
        public ClinicSchedule clinicScheduleId { get; set; }
        public Clinics clinicId { get; set; }
        public string clinicTaxYearId { get; set; }
    }

    public class ClinicScheduleLanguage
    {
        public Guid clinicScheduleTaxId { get; set; }
        public ClinicSchedule clinicScheduleId { get; set; }
        public Clinics clinicId { get; set; }
        public string clinicLanguageId { get; set; }
    }

    public class ClinicScheduleMeetingType
    {
        public Guid clinicScheduleTaxId { get; set; }
        public ClinicSchedule clinicScheduleId { get; set; }
        public Clinics clinicId { get; set; }
        public string clinicMeetingTypeId { get; set; }
    }

    public class ClinicScheduleFamilyType
    {
        public Guid clinicScheduleTaxId { get; set; }
        public ClinicSchedule clinicScheduleId { get; set; }
        public Clinics clinicId { get; set; }
        public string clinicFamilyTypeId { get; set; }
    }


    public class Channels
    {
        public string channelName { get; set;}
        public string channelChild { get; set; }
        public string channelController { get; set; }
        public string channelLocation { get; set; }

    }

    public class Settings
    {
        public Guid settingsId { get; set; }
        public string settingsName { get; set; }
        public string settingsDesc { get; set; }
        public string settingsType { get; set; }
        public string settingsDate { get; set; }
        public int settingsActive { get; set; }
        public int updatecount { get; set; }

    }

    public class ClinicTaxYear
    {
        public Guid clinicTaxYearId { get; set; }
        public string clinicId { get; set; }
        public string clinicTaxYear { get; set; }
        public int clinicTaxYearActive { get;set; }

    }

    public class ClinicLanguage
    {
        public Guid clinicLanguageId { get; set; }
        public string clinicId { get; set; }
        public string clinicLanguage { get; set; }
        public int clinicLanguageActive { get; set; }

    }

    public class ClinicMeetingType
    {
        public Guid clinicMeetingTypeId { get; set; }
        public string clinicId { get; set; }
        public string clinicMeetingType { get; set; }
        public int clinicMeetingTypeActive { get; set; }

    }

    public class ClinicFamilyType
    {
        public Guid clinicFamilyTypeId { get; set; }
        public string clinicId { get; set; }
        public string clinicFamilyType { get; set; }
        public int clinicFamilyTypeActive { get; set; }

    }

    public class Country
    {
        public string countryName { get; set; }
        public string countryCode { get; set; }
    }
    

    public class ModelMix
    {
        public IEnumerable<Clinics> ClinicList { get; set; }
        public IEnumerable<Channels> ChannelList { get; set; }
        public IEnumerable<Channels> ChannelListLogin { get; set; }
        public IEnumerable<Channels> ChannelChildList { get; set; }
        public IEnumerable<Settings> SettingsList { get; set; }
        public IEnumerable<Settings> TaxYear { get; set; }
        public IEnumerable<Settings> Language { get; set; }
        public IEnumerable<Settings> MeetingType { get; set; }
        public IEnumerable<Settings> FamilyType { get; set; }
        public IEnumerable<Country> CountryList { get; set; }

        public IEnumerable<ClinicMessage> MessageList { get; set; }

        public IEnumerable<ClinicTaxYear> ClinicTaxYears { get; set; }  
        public IEnumerable<ClinicLanguage> ClinicLanguages { get; set; }  
        public IEnumerable<ClinicMeetingType> ClinicMeetingTypes { get; set; }
        public IEnumerable<ClinicFamilyType> clinicFamilyTypes { get; set; }

        public IEnumerable<ClinicSchedule> ClinicSchedules { get; set; }


        public IEnumerable<ClinicScheduleTaxYear> ClinicScheduleTaxYears { get; set; }
        public IEnumerable<ClinicScheduleLanguage> ClinicScheduleLanguages { get; set; }
        public IEnumerable<ClinicScheduleMeetingType> ClinicScheduleMeetingTypes { get; set; }  
        public IEnumerable<ClinicScheduleFamilyType> clinicScheduleFamilyTypes { get; set; }  
        public IEnumerable<Appointment> Appointments { get; set; }
        public IEnumerable<ClinicAdmin> ClinicAdmin { get; set; }
        public IEnumerable<User> UserList { get; set; }
        public string Message { get; set; }
    }

    public class Appointment
    {
        public Guid appointmentId { get; set; }
        public string clinicScheduleId { get; set; }
        public Guid clinicId { get; set; }
        public string clinicTaxYearId { get; set; }
        public string clinicLanguageId { get; set; }
        public string clinicFamilyTypeId { get; set; }
        public string clinicMeetingTypeId { get; set; }
        public string appointmentContactName { get; set; }
        public string appointmentContactNumber { get; set; }
        public string appointmentContactEmail { get; set; }
        public string appointmentContactDate { get; set; }
        public string appointmentContactTimeStart { get; set; }
        public string appointmentContactTimeEnd { get; set; }
        public int appointmentStatus { get; set; }

        public string clinicName { get; set; }
        public string userFullName { get; set; }
    }

    public class ClinicAdmin
    {
        public Guid appointmentId { get; set; }
        public string clinicScheduleId { get; set; }
        public string clinicId { get; set; }
        public string clinicTaxYearId { get; set; }
        public string clinicLanguageId { get; set; }
        public string clinicFamilyTypeId { get; set; }
        public string clinicMeetingTypeId { get; set; }
        public string appointmentContactName { get; set; }
        public string appointmentContactNumber { get; set; }
        public string appointmentContactEmail { get; set; }
        public string appointmentContactDate { get; set; }
        public string appointmentContactTimeStart { get; set; }
        public string appointmentContactTimeEnd { get; set; }
        public int appointmentStatus { get; set; }

        public string clinicName { get; set; }
        public string userFullName { get; set; }

    }

    public class User
    {
        public Guid userId { get; set; }
        public string userFullName { get; set; }
        public string userName { get; set; }
        public string userPassword { get; set; }
        public string userEmailAddress { get; set; }
        public int userActive { get; set; }
        public string userContactNumber { get; set; }
    }
}