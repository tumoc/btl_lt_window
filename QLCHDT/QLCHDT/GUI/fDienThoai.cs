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
    public partial class fDienThoai : Form
    {
        QLCuaHangDTEntities CSDL = new QLCuaHangDTEntities();
        public int m { get; set; }
        public fDienThoai()
        {
            InitializeComponent();
        }

        private void frmQLDienThoai_Load(object sender, EventArgs e)
        {
            tbxMa.Enabled = false;
            cbxBaoHanh.Items.Add("6 tháng");
            cbxBaoHanh.Items.Add("12 tháng");
            cbxBaoHanh.Text = "12 tháng";
            hienThi();
        }
        void hienThi()
        {
            dgvDt.DataSource = CSDL.DienThoais.ToList(); ;
            dgvDt.Columns[0].HeaderText = "Mã ĐT";
            dgvDt.Columns[1].HeaderText = "Tên Điện Thoại";
            dgvDt.Columns[2].HeaderText = "Giá Bán";
            dgvDt.Columns[3].HeaderText = "Bảo Hành";
            dgvDt.Columns[4].Visible = false;
            dgvDt.Columns[5].Visible = false;
            dgvDt.Columns[0].Width = 70;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbxTen.Text.Trim()))
            {
                errorProvider1.SetError(tbxTen, "không để trống");
                return;
            }
            if (string.IsNullOrEmpty(tbxGia.Text.Trim()))
            {
                errorProvider2.SetError(tbxGia, "không để trống");
                return;
            }

            DienThoai dt = new DienThoai()
            {
                TenDT = tbxTen.Text,
                GiaBan = Convert.ToDecimal(tbxGia.Text),
                BaoHanh = cbxBaoHanh.Text              
            };

            CSDL.DienThoais.Add(dt);
            CSDL.SaveChanges();
            xoaText();
            hienThi();

        }
        private void xoaText()
        {
            tbxMa.Clear();
            tbxTen.Clear();
            tbxGia.Clear();
            tbxTen.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult d = MessageBox.Show("Xác Nhận Sửa", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (d == DialogResult.Yes)
                {
                    int m = Convert.ToInt32(tbxMa.Text);
                    DienThoai dt = CSDL.DienThoais.Find(m);
                    dt.TenDT = tbxTen.Text;
                    dt.GiaBan = Convert.ToDecimal(tbxGia.Text);
                    dt.BaoHanh = cbxBaoHanh.Text;
                    CSDL.SaveChanges();
                    hienThi();
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Bạn Phải Chọn Ít Nhất 1 Điện Thoại", "Thông Báo");
            }

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
          
            
                DialogResult d = MessageBox.Show("Xác Nhận Xóa ?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (d == DialogResult.Yes)
                {
                    int m = Convert.ToInt32(tbxMa.Text);
                    DienThoai dt = CSDL.DienThoais.Find(m);
                    CSDL.DienThoais.Remove(dt);
                    CSDL.SaveChanges();
                }


        }

        private void dgvDt_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int r = e.RowIndex;
                tbxMa.Text = dgvDt.Rows[r].Cells[0].Value.ToString();
                tbxTen.Text = dgvDt.Rows[r].Cells[1].Value.ToString();
                tbxGia.Text = dgvDt.Rows[r].Cells[2].Value.ToString();
                cbxBaoHanh.Text = dgvDt.Rows[r].Cells[3].Value.ToString();
            }
            catch (Exception)
            {
                hienThi();
            }
        }

        private void tbxTim_Click(object sender, EventArgs e)
        {
            tbxTim.Clear();
        }

        private void tbxTim_TextChanged(object sender, EventArgs e)
        {
            tbxTim.ForeColor = Color.Black;
            try
            {
                int m = 0;
                m = Convert.ToInt32(tbxTim.Text);
                dgvDt.DataSource = CSDL.DienThoais.Where(dt => dt.MaDT == m).ToList();

                DienThoai dtt = CSDL.DienThoais.Find(m);
                tbxMa.Text = dtt.MaDT.ToString();
                tbxTen.Text = dtt.TenDT;
                tbxGia.Text = dtt.GiaBan.ToString();
                cbxBaoHanh.Text = dtt.BaoHanh;
            }
            catch (Exception)
            {
                hienThi();
                xoaText();
                tbxTim.Focus();
            }

        }
    }

}
