//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QLCHDT_Nhom7
{
    using System;
    using System.Collections.Generic;
    
    public partial class KhoHang
    {
        public int MaNCC { get; set; }
        public int MaDT { get; set; }
        public Nullable<int> SoLuongCon { get; set; }
        public Nullable<System.DateTime> NgayNhap { get; set; }
        public Nullable<decimal> GiaNhap { get; set; }
    
        public virtual DienThoai DienThoai { get; set; }
        public virtual NhaCungCap NhaCungCap { get; set; }
    }
}
