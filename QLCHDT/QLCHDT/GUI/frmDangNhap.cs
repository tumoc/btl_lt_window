using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QLCHDT_Nhom7
{
    public partial class frmDangNhap : Form
    {
        QLCuaHangDTEntities CSDL = new QLCuaHangDTEntities();
        public frmDangNhap()
        {
            InitializeComponent();
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void tbxTaiKhoan_TextChanged(object sender, EventArgs e)
        {
            tbxTaiKhoan.ForeColor = Color.Black;
        }

        private void btnDangNhap_Click_1(object sender, EventArgs e)
        {
            NhanVien a = CSDL.NhanViens.Where(q => q.TaiKhoan.Equals(tbxTaiKhoan.Text) && q.MatKhau.Equals(tbxMatKhau.Text)).SingleOrDefault();
            if (a != null)
            {
                MessageBox.Show("Login Sucess", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Menu m = new Menu();
                m.hello = a.HoTenNV;
                m.ChucVu = a.ChucVu;
                this.Hide();
                m.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Incorect Username or Password","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private void tbxTaiKhoan_Click(object sender, EventArgs e)
        {
            tbxTaiKhoan.Clear();
        }

        private void tbxMatKhau_Click(object sender, EventArgs e)
        {
            tbxMatKhau.Clear();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Liên Hệ Quản Lý Để Được Cấp Lại", "Thông Báo");
        }

    }
}
