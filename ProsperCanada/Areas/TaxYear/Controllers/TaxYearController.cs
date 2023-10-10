using ProsperCanada.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProsperCanada.Areas.TaxYear.Controllers
{
    public class TaxYearController : Controller
    {
        // GET: TaxYear/TaxYear
        public ActionResult Index()
        {
            return View();
        }

        public IList<Settings> TaxYearList(string settingType)
        {

            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var TaxYear = liDBq.Database.SqlQuery<Settings>("EXEC spSelSettings '" + settingType + "'").ToList();

                return TaxYear;
            }
        }
    }
}