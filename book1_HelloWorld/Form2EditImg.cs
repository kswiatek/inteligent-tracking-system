using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System.Diagnostics;

using Emgu.CV.Features2D;
using Emgu.CV.UI;
using Emgu.CV.Util;

namespace InteligentnySystemSledzacy
{
    public partial class Form2EditImg : Form
    {
        Form1Surf frmS2;  //tym posługujemy się gdy chcemy coś zmienić w formie frmSURF

        public Form2EditImg(Form1Surf frmS, Image<Bgr, Byte> imgToFindFromfrmSURF)  //żeby w tamtym formie zmieniać
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;    //stałe rozmiary okna

            frmS2 = frmS;           
            localImgToFind = imgToFindFromfrmSURF;   //localImgToFind staje się obrazkiem z tamtego forma
        }

        private void EditImgFrm_Load(object sender, EventArgs e)
        {
            localImgToFindCopy = localImgToFind;                    //zrobienie kopi lokalnie obrazka do znalezienia
            picBoxImgToFind.Image = localImgToFind.ToBitmap();
        }

//-------------------zmienne----------------------------------------------------------------------
        public Image<Bgr, byte> localImgToFind;    //obrazek do znalezienia lokalnie w tym formie
        private Image<Bgr, byte> localImgToFindCopy;    //kopia do podmiany
        private Point rectStartPoint;
        private Rectangle rectangle = new Rectangle();      ///prostokąt rysowany na obrazie nr1 wejściowym, kursorem myszy
        private Brush selectBrush = new SolidBrush(Color.FromArgb(128, 64, 64, 64));
        private int thickness = 3;
//-------------------------------------------------------------------------------------------------
        private void btnSaveImg_Click(object sender, EventArgs e)
        {
            frmS2.imgEdited = localImgToFind;   //imgEdited w tamtym formie jest podmieniany na ten stąd
            frmS2.replaceImg();                //funkcja podmieniająca w tamtym formie
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
//-----------------------------------------------------------------------------------------
        private void pictureBox_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //definicja początkowych współrzędnych prostokąta
            rectStartPoint = e.Location;
            Invalidate();
        }
//-----------------------------------------------------------------------------------------
        private void pictureBox_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //ustawianie współrzędnych na picBoxie1
            lbCoordinates.Text = "pozycja: X:" + e.X + " Y:" + e.Y;    //współrzędne myszy

            if (e.Button != MouseButtons.Left)  //jeśli wciśnięty klawisz jest inny niż LPM to przerwij przez return;
                return;

            Point tempEndPoint = e.Location;    //tymczasowy punkt końca prostokątu to aktualna pozycja myszy
            rectangle.Location = new Point(
                Math.Min(rectStartPoint.X, tempEndPoint.X), Math.Min(rectStartPoint.Y, tempEndPoint.Y));    //wart. mniejsza z tych dwóch aby określić początek
            rectangle.Size = new Size(
                Math.Abs(rectStartPoint.X - tempEndPoint.X), Math.Abs(rectStartPoint.Y - tempEndPoint.Y));  //wartość bezwzględna - długość boków
            ////////////////////////////////////////
            //współrzędne na obrazku - tworzenie ROI

            localImgToFind = new Image<Bgr, byte>(localImgToFindCopy.Bitmap);   //jeszcze raz to samo z kopii(musi być new image a nie sama zmienna)
            localImgToFind.Draw(rectangle, new Bgr(Color.Red), thickness);
            
            //test prostokąta na picBoxie1 a nie 2
            picBoxImgToFind.Image = localImgToFind.ToBitmap();

            ((PictureBox)sender).Invalidate();
        }

//-----------------------------------------------------------------------------------------
        private void pictureBox_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            //rysujamy kwadrat
            if (picBoxImgToFind.Image != null)
            {
                if (rectangle != null && rectangle.Width > 0 && rectangle.Height > 0)
                {
                    //wybieranie ROI
                    e.Graphics.SetClip(rectangle, System.Drawing.Drawing2D.CombineMode.Exclude);
                    e.Graphics.FillRectangle(selectBrush, new Rectangle(0, 0, ((PictureBox)sender).Width, ((PictureBox)sender).Height));
                }
            }
        }
//-----------------------------------------------------------------------------------------
        private void pictureBox_MouseUp(object sender, MouseEventArgs e)    //przy końcu zaznaczania
        {
            //Definiujemy ROI
            if (rectangle.Width > 0 && rectangle.Height > 0)
            {
                localImgToFind.ROI = rectangle;
                picBoxImgToFindEdited.Image = localImgToFind.ToBitmap();
            }
        }
//-----------------------------------------------------------------------------------------
        private void pictureBox_MouseDoubleClick(object sender, MouseEventArgs e)   //żeby zaznaczenie wyczyścić
        {
            rectangle = new Rectangle();
            ((PictureBox)sender).Invalidate();
        }
//-----------------------------------------------------------------------------------------
    }
}
