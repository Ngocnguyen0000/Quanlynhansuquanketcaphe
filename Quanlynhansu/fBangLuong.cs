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
using Quanlynhansuquancaphe;
using TAIKHOAN;
using WindowsFormsApp1;



namespace Quanlynhansu
{
    public partial class fBangLuong : Form
    {
        // Chuỗi kết nối đến cơ sở dữ liệu
        string sCon = @"Data Source=DESKTOP-3PUL2R2;Initial Catalog=QL_NHANSUQUANCAPHE;Integrated Security=True;Trust Server Certificate=True";


        public fBangLuong()
        {
            InitializeComponent();

        }

        // Hàm xử lý sao lưu khi Timer tick
        private void BackupTimer_Tick(object sender, EventArgs e)
        {
            BackupDatabase();
        }

        // Hàm thực hiện sao lưu
        private void BackupDatabase()
        {
            using (SqlConnection con = new SqlConnection(sCon))
            {
                try
                {
                    con.Open();

                    // Câu lệnh T-SQL để sao lưu
                    string backupQuery = @"
                BACKUP DATABASE [QL_NHANSU]
                TO DISK = @BackupFilePath
                WITH INIT, FORMAT";

                    // Tạo đường dẫn sao lưu với ngày tháng
                    string backupFilePath = @"C:\Backup\QL_NHANSU_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".bak";

                    // Tạo đối tượng SqlCommand để thực thi câu lệnh T-SQL
                    using (SqlCommand cmd = new SqlCommand(backupQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@BackupFilePath", backupFilePath);

                        // Thực thi câu lệnh SQL
                        cmd.ExecuteNonQuery();

                        // Hiển thị thông báo thành công
                        MessageBox.Show("Backup dữ liệu thành công tại " + backupFilePath);
                    }
                }
                catch (Exception ex)
                {
                    // Hiển thị thông báo lỗi
                    MessageBox.Show("Lỗi trong quá trình sao lưu: " + ex.Message);
                }
            }
        }



        private void fBangLuong_Load(object sender, EventArgs e)
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
                string query = "SELECT MANV, BANGLUONG_DATE, LUONGCOBAN, TONGLUONG FROM BANGLUONG";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, con);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                // Cập nhật DataGridView
                dataGridView1.DataSource = dataTable;
            }
        }

        private void buttonThem_Click(object sender, EventArgs e)
        {
            // Kiểm tra các trường dữ liệu có bị trống không
            if (string.IsNullOrWhiteSpace(textManhanvien.Text))
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên!");
                textManhanvien.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(textLuongCoBan.Text))
            {
                MessageBox.Show("Vui lòng nhập lương cơ bản!");
                textLuongCoBan.Focus();
                return;
            }

            if (dateTimeNgayLuong.Value == null || dateTimeNgayLuong.Value > DateTime.Now)
            {
                MessageBox.Show("Vui lòng chọn ngày lương hợp lệ!");
                dateTimeNgayLuong.Focus();
                return;
            }

            using (SqlConnection con = new SqlConnection(sCon))
            {
                try
                {
                    con.Open();

                    // Kiểm tra dữ liệu đã tồn tại chưa
                    string checkQuery = "SELECT COUNT(*) FROM BANGLUONG WHERE MANV = @MANV AND BANGLUONG_DATE = @BANGLUONG_DATE";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, con))
                    {
                        checkCmd.Parameters.AddWithValue("@MANV", textManhanvien.Text.Trim());
                        checkCmd.Parameters.AddWithValue("@BANGLUONG_DATE", dateTimeNgayLuong.Value.ToString("yyyy-MM-dd"));

                        int exists = (int)checkCmd.ExecuteScalar();
                        if (exists > 0)
                        {
                            MessageBox.Show("Dữ liệu đã tồn tại! Vui lòng nhập dữ liệu mới.");

                            // Reset các trường nhập liệu
                            textManhanvien.Enabled = true;

                            textManhanvien.Clear();
                            textLuongCoBan.Clear();
                            textTongLuong.Clear();
                            dateTimeNgayLuong.Value = DateTime.Now; // Đặt lại ngày hiện tại
                            textManhanvien.Focus(); // Đặt con trỏ vào ô nhập mã nhân viên


                            return;
                        }

                    }

                    // Chuẩn bị dữ liệu thêm mới
                    string sMaNhanVien = textManhanvien.Text.Trim();
                    string sNgayLuong = dateTimeNgayLuong.Value.ToString("yyyy-MM-dd");

                    decimal luongCoBan = 0;
                    decimal tongLuong = 0;

                    if (decimal.TryParse(textLuongCoBan.Text.Trim(), out luongCoBan) && luongCoBan >= 0)
                    {
                        tongLuong = luongCoBan;
                    }
                    else
                    {
                        MessageBox.Show("Lương cơ bản không hợp lệ. Vui lòng nhập số hợp lệ!");
                        return;
                    }

                    // Thêm dữ liệu vào cơ sở dữ liệu
                    string sQuery = "INSERT INTO BANGLUONG (MANV, BANGLUONG_DATE, LUONGCOBAN, TONGLUONG) " +
                                    "VALUES (@MANV, @BANGLUONG_DATE, @LUONGCOBAN, @TONGLUONG)";
                    using (SqlCommand cmd = new SqlCommand(sQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@MANV", sMaNhanVien);
                        cmd.Parameters.AddWithValue("@BANGLUONG_DATE", sNgayLuong);
                        cmd.Parameters.AddWithValue("@LUONGCOBAN", luongCoBan);
                        cmd.Parameters.AddWithValue("@TONGLUONG", tongLuong);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Thêm mới thành công!");

                        // Sau khi xóa, tải lại dữ liệu vào DataGridView

                        fBangLuong_Load(sender, e);  // Gọi lại fBangLuong_Load để làm mới DataGridView

                        // Reset các trường dữ liệu sau khi lưu thành công
                        textManhanvien.Enabled = true;
                        textManhanvien.Clear();
                        textLuongCoBan.Clear();
                        textTongLuong.Clear();
                        dateTimeNgayLuong.Value = DateTime.Now;
                    }

                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("Xảy ra lỗi SQL: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xảy ra lỗi: " + ex.Message);
                }
            }
        }





        private bool isEditing = false;

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Kiểm tra để tránh lỗi khi nhấn vào tiêu đề cột
            {
                if (e.RowIndex >= 0)
                {
                    // Kiểm tra nếu người dùng không nhấp vào hàng đầu tiên (chỉ số dòng e.RowIndex không hợp lệ)


                    isEditing = true;

                    // Lấy mã nhân viên (MANV)
                    textManhanvien.Text = dataGridView1.Rows[e.RowIndex].Cells["MANV"].Value.ToString();

                    // Lấy ngày lương (BANGLUONG_DATE)
                    DateTime sngayLuong = dataGridView1.Rows[e.RowIndex].Cells["BANGLUONG_DATE"].Value != DBNull.Value ?
                                         Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells["BANGLUONG_DATE"].Value) : DateTime.Now;
                    dateTimeNgayLuong.Value = sngayLuong;

                    // Lấy lương cơ bản (LUONGCOBAN)
                    decimal luongCoBan = dataGridView1.Rows[e.RowIndex].Cells["LUONGCOBAN"].Value != DBNull.Value ?
                                         Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells["LUONGCOBAN"].Value) : 0;
                    textLuongCoBan.Text = luongCoBan.ToString();

                    // Lấy tổng lương (TONGLUONG)
                    decimal tongLuong = dataGridView1.Rows[e.RowIndex].Cells["TONGLUONG"].Value != DBNull.Value ?
                                        Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells["TONGLUONG"].Value) : 0;
                    textTongLuong.Text = tongLuong.ToString();
                }
            }

            // Khóa trường MANV khi đang sửa thông tin
            textManhanvien.Enabled = !isEditing;  // Khi chỉnh sửa thì khóa, khi thêm mới thì không khóa

            // Đảm bảo các trường còn lại có thể sửa được
            textLuongCoBan.Enabled = true;
            textTongLuong.Enabled = true;
            dateTimeNgayLuong.Enabled = true;
        }



        private void buttonSua_Click(object sender, EventArgs e)
        {
            // B1: Khởi tạo kết nối 
            using (SqlConnection con = new SqlConnection(sCon))
            {
                try
                {
                    // Mở kết nối
                    con.Open();
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("Lỗi SQL: " + sqlEx.Message);
                    return; // Nếu có lỗi thì thoát khỏi phương thức
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xảy ra lỗi trong quá trình kết nối DB: " + ex.Message);
                    return; // Nếu có lỗi thì thoát khỏi phương thức
                }

                // B2: Chuẩn bị dữ liệu
                string sMaNhanVien = textManhanvien.Text;
                string sNgayLuong = dateTimeNgayLuong.Value.ToString("yyyy-MM-dd");

                decimal luongCoBan = 0;
                decimal tongLuong = 0;

                // Kiểm tra tính hợp lệ của lương cơ bản
                if (decimal.TryParse(textLuongCoBan.Text, out luongCoBan) && luongCoBan >= 0)
                {
                    // Lương cơ bản hợp lệ
                }
                else
                {
                    MessageBox.Show("Lương cơ bản không hợp lệ.");
                    return;
                }

                // Kiểm tra tính hợp lệ của tổng lương
                if (decimal.TryParse(textTongLuong.Text, out tongLuong) && tongLuong >= 0)
                {
                    // Tổng lương hợp lệ
                }
                else
                {
                    MessageBox.Show("Tổng lương không hợp lệ.");
                    return;
                }

                // B3: Cập nhật dữ liệu vào cơ sở dữ liệu
                string sQuery = "UPDATE BANGLUONG " +
                                "SET BANGLUONG_DATE = @BANGLUONG_DATE, " +
                                "LUONGCOBAN = @LUONGCOBAN, " +
                                "TONGLUONG = @TONGLUONG " +
                                "WHERE MANV = @MANV"; // Cập nhật theo MANV

                using (SqlCommand cmd = new SqlCommand(sQuery, con))
                {
                    // Thêm các tham số vào câu lệnh SQL
                    cmd.Parameters.AddWithValue("@MANV", sMaNhanVien);
                    cmd.Parameters.AddWithValue("@BANGLUONG_DATE", sNgayLuong);
                    cmd.Parameters.AddWithValue("@LUONGCOBAN", luongCoBan);
                    cmd.Parameters.AddWithValue("@TONGLUONG", tongLuong);

                    try
                    {
                        // Thực thi câu lệnh SQL
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Cập nhật thành công!");

                        // Sau khi sửa, tải lại dữ liệu vào DataGridView
                        fBangLuong_Load(sender, e);  // Gọi lại fBangLuong_Load để làm mới DataGridView

                        // Reset các trường dữ liệu sau khi lưu thành công
                        textManhanvien.Enabled = true;
                        textManhanvien.Clear();
                        textLuongCoBan.Clear();
                        textTongLuong.Clear();
                        dateTimeNgayLuong.Value = DateTime.Now;
                    }
                    catch (Exception ex)
                    {
                        // Xử lý lỗi nếu có
                        MessageBox.Show("Xảy ra lỗi khi cập nhật dữ liệu: " + ex.Message);
                    }
                }
            }
        }


        private void buttonXoa_Click(object sender, EventArgs e)
        {
            DialogResult ret = MessageBox.Show("Có chắc chắn xóa không?", "Thông báo", MessageBoxButtons.OKCancel);
            if (ret == DialogResult.OK)
            {
                // B1: Khởi tạo kết nối 
                using (SqlConnection con = new SqlConnection(sCon))
                {
                    try
                    {
                        // Mở kết nối
                        con.Open();
                    }
                    catch (SqlException sqlEx)
                    {
                        MessageBox.Show("Lỗi SQL: " + sqlEx.Message);
                        return; // Nếu có lỗi thì thoát khỏi phương thức
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Xảy ra lỗi trong quá trình kết nối DB: " + ex.Message);
                        return; // Nếu có lỗi thì thoát khỏi phương thức
                    }

                    // B2: Kiểm tra xem mã nhân viên có tồn tại trong cơ sở dữ liệu không
                    string sMaNhanVien = textManhanvien.Text;

                    string checkQuery = "SELECT COUNT(*) FROM BANGLUONG WHERE MANV = @MANV";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, con))
                    {
                        checkCmd.Parameters.AddWithValue("@MANV", sMaNhanVien);
                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (count == 0)
                        {
                            MessageBox.Show("Mã nhân viên không tồn tại trong bảng lương.");
                            return; // Nếu không có bản ghi, không thực hiện xóa
                        }
                    }

                    // B3: Câu lệnh DELETE
                    string sQuery = "DELETE FROM BANGLUONG WHERE MANV = @MANV";

                    using (SqlCommand cmd = new SqlCommand(sQuery, con))
                    {
                        // Thêm tham số vào câu lệnh SQL
                        cmd.Parameters.AddWithValue("@MANV", sMaNhanVien);

                        try
                        {
                            // Thực thi câu lệnh SQL
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Xóa thành công!");

                            // Sau khi xóa, tải lại dữ liệu vào DataGridView
                            
                            fBangLuong_Load(sender, e);  // Gọi lại fBangLuong_Load để làm mới DataGridView

                            // Reset các trường dữ liệu sau khi lưu thành công
                            textManhanvien.Enabled = true;
                            textManhanvien.Clear();
                            textLuongCoBan.Clear();
                            textTongLuong.Clear();
                            dateTimeNgayLuong.Value = DateTime.Now;
                        }
                        catch (Exception ex)
                        {
                            // Xử lý lỗi nếu có
                            MessageBox.Show("Xảy ra lỗi khi xóa dữ liệu: " + ex.Message);
                        }
                    }
                }
            }
        }





        private void fBangLuong_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có thực sự muốn thoát khỏi Quản lý bảng lương?", "Thông báo!", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }

        }


        //Tính phần thưởng
        private void buttonTinhThuong_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(sCon))
            {
                try
                {
                    // Mở kết nối đến cơ sở dữ liệu
                    con.Open();

                    // Thực thi thủ tục SP_THUONG_HIEUQUA
                    using (SqlCommand cmd = new SqlCommand("SP_THUONG_HIEUQUA", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Thực thi thủ tục và nhận kết quả trả về
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        dataAdapter.Fill(dt);

                        // Kiểm tra xem có dữ liệu hay không
                        if (dt.Rows.Count > 0)
                        {
                            // Gán kết quả vào DataGridView để hiển thị
                            dataGridView1.DataSource = dt;
                        }
                        else
                        {
                            MessageBox.Show("Không có dữ liệu để hiển thị.");
                        }
                    }

                    MessageBox.Show("Tính thưởng thành công!");
                }
                catch (Exception ex)
                {
                    // Hiển thị lỗi nếu có
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
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





