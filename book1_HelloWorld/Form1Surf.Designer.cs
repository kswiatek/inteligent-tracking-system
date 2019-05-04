namespace InteligentnySystemSledzacy
{
    partial class Form1Surf
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
            this.lblImageToFind = new System.Windows.Forms.Label();
            this.txtImageToFind = new System.Windows.Forms.TextBox();
            this.btnImageToFind = new System.Windows.Forms.Button();
            this.ckDrawKeyPoints = new System.Windows.Forms.CheckBox();
            this.ckDrawMatchingLines = new System.Windows.Forms.CheckBox();
            this.ibResult = new System.Windows.Forms.PictureBox();
            this.ofdImageToFind = new System.Windows.Forms.OpenFileDialog();
            this.btnGetImageToTrack = new System.Windows.Forms.Button();
            this.btnEditImg = new System.Windows.Forms.Button();
            this.btnEngage = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lbXbar = new System.Windows.Forms.Label();
            this.lbYbar = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbPreparingCamera = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbVisibilityLvl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ibResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblImageToFind
            // 
            this.lblImageToFind.AutoSize = true;
            this.lblImageToFind.Location = new System.Drawing.Point(8, 27);
            this.lblImageToFind.Name = "lblImageToFind";
            this.lblImageToFind.Size = new System.Drawing.Size(161, 13);
            this.lblImageToFind.TabIndex = 2;
            this.lblImageToFind.Text = "wybierz obraz z obiektem z pliku:";
            // 
            // txtImageToFind
            // 
            this.txtImageToFind.Location = new System.Drawing.Point(11, 51);
            this.txtImageToFind.Name = "txtImageToFind";
            this.txtImageToFind.Size = new System.Drawing.Size(163, 20);
            this.txtImageToFind.TabIndex = 4;
            this.txtImageToFind.TextChanged += new System.EventHandler(this.txtImageToFind_TextChanged);
            // 
            // btnImageToFind
            // 
            this.btnImageToFind.Location = new System.Drawing.Point(180, 48);
            this.btnImageToFind.Name = "btnImageToFind";
            this.btnImageToFind.Size = new System.Drawing.Size(48, 23);
            this.btnImageToFind.TabIndex = 6;
            this.btnImageToFind.Text = "...";
            this.btnImageToFind.UseVisualStyleBackColor = true;
            this.btnImageToFind.Click += new System.EventHandler(this.btnImageToFind_Click);
            // 
            // ckDrawKeyPoints
            // 
            this.ckDrawKeyPoints.AutoSize = true;
            this.ckDrawKeyPoints.Checked = true;
            this.ckDrawKeyPoints.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckDrawKeyPoints.Location = new System.Drawing.Point(6, 33);
            this.ckDrawKeyPoints.Name = "ckDrawKeyPoints";
            this.ckDrawKeyPoints.Size = new System.Drawing.Size(122, 17);
            this.ckDrawKeyPoints.TabIndex = 8;
            this.ckDrawKeyPoints.Text = "rysuj punkty bazowe";
            this.ckDrawKeyPoints.UseVisualStyleBackColor = true;
            this.ckDrawKeyPoints.CheckedChanged += new System.EventHandler(this.ckDrawKeyPoints_CheckedChanged);
            // 
            // ckDrawMatchingLines
            // 
            this.ckDrawMatchingLines.AutoSize = true;
            this.ckDrawMatchingLines.Checked = true;
            this.ckDrawMatchingLines.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckDrawMatchingLines.Location = new System.Drawing.Point(6, 56);
            this.ckDrawMatchingLines.Name = "ckDrawMatchingLines";
            this.ckDrawMatchingLines.Size = new System.Drawing.Size(120, 17);
            this.ckDrawMatchingLines.TabIndex = 9;
            this.ckDrawMatchingLines.Text = "rysuj linie zbieżności";
            this.ckDrawMatchingLines.UseVisualStyleBackColor = true;
            this.ckDrawMatchingLines.CheckedChanged += new System.EventHandler(this.ckDrawMatchingLines_CheckedChanged);
            // 
            // ibResult
            // 
            this.ibResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ibResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ibResult.Location = new System.Drawing.Point(21, 134);
            this.ibResult.Name = "ibResult";
            this.ibResult.Size = new System.Drawing.Size(960, 480);
            this.ibResult.TabIndex = 10;
            this.ibResult.TabStop = false;
            // 
            // ofdImageToFind
            // 
            this.ofdImageToFind.FileName = "openFileDialog2";
            // 
            // btnGetImageToTrack
            // 
            this.btnGetImageToTrack.Location = new System.Drawing.Point(414, 19);
            this.btnGetImageToTrack.Name = "btnGetImageToTrack";
            this.btnGetImageToTrack.Size = new System.Drawing.Size(89, 68);
            this.btnGetImageToTrack.TabIndex = 11;
            this.btnGetImageToTrack.Text = "Przechwyć obraz do śledzenia";
            this.btnGetImageToTrack.UseVisualStyleBackColor = true;
            this.btnGetImageToTrack.Click += new System.EventHandler(this.btnGetImageToTrack_Click);
            // 
            // btnEditImg
            // 
            this.btnEditImg.Location = new System.Drawing.Point(509, 19);
            this.btnEditImg.Name = "btnEditImg";
            this.btnEditImg.Size = new System.Drawing.Size(89, 66);
            this.btnEditImg.TabIndex = 12;
            this.btnEditImg.Text = "Edytuj  bierzący obraz";
            this.btnEditImg.UseVisualStyleBackColor = true;
            this.btnEditImg.Click += new System.EventHandler(this.btnEditImg_Click);
            // 
            // btnEngage
            // 
            this.btnEngage.Location = new System.Drawing.Point(607, 19);
            this.btnEngage.Name = "btnEngage";
            this.btnEngage.Size = new System.Drawing.Size(89, 66);
            this.btnEngage.TabIndex = 13;
            this.btnEngage.Text = "Namierzaj";
            this.btnEngage.UseVisualStyleBackColor = true;
            this.btnEngage.Click += new System.EventHandler(this.btnEngage_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(411, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Pozycja wykrytego obiektu: ";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(730, 30);
            this.trackBar1.Maximum = 180;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(257, 45);
            this.trackBar1.TabIndex = 15;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(730, 70);
            this.trackBar2.Maximum = 180;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(257, 45);
            this.trackBar2.TabIndex = 16;
            this.trackBar2.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ckDrawKeyPoints);
            this.groupBox1.Controls.Add(this.ckDrawMatchingLines);
            this.groupBox1.Location = new System.Drawing.Point(21, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(144, 96);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "pomoc:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblImageToFind);
            this.groupBox2.Controls.Add(this.txtImageToFind);
            this.groupBox2.Controls.Add(this.btnImageToFind);
            this.groupBox2.Location = new System.Drawing.Point(171, 14);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(236, 96);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "wybór obiektu:";
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(702, 14);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(295, 100);
            this.groupBox3.TabIndex = 21;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "ustawianie położenia kamery:";
            // 
            // lbXbar
            // 
            this.lbXbar.AutoSize = true;
            this.lbXbar.Location = new System.Drawing.Point(712, 30);
            this.lbXbar.Name = "lbXbar";
            this.lbXbar.Size = new System.Drawing.Size(25, 13);
            this.lbXbar.TabIndex = 22;
            this.lbXbar.Text = "333";
            // 
            // lbYbar
            // 
            this.lbYbar.AutoSize = true;
            this.lbYbar.Location = new System.Drawing.Point(712, 71);
            this.lbYbar.Name = "lbYbar";
            this.lbYbar.Size = new System.Drawing.Size(25, 13);
            this.lbYbar.TabIndex = 23;
            this.lbYbar.Text = "333";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(983, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "X";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(983, 72);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 13);
            this.label5.TabIndex = 25;
            this.label5.Text = "Y";
            // 
            // lbPreparingCamera
            // 
            this.lbPreparingCamera.AutoSize = true;
            this.lbPreparingCamera.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lbPreparingCamera.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lbPreparingCamera.Location = new System.Drawing.Point(332, 366);
            this.lbPreparingCamera.Name = "lbPreparingCamera";
            this.lbPreparingCamera.Size = new System.Drawing.Size(324, 25);
            this.lbPreparingCamera.TabIndex = 26;
            this.lbPreparingCamera.Text = "Trwa przygotowywanie kamery...";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(411, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "Widoczność celu:";
            // 
            // lbVisibilityLvl
            // 
            this.lbVisibilityLvl.AutoSize = true;
            this.lbVisibilityLvl.Location = new System.Drawing.Point(506, 118);
            this.lbVisibilityLvl.Name = "lbVisibilityLvl";
            this.lbVisibilityLvl.Size = new System.Drawing.Size(28, 13);
            this.lbVisibilityLvl.TabIndex = 29;
            this.lbVisibilityLvl.Text = "brak";
            // 
            // Form1Surf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(999, 651);
            this.Controls.Add(this.lbVisibilityLvl);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbPreparingCamera);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbYbar);
            this.Controls.Add(this.lbXbar);
            this.Controls.Add(this.trackBar2);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnEngage);
            this.Controls.Add(this.btnEditImg);
            this.Controls.Add(this.btnGetImageToTrack);
            this.Controls.Add(this.ibResult);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1Surf";
            this.Text = "Inteligentny system śledzący - aplikacja obsługi";
            this.Load += new System.EventHandler(this.Form1_Load);
            
            this.Resize += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ibResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblImageToFind;
        private System.Windows.Forms.TextBox txtImageToFind;
        private System.Windows.Forms.Button btnImageToFind;
        private System.Windows.Forms.CheckBox ckDrawKeyPoints;
        private System.Windows.Forms.CheckBox ckDrawMatchingLines;
        private System.Windows.Forms.PictureBox ibResult;
        private System.Windows.Forms.OpenFileDialog ofdImageToFind;
        private System.Windows.Forms.Button btnGetImageToTrack;
        private System.Windows.Forms.Button btnEditImg;
        private System.Windows.Forms.Button btnEngage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lbXbar;
        private System.Windows.Forms.Label lbYbar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbPreparingCamera;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbVisibilityLvl;


    }
}

