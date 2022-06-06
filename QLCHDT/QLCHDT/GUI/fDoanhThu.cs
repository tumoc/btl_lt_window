using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCHDT_Nhom7
{
    public partial class fDoanhThu : Form
    {
        QLCuaHangDTEntities CSDL = new QLCuaHangDTEntities();
        public fDoanhThu()
        {
            InitializeComponent();
        }

        private void fDoanhThu_Load(object sender, EventArgs e)
        {
            tinhDoanhThu();
        }

        private void dtpTu_ValueChanged(object sender, EventArgs e)
        {
            tinhDoanhThu();
        }

        private void dtpDen_ValueChanged(object sender, EventArgs e)
        {
            tinhDoanhThu();
        }

        private void tinhDoanhThu()
        {
            double doanhThu = 0;

            var kq = from k in CSDL.ChiTietHDs
                     where k.HoaDon.NgayLap.Value <= dtpDen.Value
                     && k.HoaDon.NgayLap.Value >= dtpTu.Value
                     select new
                     {
                         dt = k.DienThoai.TenDT,
                         sl = k.SoLuong,
                         tong = k.DienThoai.GiaBan * k.SoLuong,
                         ngay = k.HoaDon.NgayLap,
                         k.MaHD,
                         k.MaDT,
                     };


            dgvDoanhThu.DataSource = kq.ToList();
            dgvDoanhThu.Columns[0].HeaderText = "Điện Thoại";
            dgvDoanhThu.Columns[1].HeaderText = "Số Lượng";
            dgvDoanhThu.Columns[2].HeaderText = "Tổng Tiền";
            dgvDoanhThu.Columns[3].HeaderText = "Ngày Bán";
            dgvDoanhThu.Columns[4].Visible = false;
            dgvDoanhThu.Columns[5].Visible = false;
            foreach (var i in kq)
                doanhThu += Convert.ToDouble(i.tong);

            lblDoanhThu.Text = doanhThu.ToString() + " VND";
        }

        private void radHomNay_CheckedChanged(object sender, EventArgs e)
        {
            if(radHomNay.Checked)
            {
                dtpTu.Value = DateTime.Today;
                dtpDen.Value = DateTime.Today;
                tinhDoanhThu();
            }
        }

        private void radTuan_CheckedChanged(object sender, EventArgs e)
        {
            if (radTuan.Checked)
            {
                dtpTu.Value = DateTime.Today.AddDays(-7);
                dtpDen.Value = DateTime.Today;
                tinhDoanhThu();
            }
        }

        private void radThang_CheckedChanged(object sender, EventArgs e)
        {
            if (radThang.Checked)
            {
                dtpTu.Value = DateTime.Today.AddDays(-30);
                dtpDen.Value = DateTime.Today;
                tinhDoanhThu();
            }
        }
    }
}
