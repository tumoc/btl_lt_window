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
    public partial class fNhanVien : Form
    {
        QLCuaHangDTEntities CSDL = new QLCuaHangDTEntities();
        public fNhanVien()
        {
            InitializeComponent();
        }

        private void fNhanVien_Load(object sender, EventArgs e)
        {
            tbxMa.Enabled = false;
            cbxChucVu.Items.Add("Nhân Viên");
            cbxChucVu.Items.Add("Quản Lý");
            cbxChucVu.Text = "Nhân Viên";
            hienThi();
        }
        void hienThi()
        {
            dgvNhanVien.DataSource = CSDL.NhanViens.Select(p => p).ToList();

            dgvNhanVien.Columns[0].HeaderText = "Mã NV";
            dgvNhanVien.Columns[1].HeaderText = "Họ Tên";
            dgvNhanVien.Columns[2].HeaderText = "Điện Thoại";
            dgvNhanVien.Columns[3].HeaderText = "Địa Chỉ";
            dgvNhanVien.Columns[4].HeaderText = "Tài Khoản";
            dgvNhanVien.Columns[5].HeaderText = "Mật Khẩu";
            dgvNhanVien.Columns[6].HeaderText = "Ngày Vào";
            dgvNhanVien.Columns[7].HeaderText = "Chức Vụ";
            dgvNhanVien.Columns[8].Visible = false;

            dgvNhanVien.Columns[0].Width = 50;
            dgvNhanVien.Columns[3].Width = 250;
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tbxMa.Visible = true;
            tbxMa.Enabled = false;
            try
            {
                int r = e.RowIndex;
                tbxMa.Text = dgvNhanVien.Rows[r].Cells[0].Value.ToString();
                tbxTen.Text = dgvNhanVien.Rows[r].Cells[1].Value.ToString();
                tbxSDT.Text = dgvNhanVien.Rows[r].Cells[2].Value.ToString();
                tbxDC.Text = dgvNhanVien.Rows[r].Cells[3].Value.ToString();
                tbxTK.Text = dgvNhanVien.Rows[r].Cells[4].Value.ToString();
                tbxMK.Text = dgvNhanVien.Rows[r].Cells[5].Value.ToString();
                dtpNgayVao.Text = dgvNhanVien.Rows[r].Cells[6].Value.ToString();
                cbxChucVu.Text = dgvNhanVien.Rows[r].Cells[7].Value.ToString();
            }
            catch (Exception)
            {

                hienThi();
            }

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbxTen.Text.Trim()))
            {
                errorProvider1.SetError(tbxTen, "Không để trống");
                return;
            }
            if (string.IsNullOrEmpty(tbxSDT.Text.Trim()))
            {
                errorProvider2.SetError(tbxSDT, "Không để trống");
                return;
            }
            if (string.IsNullOrEmpty(tbxDC.Text.Trim()))
            {
                errorProvider3.SetError(tbxDC, "Không để trống");
                return;
            }
            if (string.IsNullOrEmpty(tbxTK.Text.Trim()))
            {
                errorProvider4.SetError(tbxTK, "Không để trống");
                return;
            }
            if (string.IsNullOrEmpty(tbxMK.Text.Trim()))
            {
                errorProvider5.SetError(tbxMK, "Không để trống");
                return;
            }

            NhanVien nvMoi = new NhanVien()
            {
                HoTenNV = tbxTen.Text,
                SoDTNV = tbxSDT.Text,
                DiaChiNV = tbxDC.Text,
                TaiKhoan = tbxTK.Text,
                MatKhau = tbxMK.Text,
                NgayVao = dtpNgayVao.Value,
            };

            CSDL.NhanViens.Add(nvMoi);
            CSDL.SaveChanges();
            xoaText();
            hienThi();
        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Xóa Nhân Viên?", "Cảnh Bảo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if(d == DialogResult.Yes)
            {
                int maxoa = Convert.ToInt32(tbxMa.Text);
                NhanVien nvXoa = CSDL.NhanViens.Find(maxoa);
                
                CSDL.NhanViens.Remove(nvXoa);

                CSDL.SaveChanges();
                xoaText();
            }

            hienThi();
        }

        private void tbxTim_Click(object sender, EventArgs e)
        {
            tbxTim.Clear();
        }

        private void tbxTim_TextChanged(object sender, EventArgs e)
        {
            xoaText();
            
            tbxTim.ForeColor = Color.Black;
            int maTim = 0;
            try
            {
                maTim = Convert.ToInt32(tbxTim.Text);
                var kq = CSDL.NhanViens.Where(p => p.MaNV.Equals(maTim)).Select(p => p);
                dgvNhanVien.DataSource = kq.ToList();

                NhanVien n = CSDL.NhanViens.Find(maTim);
                tbxMa.Text = n.MaNV.ToString();
                tbxTen.Text = n.HoTenNV;
                tbxSDT.Text = n.SoDTNV;
                tbxTK.Text = n.TaiKhoan;
                tbxMK.Text = n.MatKhau;
                dtpNgayVao.Value = n.NgayVao.Value;
                tbxDC.Text = n.DiaChiNV;
                
            }
            catch (Exception)
            {
                hienThi();
            }
            tbxTim.Focus();

        }

        private void btSua_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("xác nhận sửa thông tin?", "cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (d == DialogResult.Yes)
            {
                int m = Convert.ToInt32(dgvNhanVien.SelectedCells[0].OwningRow.Cells["MaNV"].Value.ToString());
                NhanVien nvSua = CSDL.NhanViens.Find(m);
                nvSua.HoTenNV = tbxTen.Text;
                nvSua.SoDTNV = tbxSDT.Text;
                nvSua.DiaChiNV = tbxDC.Text;
                nvSua.TaiKhoan = tbxTK.Text;
                nvSua.MatKhau = tbxMK.Text;

                CSDL.SaveChanges();
                xoaText();
            }
            hienThi();
        }

        private void xoaText()
        {
            tbxTen.Clear();
            tbxMa.Clear();
            tbxSDT.Clear();
            tbxDC.Clear();
            tbxTK.Clear();
            tbxMK.Clear();
            tbxTen.Focus();
        }

    }
}
