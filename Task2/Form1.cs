using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Task2
{
    struct Group
    {
        public PictureBox picture;
        public Label cost;
        public Label count;
    }
    public partial class Form1 : Form
    {
        Manager man;
        Thread t;
        Point pos1 = new Point( 840, 202 );
        Point pos2 = new Point( 840, 487 );
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void Start_Click(object sender, EventArgs e)
        {
            string filename = @"images\table.png";
            Table_1.Image = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + filename);
            Table_2.Image = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + filename);
            filename = @"images\cash.jpg";
            Cash1.Image = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + filename);
            Cash2.Image = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + filename);
            Group[] animal_gr = {
                                    new Group(){picture = animal1, cost=Price_an1, count= Count_an1}, 
                                    new Group(){picture = animal2, cost=Price_an2, count= Count_an2},
                                    new Group(){picture = animal3, cost=Price_an3, count= Count_an3},
                                    new Group(){picture = animal4, cost=Price_an4, count= Count_an4},
                                    new Group(){picture = animal5, cost=Price_an5, count= Count_an5},
                                };
            Group[] item_gr = {
                                    new Group(){picture = item1, cost=Price_it1, count= Count_it1}, 
                                    new Group(){picture = item2, cost=Price_it2, count= Count_it2},
                                    new Group(){picture = item3, cost=Price_it3, count= Count_it3},
                                    new Group(){picture = item4, cost=Price_it4, count= Count_it4},
                                    new Group(){picture = item5, cost=Price_it5, count= Count_it5},
                                };
            //ZooShop shop = new ZooShop(animal_gr, item_gr,Cash_1_Goods,Cash_2_Goods);
            man = new Manager(animal_gr, item_gr, Cash_1_Goods, Cash_2_Goods, log,Client1,Client2,Cash1_lb,Cash2_lb, CashLabel);
            t = new Thread(new ThreadStart(man.run));
            t.Start();
            Stop.Enabled = true;
            Start.Enabled = false;
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            man.Stop();
            t.Abort();
            Client1.Image = null;
            Client2.Image = null;
            Client1.Location = pos1;
            Client2.Location = pos2;
            log.Items.Clear();
            Stop.Enabled = false;
            Start.Enabled = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Stop.PerformClick();
        }
    }
}
