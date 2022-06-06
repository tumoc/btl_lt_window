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
    public partial class fNhaCungCap : Form
    {
        QLCuaHangDTEntities CSDL = new QLCuaHangDTEntities();
        public int m { get; set; }
        public fNhaCungCap()
        {
            InitializeComponent();
        }

        private void frmQLNhaCungCap_Load(object sender, EventArgs e)
        {
            tbxMa.Enabled = false;
            hienThi();
        }
        void hienThi()
        {
            dgvNCC.DataSource = CSDL.NhaCungCaps.Select(p => p).ToList();
            dgvNCC.Columns[0].HeaderText = "Mã NCC";
            dgvNCC.Columns[1].HeaderText = "Tên NCC";
            dgvNCC.Columns[2].HeaderText = "Điện Thoại";
            dgvNCC.Columns[3].HeaderText = "Địa Chỉ";
            dgvNCC.Columns[4].Visible = false;
        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbxTen.Text.Trim()))
            {
                errorProvider1.SetError(tbxTen, "Không để trống");
                return;
            }

            if (string.IsNullOrEmpty(tbxDt.Text.Trim()))
            {
                errorProvider3.SetError(tbxDt, "Không để trống");
                return;
            }

            if (string.IsNullOrEmpty(tbxDc.Text.Trim()))
            {
                errorProvider2.SetError(tbxDc, "Không để trống");
                return;
            }
            NhaCungCap kh = new NhaCungCap()
            {
                TenNCC = tbxTen.Text.Trim(),
                DiaChiNCC = tbxDc.Text.Trim(),
                SoDTNCC = tbxDt.Text.Trim(),

            };
            CSDL.NhaCungCaps.Add(kh);
            CSDL.SaveChanges();

            hienThi();
            tbxMa.Enabled = false;
            tbxTen.Clear();
            tbxDc.Clear();
            tbxDt.Clear();
            tbxTen.Focus();

        }

        private void btnSua_Click_1(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Xác Nhận Sửa ?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if(d == DialogResult.Yes)
            {
                int m = Convert.ToInt32(tbxMa.Text);
                NhaCungCap kh = CSDL.NhaCungCaps.Find(m);
                kh.TenNCC = tbxTen.Text;
                kh.SoDTNCC = tbxDt.Text;
                kh.DiaChiNCC = tbxDc.Text;

                CSDL.SaveChanges();
            }
            hienThi();
        }

        private void dgvNCC_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int r = e.RowIndex;
                tbxMa.Visible = true;
                tbxMa.Enabled = false;
                tbxMa.Text = dgvNCC.Rows[r].Cells[0].Value.ToString();
                tbxTen.Text = dgvNCC.Rows[r].Cells[1].Value.ToString();
                tbxDt.Text = dgvNCC.Rows[r].Cells[2].Value.ToString();
                tbxDc.Text = dgvNCC.Rows[r].Cells[3].Value.ToString();
            }
            catch (Exception)
            {

                hienThi();
            }

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult d = MessageBox.Show("Xác Nhận Xóa?", "Cảnh Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (d == DialogResult.Yes)
                {
                    int maxoa = Convert.ToInt32(tbxMa.Text);
                    NhaCungCap ncc = CSDL.NhaCungCaps.Find(maxoa);

                    List<KhoHang> ds = CSDL.KhoHangs.Where(x => x.MaNCC == ncc.MaNCC).ToList();
                    foreach (KhoHang hd in ds)
                    {
                        CSDL.KhoHangs.Remove(hd);
                    }
                    CSDL.NhaCungCaps.Remove(ncc);
                    CSDL.SaveChanges();
                    hienThi();
                    xoaText();
                    tbxTim.Focus();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Bạn Phải Chọn Ít Nhăt 1  NCC","Thông Báo");
                tbxTim.Focus();
            }


        }
        private void tbxTim_TextChanged(object sender, EventArgs e)
        {
            try
            {
                tbxTim.ForeColor = Color.Black;
                int m = 0;
                m = Convert.ToInt32( tbxTim.Text);
                dgvNCC.DataSource = CSDL.NhaCungCaps.Where(p => p.MaNCC.Equals(m)).ToList();
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
        private void xoaText()
        {
            tbxMa.Clear();
            tbxTen.Clear();
            tbxDc.Clear();
            tbxDt.Clear();
            tbxTen.Focus();
        }
    }
}
