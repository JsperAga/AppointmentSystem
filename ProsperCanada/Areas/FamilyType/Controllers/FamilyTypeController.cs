using ProsperCanada.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProsperCanada.Areas.FamilyType.Controllers
{
    public class FamilyTypeController : Controller
    {
        // GET: FamilyType/FamilyType
        public ActionResult Index()
        {
            return View();
        }

        public IList<Settings> FamilyTypeList(string settingType)
        {

            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var FamilyType = liDBq.Database.SqlQuery<Settings>("EXEC spSelSettings '" + settingType + "'").ToList();

                return FamilyType;
            }
        }
    }
}