using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Task2
{
    class Client
    { 
        GoodsEnum ge;
        int count;
        Image left, right;
        PictureBox pic;
        Point Start;
        ZooShop Shop;
        int cash_num;
        static Semaphore c1 = new Semaphore(1, 1);
        static Semaphore c2 = new Semaphore(1, 1);
        public Client(GoodsEnum _ge, int _count, ZooShop shop, Image _left, Image _right, PictureBox _pic, int _cash_num)
        {
            ge = _ge;
            count = _count;
            left = _left;
            right = _right;
            cash_num = _cash_num;
            pic = _pic;
            Start = pic.Location;
            Shop = shop;
        }

        public void MoveLeft()
        {
            int left_border = 200;
            pic.Image = left;
            for (int i = pic.Location.X; i > left_border; i-=5)
            {
                Thread.Sleep(30);
                pic.Location = new Point(i, Start.Y);
            }
        }

        public void MoveRightAndExit()
        {
            pic.Image = right;
            for (int i = pic.Location.X; i < Start.X; i+=5)
            {
                Thread.Sleep(30);
                pic.Location = new Point(i, Start.Y);
            }
            pic.Image = null;
         }

        public void Buy()
        {
            if (cash_num == 1)
                c1.WaitOne();
            else c2.WaitOne();
            MoveLeft();
            Shop.Sell(ge, count, cash_num);
            MoveRightAndExit();
            if (cash_num == 1)
                c1.Release();
            else c2.Release();
        }

        public static void ResetSemaphores()
        {
            c1 = new Semaphore(1, 1);
            c2 = new Semaphore(1, 1);
        }

        public GoodsEnum Type
        {
            get
            {
                return ge;
            }
        }
        public int Count
        {
            get
            {
                return count;
            }
        }
    }
}
