using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections;
using System.Data.Common;
using Quanlynhansu;
using Quanlynhansuquancaphe;
using TAIKHOAN;
using WindowsFormsApp1;

namespace MyApp
{
    public partial class fCaLam : Form
    {
        private string sCon = "Data Source=DESKTOP-3PUL2R2;Initial Catalog=QL_NHANSUQUANCAPHE;Integrated Security=True;Trust Server Certificate=True";

        public fCaLam()
        {
            InitializeComponent();
        }

        private void CALAM_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có thực sự muốn thoát khỏi Quản lý ca làm?", "Thông báo!", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void THEM_CALAM_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMACA_CALAM.Text) ||
                (!Sang.Checked && !Chieu.Checked && !Toi.Checked))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin trước khi thêm!");
                return;
            }

            string sMACA = txtMACA_CALAM.Text.Trim();
            string sNGAY_LV = dateNGAY_LV.Value.ToString("yyyy-MM-dd");
            string sBUOI_LAM = Sang.Checked ? "Sáng" : Chieu.Checked ? "Chiều" : "Tối";

            TimeSpan gioBatDau;
            TimeSpan gioKetThuc;

            // Xác định giờ bắt đầu và giờ kết thúc dựa trên buổi làm
            if (Sang.Checked)
            {
                gioBatDau = new TimeSpan(6, 0, 0);
                gioKetThuc = new TimeSpan(11, 0, 0);
            }
            else if (Chieu.Checked)
            {
                gioBatDau = new TimeSpan(12, 0, 0);
                gioKetThuc = new TimeSpan(17, 0, 0);
            }
            else // Tối
            {
                gioBatDau = new TimeSpan(18, 0, 0);
                gioKetThuc = new TimeSpan(22, 0, 0);
            }

            double soGioLamViec = (gioKetThuc - gioBatDau).TotalHours;

            try
            {
                using (SqlConnection con = new SqlConnection(sCon))
                {
                    con.Open();
                    string sQuery = "INSERT INTO CALAM (MACA, NGAY_LV, BUOI_LAM, GIO_BATDAU, GIO_KETTHUC, SOGIOLAMVIEC) " +
                                    "VALUES (@MACA, @NGAY_LV, @BUOI_LAM, @GIO_BATDAU, @GIO_KETTHUC, @SOGIOLAMVIEC)";
                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    cmd.Parameters.AddWithValue("@MACA", sMACA);
                    cmd.Parameters.AddWithValue("@NGAY_LV", sNGAY_LV);
                    cmd.Parameters.AddWithValue("@BUOI_LAM", sBUOI_LAM);
                    cmd.Parameters.AddWithValue("@GIO_BATDAU", gioBatDau);
                    cmd.Parameters.AddWithValue("@GIO_KETTHUC", gioKetThuc);
                    cmd.Parameters.AddWithValue("@SOGIOLAMVIEC", soGioLamViec);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm thông tin ca làm thành công!");
                    CALAM_Load(sender, e);
                    ClearInputFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xảy ra lỗi trong quá trình thêm thông tin ca làm: " + ex.Message);
            }
        }

        private void CALAM_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(sCon))
                {
                    con.Open();
                    string query = "SELECT * FROM CALAM";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dataGridView1.DataSource = table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xảy ra lỗi trong quá trình tải dữ liệu: " + ex.Message);
            }
        }

        private void ClearInputFields()
        {
            txtMACA_CALAM.Enabled = true;

            txtMACA_CALAM.Text = string.Empty;
            Sang.Checked = false;
            Chieu.Checked = false;
            Toi.Checked = false;
            textBoxSoGioLam.Text = string.Empty;
            textBoxGioStart.Text = string.Empty;
            textBoxGioEnd.Text = string.Empty;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtMACA_CALAM.Text = dataGridView1.Rows[e.RowIndex].Cells["MACA"].Value.ToString();
                dateNGAY_LV.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells["NGAY_LV"].Value);
                string txtBUOILAM = dataGridView1.Rows[e.RowIndex].Cells["BUOI_LAM"].Value.ToString();
                textBoxSoGioLam.Text = dataGridView1.Rows[e.RowIndex].Cells["SOGIOLAMVIEC"].Value.ToString();
                textBoxGioStart.Text = dataGridView1.Rows[e.RowIndex].Cells["GIO_BATDAU"].Value.ToString();
                textBoxGioEnd.Text = dataGridView1.Rows[e.RowIndex].Cells["GIO_KETTHUC"].Value.ToString();


                Sang.Checked = txtBUOILAM == "Sáng";
                Chieu.Checked = txtBUOILAM == "Chiều";
                Toi.Checked = txtBUOILAM == "Tối";
                txtMACA_CALAM.Enabled = false;
            }
        }

        private void XoaCALAM_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa thông tin ca làm này không?", "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(sCon))
                    {
                        con.Open();
                        string sQuery = "DELETE FROM LICHLAM WHERE MACA = @MACA; DELETE FROM CALAM WHERE MACA = @MACA;";
                        SqlCommand cmd = new SqlCommand(sQuery, con);
                        cmd.Parameters.AddWithValue("@MACA", txtMACA_CALAM.Text.Trim());
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Xóa thông tin ca làm thành công!");
                        CALAM_Load(sender, e);
                        ClearInputFields();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xảy ra lỗi trong quá trình xóa thông tin ca làm: " + ex.Message);
                }
            }
        }

        private void SuaCALAM_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(sCon))
                {
                    con.Open();
                    string sQuery = "UPDATE CALAM SET NGAY_LV = @NGAY_LV, BUOI_LAM = @BUOI_LAM WHERE MACA = @MACA";
                    SqlCommand cmd = new SqlCommand(sQuery, con);
                    cmd.Parameters.AddWithValue("@MACA", txtMACA_CALAM.Text.Trim());
                    cmd.Parameters.AddWithValue("@NGAY_LV", dateNGAY_LV.Value.ToString("yyyy-MM-dd"));
                    string sBUOI_LAM = Sang.Checked ? "Sáng" : Chieu.Checked ? "Chiều" : "Tối";
                    cmd.Parameters.AddWithValue("@BUOI_LAM", sBUOI_LAM);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Sửa thông tin ca làm thành công!");
                    CALAM_Load(sender, e);
                    ClearInputFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xảy ra lỗi trong quá trình sửa thông tin ca làm: " + ex.Message);
            }
        }



        private void quảnLýTàiKhoảnToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                using (fTaiKhoan f = new fTaiKhoan())
                {
                    this.Hide();
                    f.ShowDialog(); // Hiển thị form con (cửa sổ quản lý tài khoản)
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở form " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Show(); // Hiển thị lại form chính
            }
        }

        private void quảnLýNhânViênToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                using (fNhanVien f = new fNhanVien())
                {
                    this.Hide();
                    f.ShowDialog(); // Hiển thị form con (cửa sổ quản lý tài khoản)
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở form " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Show(); // Hiển thị lại form chính
            }
        }

        private void quảnLýCaLàmToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                using (fCaLam f = new fCaLam())
                {
                    this.Hide();
                    f.ShowDialog(); // Hiển thị form con (cửa sổ quản lý tài khoản)
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở form " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Show(); // Hiển thị lại form chính
            }
        }

        private void quảnLýLịchLàmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (fLichLam f = new fLichLam())
                {
                    this.Hide();
                    f.ShowDialog(); // Hiển thị form con (cửa sổ quản lý tài khoản)
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở form " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Show(); // Hiển thị lại form chính
            }
        }

        private void quảnLýLươngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (fBangLuong f = new fBangLuong())
                {
                    this.Hide();
                    f.ShowDialog(); // Hiển thị form con (cửa sổ quản lý tài khoản)
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở form " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Show(); // Hiển thị lại form chính
            }
        }

        private void quảnLýChấmCôngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (fChamCong f = new fChamCong())
                {
                    this.Hide();
                    f.ShowDialog(); // Hiển thị form con (cửa sổ quản lý tài khoản)
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở form " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Show(); // Hiển thị lại form chính
            }
        }
    }
}