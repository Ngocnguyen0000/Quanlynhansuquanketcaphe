namespace WindowsFormsApp1
{
    partial class fLichLam
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fLichLam));
            rbKhongDiLam = new RadioButton();
            dataGridView1 = new DataGridView();
            label4 = new Label();
            btnXoa = new Button();
            btnSua = new Button();
            btnLuu = new Button();
            rbDiTre = new RadioButton();
            rbDiLam = new RadioButton();
            label3 = new Label();
            txtMaCa = new TextBox();
            label2 = new Label();
            txtMaNhanVien = new TextBox();
            label1 = new Label();
            menuStrip1 = new MenuStrip();
            quảnLýTàiKhoảnToolStripMenuItem = new ToolStripMenuItem();
            quảnLýNhânViênToolStripMenuItem = new ToolStripMenuItem();
            quảnLýCaLàmToolStripMenuItem = new ToolStripMenuItem();
            quảnLýLịchLàmToolStripMenuItem = new ToolStripMenuItem();
            quảnLýLươngToolStripMenuItem = new ToolStripMenuItem();
            quảnLýChấmCôngToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // rbKhongDiLam
            // 
            rbKhongDiLam.AutoSize = true;
            rbKhongDiLam.BackColor = Color.Transparent;
            rbKhongDiLam.Font = new Font("Times New Roman", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rbKhongDiLam.ForeColor = Color.Transparent;
            rbKhongDiLam.Location = new Point(721, 255);
            rbKhongDiLam.Margin = new Padding(3, 2, 3, 2);
            rbKhongDiLam.Name = "rbKhongDiLam";
            rbKhongDiLam.Size = new Size(98, 19);
            rbKhongDiLam.TabIndex = 21;
            rbKhongDiLam.TabStop = true;
            rbKhongDiLam.Text = "Không Đi làm";
            rbKhongDiLam.UseVisualStyleBackColor = false;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
            dataGridView1.Location = new Point(182, 348);
            dataGridView1.Margin = new Padding(3, 2, 3, 2);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 24;
            dataGridView1.Size = new Size(884, 186);
            dataGridView1.TabIndex = 19;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Times New Roman", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Transparent;
            label4.Location = new Point(361, 63);
            label4.Name = "label4";
            label4.Size = new Size(505, 55);
            label4.TabIndex = 20;
            label4.Text = "QUẢN LÝ LỊCH LÀM";
            // 
            // btnXoa
            // 
            btnXoa.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnXoa.Location = new Point(704, 304);
            btnXoa.Margin = new Padding(3, 2, 3, 2);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(75, 23);
            btnXoa.TabIndex = 18;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            btnXoa.Click += btnXoa_Click_1;
            // 
            // btnSua
            // 
            btnSua.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSua.Location = new Point(572, 304);
            btnSua.Margin = new Padding(3, 2, 3, 2);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(75, 23);
            btnSua.TabIndex = 17;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = true;
            btnSua.Click += btnSua_Click_1;
            // 
            // btnLuu
            // 
            btnLuu.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLuu.Location = new Point(435, 304);
            btnLuu.Margin = new Padding(3, 2, 3, 2);
            btnLuu.Name = "btnLuu";
            btnLuu.Size = new Size(75, 23);
            btnLuu.TabIndex = 15;
            btnLuu.Text = "Thêm";
            btnLuu.UseVisualStyleBackColor = true;
            btnLuu.Click += btnLuu_Click_1;
            // 
            // rbDiTre
            // 
            rbDiTre.AutoSize = true;
            rbDiTre.BackColor = Color.Transparent;
            rbDiTre.Font = new Font("Times New Roman", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rbDiTre.ForeColor = SystemColors.ButtonHighlight;
            rbDiTre.Location = new Point(633, 255);
            rbDiTre.Margin = new Padding(3, 2, 3, 2);
            rbDiTre.Name = "rbDiTre";
            rbDiTre.Size = new Size(58, 19);
            rbDiTre.TabIndex = 14;
            rbDiTre.TabStop = true;
            rbDiTre.Text = "Đi Trễ";
            rbDiTre.UseVisualStyleBackColor = false;
            // 
            // rbDiLam
            // 
            rbDiLam.AutoSize = true;
            rbDiLam.BackColor = Color.Transparent;
            rbDiLam.Font = new Font("Times New Roman", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rbDiLam.ForeColor = SystemColors.ButtonHighlight;
            rbDiLam.Location = new Point(546, 255);
            rbDiLam.Margin = new Padding(3, 2, 3, 2);
            rbDiLam.Name = "rbDiLam";
            rbDiLam.Size = new Size(62, 19);
            rbDiLam.TabIndex = 12;
            rbDiLam.TabStop = true;
            rbDiLam.Text = "Đi Làm";
            rbDiLam.UseVisualStyleBackColor = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Times New Roman", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Transparent;
            label3.Location = new Point(361, 255);
            label3.Name = "label3";
            label3.Size = new Size(101, 17);
            label3.TabIndex = 16;
            label3.Text = "Trạng Thái (*)";
            // 
            // txtMaCa
            // 
            txtMaCa.Location = new Point(556, 200);
            txtMaCa.Margin = new Padding(3, 2, 3, 2);
            txtMaCa.Name = "txtMaCa";
            txtMaCa.Size = new Size(194, 22);
            txtMaCa.TabIndex = 11;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Times New Roman", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ButtonHighlight;
            label2.Location = new Point(361, 206);
            label2.Name = "label2";
            label2.Size = new Size(79, 17);
            label2.TabIndex = 13;
            label2.Text = "Mã Ca (*):";
            // 
            // txtMaNhanVien
            // 
            txtMaNhanVien.Location = new Point(556, 147);
            txtMaNhanVien.Margin = new Padding(3, 2, 3, 2);
            txtMaNhanVien.Name = "txtMaNhanVien";
            txtMaNhanVien.Size = new Size(194, 22);
            txtMaNhanVien.TabIndex = 9;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Times New Roman", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(361, 150);
            label1.Name = "label1";
            label1.Size = new Size(132, 17);
            label1.TabIndex = 10;
            label1.Text = "Mã Nhân Viên (*) :";
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { quảnLýTàiKhoảnToolStripMenuItem, quảnLýNhânViênToolStripMenuItem, quảnLýCaLàmToolStripMenuItem, quảnLýLịchLàmToolStripMenuItem, quảnLýLươngToolStripMenuItem, quảnLýChấmCôngToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1177, 24);
            menuStrip1.TabIndex = 22;
            menuStrip1.Text = "menuStrip1";
            // 
            // quảnLýTàiKhoảnToolStripMenuItem
            // 
            quảnLýTàiKhoảnToolStripMenuItem.Name = "quảnLýTàiKhoảnToolStripMenuItem";
            quảnLýTàiKhoảnToolStripMenuItem.Size = new Size(112, 20);
            quảnLýTàiKhoảnToolStripMenuItem.Text = "Quản lý tài khoản";
            quảnLýTàiKhoảnToolStripMenuItem.Click += quảnLýTàiKhoảnToolStripMenuItem_Click_1;
            // 
            // quảnLýNhânViênToolStripMenuItem
            // 
            quảnLýNhânViênToolStripMenuItem.Name = "quảnLýNhânViênToolStripMenuItem";
            quảnLýNhânViênToolStripMenuItem.Size = new Size(115, 20);
            quảnLýNhânViênToolStripMenuItem.Text = "Quản lý nhân viên";
            quảnLýNhânViênToolStripMenuItem.Click += quảnLýNhânViênToolStripMenuItem_Click;
            // 
            // quảnLýCaLàmToolStripMenuItem
            // 
            quảnLýCaLàmToolStripMenuItem.Name = "quảnLýCaLàmToolStripMenuItem";
            quảnLýCaLàmToolStripMenuItem.Size = new Size(98, 20);
            quảnLýCaLàmToolStripMenuItem.Text = "Quản lý ca làm";
            quảnLýCaLàmToolStripMenuItem.Click += quảnLýCaLàmToolStripMenuItem_Click;
            // 
            // quảnLýLịchLàmToolStripMenuItem
            // 
            quảnLýLịchLàmToolStripMenuItem.Name = "quảnLýLịchLàmToolStripMenuItem";
            quảnLýLịchLàmToolStripMenuItem.Size = new Size(105, 20);
            quảnLýLịchLàmToolStripMenuItem.Text = "Quản lý lịch làm";
            quảnLýLịchLàmToolStripMenuItem.Click += quảnLýLịchLàmToolStripMenuItem_Click;
            // 
            // quảnLýLươngToolStripMenuItem
            // 
            quảnLýLươngToolStripMenuItem.Name = "quảnLýLươngToolStripMenuItem";
            quảnLýLươngToolStripMenuItem.Size = new Size(94, 20);
            quảnLýLươngToolStripMenuItem.Text = "Quản lý lương";
            quảnLýLươngToolStripMenuItem.Click += quảnLýLươngToolStripMenuItem_Click;
            // 
            // quảnLýChấmCôngToolStripMenuItem
            // 
            quảnLýChấmCôngToolStripMenuItem.Name = "quảnLýChấmCôngToolStripMenuItem";
            quảnLýChấmCôngToolStripMenuItem.Size = new Size(123, 20);
            quảnLýChấmCôngToolStripMenuItem.Text = "Quản lý chấm công";
            quảnLýChấmCôngToolStripMenuItem.Click += quảnLýChấmCôngToolStripMenuItem_Click;
            // 
            // fLichLam
            // 
            AutoScaleDimensions = new SizeF(8F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1177, 545);
            Controls.Add(menuStrip1);
            Controls.Add(rbKhongDiLam);
            Controls.Add(dataGridView1);
            Controls.Add(label4);
            Controls.Add(btnXoa);
            Controls.Add(btnSua);
            Controls.Add(btnLuu);
            Controls.Add(rbDiTre);
            Controls.Add(rbDiLam);
            Controls.Add(label3);
            Controls.Add(txtMaCa);
            Controls.Add(label2);
            Controls.Add(txtMaNhanVien);
            Controls.Add(label1);
            Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(4);
            Name = "fLichLam";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "QUẢN LÝ LỊCH LÀM";
            FormClosing += fLichLam_FormClosing;
            Load += fLichLam_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.RadioButton rbKhongDiLam;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.RadioButton rbDiTre;
        private System.Windows.Forms.RadioButton rbDiLam;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMaCa;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMaNhanVien;
        private System.Windows.Forms.Label label1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem quảnLýTàiKhoảnToolStripMenuItem;
        private ToolStripMenuItem quảnLýNhânViênToolStripMenuItem;
        private ToolStripMenuItem quảnLýCaLàmToolStripMenuItem;
        private ToolStripMenuItem quảnLýLịchLàmToolStripMenuItem;
        private ToolStripMenuItem quảnLýLươngToolStripMenuItem;
        private ToolStripMenuItem quảnLýChấmCôngToolStripMenuItem;
    }
}