using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WebNoiThat.Models;


namespace WebNoiThat.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        MyDataDataContext data = new MyDataDataContext();
        // GET: User
        public ActionResult ListAfterLogin1(int? page)
        {
            if (page == null) page = 1;
            var all_noithat = (from s in data.NoiThats select s).OrderBy(m => m.manoithat);
            int pageSize = 6;
            int pageNum = page ?? 1;
            return View(all_noithat.ToPagedList(pageNum, pageSize));
        }
        public ActionResult ListAfterLogin2(int? page)
        {
            if (page == null) page = 1;
            var all_noithat = (from s in data.NoiThats select s).OrderBy(m => m.manoithat);
            int pageSize = 6;
            int pageNum = page ?? 1;
            return View(all_noithat.ToPagedList(pageNum, pageSize));
        }
        public ActionResult Dangky()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Dangky(FormCollection collection, NguoiDung s)
        {
            var E_hoten = collection["hoten"];
            var E_tendangnhap = collection["tendangnhap"];
            var E_matkhau = collection["matkhau"];
            var E_dienthoai = collection["dienthoai"];
            var E_diachi = collection["diachi"];
            if (string.IsNullOrEmpty(E_hoten))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                s.hoten = E_hoten.ToString();
                s.tendangnhap = E_tendangnhap.ToString();
                s.matkhau = E_matkhau.ToString();
                s.dienthoai = E_dienthoai.ToString();
                s.diachi = E_diachi;
                data.NguoiDungs.InsertOnSubmit(s);
                data.SubmitChanges();
                return RedirectToAction("Dangnhap");
            }
            return this.Dangky();
        }
        [HttpGet]
        public ActionResult Dangnhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangnhap(String username, String password)
        {

            var dt = data.NguoiDungs.Where(x => x.tendangnhap.Equals(username) && x.matkhau.Equals(password)).ToList();
            if (dt.Count() > 0 && dt.FirstOrDefault().tendangnhap.Equals("Admin"))
            {
                //add session
                Session["manguoidung"] = dt.FirstOrDefault().manguoidung;
                Session["tendangnhap"] = dt.FirstOrDefault().tendangnhap;
                Session["matkhau"] = dt.FirstOrDefault().matkhau;

                return RedirectToAction("ListAfterLogin1");
            }
            if (dt.Count() > 0 && dt.FirstOrDefault().tendangnhap != "Admin")
            {
                Session["manguoidung"] = dt.FirstOrDefault().manguoidung;
                Session["tendangnhap"] = dt.FirstOrDefault().tendangnhap;
                Session["matkhau"] = dt.FirstOrDefault().matkhau;

                return RedirectToAction("ListAfterLogin2");
            }

            else
            {
                ViewBag.error = "Login failed";
                return RedirectToAction("Login");
            }


            return View();
        }
        [HttpGet]
        public ActionResult QLtk()
        {
            var all_tk = (from s in data.NguoiDungs select s).OrderBy(m => m.manguoidung);
            return View(all_tk);
        }


        public ActionResult Edit(int id)
        {
            var E_tk = data.NguoiDungs.First(m => m.manguoidung == id);
            return View(E_tk);
        }
        [HttpPost]

        public ActionResult Edit(int id, FormCollection collection)
        {
            var E_taikhoan = data.NguoiDungs.First(m => m.manguoidung == id);
            var E_hoten = collection["hoten"];
            var E_tendangnhap = collection["tendangnhap"];
            var E_matkhau = collection["matkhau"];
            var E_dienthoai = collection["dienthoai"];
            var E_diachi = collection["diachi"];
            if (string.IsNullOrEmpty(E_tendangnhap))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                E_taikhoan.hoten = E_hoten;
                E_taikhoan.tendangnhap = E_tendangnhap;
                E_taikhoan.matkhau = E_matkhau;
                E_taikhoan.dienthoai = E_dienthoai;
                E_taikhoan.diachi = E_diachi;

                UpdateModel(E_taikhoan);
                data.SubmitChanges();
                return RedirectToAction("QLtk");
            }
            return this.Edit(id);
        }
        //-----------------------------------------

        public ActionResult Delete(int id)
        {
            var D_taikhoan = data.NguoiDungs.First(m => m.manguoidung == id);
            return View(D_taikhoan);
        }
        [HttpPost]

        public ActionResult Delete(int id, FormCollection collection)
        {
            var D_tk = data.NguoiDungs.Where(m => m.manguoidung == id).First();
            data.NguoiDungs.DeleteOnSubmit(D_tk);
            data.SubmitChanges();
            return RedirectToAction("QLtk");
        }
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

    }
}