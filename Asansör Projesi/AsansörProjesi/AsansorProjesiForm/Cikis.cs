using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace AsansorProjesiForm
{
    class Cikis
    {
        Thread cikisThread;
        List<Kat> katlar;
        Random random;

        public Cikis(List<Kat> katlar)
        {
            this.katlar = katlar;

            random = new Random();
            cikisThread = new Thread(new ThreadStart(MusteriCikart))
            {
                Name = "CikisThread",
                IsBackground = true,
            };
            cikisThread.Start();
        }

        public void MusteriCikart()
        {
            while (true)
            {
                int katSayisi = 1 + random.Next(4);

                if (katlar[katSayisi].gelenKisiler.Count != 0)
                {
                    int kisiSayisi = 1 + random.Next(5);

                    while (true)
                    {
                        if (kisiSayisi == 0 || katlar[katSayisi].gelenKisiler.Count == 0)
                        {
                            break;
                        }
                        else
                        {
                            if (katlar[katSayisi].gelenKisiler.Peek().kisiSayisi - kisiSayisi <= 0)
                            {
                                // kat tablo kontrolü
                                katlar[katSayisi].kisiSayisi -= katlar[katSayisi].gelenKisiler.Peek().kisiSayisi;
                                katlar[katSayisi].kuyrukSayisi++;
                               
                                kisiSayisi -= katlar[katSayisi].gelenKisiler.Peek().kisiSayisi;
                                katlar[katSayisi].gelenKisiler.Peek().katSayisi = 0;
                                katlar[katSayisi].cikanKisiler.Enqueue(katlar[katSayisi].gelenKisiler.Dequeue());

                                // kat tablo kontrolü
                                katlar[katSayisi].KuyrukSayisiYazdır();
                                katlar[katSayisi].KuyrukYazdir(katSayisi);
                            }

                            else if (katlar[katSayisi].gelenKisiler.Peek().kisiSayisi - kisiSayisi > 0)
                            {
                                Grup grup = new Grup()
                                {
                                    katSayisi = 0,
                                    kisiSayisi = kisiSayisi,
                                };

                                katlar[katSayisi].gelenKisiler.Peek().kisiSayisi -= kisiSayisi;
                                katlar[katSayisi].cikanKisiler.Enqueue(grup);

                                // kat tablo kontrolü
                                katlar[katSayisi].kisiSayisi -= kisiSayisi;
                                katlar[katSayisi].kuyrukSayisi++;
                                katlar[katSayisi].KuyrukSayisiYazdır();
                                katlar[katSayisi].KuyrukYazdir(katSayisi);

                                kisiSayisi = 0;
                            }
                        }
                    }
                }

                Thread.Sleep(Utilites.cikisThreadCalismaSuresi);
            }
        }
    }
}
