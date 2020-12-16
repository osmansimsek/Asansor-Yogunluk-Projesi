using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsansorProjesiForm
{
    class Asansor
    {
        public Thread asansorThread;

        List<Grup> asansordekiKisiler;
        List<Kat> katlar;
        List<Label> asansorTabloComponent;

        public int hedefKat;
        public int kat;
        public int kisiSayisi;

        public AsansorModKomutları mod = AsansorModKomutları.idle;
        public AsansorYonKomutları yon = AsansorYonKomutları.up;
        public AsansorMusteriKomutları musteriKomutları = AsansorMusteriKomutları.YolcuGotur;

        public Asansor(List<Kat> katlar, List<Label> asansorTabloComponent)
        {
            this.asansorTabloComponent = asansorTabloComponent;
            this.katlar = katlar;
            this.hedefKat = 0;
            this.kat = 0;
            this.kisiSayisi = 0;

            asansordekiKisiler = new List<Grup>();
            asansorThread = new Thread(new ThreadStart(AsansorHareket))
            {
                Name = "Asansor",
                IsBackground = true,
            };
        }

        public void AsansorHareket()
        {
            if (this.mod == AsansorModKomutları.working)
            {
                while (true)
                {
                    AsansorTabloKontrol();

                    if (musteriKomutları == AsansorMusteriKomutları.YolcuGotur)
                    {
                        if (kat == 0 && kisiSayisi == 0)
                        {
                            MusteriAl();
                            AsansorTabloKontrol();

                            HedefKatBelirle();
                            AsansorTabloKontrol();

                            HedefeGit();
                            AsansorTabloKontrol();
                        }
                        else if (kat != 0 && kisiSayisi != 0)
                        {
                            MusteriBirak();
                            AsansorTabloKontrol();

                            HedefKatBelirle();
                            AsansorTabloKontrol();

                            HedefeGit();
                            AsansorTabloKontrol();
                        }
                        else if (kat != 0 && kisiSayisi == 0)
                        {
                            musteriKomutları = AsansorMusteriKomutları.YolcuTopla;
                            AsansorTabloKontrol();
                        }
                    }
                    else if (musteriKomutları == AsansorMusteriKomutları.YolcuTopla)
                    {
                        HedefKatBelirle();
                        AsansorTabloKontrol();

                        HedefeGit();
                        AsansorTabloKontrol();

                        if (kat != 0)
                        {
                            MusteriAl();
                            AsansorTabloKontrol();
                        }
                        else if (kat == 0)
                        {
                            MusteriBirak();
                            AsansorTabloKontrol();

                            musteriKomutları = AsansorMusteriKomutları.YolcuGotur;
                        }
                    }

                    AsansorTabloKontrol();
                }
            }
        }

        private void AsansorTabloKontrol()
        {
            if (mod == AsansorModKomutları.working)
            {
                asansorTabloComponent[0].Text = true.ToString();
            }
            else
            {
                asansorTabloComponent[0].Text = false.ToString();
            }

            asansorTabloComponent[1].Text = Utilites.asansorKapasite.ToString();
            asansorTabloComponent[2].Text = kisiSayisi.ToString();
            asansorTabloComponent[3].Text = hedefKat.ToString();
            asansorTabloComponent[4].Text = yon.ToString();
            asansorTabloComponent[5].Text = kat.ToString();
            asansorTabloComponent[6].Text = "";

            lock (this)
            {
                foreach (Grup grup in asansordekiKisiler)
                {
                    asansorTabloComponent[6].Text += string.Format("({0},{1})", grup.kisiSayisi,
                                                                   grup.katSayisi);
                }
            }

            asansorTabloComponent[7].Text = mod.ToString();
        }

        public int HedefKatBelirle()
        {
            if (musteriKomutları == AsansorMusteriKomutları.YolcuGotur)
            {
                if (asansordekiKisiler.Count != 0)
                {
                    int enKucuk = Utilites.avmKatSayisi;

                    foreach (Grup grup in asansordekiKisiler)
                    {
                        if (grup.katSayisi < enKucuk)
                        {
                            enKucuk = grup.katSayisi;
                        }
                    }

                    hedefKat = enKucuk;
                }
            }

            else if (musteriKomutları == AsansorMusteriKomutları.YolcuTopla)
            {
                if (kisiSayisi < 10)
                {
                    int enBuyuk = 0;

                    foreach (Kat kat in katlar)
                    {
                        if (kat.cikanKisiler.Count != 0)
                        {
                            if (enBuyuk < kat.katNumarasi)
                            {
                                enBuyuk = kat.katNumarasi;
                            }
                        }
                    }

                    hedefKat = enBuyuk;
                }

                else if (kisiSayisi == 10)
                {
                    hedefKat = 0;
                }
            }

            return hedefKat;
        }

        public void MusteriAl()
        {

            if (musteriKomutları == AsansorMusteriKomutları.YolcuGotur)
            {
                while (true)
                {
                    if (kisiSayisi < Utilites.asansorKapasite && kisiSayisi >= 0)
                    {
                        if (kat == 0 && katlar[kat].gelenKisiler.Count > 0)
                        {
                            if (kisiSayisi + katlar[kat].gelenKisiler.Peek().kisiSayisi <= Utilites.asansorKapasite)
                            {
                                kisiSayisi += katlar[kat].gelenKisiler.Peek().kisiSayisi;
                                asansordekiKisiler.Add(katlar[kat].gelenKisiler.Dequeue());

                                // kat tablo kontrolü
                                katlar[kat].kuyrukSayisi--;
                            }
                            else if (kisiSayisi + katlar[kat].gelenKisiler.Peek().kisiSayisi > Utilites.asansorKapasite)
                            {
                                int asansorDolumFark = Utilites.asansorKapasite - kisiSayisi;

                                Grup grup = new Grup()
                                {
                                    katSayisi = katlar[kat].gelenKisiler.Peek().katSayisi,
                                    kisiSayisi = asansorDolumFark,
                                };

                                kisiSayisi += asansorDolumFark;
                                katlar[kat].gelenKisiler.Peek().kisiSayisi -= asansorDolumFark;
                                asansordekiKisiler.Add(grup);
                            }
                        }
                        else if (kat == 0 && katlar[kat].gelenKisiler.Count == 0)
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else if (musteriKomutları == AsansorMusteriKomutları.YolcuTopla)
            {
                while (true)
                {
                    if (kisiSayisi < Utilites.asansorKapasite && kisiSayisi >= 0)
                    {
                        if (kat != 0 && katlar[kat].cikanKisiler.Count > 0)
                        {
                            if (kisiSayisi + katlar[kat].cikanKisiler.Peek().kisiSayisi <= Utilites.asansorKapasite)
                            {
                                // kat tablo kontrolü
                                katlar[kat].kuyrukSayisi--;
                                katlar[kat].KuyrukYazdir(kat);
                                katlar[kat].KisiSayisiYazdır(kat);
                                katlar[kat].KuyrukSayisiYazdır();

                                kisiSayisi += katlar[kat].cikanKisiler.Peek().kisiSayisi;
                                asansordekiKisiler.Add(katlar[kat].cikanKisiler.Dequeue());

                            }
                            else if (kisiSayisi + katlar[kat].cikanKisiler.Peek().kisiSayisi > Utilites.asansorKapasite)
                            {

                                int asansorDolumFark = Utilites.asansorKapasite - kisiSayisi;

                                Grup grup = new Grup()
                                {
                                    katSayisi = katlar[kat].cikanKisiler.Peek().katSayisi,
                                    kisiSayisi = asansorDolumFark,
                                };

                                // kat tablo kontrolü
                                katlar[kat].KuyrukYazdir(kat);
                                katlar[kat].KuyrukSayisiYazdır();
                                katlar[kat].KisiSayisiYazdır(kat);

                                kisiSayisi += asansorDolumFark;
                                katlar[kat].cikanKisiler.Peek().kisiSayisi -= asansorDolumFark;
                                asansordekiKisiler.Add(grup);
                            }
                        }
                        else if (kat != 0 && katlar[kat].cikanKisiler.Count == 0)
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        public void MusteriBirak()
        {
            if (musteriKomutları == AsansorMusteriKomutları.YolcuGotur)
            {
                if (hedefKat == kat)
                {
                    for (int i = 0; i < asansordekiKisiler.Count; i++)
                    {
                        if (asansordekiKisiler[i].katSayisi == kat)
                        {
                            kisiSayisi -= asansordekiKisiler[i].kisiSayisi;
                            katlar[kat].gelenKisiler.Enqueue(asansordekiKisiler[i]);

                            // kat tablo kontrolü
                            katlar[kat].kisiSayisi += asansordekiKisiler[i].kisiSayisi;
                            katlar[kat].KisiSayisiYazdır(kat);
                            katlar[kat].KuyrukYazdir(kat);
                            katlar[kat].KuyrukSayisiYazdır();

                            asansordekiKisiler.RemoveAt(i);

                            i = 0;
                        }
                    }
                }
            }
            else if (musteriKomutları == AsansorMusteriKomutları.YolcuTopla)
            {
                if (kat == 0)
                {
                    foreach (Grup grup in asansordekiKisiler)
                    {
                        katlar[kat].cikisYapanSayisi += grup.kisiSayisi;
                    }

                    // kat tablo kontrolü
                    katlar[kat].CikisSayisiYazdır();

                    asansordekiKisiler.Clear();
                    kisiSayisi = 0;
                }
            }
        }

        private void HedefeGit()
        {
            while (true)
            {
                if (hedefKat == kat)
                {
                    break;
                }
                else
                {
                    if (hedefKat > kat)
                    {
                        YukariHareketEt();
                    }
                    else
                    {
                        AsagiHareketEt();
                    }
                }
            }
        }

        public void YukariHareketEt()
        {
            if (hedefKat != kat)
            {
                kat++;
                yon = AsansorYonKomutları.up;

            }

            Thread.Sleep(Utilites.asansorKatGecisSuresi);
        }

        public void AsagiHareketEt()
        {
            if (hedefKat != kat)
            {
                kat--;
                yon = AsansorYonKomutları.down;

            }

            Thread.Sleep(Utilites.asansorKatGecisSuresi);
        }
    }
}
