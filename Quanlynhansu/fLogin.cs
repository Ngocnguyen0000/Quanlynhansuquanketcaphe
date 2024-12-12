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
using TAIKHOAN;




namespace Quanlynhansuquancaphe
{
    public partial class fLogin : Form
    {
        public fLogin()
        {
            string sCon = @"Data Source=DESKTOP-3PUL2R2;Initial Catalog=QL_NHANSUQUANCAPHE;Integrated Security=True;Trust Server Certificate=True";


            InitializeComponent();
        }

        bool Login(string username, string password)
        {
            string sCon = @"Data Source=DESKTOP-3PUL2R2;Initial Catalog=QL_NHANSUQUANCAPHE;Integrated Security=True;Trust Server Certificate=True";

            try
            {
                using (SqlConnection con = new SqlConnection(sCon))
                {
                    string sQuery = "SELECT COUNT(*) FROM TAIKHOAN WHERE MANV = @MANV AND MATKHAU = @MATKHAU";
                    con.Open();  // Mở kết nối

                    using (SqlCommand cmd = new SqlCommand(sQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@MANV", username);
                        cmd.Parameters.AddWithValue("@MATKHAU", password);

                        int userCount = (int)cmd.ExecuteScalar();  // Lấy số lượng người dùng khớp
                        return userCount > 0;  // Nếu có ít nhất 1 tài khoản khớp, trả về true
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Hiển thị thông báo lỗi SQL chi tiết
                MessageBox.Show("Lỗi SQL: " + sqlEx.Message + "\n" + sqlEx.StackTrace, "Lỗi");
                return false;
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi chung
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Lỗi");
                return false;
            }
        }



        private void buttonLogin_Click(object sender, EventArgs e)
        {
            // Lấy thông tin người dùng nhập vào
            string username = textUsername.Text.Trim();
            string password = textPass.Text.Trim();


            // Kiểm tra nếu thông tin trống
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập tên tài khoản và mật khẩu!", "Thông báo");
                return;
            }

            // Gọi hàm đăng nhập
            if (Login(username, password))
            {
                fTaiKhoan f = new fTaiKhoan();
                this.Hide();
                f.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Sai tên tài khoản hoặc mật khẩu!", "Thông báo!");
            }
        }

        private void fLogin_Load(object sender, EventArgs e)
        {

        }




        private void fLogin_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có thực sự muốn thoát chương trình?", "Thông báo!", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void buttonExit_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
