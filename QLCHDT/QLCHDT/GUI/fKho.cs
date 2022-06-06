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
    public partial class fKho : Form
    {
        QLCuaHangDTEntities CSDL = new QLCuaHangDTEntities();
        public fKho()
        {
            InitializeComponent();
        }
        private void frmQLKho_Load(object sender, EventArgs e)
        {
            //lấy danh sách nhà cung cấp, điện thoại trong CSDL
            cbxNCC.DataSource = CSDL.NhaCungCaps.ToList();
            cbxNCC.DisplayMember = "TenNCC";
            cbxNCC.ValueMember = "MaNCC";
            cbxNCC.Text = "Chọn sẵn có/ Thêm Mới?";
            
            cbxDT.DataSource = CSDL.DienThoais.ToList();
            cbxDT.DisplayMember = "TenDT";
            cbxDT.ValueMember = "MaDT";
            cbxDT.Text = "Chọn sẵn có/ Thêm Mới?";

            hienThi();
        }
        void hienThi()
        {
            var kq = CSDL.KhoHangs.Select(p => new
            {
                tenNCC = p.NhaCungCap.TenNCC,
                dienThoai = p.DienThoai.TenDT,
                soLuong = p.SoLuongCon,
                gia = p.GiaNhap,
                ngayNhap = p.NgayNhap
            }).OrderByDescending(p =>p.soLuong);//hiển thị theo số lượng giảm dần

            dgvKho.DataSource = kq.ToList();

            dgvKho.Columns[0].HeaderText = "Nhà Cung Cấp";
            dgvKho.Columns[1].HeaderText = "Điện Thoại";
            dgvKho.Columns[2].HeaderText = "Số Lượng";
            dgvKho.Columns[3].HeaderText = "Giá";
            dgvKho.Columns[4].HeaderText = "Ngày Nhập/ Cập Nhật";
        }

        private void cbxDT_Leave(object sender, EventArgs e)
        {
            cbxDT.ForeColor = Color.Black;      
        }

        private void cbxNCC_Leave(object sender, EventArgs e)
        {
            cbxNCC.ForeColor = Color.Black;
        }
        private void dgvKho_CellClick(object sender, DataGridViewCellEventArgs e)
        {
                int r = e.RowIndex;
            try
            {
                
                cbxNCC.Text = dgvKho.Rows[r].Cells[0].Value.ToString();
                cbxDT.Text = dgvKho.Rows[r].Cells[1].Value.ToString();
                tbxSoLuong.Text = dgvKho.Rows[r].Cells[2].Value.ToString();
                tbxGia.Text = dgvKho.Rows[r].Cells[3].Value.ToString();
            }
            catch (Exception)
            {
                r = 1;
            }

        }

        private void btnNhapKho_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbxSoLuong.Text.Trim()))
            {
                errorProvider1.SetError(tbxSoLuong, "Không để trông");
                return;
            }
            if (string.IsNullOrEmpty(tbxGia.Text.Trim()))
            {
                errorProvider2.SetError(tbxGia, "Không để trông");
                return;
            }
            int maNCC = Convert.ToInt32( cbxNCC.SelectedValue);
            int maDT =  Convert.ToInt32( cbxDT.SelectedValue);

            NhaCungCap ncc = CSDL.NhaCungCaps.Find(maNCC);
            DienThoai dt = CSDL.DienThoais.Find(maDT);
                
            if(CSDL.KhoHangs.Find(maNCC, maDT) == null)
            {

                KhoHang kh = new KhoHang()
                {
                    MaNCC = maNCC,
                    MaDT = maDT,
                    SoLuongCon = Convert.ToInt32(tbxSoLuong.Text),
                    GiaNhap = Convert.ToDecimal(tbxGia.Text),
                    NgayNhap = DateTime.Today

                };

                CSDL.KhoHangs.Add(kh);
            }
            else
            {
                KhoHang k = CSDL.KhoHangs.Find(maNCC, maDT);
                k.SoLuongCon += Convert.ToInt32(tbxSoLuong.Text);
            }
            CSDL.SaveChanges();
            hienThi();

        }

        private void btnDTMoi_Click(object sender, EventArgs e)
        {
            //hiển thị form tạo điện thoại mới
            fDienThoai dt = new fDienThoai();
            dt.Width = 600;
            dt.Height = 900;
            dt.WindowState = FormWindowState.Normal;
            dt.StartPosition = FormStartPosition.CenterScreen;
            dt.ShowDialog();

            cbxDT.DataSource = CSDL.DienThoais.ToList();
            cbxDT.DisplayMember = "TenDT";
            cbxDT.ValueMember = "MaDT";
            //combobox trỏ vào điện thoại vừa thêm
            cbxDT.SelectedValue = dt.m;

        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
                //cập nhật kho hàng dựa vào MaNCC, MaDT
                int maNCC = Convert.ToInt32(cbxNCC.SelectedValue);

                int maDT = Convert.ToInt32(cbxDT.SelectedValue);

                KhoHang kh = CSDL.KhoHangs.Find(maNCC, maDT);

                kh.SoLuongCon = Convert.ToInt32(tbxSoLuong.Text);
                kh.GiaNhap = Convert.ToDecimal(tbxGia.Text);

                CSDL.SaveChanges();
                hienThi();
            
        }

        private void btnNCCMoi_Click(object sender, EventArgs e)
        {
            //hiển thị form thêm nhà cung cấp mới
            fNhaCungCap ncc = new fNhaCungCap();

            ncc.Width = 600;
            ncc.Height = 900;
            ncc.WindowState = FormWindowState.Normal;
            ncc.StartPosition = FormStartPosition.CenterScreen;
            ncc.ShowDialog();

            cbxNCC.DataSource = CSDL.NhaCungCaps.ToList();
            cbxNCC.DisplayMember = "TenNCC";
            cbxNCC.ValueMember = "MaNCC";
            //combobox trỏ vào nhà cung cấp vừa tạo
            cbxNCC.SelectedValue = ncc.m;

        }

        private void cbxNCC_Click(object sender, EventArgs e)
        {
            cbxNCC.ForeColor = Color.Black;
        }
        private void cbxDT_Click(object sender, EventArgs e)
        {
            cbxDT.ForeColor = Color.Black;
        }
    }
}
