using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace Simplex
{
    public partial class Form1 : Form
    {
        public double[] c;
        public double[,] A;
        public double[] b;
        public string[] yiyecek=new string[] { "Alabalik", "Dana eti", "Tavuk eti", "Fasulye", "Mercimek", "Ceviz", "Çilek", "Portakal", "Elma", "Biber", "Domates", "Patates", "Peynir", "Yumurta" };
        public double[] yiyecekfiyatları = new double[] { 22, 43, 13, 3, 3,47, 6, 4, 2,4, 3, 2, 17, 9 };

        public string[] besin=new string[] {"Kalori","Protein","Karbonhidrat","Yağ","VitaminA","VitaminB","VitaminC","Lif","Kalsiyum"};
        public double[] besindegerleri = new double[] {3000, 200, 500, 100, 1000, 3,90, 50, 1200 };

        public int[,] katsayılar;
        public int yiyeceksayisi = 14;
        public int besinmaddesisayisi = 9;
        public string txtresult;
        DataTable dtbl = new DataTable();
        DataTable dtbl2 = new DataTable();
        DataTable dtbl3 = new DataTable();
        




        private static String InsertTableInRichTextBox(DataTable dtbl, int width)
        {
            //Since too much string appending go for string builder
            StringBuilder sringTableRtf = new StringBuilder();

            //beginning of rich text format,dont customize this begining line
            sringTableRtf.Append(@"{\rtf1 ");

            //create 5 rows with 3 cells each
            int cellWidth;

            //Start the Row
            sringTableRtf.Append(@"\trowd");

            //Populate the Table header from DataTable column headings.
            for (int j = 0; j < dtbl.Columns.Count; j++)
            {
                //A cell with width 1000.
                sringTableRtf.Append(@"\cellx" + ((j + 1) * width).ToString());

                if (j == 0)
                    sringTableRtf.Append(@"\intbl  " + dtbl.Columns[j].ColumnName);
                else
                    sringTableRtf.Append(@"\cell   " + dtbl.Columns[j].ColumnName);
            }

            //Add the table header row
            sringTableRtf.Append(@"\intbl \cell \row");

            //Loop to populate the table cell data from DataTable
            for (int i = 0; i < dtbl.Rows.Count; i++)
            {
                //Start the Row
                sringTableRtf.Append(@"\trowd");

                for (int j = 0; j < dtbl.Columns.Count; j++)
                {
                    cellWidth = (j + 1) * width;

                    //A cell with width 1000.
                    sringTableRtf.Append(@"\cellx" + cellWidth.ToString());

                    if (j == 0)
                        sringTableRtf.Append(@"\intbl  " + dtbl.Rows[i][j].ToString());
                    else
                        sringTableRtf.Append(@"\cell   " + dtbl.Rows[i][j].ToString());
                }

                //Insert data row
                sringTableRtf.Append(@"\intbl \cell \row");
            }

            sringTableRtf.Append(@"\pard");
            sringTableRtf.Append(@"}");

            //convert the string builder to string
            return sringTableRtf.ToString();
        }

        public Form1()
        {
            InitializeComponent();
            
            katsayılar = new int[9, 14] { { 168,223,215,338,340,598,37,49,58,22,22,76,289,158 }, {18,18,14,25,25,19,1,1,0,1,1,2,22,12 }, {10,16,11,1,1,54,1,0,0,0, 0,0,21,11 }, {0,0,0,58,60,20,8,12,14,5,5,17,0,1 }, { 750, 900,92,3,1,2,0,80,50,150,200,600,50,127}, {1,2,0,0,0,0,0,0,0,0,0,0,1,1}, {15,36,13,4,1,1,70,50,6,200,20,10,2,6 }, {0,0,0,3,2,8,4,2,2,2,2,4,0,0 },{250,8,9,37,100,250,3,7,5,20,20,45,300,290 }};
            


            dtbl.Columns.Add("           X", typeof(string));
            for (int f = 0, er = 0; f < yiyeceksayisi; f++, er++)
            {
                dtbl.Columns.Add(yiyecek[f],typeof(double));
            }
            //Here we add five DataRows.

            
            for (int a = 0; a <besinmaddesisayisi; a++)
            {
                dtbl.Rows.Add();
                dtbl.Rows[a][0]=besin[a];
                for (int ret = 0; ret <yiyeceksayisi; ret++)
                {
                    dtbl.Rows[a][ret+1] = katsayılar[a, ret];
                }
            }



            ////
            richTextBox3.Rtf = InsertTableInRichTextBox(dtbl, 1200);


        }

        public void ekran(int i)
        {

            
            if(i==0) { // yeni yiyecek ekleme durumu
                dtbl.Columns.Add(yiyecek[yiyeceksayisi - 1], typeof(double));
                
                for (int a = 0; a < besinmaddesisayisi; a++)
                {
                    
                    dtbl.Rows[a][0] = besin[a];
                    for (int ret = 0; ret < yiyeceksayisi; ret++)
                    {
                        dtbl.Rows[a][ret + 1] = katsayılar[a, ret];
                    }
                }
                richTextBox3.Rtf = InsertTableInRichTextBox(dtbl, 1000);

                richTextBox6.Text = "";

                for (int u = 0; u < yiyeceksayisi; u++) richTextBox6.Text += yiyecek[u] + "\n";

                richTextBox1.Text = "";

                for (int u = 0; u < yiyeceksayisi; u++) richTextBox1.Text += yiyecekfiyatları[u] + "\n";

            }

            if (i == 1) // yiyecek silme durumu
            {
                richTextBox3.Rtf = InsertTableInRichTextBox(dtbl, 1000);

                richTextBox6.Text = "";

                for (int a = 0; a < yiyeceksayisi; a++) richTextBox6.Text += yiyecek[a] + "\n";

                richTextBox1.Text = "";

                for (int a = 0; a < yiyeceksayisi; a++) richTextBox1.Text += yiyecekfiyatları[a] + "\n";


            }
            if (i == 2) // besin silme durumu
            {
                richTextBox3.Rtf = InsertTableInRichTextBox(dtbl, 1000);

                richTextBox7.Text = "";

                for (int a = 0; a < besinmaddesisayisi; a++) richTextBox7.Text += besin[a] + "\n";

                richTextBox2.Text = "";

                for (int a = 0; a < besinmaddesisayisi; a++) richTextBox2.Text += besindegerleri[a] + "\n";


            }

            if(i==3) // besin ekleme durumu
            {

                dtbl.Rows.Add();


                int a = besinmaddesisayisi-1;
                dtbl.Rows[a][0] = besin[besinmaddesisayisi - 1];  
                    for (int ret = 0; ret < yiyeceksayisi; ret++)
                    {
                        dtbl.Rows[a][ret + 1] = katsayılar[a, ret];
                    }
                
                richTextBox3.Rtf = InsertTableInRichTextBox(dtbl, 1000);

                richTextBox7.Text = "";

                for (int u = 0; u < besinmaddesisayisi; u++) richTextBox7.Text += besin[u] + "\n";

                richTextBox2.Text = "";

                for (int u = 0; u < besinmaddesisayisi; u++) richTextBox2.Text += besindegerleri[u] + "\n";

            }

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            int vars = yiyeceksayisi;
            int constraints = besinmaddesisayisi;
            this.c = new double[vars];
            this.A = new double[constraints, vars];
            this.b = new double[constraints];



            for (int i = 0; i < vars; i++)
            {
                c[i] = Int32.Parse(richTextBox1.Lines[i]);
            }


            for (int i = 0; i < constraints; i++)
                b[i] = Int32.Parse(richTextBox2.Lines[i]);
            int line = 0;
            for (int i = 0; i < constraints; i++)
                for (int k = 0; k < vars; k++, line++)
                {
                    int a = katsayılar[i,k];
                    A[i, k] = a;
                }
            var max = new Simplex(A, b, c,yiyecek,besin); // a besin değerleri tablosu b temel besinmaddeleri c ise yiyecek fiyatları
            var cevap = max.optimize(this);



            //var max = new Simplex(
            //    // kar fonksiyonu maksimizasyonu z=5x +9y çünkü cins değişkeni 0
            //    new[,]{
            //        { 1.0,1,1 }, // katsayılar
            //        { 0,1,2 }, 
            //        { -1,2,2 }
            //},
            //    new double[] { 6, 8, 4 }, new[] { 2.0,10, 8 }// kısıtlamalar
            //);
            //var answer = max.optimize(this);

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {

            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox4.Text = "";
            richTextBox8.Text = "";
            richTextBox5.Text = "";
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click_1(object sender, EventArgs e) // YENİ YİYECEK EKLER
        {
            yiyeceksayisi++;

            string[] temp = new string [yiyeceksayisi-1];
            Array.Copy(yiyecek, temp, yiyeceksayisi - 1);
            yiyecek = new string[yiyeceksayisi];
            Array.Copy(temp, yiyecek, yiyeceksayisi - 1);
            ////////////////
            double[] temp4 = new double[yiyeceksayisi - 1];
            Array.Copy(yiyecekfiyatları, temp4, yiyeceksayisi - 1);
            yiyecekfiyatları = new double[yiyeceksayisi];
            Array.Copy(temp4, yiyecekfiyatları, yiyeceksayisi - 1);
            /////


            // increase the length of jag_array by one
            var old_jag_array = katsayılar; // store a reference to the smaller array
            katsayılar = new int[besinmaddesisayisi,yiyeceksayisi]; // create the new, larger array
            for (int i = 0; i < yiyeceksayisi-1; i++)
            {
                for (int k=0;k<besinmaddesisayisi;k++)
                {
                    katsayılar[k,i] = old_jag_array[k,i];
                }
                 
            }
            


            //////
            Form2 testDialog = new Form2();
            testDialog.label1.Text = "Yiyecek ismini girin";
            testDialog.ShowDialog();
            
            yiyecek[yiyeceksayisi - 1] = testDialog.textBox1.Text;
            testDialog.Dispose();

            testDialog = new Form2();
            testDialog.label1.Text = "Yiyecek fiyatını girin";
            testDialog.ShowDialog();

            yiyecekfiyatları[yiyeceksayisi - 1] = double.Parse(testDialog.textBox1.Text);
            testDialog.Dispose();



            for (int q=0;q<besinmaddesisayisi;q++)
            {
                Form2 testDialog2 = new Form2();
                
                testDialog2.label1.Text = besin[q]+" miktarini girin";
                testDialog2.ShowDialog();

                int t;
                try
                {
                     t = Int32.Parse(testDialog2.textBox1.Text);
                    
                }
                catch (Exception hataTuru)
                {
                    MessageBox.Show("Hatalı değer girdiniz. Lütfen tekrar deneyin","Hata");
                    q--;
                    testDialog2.Dispose();
                    continue;
                }

                katsayılar[q, yiyeceksayisi - 1] = t;



                testDialog2.Dispose();
            }
            ekran(0);


        }

        private void button3_Click(object sender, EventArgs e) // yiyecek siler
        {
            yiyeceksayisi--;
            

            int a;

            while (true)
            {
                Form2 testDialog2 = new Form2();

                testDialog2.label1.Text = "silinecek yiyeceğin sırasını söyleyin\nilk yiyecek 0. sıradadır";
                testDialog2.ShowDialog();

                try
                {
                    a = Int32.Parse(testDialog2.textBox1.Text);
                }
                catch (Exception hataTuru)
                {
                    MessageBox.Show("lütfen doğru bir değer girin","HATA");
                    testDialog2.Dispose();
                    continue;
                }
                testDialog2.Dispose();
                break;
            }
            

            
            dtbl.Columns.RemoveAt(a+1);
            // katsayılar dizisi

            var old_jag_array = katsayılar; // store a reference to the smaller array
            katsayılar = new int[besinmaddesisayisi, yiyeceksayisi]; // create the new, larger array
            for (int i = 0,c=0; i < yiyeceksayisi; c++,i++)
            {
                if (i == a) c++;
                for (int k = 0; k < besinmaddesisayisi; k++)
                {
                    katsayılar[k, i] = old_jag_array[k, c];
                }

            }
             //  yiyecek dizisi

            string[] temp = new string[yiyeceksayisi +1];
            Array.Copy(yiyecek, temp, yiyeceksayisi+1);
            yiyecek = new string[yiyeceksayisi];
            Array.Copy(temp, yiyecek, a);
            Array.Copy(temp,a+1, yiyecek, a,yiyeceksayisi-a);
            // yiyecek fiyat dizisi

            double[] temp2 = new double[yiyeceksayisi + 1];
            Array.Copy(yiyecekfiyatları, temp2, yiyeceksayisi + 1);
            yiyecekfiyatları = new double[yiyeceksayisi];
            Array.Copy(temp2, yiyecekfiyatları, a);
            Array.Copy(temp2, a + 1, yiyecekfiyatları, a, yiyeceksayisi - a);

            
            ekran(1);
        }

        private void button5_Click(object sender, EventArgs e) // besin maddesi ekler
        {
            besinmaddesisayisi++;

            string[] temp = new string[besinmaddesisayisi - 1];
            Array.Copy(besin, temp, besinmaddesisayisi - 1);
            besin = new string[besinmaddesisayisi];
            Array.Copy(temp, besin, besinmaddesisayisi - 1);
            ////////////////
            double[] temp4 = new double[besinmaddesisayisi - 1];
            Array.Copy(besindegerleri, temp4, besinmaddesisayisi - 1);
            besindegerleri = new double[besinmaddesisayisi];
            Array.Copy(temp4, besindegerleri, besinmaddesisayisi - 1);
            /////


            // increase the length of jag_array by one
            var old_jag_array = katsayılar; // store a reference to the smaller array
            katsayılar = new int[besinmaddesisayisi, yiyeceksayisi]; // create the new, larger array
            for (int i = 0; i < yiyeceksayisi ; i++)
            {
                for (int k = 0; k < besinmaddesisayisi-1; k++)
                {
                    katsayılar[k, i] = old_jag_array[k, i];
                }

            }



            //////
            Form2 testDialog = new Form2();
            testDialog.label1.Text = "besin ismini girin";
            testDialog.ShowDialog();

            besin[besinmaddesisayisi - 1] = testDialog.textBox1.Text;
            testDialog.Dispose();

            testDialog = new Form2();
            testDialog.label1.Text = "Bu besin için günlük alınması gereken\n minumum değeri girin";
            testDialog.ShowDialog();

            besindegerleri[besinmaddesisayisi - 1] = double.Parse(testDialog.textBox1.Text);
            testDialog.Dispose();



            for (int q = 0; q < yiyeceksayisi; q++)
            {
                Form2 testDialog2 = new Form2();

                testDialog2.label1.Text = yiyecek[q] + "nin içerdiği miktarini girin";
                testDialog2.ShowDialog();

                int t;
                try
                {
                    t = Int32.Parse(testDialog2.textBox1.Text);

                }
                catch (Exception hataTuru)
                {
                    MessageBox.Show("Hatalı değer girdiniz. Lütfen tekrar deneyin", "Hata");
                    q--;
                    testDialog2.Dispose();
                    continue;
                }

                katsayılar[besinmaddesisayisi-1, q] = t;



                testDialog2.Dispose();
            }
            ekran(3);
        }

        private void button6_Click(object sender, EventArgs e) // besin maddesi siler
        {
            besinmaddesisayisi--;
            Form2 testDialog2 = new Form2();

            testDialog2.label1.Text = "silinecek besin değerinin sırasını söyleyin\nilk besin değeri 0. sıradadır";
            testDialog2.ShowDialog();
            int a = Int32.Parse(testDialog2.textBox1.Text);
            dtbl.Rows.RemoveAt(a);

            var old_jag_array = katsayılar; // 
            katsayılar = new int[besinmaddesisayisi, yiyeceksayisi]; // 
            for (int i = 0, c = 0; i < besinmaddesisayisi; c++, i++)
            {
                if (i == a) c++;
                for (int k = 0; k < yiyeceksayisi; k++)
                {
                    katsayılar[i, k] = old_jag_array[c,k];
                }

            }


            string[] temp = new string[besinmaddesisayisi + 1];
            Array.Copy(besin, temp,besinmaddesisayisi + 1);
            besin = new string[besinmaddesisayisi];
            Array.Copy(temp, besin, a);
            Array.Copy(temp, a + 1, besin, a, besinmaddesisayisi - a);

            double[] temp2 = new double[besinmaddesisayisi + 1];
            Array.Copy(besindegerleri, temp2, besinmaddesisayisi + 1);
            besindegerleri = new double[besinmaddesisayisi];
            Array.Copy(temp2, besindegerleri, a);
            Array.Copy(temp2, a + 1, besindegerleri, a, besinmaddesisayisi - a);

            testDialog2.Dispose();
            ekran(2);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            richTextBox7.Text = "";
            richTextBox2.Text = "";
            richTextBox6.Text = "";
            richTextBox1.Text = "";
            for (int g=0;g<besinmaddesisayisi;g++)
            dtbl.Rows.RemoveAt(besinmaddesisayisi -1 -g);
            for (int g = 0; g < yiyeceksayisi; g++)
                dtbl.Columns.RemoveAt(yiyeceksayisi -g);
            richTextBox3.Rtf = InsertTableInRichTextBox(dtbl,700);
                yiyeceksayisi = 0;
            besinmaddesisayisi = 0;
            katsayılar = new int[besinmaddesisayisi, yiyeceksayisi];
            yiyecek = new string[yiyeceksayisi];
            yiyecekfiyatları = new double[yiyeceksayisi];
            besin = new string[besinmaddesisayisi];
            besindegerleri = new double[besinmaddesisayisi];
        }

        /// <summary>
        /// for cool ı mean  borderless ui can move with this :D
        /// </summary>
        //const and dll functions for moving form
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
            int Msg, int wParam, int lParam);

        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        //call functions to move the form in your form's MouseDown event
        private void Form1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void richTextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }

    class Simplex
    {
        private double[] c;
        private double[,] A;
        private double[,] AA;
        private double[] b;
        private int tablosayisi;
        private int var;
        private int cons;
        DataTable dtbl = new DataTable();
        DataTable dtbl1 = new DataTable();
        DataTable dtbl2 = new DataTable();
        DataTable dtbl3 = new DataTable(); //nihai
        string[] yiyecek = new string[] { "Alabalik", "Dana eti", "Tavuk eti", "Fasülye", "Mercimek", "Ceviz", "Çilek", "Portakal", "Elma", "biber", "Domates", "Patates", "Peynir", "Yumurta" };
        public string[] besin = new string[] { "Kalori", "Protein (gr)", "Karbonhidrat (gr)", "Yağ (gr)", "Vitamin A (mcg)", "Vitamin B12 (mcg)", "Vitamin C  (mg)", "Lif (gr)", "Kalsiyum (gr)" };
        int satir=0;

        public HashSet<Tuple<int, int>> degiskenlistesi = new HashSet<Tuple<int,int>>();

        public Simplex( double[,] A, double[] b, double[] c,string[] yyck,string [] bsn) // c fonksiyon a katsayılar b kısıtlamalar
        {
            tablosayisi = 0;
            var = 0;
            cons = 0;
            

            yiyecek = new string[c.Length];
            Array.Copy(yyck,yiyecek,c.Length);

            besin = new string[b.Length];
            Array.Copy(bsn, besin, b.Length);


            
            var = c.Length; cons = b.Length;
            this.c = new double[cons];
            this.b = new double[var];

            Array.Copy(c, this.b, var);
                Array.Copy(b, this.c, cons);

                this.A = new double[var, cons];
                for (int i = 0; i < var; i++)
                {
                    for (int j = 0; j < cons; j++)
                    {
                        this.A[i, j] = A[j,i];

                    }
                }
                var = this.c.Length;
                cons = this.b.Length;

            


            AA = new double[cons + 1, var + cons + 1];
            for (int i = 0; i < cons; i++)
                {
                    for (int j = 0; j < var; j++)
                    {
                        AA[i, j] = this.A[i, j];

                    }
                    AA[i,var+i]= 1;
                }
                for (int j = 0; j < var; j++)
                {
                    double a= -this.c[j];
                    AA[cons , j] = a; 

                }
                for (int j = 0; j < cons; j++)
                {
                    AA[j, var+cons] = this.b[j];

                }

            
            
           

           

        }

        private static String InsertTableInRichTextBox(DataTable dtbl, int width)
        {
            //Since too much string appending go for string builder
            StringBuilder sringTableRtf = new StringBuilder();

            //beginning of rich text format,dont customize this begining line
            sringTableRtf.Append(@"{\rtf1 ");

            
            int cellWidth;

            //Start the Row
            sringTableRtf.Append(@"\trowd");

            //Populate the Table header from DataTable column headings.
            for (int j = 0; j < dtbl.Columns.Count; j++)
            {
                
                sringTableRtf.Append(@"\cellx" + ((j + 1) * width).ToString());

                if (j == 0)
                    sringTableRtf.Append(@"\intbl  " + dtbl.Columns[j].ColumnName);
                else
                    sringTableRtf.Append(@"\cell   " + dtbl.Columns[j].ColumnName);
            }

            //Add the table header row
            sringTableRtf.Append(@"\intbl \cell \row");

            //Loop to populate the table cell data from DataTable
            for (int i = 0; i < dtbl.Rows.Count; i++)
            {
                //Start the Row
                sringTableRtf.Append(@"\trowd");

                for (int j = 0; j < dtbl.Columns.Count; j++)
                {
                    cellWidth = (j + 1) * width;

                    //A cell with width 1000.
                    sringTableRtf.Append(@"\cellx" + cellWidth.ToString());

                    if (j == 0)
                        sringTableRtf.Append(@"\intbl  " + dtbl.Rows[i][j].ToString());
                    else
                        sringTableRtf.Append(@"\cell   " + dtbl.Rows[i][j].ToString());
                }

                //Insert data row
                sringTableRtf.Append(@"\intbl \cell \row");
            }

            sringTableRtf.Append(@"\pard");
            sringTableRtf.Append(@"}");

            //convert the string builder to string
            return sringTableRtf.ToString();
        }

        public int optimize(Form1 form1)
        {


            ekranıgüncelle(form1);
            
            while (true)
            {
                // en düşük katsayıyı bul
                int e = -1;
                double ce = 0;
                for(int _e=0;_e<var+cons+1;_e++) // normalde sadece var idi
                {
                    if (AA[cons,_e] < ce)
                    {
                        ce = AA[cons,_e];
                        e = _e;
                    }
                }

                // 0 dan küçük katsayı yoksa işimiz bitti döngüden çık
                if (e == -1) break; // e pivot sütun ce 0dan büyük katsayı


                // en düşük ratio ile pivot satır bulunur.
                double minRatio = double.PositiveInfinity;
                int l = -1;
                for (int i=0;i<cons;i++) //
                {
                    if (AA[i, e] > 0)
                    {
                        double r = AA[i,var+cons] / AA[i, e];
                        if (r < minRatio)
                        {
                            minRatio = r;
                            l = i;
                        }
                    }
                }




                // sonu olmayan optimizasyon
                if (double.IsInfinity(minRatio))
                {
                    MessageBox.Show("Elimizde var olan yiyecek çeşitleri bir yada birden fazla temel besin maddesini içermediğinden  dolayı Optimizasyon başarısız olmuştur", "Başarısızlık");
                    return 1;
                    // sonu olmayan optimizasyon durumu
                }

                pivot(l, e); // l satırı e sütunu
                
                degiskenlistesi.Add(Tuple.Create<int, int>(e+1, l)); // değişken ismi ve son sütunda ki pozisyon
               
                tablosayisi++;
                ekranıgüncelle(form1);
                ///////////////////////////////
                
            }




            ////
            dtbl.Columns.Add("isim", typeof(string));
            dtbl.Columns.Add("Miktar (kg)", typeof(double));



            for (int f = var, er = 0; f < var + cons; f++, er++)
            {
                dtbl.Rows.Add(yiyecek[er], Math.Round(AA[cons, f],2)/10 );
            }
            ////

            form1.richTextBox5.Rtf = InsertTableInRichTextBox(dtbl, 2000);
            form1.richTextBox8.Text = "Diyet listesi maliyeti :=" + Math.Round( AA[cons, cons + var] / 10 ,3)+ "  tldir" + "\n";

           
            return 0;// başarılı optimisazyon

        }

        public void ekranıgüncelle(Form1 form1)
        {

            if(tablosayisi==0)
            {
                dtbl2.Columns.Add("ID", typeof(string));
                for (int i=0;i<var+cons+1 ;i++)
                {
                    if (i<var)
                        dtbl2.Columns.Add("X"+(i+1), typeof(double));
                    else 
                    if (i<var +cons)
                        dtbl2.Columns.Add("Z"+(i-var+1), typeof(double));
                    else
                        dtbl2.Columns.Add("Çözüm", typeof(double));


                }
             }




            dtbl2.Rows.Add();
            dtbl2.Rows[satir][0]=tablosayisi+". tablo";
            satir++;
            int t = 0;
            int p = 0;
            if (var + cons < 11)
            {
                t = 300;
                p = 1;
            }
            //Here we add five DataRows.
            for (int d=0; d<cons+1 ;d++)
            {
                dtbl2.Rows.Add();
                for (int f = 0; f < var + cons+1; f++)
                {
                    dtbl2.Rows[satir][f+1]=Math.Round(AA[d,f],1+p);
                }
                satir++;
            }

            
            form1.richTextBox4.Rtf= InsertTableInRichTextBox(dtbl2, 700+t);
            

            //string rtf = InsertTableInRichTextBox(dtbl2, 2000);
            //form1.richTextBox4.AppendText(rtf);



            //form1.richTextBox4.Text += "bu " + tablosayisi + ". tablo" + "\n";
            //for (int i = -1; i < cons + 1; i++)
            //{
            //    if (i==-1)
            //    {

            //        continue;
            //    }
            //    for (int k = 0; k < var + cons + 1; k++)
            //    {
            //        form1.richTextBox4.Text += AA[i, k] + "  ";
            //    }

            //    form1.richTextBox4.Text += "\n";
            //}


            //form1.richTextBox4.Text += "\n";
            //form1.richTextBox4.Text += "*****************************";
            //form1.richTextBox4.Text += "\n";
        }



        private void pivot(int pivotsatiri, int pivotsutunu) // sütun  e, satır l
        {

            double pivot = AA[pivotsatiri, pivotsutunu];
            for(int i = 0; i < var + cons + 1; i++) //pivot satırı pivota bölünüyor
            {
                AA[pivotsatiri, i] = AA[pivotsatiri, i] / pivot;
            }


            for (int i=0;i<cons+1;i++) // pivot sütunu pivot haric 0 yapılıyor
            {
                if (i == pivotsatiri) continue;
                double katsayi = -AA[i, pivotsutunu];
                for (int a=0;a<var+cons+1;a++)
                {
                    AA[i, a] = AA[i, a] + katsayi * AA[pivotsatiri, a];
                }
            }
            
            
        }
    }



   

}
