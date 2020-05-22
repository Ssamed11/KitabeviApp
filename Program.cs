using System;
using System.IO;
using System.Reflection.Metadata;

namespace KitapKayit
{
    class Kitap
    {
        private string KitapAd;
        private string KitapYazar;
        private DateTime KitapBasimTarihi; //Fieldlarımız veri girişi için
        private string KitapTuru;
        public string kitapAd
        {
            get { return KitapAd; }
            set { KitapAd = value; }  
        }
        
        public string kitapYazar
        {
            get { return KitapYazar; }
            set { KitapYazar = value; }
        }
        public DateTime kitapBasimTarihi
        {
            get { return KitapBasimTarihi; }
            set { KitapBasimTarihi = value; }
        }
        public string kitapTuru
        {
            get { return KitapTuru; }
            set { KitapTuru = value; }
        }
        public Kitap()
        {
            this.KitapAd = kitapAd;
            this.KitapYazar = kitapYazar;
            this.KitapBasimTarihi = kitapBasimTarihi;
            this.KitapTuru = kitapTuru;
        }
        
        public static void DosyayaYaz(string[ , ]dizi, string adet)
        {
           
            StreamWriter textyaz = new StreamWriter(@"KitapBilgi.txt");//text yazma fonksiyonu
            for (int i = 0; i < Convert.ToInt32(adet); i++)//yazılacak kitap adedi kadar dönen döngü(çift boyutlu dizi kullanılarak bilgiler alınır) ilk for yani bu for dmngüsü kitap adedi için döner ikinci for kitap bilgileri için döner
            {
                for (int j = 0; j <= 4; j++)//kitap bilgileri için dönen for döngüsü
                {
                    textyaz.WriteLine(dizi[i,j]); 
                }
            }
            textyaz.Close();
        }
        public void EkranaYaz()
        {
            Console.WriteLine("KITAP ADI         KITAP YAZARI           KITAP TARIHI                 KITAP TURU");
            string satir;
            StreamReader textoku = new StreamReader(@"KitapBilgi.txt");//text okuma fonksiyonu
            int sayac = 0;
            string[] Bilgi = new string[1000];
            while(true)
            {
                satir = textoku.ReadLine();
                if(satir==null)//text dokümanında okuma işlemi sona gelince döngüden çıkması için hazırlanan koşul
                {
                    break;
                }
                Bilgi[sayac] = satir;
                sayac++;
            }
            textoku.Close();
            for (int i = 0; i <sayac; i++)//text dokümanında okunan bilgiler kadar dönen koşul
            {
                if (i % 5 != 0)//text dokümanında n. kitap yazısı ekrana çıkartmamak için yazılan koşul
                {
                    Console.Write(Bilgi[i] + "               ");
                }
                else//text dokümanında kitap bilgilerini ekrana çıkartan koşul
                {
                    Console.WriteLine();
                }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            while (true) 
            {

                try
                {
                    Kitap kitap = new Kitap();
                    Console.WriteLine("Merhaba! Kitabevi sistemimize hosgeldiniz. Lutfen yapmak istediginiz islemin numarasını seçer misiniz?");
                    Console.WriteLine("1.Islem: Kitabevi Sistemine Kitap Ekleme");
                    Console.WriteLine("2.Islem: Kitabevi Sisteminde Bulunan Kitap Listesini Goruntuleme");
                    string islem;
                    string KitapAdet;
                    DateTime date = new DateTime(2020, 5, 1, 0, 0, 0);
                    string basimtarihi;
                    islem = Console.ReadLine();
                    string[,] bilgi = new string[100, 5];
                    if (Convert.ToInt32(islem) == 1)//menüde işlem 1 seçilirse yani kitap ekleme işlemi seçilirse çalışacak koşul
                    {
                        Console.WriteLine("Kitap Adedi Giriniz");
                        KitapAdet = Console.ReadLine();
                        for (int i = 0; i < Convert.ToInt32(KitapAdet); i++)//yazılacak kitap adedi kadar dönen döngü
                        {
                            bilgi[i, 0] = Convert.ToString(i + 1) + ". Kitap";
                            for (int j = 1; j <= 4; j++)//kitap bilgi adedi kadar dönen ve dizi aktarım işlemleri gerçekleştiren döngü
                            {
                                if (j == 1)//kitap ad bilgi bloğu
                                {
                                    Console.WriteLine("Kitap Adını Giriniz:");
                                    kitap.kitapAd = Console.ReadLine();
                                    bilgi[i, j] = kitap.kitapAd;
                                }
                                else if (j == 2)//yazar bilgi bloğu
                                {
                                    Console.WriteLine("Kitap Yazarını Giriniz:");
                                    kitap.kitapYazar = Console.ReadLine();
                                    bilgi[i, j] = kitap.kitapYazar;
                                }
                                else if (j == 3)//tarih bilgi bloğu
                                {
                                    Console.WriteLine("Kitap Basım Tarihini Giriniz:");
                                    basimtarihi = Console.ReadLine();
                                    kitap.kitapBasimTarihi = Convert.ToDateTime(basimtarihi);
                                    int result = DateTime.Compare(kitap.kitapBasimTarihi, date);
                                    while (result > 0)//tarih karşılaştırma döngüsü 
                                    {
                                        Console.WriteLine("Kitap Basım Tarihini 2020 den Once Giriniz:");
                                        basimtarihi = Console.ReadLine();
                                        kitap.kitapBasimTarihi = Convert.ToDateTime(basimtarihi);
                                        result = DateTime.Compare(kitap.kitapBasimTarihi, date);
                                    }
                                    if (result <= 0)//girilen tarih 2020 den küçükse diziye ekleme koşulu
                                    {
                                        bilgi[i, j] = Convert.ToString(kitap.kitapBasimTarihi);
                                    }
                                }
                                else//kitap türü bloğu
                                {
                                    Console.WriteLine("Kitap turunu Giriniz:");
                                    kitap.kitapTuru = Console.ReadLine();
                                    bilgi[i, j] = kitap.kitapTuru;
                                }
                            }
                        }
                         Kitap.DosyayaYaz(bilgi, KitapAdet);
                    }
                    if (Convert.ToInt32(islem) == 2)//Menüde 2. işlem seçilirse dosyadaki kitap bilgi verilerini ekrana yazdıracak koşul 
                    {
                        kitap.EkranaYaz();
                    }
                    Console.WriteLine("DEVAM ETMEK ISTIYOR MUSUNUZ?");
                    Console.WriteLine("DEVAM ETMEK ISTIYORSANIZ 1'E ISTEMIYORSANIZ 2'YE BASINIZ");
                    string cevap = Console.ReadLine();
                    if (cevap=="2")
                    {
                        break;
                    }
                }
                catch (Exception Ex)//tur hatası verıldıgınde calışacak olan blok
                {
                    Console.WriteLine("Lütfen sayı girmeniz gereken yerde sayı, yazı girmeniz gereken yerde yazı giriniz ve tarih bolumunu G/A/Y seklinde giriniz.");
                }

            }
        }
    }
}
