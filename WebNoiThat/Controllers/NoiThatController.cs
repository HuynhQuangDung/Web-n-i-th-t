using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNoiThat.Models;

namespace WebNoiThat.Controllers
{
    public class NoiThatController : Controller
    {
        // GET: Sach
        MyDataDataContext data = new MyDataDataContext();
        public ActionResult ListNoiThat(string SearchString , int maloai = 0)
        {
            var all_noithat = from ss in data.NoiThats select ss;
            var theloai = from ss in data.TheLoais select ss;
            if (!String.IsNullOrEmpty(SearchString))
            {
                all_noithat = all_noithat.Where(b => b.tennoithat.Contains(SearchString));
            }
            if(maloai != 0)
            {
                all_noithat = all_noithat.Where(c => c.maloai == maloai);
            }
            ViewBag.maloai = new SelectList(theloai, "maloai", "tenloai");
            return View(all_noithat.ToList());

        }
        public ActionResult Detail(int id)
        {
            var D_noithat = data.NoiThats.Where(m => m.manoithat == id).First();
            return View(D_noithat);
        }

        public ActionResult Detail1(int id)
        {
            var D_noithat = data.NoiThats.Where(m => m.manoithat == id).First();
            return View(D_noithat);
        }
        public ActionResult Detail2(string tennoithat)
        {
            
            var D_noithat = data.NoiThats.Where(m => m.tennoithat == "Phòng khách").ToList();
            return View(D_noithat);
        }
        public ActionResult Phanloai(int? id)
        {
            var D_noithat = data.NoiThats.Where(m => m.maloai == id);
            return View(D_noithat.ToList());
        }
        public ActionResult Phanloai2(int? id)
        {
            var D_noithat = data.NoiThats.Where(m => m.maloai == id);
            return View(D_noithat.ToList());
        }
        public ActionResult Phanloai3(int? id)
        {
            var D_noithat = data.NoiThats.Where(m => m.maloai == id);
            return View(D_noithat.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, NoiThat s)
        {
            var E_tennoithat = collection["tennoithat"];
            var E_hinh = collection["hinh"];
            var E_giaban = Convert.ToDecimal(collection["giaban"]);
            var E_ngaycapnhat = Convert.ToDateTime(collection["ngaycapnhat"]);
            var E_soluongton = Convert.ToInt32(collection["soluongton"]);
            var E_chitiet = collection["chitiet"];
            if (string.IsNullOrEmpty(E_tennoithat))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                s.tennoithat = E_tennoithat.ToString();
                s.hinh = E_hinh.ToString();
                s.giaban = E_giaban;
                s.ngaycapnhat = E_ngaycapnhat;
                s.soluongton = E_soluongton;
                data.NoiThats.InsertOnSubmit(s);
                data.SubmitChanges();
                return RedirectToAction("ListNoiThat");
            }
            return this.Create();
        }
        public ActionResult Edit(int id)
        {
            var E_noithat = data.NoiThats.First(m => m.manoithat == id);
            return View(E_noithat);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var E_noithat = data.NoiThats.First(m => m.manoithat == id);
            var E_tennoithat = collection["tennoithat"];
            var E_hinh = collection["hinh"];
            var E_giaban = Convert.ToDecimal(collection["giaban"]);
            var E_ngaycapnhat = Convert.ToDateTime(collection["ngaycatnhat"]);
            var E_soluongton = Convert.ToInt32(collection["soluongton"]);
            var E_chitiet = collection["chitiet"];
            E_noithat.manoithat = id;
            if (string.IsNullOrEmpty(E_tennoithat))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                E_noithat.tennoithat = E_tennoithat;
                E_noithat.hinh = E_hinh;
                E_noithat.giaban = E_giaban;
                E_noithat.ngaycapnhat = E_ngaycapnhat;
                E_noithat.soluongton = E_soluongton;
                E_noithat.chitiet = E_chitiet;
                UpdateModel(E_noithat);
                data.SubmitChanges();
                return RedirectToAction("ListNoiThat");
            }
            return this.Edit(id);
        }
        //-----------------------------------------
        public ActionResult Delete(int id)
        {
            var D_noithat = data.NoiThats.First(m => m.manoithat == id);
            return View(D_noithat);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var D_noithat = data.NoiThats.Where(m => m.manoithat == id).First();
            data.NoiThats.DeleteOnSubmit(D_noithat);
            data.SubmitChanges();
            return RedirectToAction("ListNoiThat");
        }
    }
}
