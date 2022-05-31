using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biyo_Watermann
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string gen1, gen2, gen1_l, gen2_l;

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView.Rows.Clear();//TEMİZLE
            dataGridView.Columns.Clear();//TEMİZLE
            dataGridView.Refresh();//TEMİZLE
            textBoxSeq1.Text = String.Empty;//textbox boş
            textBoxSeq2.Text = String.Empty;//textbox boş
            Stopwatch süredurdur = new Stopwatch();//süreyi durdur
            süredurdur.Start(); //süreyi başlat
            try //hata yakalama
            {   //dosyayı oku 
                using (StreamReader sr = new StreamReader(@"C:\Users\sefa_\source\repos\Biyo_Watermann\Biyo_Watermann\Text\gen1.txt"))
                {
                    string sıra;
                    //dosyanın içindekileri oku
                    while ((sıra = sr.ReadLine()) != null) //null oluncaya kadar devam et
                    {
                        gen1_l = sıra;
                        //boşluk olana kadar
                        gen1 = sr.ReadToEnd().Replace("\n", "").Replace("\r", "");
                    }
                }
            }
            catch (Exception ex)//hata göster
            {
                //hata mesajı
                MessageBox.Show("Dosya okunamadı..." + ex);
            }
            //hata yakala
            try
            {   //dosyayı oku
                using (StreamReader sr2 = new StreamReader(@"C:\Users\sefa_\source\repos\Biyo_Watermann\Biyo_Watermann\Text\gen2.txt"))
                {
                    string sıra2;
                    //dosyanın içindekileri oku
                    while ((sıra2 = sr2.ReadLine()) != null)//null oluncaya kadar devam et
                    {
                        gen2_l = sıra2;
                        //boşluk olana kadar
                        gen2 = sr2.ReadToEnd().Replace("\n", "").Replace("\r", "");
                    }
                }
            }
            catch (Exception ex)//hata göster
            {
                //hata mesajı
                MessageBox.Show("Dosya okunamadı..." + ex);
            }
            string s1 = gen1.ToUpper(); //büyük harfe çevir
            string s2 = gen2.ToUpper(); //büyük harfe çevir
            int eslesme = Convert.ToInt32(textBoxMatch.Text); //eşleşme
            int eslesme_olmadı = Convert.ToInt32(textBoxMiss.Text);//eşleşme olmadı
            int bosluk = Convert.ToInt32(textBoxGap.Text); //boşluk
            char[] aracı_1 = new char[s1.Length]; //gececi dizi1
            char[] aracı_2 = new char[s2.Length]; //gecici dizi2
            aracı_1 = s1.ToCharArray(); //ayır
            aracı_2 = s2.ToCharArray(); //ayır
            dataGridView.ColumnCount = aracı_1.Length + 1; //gridview de yanlarda yazması
            dataGridView.RowCount = aracı_2.Length + 1; //gridview de yanlara yazması
            dataGridView.Columns[0].Name = "i"; //yanalrda gözükecek default 
            dataGridView.Rows[0].HeaderCell.Value = "j"; //yanlarda gözükecek default
            //gridview de değerleri yanlara yazma
            for (int i = 0; i < aracı_1.Length; i++)
            {
                //ismini aktar i ye
                dataGridView.Columns[i + 1].Name = aracı_1[i].ToString(); //yazılan i+1 den i ye yazdır
            }
            for (int j = 0; j < aracı_2.Length; j++)
            {
                //ismini aktar j ye
                dataGridView.Rows[j + 1].HeaderCell.Value = aracı_2[j].ToString(); //yazılan j+1 den j ye yazdır
            }
            int hesap_a;
            for (int i = 0; i < dataGridView.Columns.Count; i++)//gridviewin sütunları kadar 
            {
                //değerleri aktar i
                dataGridView[i, 0].Value = 0; //ilk sütunu sıfır yap
            }
            for (int j = 0; j < dataGridView.Rows.Count; j++) //gridview satırları kadar
            {
                //değerleri aktar j
                dataGridView[0, j].Value = 0; //ilk satırı 0 yap
            }
            for (int i = 1; i < dataGridView.Columns.Count; i++) //sütun kadar 
            {
                for (int j = 1; j < dataGridView.Rows.Count; j++)//satır kadar
                {
                    if (dataGridView.Columns[i].Name.ToString() == dataGridView.Rows[j].HeaderCell.Value.ToString())
                    {
                        //maksimum değeri hesapla
                        hesap_a = Math.Max(int.Parse(dataGridView[i - 1, j].Value.ToString()) + bosluk,
                        Math.Max(int.Parse(dataGridView[i, j - 1].Value.ToString()) + bosluk, int.Parse(dataGridView[i - 1, j - 1].Value.ToString()) + eslesme));
                        //sıfır ile doldurma
                        if (hesap_a > 0) //sıfırdan büyük ise
                        {
                            dataGridView[i, j].Value = hesap_a; //i ve j değerini hesap aya aktar
                        }
                        else
                        {
                            dataGridView[i, j].Value = 0; //değilse 0 yap
                        }
                    }
                    else
                    {
                        //hesaplama
                        hesap_a = Math.Max(int.Parse(dataGridView[i - 1, j].Value.ToString()) + bosluk,
                            Math.Max(int.Parse(dataGridView[i, j - 1].Value.ToString()) + bosluk, int.Parse(dataGridView[i - 1, j - 1].Value.ToString()) + eslesme_olmadı));
                        //sıfır ile doldurma
                        if (hesap_a > 0)//sıfırdan büyükse
                        {
                            dataGridView[i, j].Value = hesap_a; //i ve j yi hesap a yap
                        }
                        else
                        {
                            dataGridView[i, j].Value = 0;//değiilse 0 yap
                        }

                    }
                }
            }
            //hizlama 
            int max = int.Parse(dataGridView[0, 0].Value.ToString()); //datagridview 0 0 değerine ata
            StringBuilder gen1_hiza = new StringBuilder(); //tanımlama
            StringBuilder gen2_hiza = new StringBuilder(); //tanımlama
            List<string> gen1_list = new List<string>(); //list
            List<string> gen2_list = new List<string>(); //list2
            List<int> score_list = new List<int>(); //score list
            s1 = "-" + s1; // -
            s2 = "-" + s2; // -
            int score = 0; //score 0
            for (int i = 0; i < dataGridView.Columns.Count; i++) //gridview sütuna kadar
            {
                for (int j = 0; j < dataGridView.Rows.Count; j++)//gridview satıra kadar 
                {
                    if (int.Parse(dataGridView[i, j].Value.ToString()) > max) //şayet max tan büyükse
                    {
                        max = int.Parse(dataGridView[i, j].Value.ToString()); //gridviewdeki değere aktar maxı
                    }
                }
            }
            for (int i = 0; i < dataGridView.Columns.Count; i++) //gridview sütuna kadar
            {
                for (int j = 0; j < dataGridView.Rows.Count; j++) //gridview satıra kadar
                {
                    if (int.Parse(dataGridView[i, j].Value.ToString()) == max)//şayet maxsa eşitse
                    {
                        int x = i; //x = i
                        int y = j; // y = j
                        while (true)
                        {
                            int temp_score = 0; //atama
                            //geri izleme
                            while (int.Parse(dataGridView[x, y].Value.ToString()) != 0)
                            {
                                if (dataGridView.Columns[x].Name.ToString() == dataGridView.Rows[y].HeaderCell.Value.ToString())
                                {
                                    //şayet çapraz ise
                                    gen1_hiza.Insert(0, s1[x]);
                                    gen2_hiza.Insert(0, s2[y]);
                                    dataGridView[x, y].Style.BackColor = Color.Aqua; //renk
                                    temp_score += eslesme;
                                    x--;
                                    y--;
                                }
                                else
                                {
                                    //çapraz max ise
                                    if (int.Parse(dataGridView[x, y].Value.ToString()) == (int.Parse(dataGridView[x, y - 1].Value.ToString()) + bosluk)) // çapraz max ise
                                    {
                                        gen1_hiza.Insert(0, "-");
                                        gen2_hiza.Insert(0, s2[y]);
                                        dataGridView[x, y].Style.BackColor = Color.Aqua;//renk
                                        temp_score += bosluk;
                                        y--;
                                    }
                                    //sol max ise
                                    else if (int.Parse(dataGridView[x, y].Value.ToString()) == (int.Parse(dataGridView[x - 1, y - 1].Value.ToString()) + eslesme_olmadı)) //sol max ise
                                    {
                                        gen1_hiza.Insert(0, s1[x]);
                                        gen2_hiza.Insert(0, s2[y]);
                                        dataGridView[x, y].Style.BackColor = Color.Aqua;//renk
                                        temp_score += eslesme_olmadı;
                                        x--;
                                        y--;
                                    }
                                    //yukarı max ise
                                    else if (int.Parse(dataGridView[x, y].Value.ToString()) == (int.Parse(dataGridView[x - 1, y].Value.ToString()) + bosluk)) //yukarı max ise
                                    {
                                        gen1_hiza.Insert(0, s1[x]);
                                        gen2_hiza.Insert(0, "-");
                                        dataGridView[x, y].Style.BackColor = Color.Aqua; //renk
                                        temp_score += bosluk;
                                        x--;
                                    }
                                }
                            }
                            score_list.Add(temp_score); //skoru ekle
                            gen1_list.Add(gen1_hiza.ToString()); //hizalamak için listeye ekle
                            gen2_list.Add(gen2_hiza.ToString());//hizalamak için listeye ekle
                            gen1_hiza.Length = 0;
                            gen2_hiza.Length = 0;
                            if (temp_score >= score) //temp skor büyük eşit skordan ise
                            {
                                score = temp_score; //skoru tempe aktar
                            }
                            else
                            {
                                continue; //değilse devam et
                            }
                            if (int.Parse(dataGridView[x, y].Value.ToString()) == 0)
                            {
                                dataGridView[x, y].Style.BackColor = Color.Aqua; //renk
                                break;
                            }
                        }
                    }
                    else continue;
                }
            }
            int count = 0; //sayac
            for (int i = 0; i < score_list.Count; i++) //skor list kadar
            {
                if (score_list[i] == score)
                {
                    count++;
                    if (count > 1) //1 den büyükse
                    {
                        textBoxSeq1.Text += gen1_list[i].ToString() + "          "; //yazdırma
                        textBoxSeq2.Text += gen2_list[i].ToString() + "          "; //yazdırma
                    }
                    else //eşit ya da küçükse
                    {
                        textBoxSeq1.Text += gen1_list[i].ToString() + "          "; //yazdırma
                        textBoxSeq2.Text += gen2_list[i].ToString() + "          "; //yazdırma
                    }
                }
            }
            textBoxScore.Text = score.ToString(); //textbox a yazdır
            süredurdur.Stop(); //süreyi durdur
            TimeSpan ts = süredurdur.Elapsed; //zaman
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            labelRunTime.Text = (elapsedTime); //zamanı label a aktar
        }
    }
}

