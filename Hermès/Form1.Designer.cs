namespace Hermès
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox tbSrc;
        private TextBox tbDst;
        private Button btnSrcBrowse;
        private Button btnDstBrowse;
        private Button btnScr;
        private Label lbTmr;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            tbSrc = new TextBox();
            tbDst = new TextBox();
            btnSrcBrowse = new Button();
            btnDstBrowse = new Button();
            btnScr = new Button();
            lbTmr = new Label();
            btnPTT = new Button();
            checkBox1 = new CheckBox();
            cbLG = new ComboBox();
            SuspendLayout();
            // 
            // tbSrc
            // 
            tbSrc.BackColor = Color.FromArgb(27, 27, 27);
            tbSrc.ForeColor = Color.White;
            tbSrc.Location = new Point(12, 18);
            tbSrc.Name = "tbSrc";
            tbSrc.PlaceholderText = "Source folder path...";
            tbSrc.Size = new Size(617, 43);
            tbSrc.TabIndex = 0;
            // 
            // tbDst
            // 
            tbDst.BackColor = Color.FromArgb(27, 27, 27);
            tbDst.ForeColor = Color.White;
            tbDst.Location = new Point(12, 78);
            tbDst.Name = "tbDst";
            tbDst.PlaceholderText = "Destination folder path...";
            tbDst.Size = new Size(617, 43);
            tbDst.TabIndex = 1;
            // 
            // btnSrcBrowse
            // 
            btnSrcBrowse.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSrcBrowse.BackColor = Color.FromArgb(27, 27, 27);
            btnSrcBrowse.ForeColor = Color.White;
            btnSrcBrowse.Location = new Point(635, 12);
            btnSrcBrowse.Name = "btnSrcBrowse";
            btnSrcBrowse.Size = new Size(179, 56);
            btnSrcBrowse.TabIndex = 2;
            btnSrcBrowse.Text = "Browse...";
            btnSrcBrowse.UseVisualStyleBackColor = false;
            btnSrcBrowse.Click += btnSrcBrowse_Click;
            // 
            // btnDstBrowse
            // 
            btnDstBrowse.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDstBrowse.BackColor = Color.FromArgb(27, 27, 27);
            btnDstBrowse.ForeColor = Color.White;
            btnDstBrowse.Location = new Point(635, 74);
            btnDstBrowse.Name = "btnDstBrowse";
            btnDstBrowse.Size = new Size(179, 52);
            btnDstBrowse.TabIndex = 3;
            btnDstBrowse.Text = "Browse...";
            btnDstBrowse.UseVisualStyleBackColor = false;
            btnDstBrowse.Click += btnDstBrowse_Click;
            // 
            // btnScr
            // 
            btnScr.Anchor = AnchorStyles.Bottom;
            btnScr.BackColor = Color.FromArgb(27, 27, 27);
            btnScr.Font = new Font("Tw Cen MT", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            btnScr.ForeColor = Color.White;
            btnScr.Location = new Point(240, 206);
            btnScr.Name = "btnScr";
            btnScr.Size = new Size(346, 68);
            btnScr.TabIndex = 4;
            btnScr.Text = "Start Scrutation";
            btnScr.UseVisualStyleBackColor = false;
            btnScr.Click += btnScr_Click;
            // 
            // lbTmr
            // 
            lbTmr.Anchor = AnchorStyles.Bottom;
            lbTmr.Font = new Font("Tw Cen MT", 20.1428585F, FontStyle.Regular, GraphicsUnit.Point);
            lbTmr.ForeColor = Color.White;
            lbTmr.Location = new Point(37, 133);
            lbTmr.Name = "lbTmr";
            lbTmr.Size = new Size(752, 65);
            lbTmr.TabIndex = 5;
            lbTmr.Text = "Scrutation not launched";
            lbTmr.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnPTT
            // 
            btnPTT.BackColor = Color.DarkRed;
            btnPTT.Dock = DockStyle.Bottom;
            btnPTT.FlatStyle = FlatStyle.Flat;
            btnPTT.ForeColor = Color.White;
            btnPTT.Location = new Point(0, 280);
            btnPTT.Name = "btnPTT";
            btnPTT.Size = new Size(826, 56);
            btnPTT.TabIndex = 6;
            btnPTT.Text = "Pin To Top";
            btnPTT.UseVisualStyleBackColor = false;
            btnPTT.Click += btnPTT_Click;
            // 
            // checkBox1
            // 
            checkBox1.Anchor = AnchorStyles.None;
            checkBox1.AutoSize = true;
            checkBox1.Font = new Font("Tw Cen MT", 14.1428576F, FontStyle.Regular, GraphicsUnit.Point);
            checkBox1.ForeColor = Color.White;
            checkBox1.Location = new Point(592, 221);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(189, 43);
            checkBox1.TabIndex = 7;
            checkBox1.Text = "Dark mode";
            checkBox1.TextAlign = ContentAlignment.MiddleRight;
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // cbLG
            // 
            cbLG.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cbLG.BackColor = Color.FromArgb(27, 27, 27);
            cbLG.ForeColor = Color.White;
            cbLG.FormattingEnabled = true;
            cbLG.Location = new Point(12, 219);
            cbLG.Name = "cbLG";
            cbLG.Size = new Size(222, 47);
            cbLG.TabIndex = 8;
            cbLG.SelectedIndexChanged += cbLG_SelectedIndexChanged;
            // 
            // Form1
            // 
            BackColor = Color.FromArgb(27, 27, 27);
            ClientSize = new Size(826, 336);
            Controls.Add(cbLG);
            Controls.Add(checkBox1);
            Controls.Add(btnPTT);
            Controls.Add(lbTmr);
            Controls.Add(btnScr);
            Controls.Add(btnDstBrowse);
            Controls.Add(btnSrcBrowse);
            Controls.Add(tbDst);
            Controls.Add(tbSrc);
            Font = new Font("Tw Cen MT", 14.1428576F, FontStyle.Regular, GraphicsUnit.Point);
            ForeColor = SystemColors.ControlLightLight;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Form1";
            Text = "Hermès";
            Load += Form1_Load_1;
            ResumeLayout(false);
            PerformLayout();
        }

        private Button btnPTT;
        private CheckBox checkBox1;
        private ComboBox cbLG;
    }
}