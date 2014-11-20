using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Task2
{
    class ClientFactory
    {
        Random rnd = new Random();
        string[] path = {   
                            @"images\clients\client1_l.jpg", @"images\clients\client1_r.jpg", 
                            @"images\clients\client2_l.jpg", @"images\clients\client2_r.jpg", 
                            @"images\clients\client3_l.jpg", @"images\clients\client3_r.jpg", 
                        };

        public ClientFactory()
        {
        }

        public Client MakeOne(PictureBox _pic, ZooShop shop, int cash_num)
        {
            int tmp = rnd.Next(0, 3);
            GoodsEnum ge = (GoodsEnum)rnd.Next(0, 10);
            int count = 0;
            if ((int)ge < 4)
                count = rnd.Next(1, 5);
            else count = rnd.Next(1, 7);
            Image left = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + path[2*tmp]);
            Image right = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + path[2 * tmp+1]);
            Client result = new Client(ge,count, shop, left,right,_pic, cash_num);
            return result;
        }
    }
}
