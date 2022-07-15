using ShopCoinUSAv2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopCoinUSAv2.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        DataClasses1DataContext data = new DataClasses1DataContext();
        // GET: Admin/HomeAdmin
        public ActionResult Home()
        {
            int demtk = data.TaiKhoans.ToList().Count;
            ViewData["TaiKhoan"] = demtk;
            return View();
        }
    }
}