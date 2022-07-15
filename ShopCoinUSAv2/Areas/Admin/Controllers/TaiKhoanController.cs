using ShopCoinUSAv2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopCoinUSAv2.Areas.Admin.Controllers
{
    public class TaiKhoanController : Controller
    {
        DataClasses1DataContext data = new DataClasses1DataContext();
        // GET: Admin/TaiKhoan
        public ActionResult DSTaiKhoan()
        {
            var tk = data.TaiKhoans.ToList();
            return View(tk);
        }
        public ActionResult SuaTK(String tendn)
        {
            var tk = data.TaiKhoans.First(n => n.TenDN == tendn);
             if(tk == null)
                {
                Response.SubStatusCode = 404;
                return null;
            }
            return View(tk);
        }
        [HttpPost]
        public ActionResult SuaTK(String tendn,FormCollection collection)
        {
            var tk = data.TaiKhoans.First(n => n.TenDN == tendn);
            var matkhau = collection["MatKhau"];
            var user = collection["UserName"];
            if(String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi1"] = "khong dc de rong";

            }
            
            else
            {
                tk.UserName = user;
                tk.MatKhau = matkhau;
                UpdateModel(tk);
                data.SubmitChanges();
                return RedirectToAction("DSTaiKhoan");

            }
            return this.SuaTK(tendn);
        }
        public ActionResult XoaTK(string tendn)
        {
            TaiKhoan tk = data.TaiKhoans.SingleOrDefault(n => n.TenDN == tendn);
            if (tk == null)
            {
                Response.SubStatusCode = 404;
                return null;
            }
            data.TaiKhoans.DeleteOnSubmit(tk);
            data.SubmitChanges();
            return RedirectToAction("DSTaiKhoan");
        }
    }
    

}