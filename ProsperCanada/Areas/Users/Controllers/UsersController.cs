using ProsperCanada.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Sockets;
using System.Web;
using System.Web.Mvc;

namespace ProsperCanada.Areas.Users.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users/Users
        public ActionResult Index()
        {
            return View();
        }

        public IList<User> UserLogin(string username, string password)
        {
            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var UserType = liDBq.Database.SqlQuery<User>("EXEC spSelUsersLogin '" + username + "','"+password+"'").ToList();

                return UserType;
            }
        }

        public IList<Appointment> SelectManageAppointment(string username)
        {
            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var ManageAppointment = liDBq.Database.SqlQuery<Appointment>("EXEC spSelManageAppointment '" + username + "'").ToList();

                return ManageAppointment;
            }
        }

        public IList<User> SelectUser()
        {
            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var UserList = liDBq.Database.SqlQuery<User>("EXEC spSelUsersList").ToList();

                return UserList;
            }
        }

        public IList<User> SelectUserAgent(string username)
        {
            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var UserAgent = liDBq.Database.SqlQuery<User>("EXEC spSelClinicAgent '" + username + "'").ToList();

                return UserAgent;
            }
        }

        public string UserRoleAdd(string clinicId, string selectUser, string selectRole)
        {

            using (ProsperCanadaDBEntities1 liDBq = new ProsperCanadaDBEntities1())
            {
                var parameters = new[]
                {
                    new SqlParameter("@clinicId ", clinicId),
                    new SqlParameter("@selectUser", selectUser),
                    new SqlParameter("@selectRole",selectRole)

                };
                // Call the stored procedure to insert data
                var result = liDBq.Database.ExecuteSqlCommand("EXEC spInsUsersRole @clinicId , @selectUser, @selectRole", parameters);

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