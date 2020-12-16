using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsansorProjesiForm
{
    public enum AsansorModKomutları
    {
        idle, working
    }

    public enum AsansorYonKomutları
    {
        down, up
    }

    public enum AsansorMusteriKomutları
    {
        YolcuGotur, YolcuTopla
    }

    class Utilites
    {
        public static readonly int avmKatSayisi = 5;
        public static readonly int asansorSayisi = 5;
        public static readonly int asansorKapasite = 10;
        public static readonly int asansorKatGecisSuresi = 200;
        public static readonly int girisThreadCalismaSuresi = 500;
        public static readonly int cikisThreadCalismaSuresi = 1000;
    }
}
