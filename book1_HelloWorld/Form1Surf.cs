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
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using Emgu.CV.Util;

using System.Diagnostics;
using System.IO;

using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;

namespace InteligentnySystemSledzacy
{
    public partial class Form1Surf : Form
    {

        public Form1Surf()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;    //stałe rozmiary okna

            init();                     //inicjalizacjia serwomechanizmów
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            prepareProgram();
        }
//..................................................................................................   
//////////zmienne///////////////////////////////
        Capture captureWebcam;
        bool blnWebcamCapturingInProcess = false;   //czy SURF jest dodany na liście zadań aplikacji

        Image<Bgr, Byte> imgSceneColor;             //oryginalna scena w kolorze
        Image<Bgr, Byte> imgToFindColor;            //oryginalny obrazek w kolorze do znalezienia

        Image<Bgr, Byte> imgCopyOfImageToFindWithBorder;        //jako kopia obrazka do odnalezienia żeby można było ramkę mu narysować bez zmiany na oryginalnym

        bool blnImageSceneLoaded = false;                 //czy obrazek sceny załadowany poprawnie
        bool blnImageToFindLoaded = false;                //czy obrazek do znalezienia załadowany poprawnie

        Image<Bgr, Byte> imgResult;                       //rezultat sceny i obrazka do znalezienia powiązane razem,...
                                                          //...wraz z ramką na odnalezionym obrazku, i punkty oraz linie znajdywania

        Bgr bgrKeyPointColor = new Bgr(Color.Blue);         //kolor do rysowania punktów na obrazku rezultacie
        Bgr bgrMatchingLineColor = new Bgr(Color.Green);    //kolor do rysowania lini na obrazku rezultacie
        Bgr bgrFoundImageColor = new Bgr(Color.Red);         //kolor ramki na znalezionym obrazku na scenie

        bool isImgToFind = false;

        SerialPort port;    //port do komunikacji USB z arduino
//..................................................................................................   
        private void prepareProgram()
        {
            imgSceneColor = null;
            imgToFindColor = null;
            imgCopyOfImageToFindWithBorder = null;
            imgResult = null;
            blnImageSceneLoaded = false;
            blnImageToFindLoaded = false;

            txtImageToFind.Text = "";
            ibResult.Image = null;

            try                                     //tworzymy objekt Capture dla kamery
            {
                captureWebcam = new Capture();
            }
            catch (Exception ex)
            {
                this.Text = ex.ToString();
            }
            
            imgToFindColor = null;

            Application.Idle += new EventHandler(DoSURFDetectAndUpdateForm);  //dodajemy funkcję SURF do listy zadań         
            blnWebcamCapturingInProcess = true;                     //update zmiennej
        }
//..................................................................................................   
        private void btnImageToFind_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = ofdImageToFind.ShowDialog();        //przywołaj obrazek aby znaleźć dialog box

            if (dialogResult == System.Windows.Forms.DialogResult.OK || dialogResult == System.Windows.Forms.DialogResult.Yes)   //jak OK lub TAK to napisznazwę pliku w textBoxie
                txtImageToFind.Text = ofdImageToFind.FileName;

            try
            {
                imgToFindColor = new Image<Bgr, byte>(txtImageToFind.Text);     //załaduj obrazek do znalezienia
                imgToFindColor = imgToFindColor.Resize(320, 240, INTER.CV_INTER_CUBIC, true);
            }
            catch (Exception ex)                                        //jak się nie uda
            {
                this.Text = ex.ToString();                          //to pokaż iadomość error na pasku tytułu
            }

            loadImageToFindFromFile();
        }
//..................................................................................................   
        public void loadImageToFindFromFile()
        {
            //kod z wyboru obrazka, wszystko po wybraniu
            isImgToFind = true;
            blnImageToFindLoaded = true;
            imgCopyOfImageToFindWithBorder = imgToFindColor.Copy();     //zrób kopię obrazka do znalezienia
            //narysuj 2pixelową ramkę na kopi obrazka do znalezienia, używając koloru jak box ze znalezionym obrazkiem
            imgCopyOfImageToFindWithBorder.Draw(new Rectangle(1, 1, imgCopyOfImageToFindWithBorder.Width - 3,
                imgCopyOfImageToFindWithBorder.Height - 3), bgrFoundImageColor, 2);

            if (blnImageSceneLoaded == true)            //najpierw musi być scena żeby toFindImg się załadował                                                           //gdy obrazek sceny załadował się...
                ibResult.Image = imgSceneColor.ConcateHorizontal(imgCopyOfImageToFindWithBorder).ToBitmap();    //...połącz obrazek do znalezienia z ramką i scenę...
            else                                                                                                //...pokaż rezultat na pictureBoxie, jak scena się jeszcze nie załadowała...
                ibResult.Image = imgCopyOfImageToFindWithBorder.ToBitmap();                                     //...to pokaż ramkę obrazka do znalezienia, który właśnie załadowano do picBoxu

        }
//..................................................................................................   
        private void txtImageToFind_TextChanged(object sender, EventArgs e)
        {
            txtImageToFind.SelectionStart = txtImageToFind.Text.Length;     //to samo
        }
//..................................................................................................   
        private void btnGetImageToTrack_Click(object sender, EventArgs e)
        {
            imgSceneColor = captureWebcam.QueryFrame();
            imgToFindColor = imgSceneColor.Resize(320, 240, INTER.CV_INTER_CUBIC, true);
            isImgToFind = true;
        }
//..................................................................................................   
        private void ckDrawKeyPoints_CheckedChanged(object sender, EventArgs e)
        {
            if (ckDrawKeyPoints.Checked == false)               //jeśli punkty są odznaczone
            {
                ckDrawMatchingLines.Checked = false;            //odznacz rysowanie lini
                ckDrawMatchingLines.Enabled = false;            //zablokuj checkBox rysowania lini
            }
            else if (ckDrawKeyPoints.Checked == true)           //gdy punkty zostały zaznaczone
                ckDrawMatchingLines.Enabled = true;             //odblokuj checkBox rysowania lini
        }
//..................................................................................................   
        private void ckDrawMatchingLines_CheckedChanged(object sender, EventArgs e)
        {

        }
//..........metoda żeby SURF zrobić........................................................................................   
        public void DoSURFDetectAndUpdateForm(object sender, EventArgs e)
        {
                try
                {
                    imgSceneColor = captureWebcam.QueryFrame();         //try pobrać jedną klatkę z obrazu kamery
                    lbPreparingCamera.Visible = false;
                }
                catch (Exception ex)                                //jak się nie da to error wyświetlamy
                {
                    this.Text = ex.Message;
                }


                if (imgSceneColor == null)
                    this.Text = "error, nie wczytano obrazu z kamery";         //gdy nie odczytano następnej klatki do zmiennej obrazka

                if (imgToFindColor == null)                             //jeśli jeszcze nie mamy obrazka do znalezienia...
                    ibResult.Image = imgSceneColor.ToBitmap();          //...to wywołaj obraz sceny do imageBoxu


       //gdy dotarliśmy aż tutaj, obydwa obrazki są OK i możemy rozpocząć SURF detection

            SURFDetector surfDetector = new SURFDetector(500, false);       //objekt surf, parametr treshold(jak duże punkty bierze pod uwagę i extended flag

            Image<Gray, Byte> imgSceneGray = null;                          //szary obraz sceny
            Image<Gray, Byte> imgToFindGray = null;                         //szary obrazek do znalezienia

            VectorOfKeyPoint vkpSceneKeyPoints;                             //vektor punktów na obrazie sceny
            VectorOfKeyPoint vkpToFindKeyPoints;                            //vektor punktów na obrazku do znalezienia

            Matrix<Single> mtxSceneDescriptors;                         //macierz deskryptorów do pytania o najbliższe sąsiedztwo
            Matrix<Single> mtxToFindDescriptor;                         //macierz deskryptorów dla szukanego obrazka

            Matrix<int> mtxMatchIndices;                                //macierz ze wskaźnikami deskryptorów, będzie wypełniana przy trenowaniu deskryptorów (KnnMatch())
            Matrix<Single> mtxDistance;                                 //macierz z wartościami odległości, po treningu jak wyżej
            Matrix<Byte> mtxMask;                                       //input i output dla funkcji VoteForUniqueness(), wskazującej, który rząd pasuje

            BruteForceMatcher<Single> bruteForceMatcher;                //dla każdego deskryptora w pierwszym zestawie, matcher szuka...
                                                                        //...najbliższego deskryptora w drugim zestawie ustawionym przez trening każdego jednego
            
            HomographyMatrix homographyMatrix = null;                   //dla ProjectPoints() aby ustawić lokalizację znalezionego obrazka w scenie
            int intKNumNearestNeighbors = 2;                            //k, liczba najbliższego sąsiedztwa do przeszukania
            double dblUniquenessThreshold = 0.8;                        //stosunek różncy dystansu dla porównania, żeby wypadło unikalne

            int intNumNonZeroElements;                                  //jako wartość zwracana dla liczby nie-zerowych elementów obu w macierzy maski,...
                                                                        //...także z wywołania GetHomographyMatrixFromMatchedFeatures()

      //parametry do używania przy wywołaniach VoteForSizeAndOrientation()

            double dblScareIncrement = 1.5;                     //określa różnicę w skali dla sąsiadujących komórek
            int intRotationBins = 20;                           //liczba komórek dla rotacji z 360 stopni (jeśli =20 to każda komórka pokrywa 18 stopni (20*18=360))

            double dblRansacReprojectionThreshold = 2.0;        //do użycia z GetHomographyMatrixFromMatchedFeatures(), max. dozwolony błąd odwzorowania...
                                                                //...aby uznać parę punktów za ?inlier? 

            Rectangle rectImageToFind = new Rectangle();                          //prostokąt obejmujący cały obrazek do znalezienia
            PointF [] pointsF;                                  //4 punkty określające ramkę wokół lokacji znalezionego obrazka na scenie (float)
            Point [] points;                                     //4 punkty, to samo, ale (int)

            imgSceneGray = imgSceneColor.Convert<Gray, Byte>();         //ta sama scena do Graya

            if (isImgToFind == true)
            {
                try
                {
                    imgToFindGray = imgToFindColor.Convert<Gray, Byte>();       // obrazek do znalezienia do Graya
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                vkpSceneKeyPoints = surfDetector.DetectKeyPointsRaw(imgSceneGray, null);    //wykrywa punkty w scenie, drugi param. to maska, jeśli null to nie potrzebna
                mtxSceneDescriptors = surfDetector.ComputeDescriptorsRaw(imgSceneGray, null, vkpSceneKeyPoints);    //oblicza deskrptory sceny, param. to obraz sceny...
                //...maska, punkty na scenie

                vkpToFindKeyPoints = surfDetector.DetectKeyPointsRaw(imgToFindGray, null);                          //wykrywa punkty na obrazku do znalezienia, drugi param. to...
                //...maska, null bo nie potrzebna

                mtxToFindDescriptor = surfDetector.ComputeDescriptorsRaw(imgToFindGray, null, vkpToFindKeyPoints);  //oblicza aby znaleźć deskryptory(szukany obrazek, maska, szukanego o. punkty)

                bruteForceMatcher = new BruteForceMatcher<Single>(DistanceType.L2);         //objekt brute force matchera z L2, kwadrat odległ. Euklidesowej
                bruteForceMatcher.Add(mtxToFindDescriptor);                                 //dodaj macierz dla szukanych deskryptorów do brute force matchera

                if (mtxSceneDescriptors != null)    //gdy obraz nie ma cech np. ściana
                {
                    mtxMatchIndices = new Matrix<int>(mtxSceneDescriptors.Rows, intKNumNearestNeighbors);    //objekt macierzy indeksów/komórek (wiersze, kolumny)
                    mtxDistance = new Matrix<Single>(mtxSceneDescriptors.Rows, intKNumNearestNeighbors);    //to samo z dystansami

                    bruteForceMatcher.KnnMatch(mtxSceneDescriptors, mtxMatchIndices, mtxDistance, intKNumNearestNeighbors, null);   //znajduje k-najbliższy match, (jak null to maska nie potrzebna)

                    mtxMask = new Matrix<Byte>(mtxDistance.Rows, 1);        //objekt macierzy maski
                    mtxMask.SetValue(255);                                  //ustawia wartości wszystkich elementów w macierzy maski

                    Features2DToolbox.VoteForUniqueness(mtxDistance, dblUniquenessThreshold, mtxMask);      //filtruje pasujące cechy tj. czy match NIE jest unikalny to jest odrzucany

                    intNumNonZeroElements = CvInvoke.cvCountNonZero(mtxMask);       //pobierz liczbę nie-zerowych elementów w macierzy maski
                    if (intNumNonZeroElements >= 4)
                    {
                        //eliminuje dopasowanye cechy, których skla i rotacja nie zgadzają się ze skalą i rotacją większości
                        intNumNonZeroElements = Features2DToolbox.VoteForSizeAndOrientation(vkpToFindKeyPoints, vkpSceneKeyPoints, mtxMatchIndices, mtxMask, dblScareIncrement, intRotationBins);
                        if (intNumNonZeroElements >= 4)             //jeśli ciągle są co najmniej 4 nie-zerowe elementy

                            //pobierz homography matrix używając RANSAC (random sample consensus)
                            homographyMatrix = Features2DToolbox.GetHomographyMatrixFromMatchedFeatures(vkpToFindKeyPoints, vkpSceneKeyPoints, mtxMatchIndices, mtxMask, dblRansacReprojectionThreshold);

                    }

                    imgCopyOfImageToFindWithBorder = imgToFindColor.Copy();     //robi kopię obrazka do znalezienia aby na tej kopi rysować, bez zmieniania oryginalnego obrazka

                    //rysuje 2pix ramkę wkoło kopi obrazka do znalezienia, używając takiego samego koloru jaki ma box na znaleziony obrazek
                    imgCopyOfImageToFindWithBorder.Draw(new Rectangle(1, 1, imgCopyOfImageToFindWithBorder.Width - 3, imgCopyOfImageToFindWithBorder.Height - 3), bgrFoundImageColor, 2);

                    //rysowanie obrazu sceny i obrazka do znalezienia razem na obrazie rezultatu
                    //3 warunki w zależności od tego, który checkBox jest zaznaczony (rysuj punkty i/lub rysuj linie)
                    if (ckDrawKeyPoints.Checked == true && ckDrawMatchingLines.Checked == true)
                    {
                        //używa DrawMatches() aby połączyć obraz sceny z obrazkiem do znalezienia, potem rysuje punkty i linie
                        imgResult = Features2DToolbox.DrawMatches(imgCopyOfImageToFindWithBorder,
                                                                    vkpToFindKeyPoints,
                                                                    imgSceneColor,
                                                                    vkpSceneKeyPoints,
                                                                    mtxMatchIndices,
                                                                    bgrMatchingLineColor,
                                                                    bgrKeyPointColor,
                                                                    mtxMask,
                                                                    Features2DToolbox.KeypointDrawType.DEFAULT);
                    }
                    else if (ckDrawKeyPoints.Checked == true && ckDrawMatchingLines.Checked == false)
                    {
                        //rysuje scenę z punktami na obrazie rezultatu
                        imgResult = Features2DToolbox.DrawKeypoints(imgSceneColor,
                                                                    vkpSceneKeyPoints,
                                                                    bgrKeyPointColor,
                                                                    Features2DToolbox.KeypointDrawType.DEFAULT);
                        //potem rysuje punkty na kopi obrazka do znalezienia
                        imgCopyOfImageToFindWithBorder = Features2DToolbox.DrawKeypoints(imgCopyOfImageToFindWithBorder,
                                                                                            vkpToFindKeyPoints,
                                                                                            bgrKeyPointColor,
                                                                                            Features2DToolbox.KeypointDrawType.DEFAULT);
                        //potem łączy kopię obrazka do znaleienia na obrazie rezultatu
                        imgResult = imgResult.ConcateHorizontal(imgCopyOfImageToFindWithBorder);
                    }
                    else if (ckDrawKeyPoints.Checked == false && ckDrawMatchingLines.Checked == false)
                    {
                        imgResult = imgSceneColor;                                                  //dołącza scenę do obrazu rezultatu
                        imgResult = imgResult.ConcateHorizontal(imgCopyOfImageToFindWithBorder);    //wiąże kopię szukanego obrazka na obrazie rezultatu
                    }
                    else MessageBox.Show("Błąd");    //tu już nie powinno nigdy dojść


                }
                else
                {
                    imgResult = imgSceneColor;                                                  //dołącza scenę do obrazu rezultatu
                    imgResult = imgResult.ConcateHorizontal(imgCopyOfImageToFindWithBorder);    //wiąże kopię szukanego obrazka na obrazie rezultatu
                }

                if (homographyMatrix != null)    //sprawdzanie czy na pewno coś w tej macierzy jest
                {
                    //rysuje ramkę na kawałku sceny z obrazu rezultatu, w miejscu gdzie jest znaleziony szukany obrazek
                    rectImageToFind.X = 0;          //na starcie ustawia rozmiar prostokąta na pełny rozmiar obrazka do znalezienia
                    rectImageToFind.Y = 0;
                    rectImageToFind.Width = imgToFindGray.Width;
                    rectImageToFind.Height = imgToFindGray.Height;

                    //tworzymy obiekt -> array (szereg) tablica na PointF odpowiadające prostokątom
                    pointsF = new PointF[] {new PointF(rectImageToFind.Left, rectImageToFind.Top),
                                            new PointF(rectImageToFind.Right, rectImageToFind.Top),
                                            new PointF(rectImageToFind.Right, rectImageToFind.Bottom),
                                            new PointF(rectImageToFind.Left, rectImageToFind.Bottom)};

                    //ProjectionPoints() ustawia ptfPointsF(przez referencję) na bycie lokacją ramki na fragmencie sceny gdzie jest znaleziony szukany obrazek
                    homographyMatrix.ProjectPoints(pointsF);

                    //konwersja z PointF() do Point() bo ProjectPoints() używa typ PointF() a DrawPolyline() używa Point()
                    points = new Point[] {Point.Round(pointsF[0]),
                                        Point.Round(pointsF[1]),
                                        Point.Round(pointsF[2]),
                                        Point.Round(pointsF[3])};

                    //rysowanie ramki wkoło znalezionego obrazka na fragmencie sceny obrazu rezultatu
                    imgResult.DrawPolyline(points, true, new Bgr(0, 255, 0), 2);

                    //rysowanie czerwonego myślnika na środku obiektu
                    int x, y, x1, y1, xW, yW;

                    x = Convert.ToInt32(points[0].X);
                    y = Convert.ToInt32(points[0].Y);
                    x1 = Convert.ToInt32(points[2].X);
                    y1 = Convert.ToInt32(points[2].Y);

                    xW = x1 - x;
                    xW /= 2;
                    xW += x;
                    yW = y1 - y;
                    yW /= 2;
                    yW += y;
                    Point [] pp = new Point[] { new Point(xW, yW), new Point(xW + 10, yW) };    //rysowanie środka wykrytego obiektu
                    imgResult.DrawPolyline(pp, true, new Bgr(0,0,255), 5);

                    XX = xW.ToString();
                    YY = yW.ToString();
                    //////////gdy obiekt znika z pola widzenia
                    if (xW == 0 || yW == 0 || xW < -200 || yW < -200 || xW > 800|| yW > 800)
                        targetLost(-1);
                    else
                        targetLost(1);
                    //////////
                }
                else targetLost(-1);    //strzał w 10!

                //koniec SURF, update całego form

                ibResult.Image = imgResult.ToBitmap();          //pokazanie rezultatu na imageBoxie
            }         
        }
//..................................................................................................        
        private void btnEditImg_Click(object sender, EventArgs e)
        {
            if (isImgToFind == true)
                new Form2EditImg(this, imgToFindColor).Show();
            else MessageBox.Show("Nie wybrano obrazu obiektu do śledzenia");
        }
//..................................................................................................        
        public void replaceImg()
        {
            imgToFindColor = imgEdited;
            loadImageToFindFromFile();
        }
//.............zmienne nr 2.....................................................................................
        public Image<Bgr, byte> imgEdited;  //obrazek otrzymany z drugiego Formu po edycji
        public string XX = "";                      //współrzędne dla ruchu serwomechanizmów (liczba w string)
        public string YY = "";

        bool engaged = false;       //czy włączono funkcje tractTarget i właśnie śledzenie trwa      
        int positionTreshold = 12;      //próg dokładności określający jak blisko środka obrazu obiekt ma się znajdować
        int changeScrollValue = 1;      //wielkość o jaką przemieszcza się trackBar
        int[] trackingArray = new int[10];  //tablica wskazująca czy obiekt jest w polu widzenia
        int Itracking = 0;                  //licznik do tablicy
        bool isTargetLost = false;           //wskazuje czy nastąpiła utrata celu po jego wyznaczeniu
        Stopwatch stopwatch = new Stopwatch();
//..................................................................................................        
//...............ruch kamery........................................................................        

        private void init()             //inicjalizacja komunikacji przez USb z arduino
        {
            port = new SerialPort();
            port.PortName = "COM11";
            port.BaudRate = 9600;

            try
            {
                port.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            string i = "90";                //pozycja początkowa kamery na środku
            port.WriteLine("servo1 " + i);
            port.WriteLine("servo2 " + i);
            trackBar1.Value = 90;
            trackBar2.Value = 90;
            lbXbar.Text = trackBar1.Value.ToString();
            lbYbar.Text = trackBar2.Value.ToString();
        }
//...............................................................................................        
        private void btnEngage_Click(object sender, EventArgs e) 
        {
            if (isImgToFind == true)
            {
                if (engaged == false)
                {
                    Application.Idle += new EventHandler(trackTarget);
                    engaged = true;
                    btnEngage.Text = "Przestań namierzać";
                }
                else if (engaged == true)
                {
                    Application.Idle -= trackTarget;
                    engaged = false;
                    btnEngage.Text = "Namierzaj";
                }
            }
        }
//..................................................................................................
    public void trackTarget(object sender, EventArgs e)     //podążanie kamery za celem
        {
            label1.Text = "Pozycja wykrytego obiektu: " + XX + " i " + YY; //jakieś to coordy są rectangla
            int midScreenY = imgSceneColor.Height / 2;  //wyznaczenie środka obrazu sceny
            int midScreenX = imgSceneColor.Width / 2;

            if (trackBar1.Value < 179 && trackBar1.Value > 2 && trackBar2.Value < 179 && trackBar2.Value > 2 && isTargetLost == false) //179 i 2 bo jak changeScrollValue się zwiększa
        {
                int XXx = Convert.ToInt32(XX);
                int YYy = Convert.ToInt32(YY);
                
                //zmiana szybkości przemieszczania gdy obiekt jest blisko środka obrazu
                if (Math.Abs(XXx - midScreenX) > 100 || Math.Abs(YYy - midScreenY) > 100)
                    changeScrollValue = 2;
                else
                    changeScrollValue = 1;

                if (XXx < (midScreenX - positionTreshold))         //centrowanie kamery na szukanym obrazku
                    trackBar1.Value += changeScrollValue;

                else if (XXx > (midScreenX + positionTreshold))    //próg dokładności
                    trackBar1.Value -= changeScrollValue;

                if (YYy < (midScreenY - positionTreshold))         //
                    trackBar2.Value += changeScrollValue;

                else if (YYy > (midScreenY + positionTreshold))    //
                    trackBar2.Value -= changeScrollValue;

                XX = XXx.ToString();
                string valu = "servo1 " + trackBar1.Value.ToString();
                port.WriteLine(valu);
                YY = YYy.ToString();
                valu = "servo2 " + trackBar2.Value.ToString();
                port.WriteLine(valu);
                lbXbar.Text = trackBar1.Value.ToString();
                lbYbar.Text = trackBar2.Value.ToString();

                if (trackBar1.Value > 170 || trackBar1.Value < 10 || trackBar2.Value > 170 || trackBar2.Value < 10 || isTargetLost == true)
                {
                    trackBar1.Value = 90;
                    port.WriteLine("servo1 " + 90);
                    trackBar2.Value = 90;
                    port.WriteLine("servo2 " + 90);
                }

        }

        }
//..................................................................................................        
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
                string valu = "servo1 " + trackBar1.Value.ToString();
                port.WriteLine(valu);
                lbXbar.Text = trackBar1.Value.ToString();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
                string valu = "servo2 " + trackBar2.Value.ToString();
                port.WriteLine(valu);
                lbYbar.Text = trackBar2.Value.ToString();
        }
//..................................................................................................        
        public void targetLost(int targetVisibility)    //-1 gdy nie ma celu/ 1 gdy cel jest
        {
            trackingArray[Itracking] = targetVisibility;
            Itracking += 1;
            if (Itracking == 10)
                Itracking = 0;

            int targetLostSigns = 0;
            for(int j = 0; j < 10; j++)
            {
                if (trackingArray[j] == -1)
                    targetLostSigns += 1;
            }
            if (targetLostSigns >= 5 && engaged == true)
            {
                isTargetLost = true;
            }
            else
                isTargetLost = false;

            switch(targetLostSigns)
            {
                case 0: lbVisibilityLvl.Text = "znakomita";
                    break;
                case 1: lbVisibilityLvl.Text = "bardzo dobra";
                    break;
                case 2: lbVisibilityLvl.Text = "dobra";
                    break;
                case 3: lbVisibilityLvl.Text = "średnia";
                    break;
                case 4: lbVisibilityLvl.Text = "słaba";
                    break;
                case 5: lbVisibilityLvl.Text = "bardzo słaba";
                    break;
                case 6: lbVisibilityLvl.Text = "bardzo zła";
                    break;
                default: lbVisibilityLvl.Text = "cel utracony";
                    break;
            }
            //jak tego nie będzie też tutaj to utracenie celu powoduje tylko zatrzymanie a nie powrót na środek kamery
            if (trackBar1.Value > 170 || trackBar1.Value < 10 || trackBar2.Value > 170 || trackBar2.Value < 10 || isTargetLost == true)
            {
                trackBar1.Value = 90;
                port.WriteLine("servo1 " + 90);
                trackBar2.Value = 90;
                port.WriteLine("servo2 " + 90);
            }

        }
//..................................................................................................        
    }
}