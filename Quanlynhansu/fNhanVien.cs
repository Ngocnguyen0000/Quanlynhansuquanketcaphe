using Quanlynhansu;
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
using Microsoft.Data.SqlClient;
using Quanlynhansuquancaphe;
using TAIKHOAN;
using WindowsFormsApp1;
namespace MyApp
{
    public partial class fNhanVien : Form
    {
        string sCon = "Data Source=DESKTOP-3PUL2R2;Initial Catalog=QL_NHANSUQUANCAPHE;Integrated Security=True;Trust Server Certificate=True";
        public fNhanVien()
        {
            InitializeComponent();
        }
        private void NHANVIEN_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (MessageBox.Show("Bạn có thực sự muốn thoát khỏi Quản lý nhân viên?", "Thông báo!", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }
        private void LuuTTNV_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(sCon))
            {
                try
                {
                    con.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xảy ra lỗi trong quá trình kết nối DB: " + ex.Message);
                    return;
                }

                string sMANV = txtMANV.Text.Trim();
                string sTENNV = txtTENNV.Text.Trim();
                string sNgaySinh = dateNGAYSINHNV.Value.ToString("yyyy-MM-dd");
                string sDIACHI = txtDIACHI.Text.Trim();
                string sSDT = txtSDT.Text.Trim();
                string sCCCD = txtCCCD.Text.Trim();
                string sCHUCVU = PHACHE.Checked ? "Pha che" : PHUCVU.Checked ? "Phuc vu" : "";


                // Kiểm tra nếu mã nhân viên đã tồn tại
                string checkQuery = "SELECT COUNT(*) FROM NHANVIEN WHERE MANV = @MANV";
                using (SqlCommand checkCmd = new SqlCommand(checkQuery, con))
                {
                    checkCmd.Parameters.AddWithValue("@MANV", sMANV);

                    try
                    {
                        int exists = (int)checkCmd.ExecuteScalar();
                        if (exists > 0)
                        {
                            MessageBox.Show("Mã nhân viên đã tồn tại! Vui lòng nhập mã khác.");

                            // Reset các trường nhập liệu
                            ResetFields();
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Xảy ra lỗi khi kiểm tra dữ liệu: " + ex.Message);
                        return;
                    }
                }

                // Thêm dữ liệu nếu mọi thứ hợp lệ
                string sQuery = "INSERT INTO NHANVIEN (MANV, TENNV, NGAYSINH, DIACHI, SDT, CCCD, CHUCVU) " +
                                "VALUES (@MANV, @TENNV, @NGAYSINH, @DIACHI, @SDT, @CCCD, @CHUCVU)";
                using (SqlCommand cmd = new SqlCommand(sQuery, con))
                {
                    cmd.Parameters.AddWithValue("@MANV", sMANV);
                    cmd.Parameters.AddWithValue("@TENNV", sTENNV);
                    cmd.Parameters.AddWithValue("@NGAYSINH", sNgaySinh);
                    cmd.Parameters.AddWithValue("@DIACHI", sDIACHI);
                    cmd.Parameters.AddWithValue("@SDT", sSDT);
                    cmd.Parameters.AddWithValue("@CCCD", sCCCD);
                    cmd.Parameters.AddWithValue("@CHUCVU", sCHUCVU);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Thêm thông tin nhân viên thành công!");
                        NHANVIEN_Load(sender, e);
                        ResetFields();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Xảy ra lỗi trong quá trình thêm thông tin nhân viên: " + ex.Message);
                    }
                }
            }
        }



        // Phương thức reset các trường nhập liệu
        private void ResetFields()
        {
            txtMANV.Clear();
            txtMANV.Enabled = true;
            txtTENNV.Clear();
            dateNGAYSINHNV.Value = DateTime.Now;
            txtDIACHI.Clear();
            txtSDT.Clear();
            txtCCCD.Clear();
            PHACHE.Checked = false;
            PHUCVU.Checked = false;
            txtMANV.Focus();
        }




        private void NHANVIEN_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(sCon);
            try
            {
                con.Open();
                string sQuery = "SELECT * FROM NHANVIEN";
                SqlDataAdapter adapter = new SqlDataAdapter(sQuery, con);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "Nhanvien");
                dataGridView1.DataSource = ds.Tables["Nhanvien"];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xảy ra lỗi khi tải dữ liệu: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMANV.Text = dataGridView1.Rows[e.RowIndex].Cells["MANV"].Value.ToString();
            txtTENNV.Text = dataGridView1.Rows[e.RowIndex].Cells["TENNV"].Value.ToString();
            dateNGAYSINHNV.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells["NGAYSINH"].Value);
            txtDIACHI.Text = dataGridView1.Rows[e.RowIndex].Cells["DIACHI"].Value.ToString();
            txtSDT.Text = dataGridView1.Rows[e.RowIndex].Cells["SDT"].Value.ToString();
            txtCCCD.Text = dataGridView1.Rows[e.RowIndex].Cells["CCCD"].Value.ToString();
            string txtCHUCVU = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["CHUCVU"].Value);
            if (txtCHUCVU == "Pha che")
            {
                PHACHE.Checked = true;
            }
            else
            {
                PHUCVU.Checked = true;
            }
            txtMANV.Enabled = false;



        }

        private void SuaTTNV_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(sCon);
            try
            {
                con.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xảy ra lỗi trong quá trình kết nối DB");
                return;
            }

            string sMANV = txtMANV.Text;
            string sTENNV = txtTENNV.Text;
            string sNgaySinh = dateNGAYSINHNV.Value.ToString("yyyy-MM-dd");
            string sDIACHI = txtDIACHI.Text;
            string sSDT = txtSDT.Text;
            string sCCCD = txtCCCD.Text;
            string sCHUCVU = PHACHE.Checked ? "Pha che" : "Phuc vu";

            string sQuery = "UPDATE NHANVIEN SET TENNV=@TENNV, NGAYSINH=@NGAYSINH, " +
                            "DIACHI=@DIACHI, SDT=@SDT, CCCD=@CCCD, CHUCVU=@CHUCVU WHERE MANV=@MANV";

            SqlCommand cmd = new SqlCommand(sQuery, con);
            cmd.Parameters.AddWithValue("@MANV", sMANV);
            cmd.Parameters.AddWithValue("@TENNV", sTENNV);
            cmd.Parameters.AddWithValue("@NGAYSINH", sNgaySinh);
            cmd.Parameters.AddWithValue("@DIACHI", sDIACHI);
            cmd.Parameters.AddWithValue("@SDT", sSDT);
            cmd.Parameters.AddWithValue("@CCCD", sCCCD);
            cmd.Parameters.AddWithValue("@CHUCVU", sCHUCVU);

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Sửa thông tin nhân viên thành công!");
                NHANVIEN_Load(sender, e);
                ResetFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xảy ra lỗi trong quá trình sửa thông tin nhân viên: " + ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        private void XoaTTNV_Click(object sender, EventArgs e)
        {
            DialogResult ret = MessageBox.Show("Bạn có chắc chắn xóa thông tin nhân viên này không?", "Thông báo", MessageBoxButtons.OKCancel);
            if (ret == DialogResult.OK)
            {
                SqlConnection con = new SqlConnection(sCon);
                try
                {
                    con.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xảy ra lỗi trong quá trình kết nối DB");
                }
                string sMANV = txtMANV.Text;
                string sQuery = "delete CHAMCONG where MANV=@MANV;" +
                    "delete BANGLUONG where MANV=@MANV; " +
                    "delete LICHLAM where MANV=@MANV;" +
                    "delete TAIKHOAN where MANV=@MANV;" +
                    "delete NHANVIEN where MANV=@MANV;";

                SqlCommand cmd = new SqlCommand(sQuery, con);
                cmd.Parameters.AddWithValue("@MANV", sMANV);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Xóa thông tin nhân viên thành công!");
                    NHANVIEN_Load(sender, e);
                    ResetFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xảy ra lỗi trong quá trình xóa thông tin nhân viên!");
                }

                con.Close();

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
                MessageBox.Show("Lỗi khi mở form fTaiKhoan: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Show(); // Hiển thị lại form chính
            }
        }

        private void quảnLýNhânViênToolStripMenuItem_Click(object sender, EventArgs e)
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
                MessageBox.Show("Lỗi khi mở form fTaiKhoan: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Show(); // Hiển thị lại form chính
            }
        }

        private void quảnLýCaLàmToolStripMenuItem_Click(object sender, EventArgs e)
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
                MessageBox.Show("Lỗi khi mở form fTaiKhoan: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Lỗi khi mở form fTaiKhoan: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Lỗi khi mở form fTaiKhoan: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
