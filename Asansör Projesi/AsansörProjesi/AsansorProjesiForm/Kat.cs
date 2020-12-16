using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsansorProjesiForm
{
    class Kat
    {
        public Queue<Grup> cikanKisiler;
        public Queue<Grup> gelenKisiler;

        public int katNumarasi;
        public int cikisYapanSayisi;
        public int kisiSayisi;
        public int kuyrukSayisi;

        Label kuyrukSayisiLabel;
        Label kisiSayisiLabel;
        Label kuyrukYazdırLabel;
        Label cikisSayisiLabel;

        public Kat(int katNumarasi, Label kuyrukSayisiLabel, Label kisiSayisiLabel,
                       Label kuyrukYazdırLabel, Label cikisSayisiLabel)
        {
            this.katNumarasi = katNumarasi;
            this.kisiSayisi = 0;
            this.kuyrukSayisi = 0;
            this.cikisYapanSayisi = 0;

            this.kuyrukSayisiLabel = kuyrukSayisiLabel;
            this.kisiSayisiLabel = kisiSayisiLabel;
            this.kuyrukYazdırLabel = kuyrukYazdırLabel;
            this.cikisSayisiLabel = cikisSayisiLabel;

            gelenKisiler = new Queue<Grup>();
            cikanKisiler = new Queue<Grup>();
        }

        public void KuyrukSayisiYazdır()
        {
            kuyrukSayisiLabel.Text = kuyrukSayisi.ToString();
            kuyrukSayisiLabel.Refresh();
        }

        public void KuyrukYazdir(int kat)
        {
            kuyrukYazdırLabel.Text = "";

            if (kat == 0)
            {
                lock (this)
                {
                    for (int i = 0; i < gelenKisiler.Count; i++)
                    {
                        string yazdirilicakMetin = string.Format("[{0},{1}], ",
                            gelenKisiler.ElementAt<Grup>(i).kisiSayisi,
                            gelenKisiler.ElementAt<Grup>(i).katSayisi);

                        kuyrukYazdırLabel.Text += yazdirilicakMetin;
                        kuyrukYazdırLabel.Refresh();
                    }
                }
            }
            else
            {
                for (int i = 0; i < cikanKisiler.Count; i++)
                {
                    string yazdirilicakMetin = string.Format("[{0},{1}], ",
                        cikanKisiler.ElementAt<Grup>(i).kisiSayisi,
                        cikanKisiler.ElementAt<Grup>(i).katSayisi);

                    kuyrukYazdırLabel.Text += yazdirilicakMetin;
                    kuyrukYazdırLabel.Refresh();
                }
            }

        }

        public void KisiSayisiYazdır(int kat)
        {
            if (kat != 0)
            {
                int toplam = 0;

                for (int i = 0; i < gelenKisiler.Count; i++)
                {
                    toplam += gelenKisiler.ElementAt<Grup>(i).kisiSayisi;
                }

                kisiSayisiLabel.Text = toplam.ToString();
                kisiSayisiLabel.Refresh();
            }
        }

        public void CikisSayisiYazdır()
        {
            cikisSayisiLabel.Text = "";
            cikisSayisiLabel.Text = cikisYapanSayisi.ToString();
            cikisSayisiLabel.Refresh();
        }
    }
}
