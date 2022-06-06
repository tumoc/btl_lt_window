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
    public partial class fXemHD : Form
    {
        public fXemHD()
        {
            InitializeComponent();
        }
        QLCuaHangDTEntities CSDL = new QLCuaHangDTEntities();
        private void frmChiTietHD_Load(object sender, EventArgs e)
        {
            hienThi();
        } 
        void hienThi()
        {
            // lấy dữ liệu chi tiết hóa đơn => hiển thị vào datagridview
            var kq = CSDL.ChiTietHDs.Select(p => new
            {
                p.MaHD,
                p.HoaDon.KhachHang.TenKH,
                p.MaDT,
                p.DienThoai.TenDT,
                p.SoLuong,
                p.HoaDon.KhachHang.SoDTKH,
                p.HoaDon.KhachHang.DCKH,
                p.HoaDon.KhachHang.EmailKH,
                p.HoaDon.NhanVien.HoTenNV,
                p.HoaDon.GiamGia,
                p.HoaDon.NgayLap,
                p.HoaDon.MaKH
            });

            dgvCTHD.DataSource = kq.ToList();

            dgvCTHD.Columns[0].HeaderText = "Hóa Đơn";
            dgvCTHD.Columns[1].HeaderText = "Khách Hàng";
            dgvCTHD.Columns[2].Visible = false;
            dgvCTHD.Columns[3].HeaderText = "Điện Thoại";
            dgvCTHD.Columns[4].HeaderText = "SL";
            dgvCTHD.Columns[5].HeaderText = "Số Điện Thoại";
            dgvCTHD.Columns[6].HeaderText = "Địa Chỉ";
            dgvCTHD.Columns[7].HeaderText = "Email";
            dgvCTHD.Columns[7].Width = 100;
            dgvCTHD.Columns[8].HeaderText = "Nhân Viên";
            dgvCTHD.Columns[9].HeaderText = "Giảm Giá";
            dgvCTHD.Columns[10].HeaderText = "Ngày Lập";

        }

        private void dgvCTHD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int r = e.RowIndex;
                label1.Text = "HÓA ĐƠN: " + dgvCTHD.Rows[r].Cells[0].Value.ToString();
                tbxTen.Text = dgvCTHD.Rows[r].Cells[1].Value.ToString();
                tbxDt.Text = dgvCTHD.Rows[r].Cells[5].Value.ToString();
                tbxDC.Text = dgvCTHD.Rows[r].Cells[6].Value.ToString();
                tbxEmail.Text = dgvCTHD.Rows[r].Cells[7].Value.ToString();
                tbxGiamGia.Text = dgvCTHD.Rows[r].Cells[9].Value.ToString();
                dateTimePicker1.Text = dgvCTHD.Rows[r].Cells[10].Value.ToString();
                tbxNhanVien.Text = dgvCTHD.Rows[r].Cells[8].Value.ToString();

            }
            catch (Exception)
            {
                hienThi();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {

                DialogResult d = MessageBox.Show("Xác Nhận xóa?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (d == DialogResult.Yes)
            {
                int mKH = Convert.ToInt32(dgvCTHD.SelectedCells[0].OwningRow.Cells["MaKH"].Value.ToString());
                int mHD = Convert.ToInt32(dgvCTHD.SelectedCells[0].OwningRow.Cells["MaHD"].Value.ToString());
                int mDT = Convert.ToInt32(dgvCTHD.SelectedCells[0].OwningRow.Cells["MaDT"].Value.ToString());

                KhachHang kh = CSDL.KhachHangs.Find(mKH);
                HoaDon hd = CSDL.HoaDons.Find(mHD);

                List<ChiTietHD> ctt = CSDL.ChiTietHDs.Where(t => t.MaHD == hd.MaHD).ToList();
                foreach (ChiTietHD c in ctt)
                    CSDL.ChiTietHDs.Remove(c);

                CSDL.HoaDons.Remove(hd);

                CSDL.SaveChanges();
                hienThi();
                xoaText();
            }      
        }

        private void xoaText()
        {
            tbxDC.Text = "...";
            tbxDt.Text = "...";
            tbxEmail.Text = "...";
            tbxGiamGia.Text = "...";
            tbxTen.Text = "...";

        }

        private void tbxMaHD_Click(object sender, EventArgs e)
        {
            tbxMaHD.Clear();
            tbxMaHD.ForeColor = Color.Black;
        }

        private void ptbTim_Click(object sender, EventArgs e)
        {
            try
            {
                int m = 0;
                m = Convert.ToInt32(tbxMaHD.Text);

                var kq = CSDL.ChiTietHDs.Select(p => new
                {
                    p.MaHD,
                    p.HoaDon.KhachHang.TenKH,
                    p.MaDT,
                    p.DienThoai.TenDT,
                    p.SoLuong,
                    p.HoaDon.KhachHang.SoDTKH,
                    p.HoaDon.KhachHang.DCKH,
                    p.HoaDon.KhachHang.EmailKH,
                    p.HoaDon.NhanVien.HoTenNV,
                    p.HoaDon.GiamGia,
                    p.HoaDon.NgayLap,
                    p.HoaDon.MaKH,
                    thanhtien = p.DienThoai.GiaBan * p.SoLuong * (100 - p.HoaDon.GiamGia) / 100
                }).Where(p => p.MaHD == m);
                
                dgvCTHD.DataSource = kq.ToList();
                dgvCTHD.Columns[11].Visible = false;
                dgvCTHD.Columns[12].Visible = false;
                if (CSDL.HoaDons.Find(m) == null)
                {
                    MessageBox.Show("Không tìm được hóa đơn tương ứng", "Thông báo");
                    tbxMaHD.Clear();
                    tbxMaHD.Focus();
                    xoaText();
                    label1.Text = "QUẢN LÝ HÓA ĐƠN";
                }      
                    int r = 0;
                    label1.Text = "HÓA ĐƠN: " + dgvCTHD.Rows[r].Cells[0].Value.ToString();
                    tbxTen.Text = dgvCTHD.Rows[r].Cells[1].Value.ToString();
                    tbxDt.Text = dgvCTHD.Rows[r].Cells[5].Value.ToString();
                    tbxDC.Text = dgvCTHD.Rows[r].Cells[6].Value.ToString();
                    tbxEmail.Text = dgvCTHD.Rows[r].Cells[7].Value.ToString();
                    tbxGiamGia.Text = dgvCTHD.Rows[r].Cells[9].Value.ToString();
                    dateTimePicker1.Text = dgvCTHD.Rows[r].Cells[10].Value.ToString();
                    //tbxNhanVien.Text = dgvCTHD.Rows[r].Cells[8].Value.ToString();
                double tong = 0;
                foreach (var i in kq.ToList())
                {

                    tong += Convert.ToDouble(i.thanhtien);


                }
                lblTongT.Text = tong.ToString() + "VND";
            }
            catch (Exception)
            {
                hienThi();
            }
        }
    }
}
