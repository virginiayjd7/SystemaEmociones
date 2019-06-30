namespace PrjReconocimientoF.Formularios
{
    partial class Frm_AsitenciaTrabajador
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_AsitenciaTrabajador));
            this.BtnDesconectar = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.lblNadie = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblNumeroDetect = new System.Windows.Forms.Label();
            this.imageBoxFrameGrabber = new Emgu.CV.UI.ImageBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblfecha = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.lblhora = new System.Windows.Forms.Label();
            this.dgvasistencia = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.FechaHora = new System.Windows.Forms.Timer(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxFrameGrabber)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvasistencia)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnDesconectar
            // 
            this.BtnDesconectar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnDesconectar.BackColor = System.Drawing.Color.Transparent;
            this.BtnDesconectar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnDesconectar.BackgroundImage")));
            this.BtnDesconectar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BtnDesconectar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnDesconectar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDesconectar.ForeColor = System.Drawing.Color.White;
            this.BtnDesconectar.Location = new System.Drawing.Point(284, 359);
            this.BtnDesconectar.Name = "BtnDesconectar";
            this.BtnDesconectar.Size = new System.Drawing.Size(47, 43);
            this.BtnDesconectar.TabIndex = 59;
            this.BtnDesconectar.UseVisualStyleBackColor = false;
            this.BtnDesconectar.Click += new System.EventHandler(this.BtnDesconectar_Click);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(20, 328);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 16);
            this.label5.TabIndex = 57;
            this.label5.Text = "Nombres:";
            // 
            // lblNadie
            // 
            this.lblNadie.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNadie.AutoSize = true;
            this.lblNadie.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNadie.ForeColor = System.Drawing.Color.White;
            this.lblNadie.Location = new System.Drawing.Point(101, 328);
            this.lblNadie.Name = "lblNadie";
            this.lblNadie.Size = new System.Drawing.Size(216, 16);
            this.lblNadie.TabIndex = 56;
            this.lblNadie.Text = "__________________________";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(10, 291);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(158, 16);
            this.label2.TabIndex = 55;
            this.label2.Text = "Número de Personas:";
            // 
            // lblNumeroDetect
            // 
            this.lblNumeroDetect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNumeroDetect.AutoSize = true;
            this.lblNumeroDetect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumeroDetect.ForeColor = System.Drawing.Color.White;
            this.lblNumeroDetect.Location = new System.Drawing.Point(174, 291);
            this.lblNumeroDetect.Name = "lblNumeroDetect";
            this.lblNumeroDetect.Size = new System.Drawing.Size(16, 16);
            this.lblNumeroDetect.TabIndex = 60;
            this.lblNumeroDetect.Text = "0";
            // 
            // imageBoxFrameGrabber
            // 
            this.imageBoxFrameGrabber.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imageBoxFrameGrabber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageBoxFrameGrabber.Location = new System.Drawing.Point(8, 37);
            this.imageBoxFrameGrabber.Name = "imageBoxFrameGrabber";
            this.imageBoxFrameGrabber.Size = new System.Drawing.Size(324, 248);
            this.imageBoxFrameGrabber.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imageBoxFrameGrabber.TabIndex = 62;
            this.imageBoxFrameGrabber.TabStop = false;
            this.imageBoxFrameGrabber.WaitOnLoad = true;
            this.imageBoxFrameGrabber.Click += new System.EventHandler(this.imageBoxFrameGrabber_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblfecha);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.lblhora);
            this.panel1.Controls.Add(this.dgvasistencia);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.imageBoxFrameGrabber);
            this.panel1.Controls.Add(this.lblNumeroDetect);
            this.panel1.Controls.Add(this.BtnDesconectar);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lblNadie);
            this.panel1.Location = new System.Drawing.Point(29, 65);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(341, 586);
            this.panel1.TabIndex = 64;
            // 
            // lblfecha
            // 
            this.lblfecha.AutoSize = true;
            this.lblfecha.BackColor = System.Drawing.Color.Transparent;
            this.lblfecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblfecha.ForeColor = System.Drawing.Color.White;
            this.lblfecha.Location = new System.Drawing.Point(50, 18);
            this.lblfecha.Name = "lblfecha";
            this.lblfecha.Size = new System.Drawing.Size(45, 16);
            this.lblfecha.TabIndex = 1;
            this.lblfecha.Text = "label3";
            this.lblfecha.Click += new System.EventHandler(this.label3_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(170, 359);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(77, 43);
            this.button2.TabIndex = 66;
            this.button2.Text = "Marcar Salida";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // lblhora
            // 
            this.lblhora.AutoSize = true;
            this.lblhora.BackColor = System.Drawing.Color.Transparent;
            this.lblhora.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblhora.ForeColor = System.Drawing.Color.White;
            this.lblhora.Location = new System.Drawing.Point(261, 18);
            this.lblhora.Name = "lblhora";
            this.lblhora.Size = new System.Drawing.Size(45, 16);
            this.lblhora.TabIndex = 0;
            this.lblhora.Text = "label1";
            // 
            // dgvasistencia
            // 
            this.dgvasistencia.AllowUserToAddRows = false;
            this.dgvasistencia.AllowUserToDeleteRows = false;
            this.dgvasistencia.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvasistencia.Location = new System.Drawing.Point(8, 414);
            this.dgvasistencia.Name = "dgvasistencia";
            this.dgvasistencia.ReadOnly = true;
            this.dgvasistencia.Size = new System.Drawing.Size(323, 156);
            this.dgvasistencia.TabIndex = 65;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(250, 291);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 15);
            this.label1.TabIndex = 64;
            this.label1.Text = "Registrarse";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(87, 359);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(77, 43);
            this.button1.TabIndex = 63;
            this.button1.Text = "Marcar Entrada";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FechaHora
            // 
            this.FechaHora.Tick += new System.EventHandler(this.FechaHora_Tick);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Location = new System.Drawing.Point(259, 657);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(88, 43);
            this.panel3.TabIndex = 66;
            this.panel3.Click += new System.EventHandler(this.panel3_Click);
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // Frm_AsitenciaTrabajador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(404, 712);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frm_AsitenciaTrabajador";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmReconocimientoFacial";
            this.TransparencyKey = System.Drawing.SystemColors.Control;
            this.Load += new System.EventHandler(this.FrmReconocimientoFacial_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxFrameGrabber)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvasistencia)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnDesconectar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblNadie;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblNumeroDetect;
        private Emgu.CV.UI.ImageBox imageBoxFrameGrabber;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblfecha;
        private System.Windows.Forms.Label lblhora;
        private System.Windows.Forms.Timer FechaHora;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dgvasistencia;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
    }
}