using ShopCoinUSAv2.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopCoinUSAv2.Controllers
{
    public class ShopCoinController : Controller
    {
        DataClasses1DataContext data = new DataClasses1DataContext();
      
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(FormCollection collection)
        {

            var tendn = collection["TenDN"];
            var mk = collection["MatKhau"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["loi"] = "You must enter the login name";
            }
            else if (String.IsNullOrEmpty(mk))
            {
                ViewData["loi1"] = "You must enter a password";
            }
            else
            {
                TaiKhoan tk = data.TaiKhoans.SingleOrDefault(n => n.TenDN.Equals(tendn) && n.MatKhau.Equals(mk));
                if (tk != null)
                {
                    if (tk.Quyen == true)
                    {
                        @Session["ten"] = tk.TenDN;
                        @Session["TK"] = tk.TenDN;
                        @Session["quyen"] = 1;
                        ViewBag.ThongBao = "Successful login Admin";
                        return RedirectToAction("Home", "Admin/HomeAdmin");
                    }
                    if (tk.Quyen == false || tk.Quyen == null)
                    {
                        @Session["quyen"] = null;
                        @Session["TK"] = tk.TenDN;
                        @Session["ten"] = tk.TenDN;
                        ViewBag.ThongBao = "Logged in successfully";
                        return RedirectToAction("Index", "ShopCoin");
                    }

                }
                else
                {
                    ViewData["loi2"] = "account password is not correct";
                }
            }



            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(TaiKhoan tk, FormCollection coll)
        {
           
            var tendn = coll["TenDN"];
            var mk = coll["MatKhau"];
            var username = coll["UserName"];
            var taikhoan = data.TaiKhoans.ToList();
            int kt = 0;
            foreach (var item in taikhoan)
            {
                if (item.TenDN == tendn)
                    kt = 1;
            }
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi"] = "Username cannot be blank";

            }
            else if (String.IsNullOrEmpty(mk))
            {
                ViewData["Loi1"] = "Password can not be blank";
            }
            else if (kt == 1)
            {
                ViewData["Loi2"] = "Already exist";
            }
            else if (String.IsNullOrEmpty(username))
            {
                ViewData["Loi3"] = "You must enter a name";
            }
            
            
            else
            {
                tk.UserName = username;
                tk.TenDN = tendn;
                tk.MatKhau = mk;
                data.TaiKhoans.InsertOnSubmit(tk);
                data.SubmitChanges();
                return RedirectToAction("/Login");
            }
            return View();
        }
        public ActionResult DangXuat()
        {
            Session["ten"] = null;
            Session["quyen"] = null;
            Session["TK"] = null;
            return Redirect("/");
        
        }

           
        
    }
}