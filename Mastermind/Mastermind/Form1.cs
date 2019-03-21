using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mastermind
{
   
    public partial class Form1 : Form
    {
        Random rnd = new Random(); //Rastgele sayı üretmek için kullanılacak sınıf

        int artı;
        int eksi;
        int artı_tutulan;
        int eksi_tutulan;
        int yeniDeğer;
        int eskiDeğer;
        int en_iyi;
        int benzer;
        int benzer_count;
        int yasak_count;                // değişkenler
        int counter;
        int random;
        int array_to_int;
        int Genel;
        int tahminin;
        int sayaç = -1;
        bool random_flag = false;
        bool yasak_flag = false;
        bool register_flag = false;
        bool counter_flag = false;

        int[] tahmin = new int[4];
        int[] yasak = new int[10];
        int[] önceki_tahminler = new int[1000];         // Kullanılan Arrayler
        int[] last_list = new int[4];
        int[] Tutulan = new int[4];
        int[] tahmin_edilen = new int[4];




        public void SayiTut() //Basamakları birbirinden farklı 4 basamaklı random sayı üretimi
        {

            int yeni_sayi; //Her seferinde üretilecek sayımız.
            bool durum = true;

            for (int i = 0; i < Tutulan.Length; i++)
            {
                while (durum)
                {
                    yeni_sayi = rnd.Next(0, 10);

                    if (i == 0)
                    {
                        Tutulan[0] = yeni_sayi;
                        break; //While döngüsünden çıkılır.
                    }

                    //tutulan içersinde oluşturulan yeni sayıdan varmı diye kontrol ediliyor.
                    //Varsa durum true oluyor ve for döngüsünden çıkıyor. Çünkü yeni bir sayı atamamız gerekiyor.
                    for (int k = 0; k < i; k++)
                    {
                        if (Tutulan[k] == yeni_sayi) //Yeni oluşan sayımız dizide daha önceden varsa
                        {
                            durum = true;
                            break; //for döngüsünden çık
                        }
                        else
                            durum = false;
                    }

                    if (durum == false)
                        Tutulan[i] = yeni_sayi;
                }
                durum = true;

            }

        }

        public void uret()//Basamakları birbirinden farklı 4 basamaklı random sayı üretimi
        {
            
            int yeni_sayi; //Her seferinde üretilecek sayımız.
            bool durum = true;

            for (int i = 0; i < tahmin.Length; i++)
            {
                while (durum)
                {
                    yeni_sayi = rnd.Next(0, 10);

                    if (i == 0)
                    {
                        tahmin[0] = yeni_sayi;
                        break; //While döngüsünden çıkılır.
                    }

                    //tahmin içersinde oluşturulan yeni sayıdan varmı diye kontrol ediliyor.
                    //Varsa durum true oluyor ve for döngüsünden çıkıyor. Çünkü yeni bir sayı atamamız gerekiyor.
                    for (int k = 0; k < i; k++)
                    {
                        if (tahmin[k] == yeni_sayi) //Yeni oluşan sayımız dizide daha önceden varsa
                        {
                            durum = true;
                            break; //for döngüsünden çık
                        }
                        else
                            durum = false;
                    }

                    if (durum == false)
                        tahmin[i] = yeni_sayi;
                }
                durum = true;

            }

            array_to_int = (tahmin[0] * 1000) + (tahmin[1] * 100) + (tahmin[2] * 10) + tahmin[3];

        }

        public Form1()
        {
            
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            groupBox1.Enabled = true;

            uret();
            if ((array_to_int / 1000) == 0)
            {
                textBox1.Text += "0" + array_to_int + "\r\n";
            }

            else
            {
                textBox1.Text += array_to_int + "\r\n";
            }
                                                             // OYUNU BAŞLAT butonuna basıldığında bilgisayarın sayı tutma ve ilk
            önceki_tahminler[Genel] = array_to_int;         // tahmini yapması
            Genel++;
            SayiTut();

            textBox8.Text = "0";
            textBox9.Text = "0";

            button1.Enabled = false;                        // oyun penceresinin aktif hale getirilmesi 
            groupBox2.Enabled = true;
            groupBox1.Enabled = true;
            button3.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            artı = Convert.ToInt32(textBox8.Text);
            eksi = Convert.ToInt32(textBox9.Text);            // Bilgisayarın ilk tahmininin ardından bilgisayara gönderilen ipuçları

            yeniDeğer = artı + eksi;

            if (yeniDeğer > 4 || yeniDeğer < 0)           // 4 basamaklı sayılar üzerine yapılan bir oyun olduğu için
            {                           // ipuçları toplamı da en fazla 4 olabilir.
                MessageBox.Show("Girdiğiniz ipucu yanlış. Lütfen doğru ipucu giriniz...");
            }

            else if(yeniDeğer < 5 && yeniDeğer >= 0)
            {
                textBox6.Text += artı + "\r\n";
                textBox7.Text += eksi + "\r\n";

                textBox8.Text = "0";
                textBox9.Text = "0";
            }
            


            if(yeniDeğer == 0)         // ipuçlarının toplamının "0" olması durumu
            {
                for(int i=0; i<tahmin.Length; i++)
                {
                    for(int k=0; k<yasak.Length; k++)       // bu durumda bilgisayarın tahmin ettiği 4 basamaklı sayıdaki 
                    {                                       // hiçbir rakam kullanıcının tuttuğu sayıda yer almadığı için
                        if(tahmin[i] == yasak[k])           // bu rakamları kullanılmayaklar arrayine ayırmaktadır.
                        {
                            yasak_flag = true;
                            break;
                        }

                    }

                    if(yasak_flag == false)
                    {
                        yasak[yasak_count] = tahmin[i];
                        yasak_count++;

                    }
                }
                Aynı_0:
                uret();
                for(int i=0; i<4; i++)
                {                                               // üretilen sayının daha önce üretilen sayılarla 
                    for(int k=0; k<yasak.Length; k++)           // aynı olmaması için kıyaslama yapılması
                    {
                        if(tahmin[i] == yasak[k])
                        {
                            goto Aynı_0;
                        }
                    }
                }
                for(int i=0; i<önceki_tahminler.Length; i++)
                {
                    if (array_to_int == önceki_tahminler[i])
                    {
                        goto Aynı_0;
                    }
                }
                önceki_tahminler[Genel] = array_to_int;
            }

            else if (yeniDeğer == 4)                        // ipucu olarak 4 gönderilmesi durumunda tüm rakamlar
            {                                               // doğru olduğu için rakamlar arasında yer değişikliği yapılması
                if(artı == 4)
                {
                    MessageBox.Show("BEN KAZANDIM...");
                    groupBox1.Enabled = false;
                    groupBox2.Enabled = false;
                    button1.Enabled = false;
                }

                else if(artı == 2 && register_flag == false)
                {
                    benzer = array_to_int;
                    register_flag = true;

                }

                ilk_1:
                
                if (register_flag == true)
                {
                    int x1 = benzer / 1000;
                    int x2 = (benzer / 100) % 10;
                    int x3 = (benzer / 10) % 10;
                    int x4 = benzer % 10;
                    int temp;

                    switch (benzer_count)
                    {
                        case 0 :
                            temp = x1;
                            x1 = x2;
                            x2 = temp;
                            break;

                        case 1 :
                            temp = x3;
                            x3 = x4;
                            x4 = temp;
                            break;

                        case 2 :
                            temp = x1;
                            x1 = x3;
                            x3 = temp;
                            break;

                        case 3 :
                            temp = x2;
                            x2 = x4;
                            x4 = temp;
                            break;

                        case 4 :
                            temp = x1;
                            x1 = x4;
                            x4 = temp;
                            break;

                        case 5 :
                            temp = x2;
                            x2 = x3;
                            x3 = temp;
                            break;
                    }

                    benzer_count++;

                    array_to_int = (x1 * 1000) + (x2 * 100) + (x3 * 10) + x4;

                }

                else if (register_flag == false)
                {
                    for(int i = 0; i < 4; i++)
                    {
                        ilk_2:
                        random = rnd.Next(0, 4);                    // rakamların rastgele karıştırılması

                        if (tahmin[random] == -1)
                        {
                            goto ilk_2;
                        }

                        last_list[i] = tahmin[random];
                        tahmin[random] = -1;
                    }

                        for (int k = 0; k < 4; k++)
                        {
                            tahmin[k] = last_list[k];
                        }

                        array_to_int = (tahmin[0] * 1000) + (tahmin[1] * 100) + (tahmin[2] * 10) + tahmin[3];

                }
    



                for (int i = 0; i < önceki_tahminler.Length; i++)           // üretilen sayının daha önce üretilen sayılarla 
                {                                                           // aynı olmaması için kıyaslama yapılması
                    if (array_to_int == önceki_tahminler[i])
                    {
                        goto ilk_1;
                    }
                }
                önceki_tahminler[Genel] = array_to_int;

                
            }

            else if(yeniDeğer < 4 && yeniDeğer > 0)         // ipucu olarak 1 2 3 rakamlarının verildiği durumlar
            {
                if(yeniDeğer >= eskiDeğer)
                {
                    en_iyi = array_to_int;

                    if (yeniDeğer > eskiDeğer && counter_flag == false)
                    {
                        counter++;
                    }
                    counter_flag = false;

                    if (counter == 4)
                    {
                        counter = 0;
                    }
                }


                else if (yeniDeğer < eskiDeğer)                         // değiştirilen rakam dan dolayı ipucu düzeyi düşerse 
                {                                                  // değiştirilen rakamın kullanıcının tuttuğu sayının bir  
                    array_to_int = en_iyi;    // rakamı olduğunun tespiti ve eski hali üzerinden değişiklikler yapılması
                    tahmin[0] = array_to_int / 1000;
                    tahmin[1] = (array_to_int / 100) % 10;
                    tahmin[2] = (array_to_int / 10) % 10;
                    tahmin[3] = array_to_int % 10;
                    counter++;
                    counter_flag = true;

                    if (counter == 4)
                    {
                        counter = 0;
                    }
                }


                Aynı:

                random_flag = false;

                sayaç++;

                if (sayaç == 10)
                {
                    sayaç = 0;
                }

                for (int i = 0; i < 10; i++)
                {
                    if (sayaç == yasak[i])
                    {                                       // tahminler için basamak değiştirilirken kullanılacak rakamların
                        random_flag = true;                 // diğer rakamlardan farklı olarak seçilmesi ve kullanıcının tuttuğu sayıda  
                        break;                              // kullanılmadığı tespit edilen sayıların kullanılmaması
                    }
                }

                if (random_flag == false)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (sayaç == tahmin[i])
                        {
                            random_flag = true;
                            break;
                        }
                    }
                }
                                    

                if (random_flag == true)
                {
                    goto Aynı;
                }

                tahmin[counter] = sayaç;
                array_to_int = (tahmin[0] * 1000) + (tahmin[1] * 100) + (tahmin[2] * 10) + tahmin[3];

                for (int i = 0; i < önceki_tahminler.Length; i++)           // üretilen sayının daha önce üretilen sayılarla 
                {                                                           // aynı olmaması için kıyaslama yapılması
                    if (array_to_int == önceki_tahminler[i])
                    {
                        goto Aynı;
                    }
                }
                önceki_tahminler[Genel] = array_to_int;

            }


            if (yeniDeğer < 5 && yeniDeğer >= 0 )
            {
                

                eskiDeğer = yeniDeğer;
                Genel++;
                button2.Enabled = false;                    // sıranın kullanıcıya geçmesi
                button3.Enabled = true;

            }



        }

        private void button3_Click(object sender, EventArgs e)
        {
            if ((array_to_int / 1000) == 0)
            {
                textBox1.Text += "0" + array_to_int + "\r\n";       // üretilen sayının yazdırılması

            }

            else
            {
                textBox1.Text += array_to_int + "\r\n";

            }


            if (textBox5.Text.Length < 4 || textBox5.Text.Length > 4)
            {
                MessageBox.Show("Lütfen 4 Basamaklı Tahmininizi Girin");    // Hata Kontrolü

            }

            else
            {
                tahminin = Convert.ToInt32(textBox5.Text);
                int a = tahminin / 1000;
                int b = (tahminin / 100) % 10;
                int c = (tahminin / 10) % 10;
                int d = tahminin % 10;

                if (a == b || a == c || a == d || b == c || b == d || c == d)
                {
                    MessageBox.Show("Birbirinden farklı rakamlar giriniz.");    // Hata Kontrolü

                }

                else
                {
                    textBox5.Text = "";
                    tahmin_edilen[0] = tahminin / 1000;
                    tahmin_edilen[1] = (tahminin / 100) % 10;           // Kullanıcıdan alınan 4 basamaklı sayının  
                    tahmin_edilen[2] = (tahminin / 10) % 10;            // basamaklarına ayrılması
                    tahmin_edilen[3] = tahminin % 10;

                    for (int i = 0; i < 4; i++)
                    {
                        if (Tutulan[i] == tahmin_edilen[i])
                        {
                            artı_tutulan++;                   // Basamak değerleri ve rakam değerleri doğru bilinen rakamların tespiti 
                        }
                    }

                    for (int i = 0; i < 4; i++)
                    {
                        for (int k = 0; k < 4; k++)
                        {
                            if (Tutulan[i] == tahmin_edilen[k])
                            {                                           //rakam değerleri doğru bilinen rakamların tespiti
                                eksi_tutulan++;
                            }
                        }
                    }

                    eksi_tutulan = eksi_tutulan - artı_tutulan;

                    if(tahminin/1000 == 0)
                    {
                        textBox2.Text += "0" + tahminin + "\r\n";              

                    }
                    else
                    {
                        textBox2.Text += tahminin + "\r\n";
                    }
                                                                // kullanıcının tahmin ettiği sayının CPU tarafından değerlendirilip 
                    textBox3.Text += artı_tutulan + "\r\n";      // ipuclarının Kullanıcıya verilmesi
                    textBox4.Text += eksi_tutulan + "\r\n";

                    if (artı_tutulan == 4)
                    {
                        MessageBox.Show("TEBRİKLER KAZANDINIZ...");
                        groupBox1.Enabled = false;                         // Kullanıcının doğru tahmine ulaşma durumu
                        groupBox2.Enabled = false;
                        button1.Enabled = false;
                    }

                    artı_tutulan = 0;
                    eksi_tutulan = 0;

                    button2.Enabled = true;
                    button3.Enabled = false;
                }
            
            }
        }
    }
}
