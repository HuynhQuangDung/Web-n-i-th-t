using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebNoiThat.Models
{
    public class CustomRoleProvider
    {
        MyDataDataContext data = new MyDataDataContext(); //khai bao context
        public  string[] GetRolesForUser(string name)
        {
            // tạo biến getrole, so sánh xem UserID đang đăng nhập có giống với tên trong db ko
            NguoiDung account = data.NguoiDungs.Single(x => x.tendangnhap.Equals(name));
            if (account != null) // Nếu giống
            {
                return new String[] { account.Role.tenrole };
            }
            else
                return new String[] { };
        }
    }
}