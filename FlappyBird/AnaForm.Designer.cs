namespace FlappyBird
{
    partial class AnaForm
    {
        private System.ComponentModel.IContainer components = null!;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            pnlUst = new Panel();
            lblBilgi = new Label();
            btnYeniOyun = new Button();
            oyunPaneli = new OyunPaneli();
            oyunZamanlayici = new System.Windows.Forms.Timer(components);
            pnlUst.SuspendLayout();
            SuspendLayout();
            //
            // pnlUst
            //
            pnlUst.BackColor = Color.FromArgb(40, 40, 40);
            pnlUst.Controls.Add(lblBilgi);
            pnlUst.Controls.Add(btnYeniOyun);
            pnlUst.Dock = DockStyle.Top;
            pnlUst.Location = new Point(0, 0);
            pnlUst.Name = "pnlUst";
            pnlUst.Size = new Size(400, 40);
            pnlUst.TabIndex = 0;
            //
            // lblBilgi
            //
            lblBilgi.Dock = DockStyle.Fill;
            lblBilgi.ForeColor = Color.White;
            lblBilgi.Location = new Point(0, 0);
            lblBilgi.Name = "lblBilgi";
            lblBilgi.Padding = new Padding(8, 0, 0, 0);
            lblBilgi.Size = new Size(300, 40);
            lblBilgi.TabIndex = 0;
            lblBilgi.TextAlign = ContentAlignment.MiddleLeft;
            //
            // btnYeniOyun
            //
            btnYeniOyun.Dock = DockStyle.Right;
            btnYeniOyun.Location = new Point(300, 0);
            btnYeniOyun.Name = "btnYeniOyun";
            btnYeniOyun.Size = new Size(100, 40);
            btnYeniOyun.TabIndex = 1;
            btnYeniOyun.TabStop = false;
            btnYeniOyun.Text = "Yeni Oyun";
            btnYeniOyun.UseVisualStyleBackColor = true;
            btnYeniOyun.Click += btnYeniOyun_Click;
            //
            // oyunPaneli
            //
            oyunPaneli.Dock = DockStyle.Fill;
            oyunPaneli.Location = new Point(0, 40);
            oyunPaneli.Name = "oyunPaneli";
            oyunPaneli.Size = new Size(400, 600);
            oyunPaneli.TabIndex = 1;
            oyunPaneli.MouseDown += oyunPaneli_MouseDown;
            //
            // oyunZamanlayici
            //
            oyunZamanlayici.Interval = 16;
            oyunZamanlayici.Tick += oyunZamanlayici_Tick;
            //
            // AnaForm
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(400, 640);
            Controls.Add(oyunPaneli);
            Controls.Add(pnlUst);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            KeyPreview = true;
            MaximizeBox = false;
            Name = "AnaForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Flappy Bird";
            KeyDown += AnaForm_KeyDown;
            pnlUst.ResumeLayout(false);
            ResumeLayout(false);
        }

        private Panel pnlUst = null!;
        private Label lblBilgi = null!;
        private Button btnYeniOyun = null!;
        private OyunPaneli oyunPaneli = null!;
        private System.Windows.Forms.Timer oyunZamanlayici = null!;
    }
}
