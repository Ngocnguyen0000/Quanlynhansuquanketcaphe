using Microsoft.Data.SqlClient;
using MyApp;
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
using TAIKHOAN;
using WindowsFormsApp1;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Quanlynhansuquancaphe
{
    public partial class fChamCong : Form
    {
        string sCon = @"Data Source=DESKTOP-3PUL2R2;Initial Catalog=QL_NHANSUQUANCAPHE;Integrated Security=True;Trust Server Certificate=True";

        public fChamCong()
        {
            InitializeComponent();
        }

        private void fChamCong_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(sCon))
                {
                    con.Open();
                    string query = "SELECT * FROM CHAMCONG";
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

        private void fChamCong_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có thực sự muốn thoát khỏi Quản lý chấm công?", "Thông báo!", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }


        // Hàm làm trống các trường nhập liệu
        private void ClearInputFields()
        {
            // Kích hoạt lại TextBox cho Mã Nhân Viên và làm trống nó
            textManhanvien.Enabled = true;
            textManhanvien.Clear();

            textBox1.Text = string.Empty;
            textPhat.Text = string.Empty;
            textThuong.Text = string.Empty;

            // Thiết lập lại DateTimePicker về ngày hiện tại
            dateTimeNgayLuong.Value = DateTime.Now;

            // Đặt lại giá trị của các TextBox thành "0"
            textBox1.Text = "0";
            textPhat.Text = "0";
            textThuong.Text = "0";

            // Bỏ chọn tất cả các dòng trong DataGridView
            dataGridView1.ClearSelection();
        }



        private void buttonThem_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(textManhanvien.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã Nhân Viên.");
                return;
            }

            string sMaNV = textManhanvien.Text.Trim();
            DateTime chamCongDate = dateTimeNgayLuong.Value.Date;

            // Khởi tạo biến với giá trị mặc định
            int tongCaChamCong = 0;
            decimal thuong = 0m;
            decimal phat = 0m;

            // Chuyển đổi và kiểm tra giá trị từ TextBox1
            if (!int.TryParse(textBox1.Text.Trim(), out tongCaChamCong) || tongCaChamCong < 0)
            {
                MessageBox.Show("Vui lòng nhập Tổng Ca Chấm Công là số nguyên lớn hơn hoặc bằng 0.");
                return;
            }

            // Chuyển đổi và kiểm tra giá trị từ textThuong
            if (!decimal.TryParse(textThuong.Text.Trim(), out thuong) || thuong < 0)
            {
                MessageBox.Show("Vui lòng nhập Tiền Thưởng là số lớn hơn hoặc bằng 0.");
                return;
            }

            // Chuyển đổi và kiểm tra giá trị từ textPhat
            if (!decimal.TryParse(textPhat.Text.Trim(), out phat) || phat > 0)
            {
                MessageBox.Show("Vui lòng nhập Tiền Phạt là số nhỏ hơn hoặc bằng 0.");
                return;
            }

            // Câu lệnh SQL INSERT
            string sQuery = "INSERT INTO CHAMCONG (MANV, CHAMCONG_DATE, TONGSOGIOLAM, THUONG, PHAT) " +
                            "VALUES (@MANV, @CHAMCONG_DATE, @TONGSOGIOLAM, @THUONG, @PHAT)";

            using (SqlConnection con = new SqlConnection(sCon))
            {
                using (SqlCommand cmd = new SqlCommand(sQuery, con))
                {
                    cmd.Parameters.Add("@MANV", SqlDbType.VarChar, 10).Value = sMaNV;
                    cmd.Parameters.Add("@CHAMCONG_DATE", SqlDbType.Date).Value = chamCongDate;
                    cmd.Parameters.Add("@TONGSOGIOLAM", SqlDbType.Int).Value = tongCaChamCong;
                    cmd.Parameters.Add("@THUONG", SqlDbType.Decimal).Value = thuong;
                    cmd.Parameters.Add("@PHAT", SqlDbType.Decimal).Value = phat;

                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Thêm mới thành công!");

                        // Cập nhật lại DataGridView
                        fChamCong_Load(sender, e);

                        // Làm trống các trường nhập liệu
                        ClearInputFields();
                    }
                    
                    catch (Exception ex)
                    {
                        MessageBox.Show("Xảy ra lỗi: " + ex.Message);
                    }
                }
            }
        }



        private void buttonSua_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(textManhanvien.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã Nhân Viên.");
                return;
            }

            if (dateTimeNgayLuong.Value == null)
            {
                MessageBox.Show("Vui lòng chọn Ngày Chấm Công.");
                return;
            }

            string sMaNV = textManhanvien.Text.Trim();
            DateTime chamCongDate = dateTimeNgayLuong.Value.Date;

            // Khởi tạo biến với giá trị mặc định
            int tongCaChamCong = 0;
            decimal thuong = 0m;
            decimal phat = 0m;

            // Chuyển đổi và kiểm tra giá trị từ textBox1
            if (!int.TryParse(textBox1.Text.Trim(), out tongCaChamCong))
            {
                MessageBox.Show("Vui lòng nhập Tổng Ca Chấm Công là số nguyên.");
                return;
            }

            // Chuyển đổi và kiểm tra giá trị từ textThuong
            if (!decimal.TryParse(textThuong.Text.Trim(), out thuong))
            {
                MessageBox.Show("Vui lòng nhập Tiền Thưởng là số.");
                return;
            }

            // Chuyển đổi và kiểm tra giá trị từ textPhat
            if (!decimal.TryParse(textPhat.Text.Trim(), out phat))
            {
                MessageBox.Show("Vui lòng nhập Tiền Phạt là số.");
                return;
            }

            // Kiểm tra ràng buộc dữ liệu theo bảng
            if (tongCaChamCong < 0)
            {
                MessageBox.Show("Tổng ca chấm công phải lớn hơn hoặc bằng 0.");
                return;
            }

            if (thuong < 0)
            {
                MessageBox.Show("Tiền thưởng phải lớn hơn hoặc bằng 0.");
                return;
            }

            if (phat > 0)
            {
                MessageBox.Show("Tiền phạt phải nhỏ hơn hoặc bằng 0.");
                return;
            }

            string sQuery = "UPDATE CHAMCONG SET TONGSOGIOLAM = @TONGSOGIOLAM, THUONG = @THUONG, PHAT = @PHAT " +
                            "WHERE MANV = @MANV AND CHAMCONG_DATE = @CHAMCONG_DATE";

            using (SqlConnection con = new SqlConnection(sCon))
            {
                SqlCommand cmd = new SqlCommand(sQuery, con);
                cmd.Parameters.Add("@TONGSOGIOLAM", SqlDbType.Int).Value = tongCaChamCong;
                cmd.Parameters.Add("@THUONG", SqlDbType.Decimal).Value = thuong;
                cmd.Parameters.Add("@PHAT", SqlDbType.Decimal).Value = phat;
                cmd.Parameters.Add("@MANV", SqlDbType.VarChar, 10).Value = sMaNV;
                cmd.Parameters.Add("@CHAMCONG_DATE", SqlDbType.Date).Value = chamCongDate;

                try
                {
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Cập nhật thành công!");

                        // Cập nhật dữ liệu vào DataGridView
                        fChamCong_Load(sender, e);

                        // Làm trống các trường nhập liệu
                        ClearInputFields();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy bản ghi để cập nhật.");
                    }
                }
                
                catch (Exception ex)
                {
                    MessageBox.Show("Xảy ra lỗi trong quá trình cập nhật!\n" + ex.Message);
                }
            }
        }


        private void buttonXoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu đầu vào 
            if (string.IsNullOrWhiteSpace(textManhanvien.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã Nhân Viên để xóa.");
                return;
            }

            if (dateTimeNgayLuong.Value == null)
            {
                MessageBox.Show("Vui lòng chọn Ngày Chấm Công để xóa.");
                return;
            }

            string sMaNV = textManhanvien.Text.Trim();
            DateTime chamCongDate = dateTimeNgayLuong.Value.Date;

            DialogResult ret = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel);
            if (ret != DialogResult.OK)
            {
                return;
            }

            string sQuery = "DELETE FROM CHAMCONG WHERE MANV = @MANV AND CHAMCONG_DATE = @CHAMCONG_DATE";

            using (SqlConnection con = new SqlConnection(sCon))
            {
                SqlCommand cmd = new SqlCommand(sQuery, con);
                cmd.Parameters.Add("@MANV", SqlDbType.VarChar, 10).Value = sMaNV;
                cmd.Parameters.Add("@CHAMCONG_DATE", SqlDbType.Date).Value = chamCongDate;

                try
                {
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Xóa thành công!");

                        // Cập nhật dữ liệu vào DataGridView
                        fChamCong_Load(sender, e);

                        // Làm trống các trường nhập liệu
                        ClearInputFields();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy bản ghi để xóa.");
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Xảy ra lỗi trong quá trình xóa!\n" + ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xảy ra lỗi trong quá trình xóa!\n" + ex.Message);
                }
            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)

            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                if (e.RowIndex >= 0)
                {
                    textManhanvien.Text = row.Cells["MANV"].Value.ToString();
                    dateTimeNgayLuong.Value = Convert.ToDateTime(row.Cells["CHAMCONG_DATE"].Value);
                    textBox1.Text = row.Cells["TONGSOGIOLAM"].Value.ToString();
                    textThuong.Text = row.Cells["THUONG"].Value.ToString();
                    textPhat.Text = row.Cells["PHAT"].Value.ToString();


                    // Vô hiệu hóa trường Mã Nhân Viên và Ngày Chấm Công để tránh thay đổi
                    textManhanvien.Enabled = false;

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
