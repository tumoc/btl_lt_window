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
    public partial class fThemHD : Form
    {

        public fThemHD()
        {
            InitializeComponent();
        }
        QLCuaHangDTEntities CSDL = new QLCuaHangDTEntities();

        private void frmChiTietHD_Load(object sender, EventArgs e)
        {
            tbxGiamGia.Text = "0";
            List<NhanVien> dsnv = CSDL.NhanViens.ToList();
            cbxNV.DataSource = dsnv;
            cbxNV.DisplayMember = "HoTenNV";
            cbxNV.ValueMember = "MaNV";
            loadDT();
        }       
        
        //Khởi tạo MaHD, MaKH giả
        public int mahd = -1;
        public int makh = -1;
        int madt = -1;

        //khi click thêm điện thoại thì đồng thời tạo ra 1 khách hàng => hóa đơn => chi tiết hóa đơn
        private void btnThemDT_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbxTen.Text.Trim()))
            {
                errorProvider1.SetError(tbxTen, "Không để trống");
                return;
            }
            if (string.IsNullOrEmpty(tbxDC.Text.Trim()))
            {
                errorProvider2.SetError(tbxDC, "Không để trống");
                return;
            }
            if (string.IsNullOrEmpty(tbxDt.Text.Trim()))
            {
                errorProvider3.SetError(tbxDt, "Không để trống");
                return;
            }
            if (string.IsNullOrEmpty(tbxEmail.Text.Trim()))
            {
                errorProvider4.SetError(tbxEmail, "Không để trống");
                return;
            }
            if (string.IsNullOrEmpty(tbxSl.Text.Trim()))
            {
                errorProvider5.SetError(tbxSl, "Không để trống");
                return;
            }
            //click thêm điện thoại nhiều lần nhưng chỉ tạo ra 1 khách.
            if (makh < 1)
            {
                KhachHang kh = new KhachHang()
                {
                    TenKH = tbxTen.Text,
                    DCKH = tbxDC.Text,
                    SoDTKH = tbxDt.Text,
                    EmailKH = tbxEmail.Text
                };
                CSDL.KhachHangs.Add(kh);
                CSDL.SaveChanges();
                makh = kh.MaKH;

            }

            //tạo ra 1 hóa đơn tương ứng với khách vừa tạo ở trên
                if (mahd < 1)
                {
                    HoaDon hd = new HoaDon()
                    {
                        MaKH = makh,
                        MaNV = Convert.ToInt16(cbxNV.SelectedValue),
                        NgayLap =DateTime.Parse(dateTimePicker1.Value.ToShortDateString()),
                        GiamGia = Convert.ToInt32(tbxGiamGia.Text)
                    };
                    CSDL.HoaDons.Add(hd);
                    CSDL.SaveChanges();
                    mahd = hd.MaHD;
                    lblHD.Text = "HÓA ĐƠN: " + hd.MaHD.ToString();

            }

            //thêm nhiều điện thoại vào hóa đơn (bảng chi tiết hóa đơn)
            int madt = Convert.ToInt16(cbxDt.SelectedValue); //lấy mã điện thoại ở combobox

            //tìm xem ChiTietHoaDon có trong CSDL chưa , nếu chưa có => thêm mới
            if (CSDL.ChiTietHDs.Find(mahd, madt) == null)
            {
                ChiTietHD dt = new ChiTietHD()
                {
                    MaHD = mahd,
                    MaDT = Convert.ToInt32(cbxDt.SelectedValue),
                    SoLuong = Convert.ToInt32(tbxSl.Text),
                };

                CSDL.ChiTietHDs.Add(dt);
                CSDL.SaveChanges();
                loadDT();
            }
            //nếu đã tồn tại => tăng số lượng
            else
            {
                ChiTietHD c = CSDL.ChiTietHDs.Find(mahd, madt);
                c.SoLuong += Convert.ToInt16(tbxSl.Text);
                CSDL.SaveChanges();
                loadDT();
            }

            //tạo các ChiTietHD để hiển thị dữ liệu vào datagridview
            var kq = CSDL.ChiTietHDs.
                Where(h => h.MaHD == mahd).
                Select(p => new
                {
                    dt = p.DienThoai.TenDT,
                    sl = p.SoLuong,
                    gia = p.DienThoai.GiaBan,
                    thanhTien = p.DienThoai.GiaBan * p.SoLuong,
                    bh = p.DienThoai.BaoHanh,
                    mdt = p.DienThoai.MaDT
                });

            //tính tổng tiền hóa đơn
            decimal gg = Convert.ToDecimal(tbxGiamGia.Text);
            decimal tongtien = 0;
            foreach (var kx in kq)
                tongtien += Convert.ToDecimal(kx.thanhTien);

            lblGiamGia.Text = (tongtien * (gg / 100)).ToString() + "VNĐ";
            tongtien *= (100 - gg) / 100;

            lblTongT.Text = tongtien.ToString() + "VNĐ";


            dgvCTHD.DataSource = kq.ToList();
            loadDT();
            setHeader();
        }
        //tạo hóa đơn mới
        private void btnThem_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Lưu Hóa Đơn Hiện Tại & Tạo Hóa Đơn Mới ?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if(d == DialogResult.Yes)
            {
                tbxDC.Clear();
                tbxDt.Clear();
                tbxEmail.Clear();
                tbxTen.Clear();

                tbxTen.Focus();
                dgvCTHD.DataSource = null;
                mahd = -1;
                makh = 1;
            }

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Xác Nhận Lưu ?", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
            if (d == DialogResult.OK)
                MessageBox.Show("Thành Công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Xác Nhận Sửa?", "Cảnh Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if(d == DialogResult.Yes)
            {
                KhachHang k = CSDL.KhachHangs.Find(makh);
                if(k != null)
                {
                    k.EmailKH = tbxDC.Text;
                    k.DCKH = tbxEmail.Text;
                    k.SoDTKH = tbxDC.Text;
                    k.TenKH = tbxTen.Text;
                }

                HoaDon h = CSDL.HoaDons.Find(mahd);
                if(h != null)
                {
                    h.MaNV = Convert.ToInt16(cbxNV.SelectedValue);
                    h.GiamGia = Convert.ToInt16(tbxGiamGia.Text);
                }

                try
                {
                    ChiTietHD ct = CSDL.ChiTietHDs.Find(mahd, madt);
                    ct.SoLuong = Convert.ToInt16(tbxSl.Text);
                }
                catch (Exception)
                {

                }


                CSDL.SaveChanges();
                var kq = CSDL.ChiTietHDs.
                    Where(hd => hd.MaHD == mahd).
                    Select(p => new
                    {
                        dt = p.DienThoai.TenDT,
                        sl = p.SoLuong,
                        gia = p.DienThoai.GiaBan,
                        thanhTien = p.DienThoai.GiaBan * p.SoLuong,
                        bh = p.DienThoai.BaoHanh,
                        mdt = p.DienThoai.MaDT
                    });
                decimal gg = Convert.ToDecimal(tbxGiamGia.Text);
                decimal tongtien = 0;
                foreach (var kx in kq)
                    tongtien += Convert.ToDecimal( kx.thanhTien);

                lblGiamGia.Text = (tongtien * (gg / 100)).ToString() +"VNĐ";
                tongtien *= (100 - gg) / 100;


                lblTongT.Text = tongtien.ToString() +"VNĐ";

                dgvCTHD.DataSource = kq.ToList();
                loadDT();
                setHeader();
                MessageBox.Show("Thành Công", "Thông Báo");
            }


        }

        private void dgvCTHD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            cbxDt.Text = dgvCTHD.Rows[r].Cells[0].Value.ToString();
            tbxSl.Text = dgvCTHD.Rows[r].Cells[1].Value.ToString();
            madt =Convert.ToInt16( dgvCTHD.Rows[r].Cells[5].Value.ToString());

        }

        //hàm load lại số lượng điện thoại => hiển thị lên combobox số lượng
        //còn trong kho khi thêm/ cập nhật điện thoại vào hóa đơn
        private void loadDT()
        {
            var dsdt = CSDL.KhoHangs.
                Where(p => p.SoLuongCon > 0).
                Select(p => new
                {
                    maDT = p.DienThoai.MaDT,
                    tenDT = p.DienThoai.TenDT + "(" + p.NhaCungCap.TenNCC + "-còn " + p.SoLuongCon + ")",
                });
            cbxDt.DataSource = dsdt.ToList();
            cbxDt.DisplayMember = "tenDT";
            cbxDt.ValueMember = "maDT";
        }

        private void setHeader()
        {
            dgvCTHD.Columns[0].HeaderText = "Sản Phẩm";
            dgvCTHD.Columns[1].HeaderText = "Số Lượng";
            dgvCTHD.Columns[2].HeaderText = "Đơn Giá";
            dgvCTHD.Columns[3].HeaderText = "Thành Tiền";
            dgvCTHD.Columns[4].HeaderText = "Bảo Hành";
            dgvCTHD.Columns[5].Width = 0;
            dgvCTHD.Columns[1].Width = 70;
        }


    }
}
