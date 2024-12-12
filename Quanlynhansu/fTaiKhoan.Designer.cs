namespace TAIKHOAN
{
    partial class fTaiKhoan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fTaiKhoan));
            label1 = new Label();
            txtmanv = new TextBox();
            txtmk = new TextBox();
            button1 = new Button();
            dataGridView1 = new DataGridView();
            button2 = new Button();
            button3 = new Button();
            label3 = new Label();
            label2 = new Label();
            label4 = new Label();
            label5 = new Label();
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
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.ButtonHighlight;
            label1.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(535, 154);
            label1.Margin = new Padding(6, 0, 3, 0);
            label1.Name = "label1";
            label1.Size = new Size(0, 15);
            label1.TabIndex = 0;
            // 
            // txtmanv
            // 
            txtmanv.BackColor = SystemColors.Window;
            txtmanv.Location = new Point(595, 131);
            txtmanv.Name = "txtmanv";
            txtmanv.Size = new Size(182, 23);
            txtmanv.TabIndex = 2;
            // 
            // txtmk
            // 
            txtmk.Location = new Point(595, 181);
            txtmk.Name = "txtmk";
            txtmk.Size = new Size(182, 23);
            txtmk.TabIndex = 3;
            txtmk.UseSystemPasswordChar = true;
            // 
            // button1
            // 
            button1.BackColor = SystemColors.ScrollBar;
            button1.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.Location = new Point(414, 235);
            button1.Name = "button1";
            button1.Size = new Size(107, 29);
            button1.TabIndex = 4;
            button1.Text = "Thêm tài khoản";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(128, 280);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 24;
            dataGridView1.Size = new Size(955, 249);
            dataGridView1.TabIndex = 5;
            dataGridView1.CellClick += dataGridView1_CellClick;
            // 
            // button2
            // 
            button2.BackColor = SystemColors.ScrollBar;
            button2.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button2.Location = new Point(696, 235);
            button2.Name = "button2";
            button2.Size = new Size(120, 29);
            button2.TabIndex = 6;
            button2.Text = "Xoá tài khoản";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.BackColor = SystemColors.ScrollBar;
            button3.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button3.Location = new Point(550, 235);
            button3.Name = "button3";
            button3.Size = new Size(120, 29);
            button3.TabIndex = 7;
            button3.Text = "Sửa tài khoản";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Trebuchet MS", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = SystemColors.ButtonHighlight;
            label3.Location = new Point(433, 134);
            label3.Name = "label3";
            label3.Size = new Size(154, 20);
            label3.TabIndex = 10;
            label3.Text = "Mã nhân viên (*)    :";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Trebuchet MS", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ButtonHighlight;
            label2.Location = new Point(433, 184);
            label2.Name = "label2";
            label2.Size = new Size(150, 20);
            label2.TabIndex = 11;
            label2.Text = "Mật khẩu  (*)        :";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(495, 53);
            label4.Name = "label4";
            label4.Size = new Size(0, 15);
            label4.TabIndex = 12;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Times New Roman", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = SystemColors.ButtonHighlight;
            label5.Location = new Point(347, 53);
            label5.Name = "label5";
            label5.Size = new Size(537, 55);
            label5.TabIndex = 13;
            label5.Text = "QUẢN LÝ TÀI KHOẢN";
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { quảnLýTàiKhoảnToolStripMenuItem, quảnLýNhânViênToolStripMenuItem, quảnLýCaLàmToolStripMenuItem, quảnLýLịchLàmToolStripMenuItem, quảnLýLươngToolStripMenuItem, quảnLýChấmCôngToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1180, 24);
            menuStrip1.TabIndex = 14;
            menuStrip1.Text = "menuStrip1";
            // 
            // quảnLýTàiKhoảnToolStripMenuItem
            // 
            quảnLýTàiKhoảnToolStripMenuItem.Name = "quảnLýTàiKhoảnToolStripMenuItem";
            quảnLýTàiKhoảnToolStripMenuItem.Size = new Size(112, 20);
            quảnLýTàiKhoảnToolStripMenuItem.Text = "Quản lý tài khoản";
            quảnLýTàiKhoảnToolStripMenuItem.Click += quảnLýTàiKhoảnToolStripMenuItem_Click;
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
            // fTaiKhoan
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientActiveCaption;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1180, 541);
            Controls.Add(menuStrip1);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(label3);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(dataGridView1);
            Controls.Add(button1);
            Controls.Add(txtmk);
            Controls.Add(txtmanv);
            Controls.Add(label1);
            Name = "fTaiKhoan";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "QUẢN LÝ TÀI KHOẢN";
            FormClosing += fTaiKhoan_FormClosing;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtmanv;
        private System.Windows.Forms.TextBox txtmk;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private Label label3;
        private Label label2;
        private Label label4;
        private Label label5;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem quảnLýTàiKhoảnToolStripMenuItem;
        private ToolStripMenuItem quảnLýNhânViênToolStripMenuItem;
        private ToolStripMenuItem quảnLýCaLàmToolStripMenuItem;
        private ToolStripMenuItem quảnLýLịchLàmToolStripMenuItem;
        private ToolStripMenuItem quảnLýLươngToolStripMenuItem;
        private ToolStripMenuItem quảnLýChấmCôngToolStripMenuItem;
    }
}

