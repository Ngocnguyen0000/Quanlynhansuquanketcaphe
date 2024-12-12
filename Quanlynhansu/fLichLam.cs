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
using MyApp;
using Quanlynhansu;
using Quanlynhansuquancaphe;
using TAIKHOAN;


namespace WindowsFormsApp1
{
    public partial class fLichLam : Form
    {
        // Chuỗi kết nối đến cơ sở dữ liệu
        string sCon = "Data Source=DESKTOP-3PUL2R2;Initial Catalog=QL_NHANSUQUANCAPHE;Integrated Security=True;Trust Server Certificate=True";

        public fLichLam()
        {
            InitializeComponent();
        }

        private void fLichLam_Load(object sender, EventArgs e)
        {
            LoadDataToGridView();
        }

        private void fLichLam_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có thực sự muốn thoát khỏi Quản lý lịch làm?", "Thông báo!", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        // Hàm làm trống các trường nhập liệu
        private void ClearInputFields()
        {
            txtMaNhanVien.Enabled = true;
            txtMaCa.Enabled = true;

            txtMaNhanVien.Clear();
            txtMaCa.Clear();
            rbDiLam.Checked = false;
            rbDiTre.Checked = false;
            rbKhongDiLam.Checked = false;
        }

        // Hàm tải dữ liệu vào DataGridView
        private void LoadDataToGridView()
        {
            using (SqlConnection con = new SqlConnection(sCon))
            {
                string query = "SELECT * FROM LICHLAM";
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();

                try
                {
                    con.Open();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xảy ra lỗi trong quá trình tải dữ liệu!\n" + ex.Message);
                }
            }
        }

        // Hàm thêm mới dữ liệu
        private void btnLuu_Click_1(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(txtMaNhanVien.Text) || string.IsNullOrWhiteSpace(txtMaCa.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã Nhân Viên và Mã Ca.");
                return;
            }

            // Chuẩn bị dữ liệu
            string sMaNhanVien = txtMaNhanVien.Text.Trim();
            string sMaCa = txtMaCa.Text.Trim();
            int iTrangThai = 0;

            if (rbDiLam.Checked)
            {
                iTrangThai = 1;
            }
            else if (rbDiTre.Checked)
            {
                iTrangThai = 2;
            }
            else if (rbKhongDiLam.Checked)
            {
                iTrangThai = 3;
            }

            string sQuery = "INSERT INTO LICHLAM (MANV, MACA, TRANGTHAI) VALUES (@MANV, @MACA, @TRANGTHAI)";

            using (SqlConnection con = new SqlConnection(sCon))
            {
                SqlCommand cmd = new SqlCommand(sQuery, con);
                cmd.Parameters.AddWithValue("@MANV", sMaNhanVien);
                cmd.Parameters.AddWithValue("@MACA", sMaCa);
                cmd.Parameters.AddWithValue("@TRANGTHAI", iTrangThai);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm mới thành công!");

                    // Cập nhật dữ liệu vào DataGridView
                    LoadDataToGridView();

                    // Làm trống các trường nhập liệu
                    ClearInputFields();
                }
                catch (SqlException ex)
                {
                    // Kiểm tra lỗi trùng khóa chính
                    if (ex.Number == 2627) // Vi phạm ràng buộc khóa chính
                    {
                        MessageBox.Show("Mã nhân viên đã tồn tại. Vui lòng nhập mã khác.");
                    }
                    else
                    {
                        MessageBox.Show("Xảy ra lỗi trong quá trình thêm mới!\n" + ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xảy ra lỗi trong quá trình thêm mới!\n" + ex.Message);
                }
            }
        }

        // Hàm sửa dữ liệu
        private void btnSua_Click_1(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(txtMaNhanVien.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã Nhân Viên.");
                return;
            }

            // Chuẩn bị dữ liệu
            string sMaNhanVien = txtMaNhanVien.Text.Trim();
            int iTrangThai = 0;

            if (rbDiLam.Checked)
            {
                iTrangThai = 1;
            }
            else if (rbDiTre.Checked)
            {
                iTrangThai = 2;
            }
            else if (rbKhongDiLam.Checked)
            {
                iTrangThai = 3;
            }

            string sQuery = "UPDATE LICHLAM SET TRANGTHAI = @TRANGTHAI WHERE MANV = @MANV";

            using (SqlConnection con = new SqlConnection(sCon))
            {
                SqlCommand cmd = new SqlCommand(sQuery, con);
                cmd.Parameters.Add("@MANV", SqlDbType.VarChar, 50).Value = sMaNhanVien;
                cmd.Parameters.Add("@TRANGTHAI", SqlDbType.Int).Value = iTrangThai;

                try
                {
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Cập nhật thành công!");

                        // Cập nhật dữ liệu vào DataGridView
                        LoadDataToGridView();

                        // Làm trống các trường nhập liệu
                        ClearInputFields();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy Mã Nhân Viên để cập nhật.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xảy ra lỗi trong quá trình cập nhật!\n" + ex.Message);
                }
            }
        }

        // Hàm xóa dữ liệu
        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(txtMaNhanVien.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã Nhân Viên để xóa.");
                return;
            }

            DialogResult ret = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel);
            if (ret != DialogResult.OK)
            {
                return;
            }

            // Chuẩn bị dữ liệu
            string sMaNhanVien = txtMaNhanVien.Text.Trim();

            string sQuery = "DELETE FROM LICHLAM WHERE MANV = @MANV";

            using (SqlConnection con = new SqlConnection(sCon))
            {
                SqlCommand cmd = new SqlCommand(sQuery, con);
                cmd.Parameters.AddWithValue("@MANV", sMaNhanVien);

                try
                {
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Xóa thành công!");

                        // Cập nhật dữ liệu vào DataGridView
                        LoadDataToGridView();

                        // Làm trống các trường nhập liệu
                        ClearInputFields();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy Mã Nhân Viên để xóa.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xảy ra lỗi trong quá trình xóa!\n" + ex.Message);
                }
            }
        }

        // Xử lý sự kiện khi người dùng click vào một ô trong DataGridView
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem người dùng đã click vào dòng hợp lệ chưa
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                if (e.RowIndex >= 0)
                {
                    txtMaNhanVien.Text = row.Cells["MANV"].Value.ToString();
                    txtMaCa.Text = row.Cells["MACA"].Value.ToString();

                    if (row.Cells["TRANGTHAI"].Value != null)
                    {
                        int iTrangThai = Convert.ToInt32(row.Cells["TRANGTHAI"].Value);
                        switch (iTrangThai)
                        {
                            case 1:
                                rbDiLam.Checked = true;
                                break;
                            case 2:
                                rbDiTre.Checked = true;
                                break;
                            case 3:
                                rbKhongDiLam.Checked = true;
                                break;
                            default:
                                rbKhongDiLam.Checked = false;
                                rbDiLam.Checked = false;
                                rbDiTre.Checked = false;
                                break;
                        }
                    }

                    // Vô hiệu hóa trường Mã Nhân Viên để tránh thay đổi
                    txtMaNhanVien.Enabled = false;
                    txtMaCa.Enabled = false;
                }
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
                MessageBox.Show("Lỗi khi mở form " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
