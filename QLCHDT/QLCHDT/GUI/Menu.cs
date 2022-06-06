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
    public partial class Menu : Form
    {
        public string hello { get; set; }
        public string ChucVu { get; set; }
        public Menu()
        {
            InitializeComponent();
        }

        fNhanVien fnhanVien = new fNhanVien();
        fKho fKho = new fKho();
        fNhaCungCap fNcc = new fNhaCungCap();
        fThemHD fCTHD = new fThemHD();
        fBanChay fBanchay = new fBanChay();
        fXemHD fXemSua = new fXemHD();
        fDoanhThu fDanhThu = new fDoanhThu();

        private void Menu_Load(object sender, EventArgs e)
        {
            tbxHello.Text = hello;
            phanQuyen(ChucVu);
        }
        private void btnQLNV_Click(object sender, EventArgs e)
        {
            clickBorder(btnQLNV);
            pnFormChon.Controls.Clear();

            fnhanVien = new fNhanVien();
            fnhanVien.TopLevel = false;
            fnhanVien.AutoScroll = true;
            pnFormChon.Controls.Add(fnhanVien);
            fnhanVien.Show();
        }

        private void btnQLKho_Click(object sender, EventArgs e)
        {
            clickBorder(btnQLKho);

            pnFormChon.Controls.Clear();

            fKho = new fKho();
            fKho.TopLevel = false;
            fKho.AutoScroll = true;
            pnFormChon.Controls.Add(fKho);
            fKho.Show();
        }

        private void btnQLNCC_Click(object sender, EventArgs e)
        {
            clickBorder(btnQLNCC);

            pnFormChon.Controls.Clear();

            fNcc = new fNhaCungCap();
            fNcc.TopLevel = false;
            fNcc.AutoScroll = true;
            pnFormChon.Controls.Add(fNcc);
            fNcc.Show();
        }

        private void btnThemHD_Click(object sender, EventArgs e)
        {
            clickBorder(btnThemHD);

            fCTHD = new fThemHD();
            fCTHD.TopLevel = false;
            fCTHD.AutoScroll = false;
            pnFormChon.Controls.Add(fCTHD);
            fCTHD.Show();
        }

        private void btnBanChay_Click(object sender, EventArgs e)
        {
            clickBorder(btnBanChay);

            pnFormChon.Controls.Clear();

            fBanchay = new fBanChay();
            fBanchay.TopLevel = false;
            fBanchay.AutoScroll = true;
            pnFormChon.Controls.Add(fBanchay);
            fBanchay.Show();
        }

        private void btnXemHD_Click(object sender, EventArgs e)
        {
            clickBorder(btnXemHD);

            fXemSua = new fXemHD();
            fXemSua.TopLevel = false;
            fXemSua.AutoScroll = false;
            pnFormChon.Controls.Add(fXemSua);
            fXemSua.Show();

        }

        private void btnDoanhThu_Click(object sender, EventArgs e)
        {
            clickBorder(btnDoanhThu);

            fDanhThu = new fDoanhThu();
            fDanhThu.TopLevel = false;
            fDanhThu.AutoScroll = true;
            pnFormChon.Controls.Add(fDanhThu);
            fDanhThu.Show();

        }


        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Đăng Xuất?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if( d == DialogResult.Yes )
                this.Close(); 

        }

        private void clickBorder(Button a)
        {
            btnBanChay.FlatAppearance.BorderSize = 0;
            btnDangXuat.FlatAppearance.BorderSize = 0;
            btnDoanhThu.FlatAppearance.BorderSize = 0;
            btnQLKho.FlatAppearance.BorderSize = 0;
            btnQLNCC.FlatAppearance.BorderSize = 0;
            btnQLNV.FlatAppearance.BorderSize = 0;
            btnThemHD.FlatAppearance.BorderSize = 0;
            btnXemHD.FlatAppearance.BorderSize = 0;
            a.FlatAppearance.BorderSize = 1;
        }

        private void phanQuyen(string a)
        {
            if (a.Equals("Nhân Viên"))
            {
                this.btnQLNV.Visible = false;
                MessageBox.Show("Bạn đã đăng nhập với tư cách " + ChucVu+"\nMột số chức năng sẽ bị hạn chế");
            }
            else
            {
                this.btnQLNV.Visible = true;
                MessageBox.Show("Bạn đã đăng nhập với tư cách " + ChucVu);
            }

        }

    }
}
