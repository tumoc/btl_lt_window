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
    
    public partial class ChiTietHD
    {
        public int MaHD { get; set; }
        public int MaDT { get; set; }
        public Nullable<int> SoLuong { get; set; }
    
        public virtual DienThoai DienThoai { get; set; }
        public virtual HoaDon HoaDon { get; set; }
    }
}
