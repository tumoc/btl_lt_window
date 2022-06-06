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
    public partial class fBanChay : Form
    {
        QLCuaHangDTEntities CSDL = new QLCuaHangDTEntities();
        public fBanChay()
        {
            InitializeComponent();
        }

        private void fBanChay_Load(object sender, EventArgs e)
        {
            cbxTop.Items.Add(5);
            cbxTop.Items.Add(10);
            cbxTop.Items.Add(15);
            cbxTop.Items.Add(20);
            hienThi();
        }
        void hienThi()
        {
            int top = 0;
            try
            {
                int t = Convert.ToInt32(cbxTop.Text);
                top = t;
            }
            catch (Exception)
            {
            }

            var kq = (from k in CSDL.DienThoais
                      let tongsl = (from h in CSDL.ChiTietHDs
                               join dt in CSDL.DienThoais
                               on h.MaDT equals dt.MaDT
                               where h.MaDT == k.MaDT
                               select h.SoLuong).Sum()                    
                      orderby tongsl descending
                      select new
                      {

                         k.TenDT,
                         tongsl
                     }).Take(top).Where(t => t.tongsl > 0);

            dgvBanChay.DataSource = kq.ToList();
            dgvBanChay.Columns[0].HeaderText = "Sản Phẩm";
            dgvBanChay.Columns[1].HeaderText = "Số Lượng";            
        }

        private void cbxTop_SelectedIndexChanged(object sender, EventArgs e)
        {
            hienThi();
        }

        private void cbxTop_SelectedValueChanged(object sender, EventArgs e)
        {
            hienThi();
        }

        private void cbxTop_Enter(object sender, EventArgs e)
        {
            hienThi();
        }

        private void cbxTop_TextUpdate(object sender, EventArgs e)
        {
            hienThi();
        }
    }
}
