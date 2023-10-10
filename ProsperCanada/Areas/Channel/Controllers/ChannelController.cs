using ProsperCanada.Models;
using ProsperCanada.Controllers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ProsperCanada.Areas.Channel.Controllers
{
    public class ChannelController : Controller
    {
        // GET: Channel/Channel
        public ActionResult Index()
        {
            return View();
        }

        public IList<Channels> ChannelList()
        {
            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var channelParent = liDBq.Database.SqlQuery<Channels>("EXEC spSelChannel '1'").ToList();

                return channelParent;
            }
        }

        public IList<Channels> ChannelChildList(string channelName)
        {
            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var channelChild = liDBq.Database.SqlQuery<Channels>("EXEC spSelChannelChild '"+ channelName +"'").ToList();

                return channelChild;
            }
        }

        public IList<Channels> ChannelListLogin()
        {
            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var channelParent = liDBq.Database.SqlQuery<Channels>("EXEC spSelChannel '9'").ToList();

                return channelParent;
            }
        }

    }
}
