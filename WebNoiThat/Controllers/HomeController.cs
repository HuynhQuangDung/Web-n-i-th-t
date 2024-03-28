using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNoiThat.Models;

namespace WebNoiThat.Controllers
{
    public class HomeController : Controller
    {
        MyDataDataContext data = new MyDataDataContext();
        public ActionResult Index(int? page)
        {
            if (page == null) page = 1;
            var all_noithat = (from s in data.NoiThats select s).OrderBy(m => m.manoithat);
            int pageSize = 6;
            int pageNum = page ?? 1;
            return View(all_noithat.ToPagedList(pageNum, pageSize));
        }
        public ActionResult Detail(int id)
        {
            var D_noithat = data.NoiThats.Where(m => m.manoithat == id).First();
            return View(D_noithat);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}