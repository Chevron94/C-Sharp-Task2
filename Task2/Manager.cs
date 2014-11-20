using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace Task2
{
    class Manager
    {
        ZooShop shop;
        Client[] boughters;
        Thread[] threads;
        Thread DiedThread;
        bool stop;
        Random rnd;
        ListBox log;
        PictureBox cl1, cl2;
        public Manager(Group[] animals, Group[] _items, PictureBox _cash1, PictureBox _cash2, ListBox _log, PictureBox c1, PictureBox c2, Label cash1_lb, Label cash2_lb, Label _CashLabel)
        {
            shop = new ZooShop(animals, _items, _cash1, _cash2, cash1_lb, cash2_lb, _CashLabel);
            stop = false;
            log = _log;
            rnd = new Random();
            cl1 = c1;
            cl2 = c2;
            shop.AllSold += new AllSoldEventHandler(AddHandle);
            shop.Solded += new SoldObjectEventHandler(SoldHandle);
            for (int i = 0; i < 5; i++)
                shop.PetsArray[i].AllDied += new AllDiedEventHandler(AddHandle);
            DiedThread = new Thread(new ThreadStart(Died));
        }

        public void Died()
        {
            while (true)
            {
                Thread.Sleep(15000);
                int index = rnd.Next(0, 5);
                shop.PetsArray[index].Die();
                log.Items.Add("Умер " + ((GoodsEnum)index).ToString());
                Thread.Sleep(2000);
                if (shop.PetsArray[index].CountLable.ForeColor == Color.DarkGray)
                    shop.PetsArray[index].CountLable.ForeColor = Color.Black;
            }
        }

        private void AddHandle(object sender, GoodsEnum goods)
        {
            log.Items.Add("Почти закончились " + goods.ToString());;
            int index = (int)goods;
            int count = 0;
            if (index / 5 == 0)
            {
                count = rnd.Next(4, 11);
            }
            else count = rnd.Next(15, 25);
            int resSum = shop.Add(goods, count);
            log.Items.Add("Завезено " + count.ToString() + " " + ((GoodsEnum)index).ToString()+". Расходы составили "+resSum.ToString()+" руб");
        }

        private void SoldHandle(object sender, GoodsEnum goods, int count, int addit)
        {
            log.Items.Add("Продано " + count.ToString() + " " + goods.ToString() + ". Доходы составили " + addit.ToString()+" руб");
        }

        public void Stop()
        {
            stop = true;
            for (int i = 0; i < threads.Length; i++)
                threads[i].Abort();
            DiedThread.Abort();
            Client.ResetSemaphores();
        }

        public void run()
        {
            ClientFactory cl = new ClientFactory();
            boughters = new Client[5];
            threads = new Thread[boughters.Length];
            DiedThread.Start();
            while (!stop)
            {
                for (int i = 0; i < boughters.Length; i+=2)
                {
                    if (threads[i] == null || !threads[i].IsAlive)
                    {
                        boughters[i] = cl.MakeOne(cl1, shop, 0);
                        threads[i] = new Thread(new ThreadStart(boughters[i].Buy));
                        threads[i].Start();
                    }
                    
                    if (i+1 < boughters.Length)
                    {
                        if (threads[i+1] == null || !threads[i + 1].IsAlive)
                        {
                            boughters[i + 1] = cl.MakeOne(cl2, shop, 1);
                            threads[i + 1] = new Thread(new ThreadStart(boughters[i + 1].Buy));
                            threads[i + 1].Start();
                        }
                    }
                }
            }
        }
    }
}
