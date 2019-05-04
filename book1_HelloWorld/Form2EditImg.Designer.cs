namespace InteligentnySystemSledzacy
{
    partial class Form2EditImg
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
            this.picBoxImgToFind = new System.Windows.Forms.PictureBox();
            this.btnSaveImg = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.picBoxImgToFindEdited = new System.Windows.Forms.PictureBox();
            this.lbCoordinates = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxImgToFind)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxImgToFindEdited)).BeginInit();
            this.SuspendLayout();
            // 
            // picBoxImgToFind
            // 
            this.picBoxImgToFind.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBoxImgToFind.Cursor = System.Windows.Forms.Cursors.Cross;
            this.picBoxImgToFind.Location = new System.Drawing.Point(34, 21);
            this.picBoxImgToFind.Name = "picBoxImgToFind";
            this.picBoxImgToFind.Size = new System.Drawing.Size(320, 240);
            this.picBoxImgToFind.TabIndex = 0;
            this.picBoxImgToFind.TabStop = false;
            this.picBoxImgToFind.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
            this.picBoxImgToFind.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.picBoxImgToFind.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            this.picBoxImgToFind.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // btnSaveImg
            // 
            this.btnSaveImg.Location = new System.Drawing.Point(34, 288);
            this.btnSaveImg.Name = "btnSaveImg";
            this.btnSaveImg.Size = new System.Drawing.Size(152, 23);
            this.btnSaveImg.TabIndex = 1;
            this.btnSaveImg.Text = "Zapisz zmiany";
            this.btnSaveImg.UseVisualStyleBackColor = true;
            this.btnSaveImg.Click += new System.EventHandler(this.btnSaveImg_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(202, 288);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(152, 23);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "Porzuć zmiany";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // picBoxImgToFindEdited
            // 
            this.picBoxImgToFindEdited.BackColor = System.Drawing.SystemColors.Menu;
            this.picBoxImgToFindEdited.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picBoxImgToFindEdited.Location = new System.Drawing.Point(373, 21);
            this.picBoxImgToFindEdited.Name = "picBoxImgToFindEdited";
            this.picBoxImgToFindEdited.Size = new System.Drawing.Size(320, 240);
            this.picBoxImgToFindEdited.TabIndex = 3;
            this.picBoxImgToFindEdited.TabStop = false;
            // 
            // lbCoordinates
            // 
            this.lbCoordinates.AutoSize = true;
            this.lbCoordinates.Location = new System.Drawing.Point(404, 293);
            this.lbCoordinates.Name = "lbCoordinates";
            this.lbCoordinates.Size = new System.Drawing.Size(35, 13);
            this.lbCoordinates.TabIndex = 4;
            this.lbCoordinates.Text = "label1";
            // 
            // Form2EditImg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 336);
            this.Controls.Add(this.lbCoordinates);
            this.Controls.Add(this.picBoxImgToFindEdited);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSaveImg);
            this.Controls.Add(this.picBoxImgToFind);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form2EditImg";
            this.Text = "Edycja obrazu";
            this.Load += new System.EventHandler(this.EditImgFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxImgToFind)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxImgToFindEdited)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picBoxImgToFind;
        private System.Windows.Forms.Button btnSaveImg;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.PictureBox picBoxImgToFindEdited;
        private System.Windows.Forms.Label lbCoordinates;
    }
}