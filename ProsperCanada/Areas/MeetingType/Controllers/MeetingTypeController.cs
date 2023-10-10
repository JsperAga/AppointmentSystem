using ProsperCanada.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProsperCanada.Areas.MeetingType.Controllers
{
    public class MeetingTypeController : Controller
    {
        // GET: MeetingType/MeetingType
        public ActionResult Index()
        {
            return View();
        }

        public IList<Settings> MeetingTypeList(string settingType)
        {

            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var MeetingType = liDBq.Database.SqlQuery<Settings>("EXEC spSelSettings '"+ settingType + "'").ToList();

                return MeetingType;
            }
        }
    }
}