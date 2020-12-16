using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsansorProjesiForm
{
    public partial class Form1 : Form
    {
        List<Kat> katlar;
        List<Label> asansorTabloComponent;
        Asansor asansor;
        Giris giris;
        Cikis cikis;

        public Form1()
        {
            InitializeComponent();

            lblFloor1QueueYazdir.Text = "";
            lblFloor2QueueYazdir.Text = "";
            lblFloor3QueueYazdir.Text = "";
            lblFloor4QueueYazdir.Text = "";
            lblExitCount.Text = "";



        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;

            katlar = new List<Kat>()
            {
                { new Kat(0,lblFloor0Queue,null,lblFloor0QueueYazdir, lblExitCount) },
                { new Kat(1,lblFloor1Queue,lblFloor1All,lblFloor1QueueYazdir, null) },
                { new Kat(2,lblFloor2Queue,lblFloor2All,lblFloor2QueueYazdir, null) },
                { new Kat(3,lblFloor3Queue,lblFloor3All,lblFloor3QueueYazdir, null) },
                { new Kat(4,lblFloor4Queue,lblFloor4All,lblFloor4QueueYazdir, null) },
            };

            giris = new Giris(katlar);
            cikis = new Cikis(katlar);

            asansorTabloComponent = new List<Label>()
            {
                { asansor1Active },
                { asansor1capatiy },
                { asansor1Countİnside },
                { asansor1destination },
                { asansor1direction },
                { asansor1floor },
                { asansor1inside },
                { asansor1mode },
            };

            asansor = new Asansor(katlar, asansorTabloComponent)
            {
                mod = AsansorModKomutları.working,
            };

            asansor.asansorThread.Start();
        }
    }
}
