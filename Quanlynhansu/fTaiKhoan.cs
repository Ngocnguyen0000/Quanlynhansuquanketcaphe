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
using MyApp;
using Quanlynhansuquancaphe;
using WindowsFormsApp1;

namespace TAIKHOAN
{
    public partial class fTaiKhoan : Form
    {
        string sCon = @"Data Source=DESKTOP-3PUL2R2;Initial Catalog=QL_NHANSUQUANCAPHE;Integrated Security=True;Trust Server Certificate=True";
        public fTaiKhoan()
        {
            InitializeComponent();
        }



        private void Form1_Load(object sender, EventArgs e)
        {

            // Mở kết nối
            using (SqlConnection con = new SqlConnection(sCon))
            {
                try
                {
                    con.Open();
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("Lỗi SQL: " + sqlEx.Message);
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xảy ra lỗi trong quá trình kết nối DB: " + ex.Message);
                    return;
                }

                // Query để lấy dữ liệu
                string query = "SELECT * FROM TAIKHOAN";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, con);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                // Cập nhật DataGridView
                dataGridView1.DataSource = dataTable;
            }

        }

        private void ClearFields()
        {
            txtmanv.Enabled = true;


            txtmanv.Text= string.Empty;
            txtmk.Text= string.Empty;
            //Đặt con trỏ về TextBox đầu tiên để người dùng dễ nhập liệu tiếp theo
            txtmanv.Focus();
        }


        private void button1_Click(object sender, EventArgs e)
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

                string sMaNV = txtmanv.Text.Trim();
                string sMatkhau = txtmk.Text.Trim();

                // Kiểm tra các trường không để trống
                if (string.IsNullOrEmpty(sMaNV) || string.IsNullOrEmpty(sMatkhau))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ mã nhân viên và mật khẩu!");
                    return;
                }




                // Câu lệnh thêm dữ liệu
                string Squery = "INSERT INTO TAIKHOAN (MANV, MATKHAU) VALUES (@MANV, @MATKHAU)";
                using (SqlCommand cmd = new SqlCommand(Squery, con))
                {
                    cmd.Parameters.AddWithValue("@MANV", sMaNV);
                    cmd.Parameters.AddWithValue("@MATKHAU", sMatkhau);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Thêm mới thành công!");

                        // Làm mới dữ liệu trên giao diện
                        Form1_Load(sender, e);

                        // Làm trống các trường nhập liệu
                        ClearFields();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Xảy ra lỗi trong quá trình thêm mới: " + ex.Message);
                    }
                }
            }
        }






        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Kiểm tra để tránh lỗi khi nhấn vào tiêu đề cột
            {
                if (e.RowIndex >= 0)
                {
                    // Lấy giá trị của các cột trong dòng đó
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                    // Điền vào các trường nhập liệu
                    txtmanv.Text = row.Cells["MANV"].Value.ToString();
                    txtmk.Text = row.Cells["MATKHAU"].Value.ToString(); // Nếu cần hiển thị mật khẩu (nên ẩn mật khẩu trong trường hợp thực tế)

                    txtmanv.Enabled = false;
                }
            }
        }



        private void button3_Click(object sender, EventArgs e)
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

                string sMaNV = txtmanv.Text.Trim();
                string sMatkhau = txtmk.Text.Trim();

                // Kiểm tra các trường không để trống
                if (string.IsNullOrEmpty(sMatkhau))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ mã nhân viên và mật khẩu!");
                    return;
                }

                // Kiểm tra định dạng của mật khẩu
                if (!System.Text.RegularExpressions.Regex.IsMatch(sMatkhau, @"^[a-zA-Z0-9]+$"))
                {
                    MessageBox.Show("Mật khẩu không hợp lệ. Vui lòng chỉ nhập chữ và số!");
                    return;
                }


                // Câu lệnh cập nhật
                string Squery = "UPDATE TAIKHOAN SET MATKHAU = @MATKHAU WHERE MANV = @MANV";
                using (SqlCommand cmd = new SqlCommand(Squery, con))
                {
                    cmd.Parameters.AddWithValue("@MANV", sMaNV);
                    cmd.Parameters.AddWithValue("@MATKHAU", sMatkhau);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Cập nhật thành công!");

                        // Làm mới dữ liệu trên giao diện
                        Form1_Load(sender, e);

                        // Làm trống các trường nhập liệu
                        ClearFields();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Xảy ra lỗi trong quá trình cập nhật: " + ex.Message);
                    }
                }
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult ret = MessageBox.Show("Bạn có chắc chắn muốn xóa tài khoản này không?", "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (ret == DialogResult.OK)
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

                    string sMaNV = txtmanv.Text.Trim();
                    string sQuery = "DELETE FROM TAIKHOAN WHERE MANV = @MANV";
                    using (SqlCommand cmd = new SqlCommand(sQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@MANV", sMaNV);

                        try
                        {
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Xóa tài khoản thành công!");

                            // Làm mới dữ liệu trên giao diện
                            Form1_Load(sender, e);

                            // Làm trống các trường nhập liệu
                            ClearFields();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Xảy ra lỗi trong quá trình xóa tài khoản: " + ex.Message);
                        }
                    }
                }
            }
        }



        private void fTaiKhoan_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (MessageBox.Show("Bạn có thực sự muốn thoát khỏi Quản lý tài khoản?", "Thông báo!", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }





        private void quảnLýTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
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
