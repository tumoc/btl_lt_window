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
    
    public partial class DienThoai
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DienThoai()
        {
            this.ChiTietHDs = new HashSet<ChiTietHD>();
            this.KhoHangs = new HashSet<KhoHang>();
        }
    
        public int MaDT { get; set; }
        public string TenDT { get; set; }
        public Nullable<decimal> GiaBan { get; set; }
        public string BaoHanh { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHD> ChiTietHDs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KhoHang> KhoHangs { get; set; }
    }
}
