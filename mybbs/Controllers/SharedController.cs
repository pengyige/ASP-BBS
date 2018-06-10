using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mybbs.Controllers
{
    public class SharedController : Controller
    {
        //
        // GET: /Shared/

        public ActionResult Success()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }


        public ActionResult UserNOTLogin() {
            return View();
        }
    }
}
