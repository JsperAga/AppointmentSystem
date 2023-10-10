using ProsperCanada.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProsperCanada.Areas.Language.Controllers
{
    public class LanguageController : Controller
    {
        // GET: Language/Language
        public ActionResult Index()
        {
            return View();
        }

        public IList<Settings> LanguageList(string settingType)
        {

            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var Language = liDBq.Database.SqlQuery<Settings>("EXEC spSelSettings '" + settingType + "'").ToList();

                return Language;
            }
        }
    }
}