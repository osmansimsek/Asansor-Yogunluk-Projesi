using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace AsansorProjesiForm
{
    class Giris
    {
        public Thread girisThread;

        List<Kat> katlar;
        Random random;

        public Giris(List<Kat> katlar)
        {
            this.katlar = katlar;
  
            random = new Random();
            girisThread = new Thread(new ThreadStart(MusteriEkle))
            {
                Name = "GirisThread",
                IsBackground = false,
            };
            girisThread.Start();
        }

        private void MusteriEkle()
        {
            while (true)
            {
                int kisiSayisi = 1 + random.Next(10);
                int katSayisi = 1 + random.Next(4);

                Grup grup = new Grup()
                {
                    kisiSayisi = kisiSayisi,
                    katSayisi = katSayisi,
                };

                katlar[0].kuyrukSayisi++;
                katlar[0].gelenKisiler.Enqueue(grup);

                // kat tablo kontrolü
                katlar[0].KuyrukSayisiYazdır();
                katlar[0].KuyrukYazdir(0);

                Thread.Sleep(Utilites.girisThreadCalismaSuresi);
            }
        }
    }
}
