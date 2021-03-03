
namespace kutuphaneSistemi
{
    partial class Form1
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
            this.btnOgrenciEkle = new System.Windows.Forms.Button();
            this.btnKitapEkle = new System.Windows.Forms.Button();
            this.btnKutuphane = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnOgrenciEkle
            // 
            this.btnOgrenciEkle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOgrenciEkle.Location = new System.Drawing.Point(142, 86);
            this.btnOgrenciEkle.Margin = new System.Windows.Forms.Padding(2);
            this.btnOgrenciEkle.Name = "btnOgrenciEkle";
            this.btnOgrenciEkle.Size = new System.Drawing.Size(141, 56);
            this.btnOgrenciEkle.TabIndex = 0;
            this.btnOgrenciEkle.Text = "Öğrenci Ekle";
            this.btnOgrenciEkle.UseVisualStyleBackColor = true;
            this.btnOgrenciEkle.Click += new System.EventHandler(this.btnOgrenciEkle_Click);
            // 
            // btnKitapEkle
            // 
            this.btnKitapEkle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKitapEkle.Location = new System.Drawing.Point(298, 86);
            this.btnKitapEkle.Margin = new System.Windows.Forms.Padding(2);
            this.btnKitapEkle.Name = "btnKitapEkle";
            this.btnKitapEkle.Size = new System.Drawing.Size(141, 56);
            this.btnKitapEkle.TabIndex = 1;
            this.btnKitapEkle.Text = "Kitap Ekle / Listele";
            this.btnKitapEkle.UseVisualStyleBackColor = true;
            this.btnKitapEkle.Click += new System.EventHandler(this.btnKitapEkle_Click);
            // 
            // btnKutuphane
            // 
            this.btnKutuphane.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKutuphane.Location = new System.Drawing.Point(142, 146);
            this.btnKutuphane.Margin = new System.Windows.Forms.Padding(2);
            this.btnKutuphane.Name = "btnKutuphane";
            this.btnKutuphane.Size = new System.Drawing.Size(141, 59);
            this.btnKutuphane.TabIndex = 2;
            this.btnKutuphane.Text = "Kutuphane";
            this.btnKutuphane.UseVisualStyleBackColor = true;
            this.btnKutuphane.Click += new System.EventHandler(this.btnKutuphane_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(298, 146);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(141, 59);
            this.button1.TabIndex = 3;
            this.button1.Text = "Ogrenci Kitapları";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 325);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnKutuphane);
            this.Controls.Add(this.btnKitapEkle);
            this.Controls.Add(this.btnOgrenciEkle);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOgrenciEkle;
        private System.Windows.Forms.Button btnKitapEkle;
        private System.Windows.Forms.Button btnKutuphane;
        private System.Windows.Forms.Button button1;
    }
}

