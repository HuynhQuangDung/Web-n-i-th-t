using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace WebNoiThat.Models
{

        public class Giohang
        {
            MyDataDataContext data = new MyDataDataContext();
            public int manoithat { get; set; }
            [Display(Name = "Tên nội thất")]
            public string tennoithat { get; set; }
            [Display(Name = "Ảnh bìa")]
            public string hinh { get; set; }
            [Display(Name = "Giá bán")]
            public double giaban { get; set; }
            [Display(Name = "Số lượng")]
            public int iSoluong { get; set; }
            [Display(Name = "Thành tiền")]
            public double dThanhtien
            {
                get { return iSoluong * giaban; }
            }
            public Giohang(int id)
            {
                manoithat = id;
                NoiThat sach = data.NoiThats.Single(n => n.manoithat == manoithat);
                tennoithat = sach.tennoithat;
                hinh = sach.hinh;
                giaban = double.Parse(sach.giaban.ToString());
                iSoluong = 1;
            }

        }
    }
