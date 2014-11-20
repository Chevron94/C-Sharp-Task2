using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Task2
{
    delegate void SoldObjectEventHandler(object Sender, GoodsEnum goods, int count, int addit);
    delegate void AllSoldEventHandler(object Sender, GoodsEnum goods);

    class ZooShop
    {
        int cash = 10000;

        public event SoldObjectEventHandler Solded;
        public event AllSoldEventHandler AllSold;

        Pet[] Pets = new Pet[5];
        Item[] Items = new Item[5];
        PictureBox cash1;
        PictureBox cash2;
        Label c1_l;
        Label c2_l;
        Label CashLabel;

        string[] path_animals = { @"images\cat.jpg", @"images\fishes.jpg", @"images\mouse.jpg", @"images\parrot.jpg", @"images\tortoise.jpg" };
        string[] path_items = { @"images\auqarium.jpg", @"images\brush.jpg", @"images\cell.jpg", @"images\feed.jpg", @"images\toys.jpg" };
        
        GoodsEnum[] petEnum = { GoodsEnum.cat, GoodsEnum.fish, GoodsEnum.mouse, GoodsEnum.parrot, GoodsEnum.tortoise };
        GoodsEnum[] itemEnum = { GoodsEnum.aquarium, GoodsEnum.brush, GoodsEnum.cell, GoodsEnum.fish, GoodsEnum.toys };
        
        public ZooShop(Group[] animals, Group[] _items, PictureBox _cash1, PictureBox _cash2, Label cash1_lb, Label cash2_lb,Label _CashLabel)
        {
            cash1 = _cash1;
            cash2 = _cash2;
            c1_l = cash1_lb;
            c2_l = cash2_lb;
            CashLabel = _CashLabel;
            CashLabel.Text = cash.ToString();
            CashLabel.ForeColor = Color.Black;
            Random rnd = new Random();
            for (int i = 0; i < 5; i++)
            {
                Pets[i] = new Pet(animals[i].picture,path_animals[i],petEnum[i],animals[i].cost,animals[i].count,rnd.Next(5,20),rnd.Next(300,800));
                Items[i] = new Item(_items[i].picture, path_items[i], itemEnum[i], _items[i].cost, _items[i].count, rnd.Next(10, 40), rnd.Next(50, 500));
            }
        }

        public int Add(GoodsEnum ge, int count)
        {
            int tmp = (int)ge;
            int res = 0;
            if (tmp > 4)
            {
                res = count * Items[tmp - 5].Cost*8/10;
                Items[tmp - 5].Add(count);
                cash -= res;
                
            }
            else
            {
                res = count * Pets[tmp].Cost*8/10; 
                Pets[tmp].Add(count);
                cash -= res;
                
            }
            CashLabel.Text = cash.ToString();
            CashLabel.ForeColor = Color.Red;
            return res;
        }

        public int Sell(GoodsEnum ge, int count, int cash_num)
        {
            int tmp = (int)ge;
            int res = 0;
            Image tmp1;
            bool nul = false;
            if (tmp > 4)
            {
                if (!Items[tmp - 5].Sold(count))
                {
                    return 0;
                }
                else
                {
                    res = count * Items[tmp - 5].Cost;
                    OnSold(ge, count, res);
                    Items[tmp - 5].CountLable.ForeColor = Color.Red;
                    cash += res;
                    tmp1 = Items[tmp - 5].Image;
                    if (Items[tmp-5].Count <= 2)
                        nul = true;
                }
            }
            else
            {
                if (!Pets[tmp].Sold(count))
                {
                    return 0;
                }
                else
                {
                    res = count * Pets[tmp].Cost;
                    OnSold(ge, count, res);
                    Pets[tmp].CountLable.ForeColor = Color.Red;
                    cash += res;
                    tmp1 = Pets[tmp].Image;
                    if (Pets[tmp].Count <= 2)
                        nul = true;
                }
            }
            CashLabel.Text = cash.ToString();
            CashLabel.ForeColor = Color.GreenYellow;
            if (cash_num == 0)
            {
                cash1.Image = tmp1;
                c1_l.Text = count.ToString();
            }
            else
            {
                cash2.Image = tmp1;
                c2_l.Text = count.ToString();
            }

            lock(this)
            {
                Random x = new Random();
                Thread.Sleep (x.Next(900,1800));
            }

            if (cash_num == 0)
            {
                cash1.Image = null;
                c1_l.Text = null;
            }
            else
            {
                cash2.Image = null;
                c2_l.Text = null;
            }
            if (nul)
                OnAllSold(ge);

            if (tmp > 4)
            {
                if (Items[tmp - 5].CountLable.ForeColor == Color.Red)
                    Items[tmp - 5].CountLable.ForeColor = Color.Black;
            }
            else
            {
                if (Pets[tmp].CountLable.ForeColor == Color.Red)
                    Pets[tmp].CountLable.ForeColor = Color.Black;
            }
            return res;
        }

        protected void OnSold(GoodsEnum goods, int count, int addit)
        {
            if (Solded != null)
                Solded(this, goods,count,addit);
        }

        protected void OnAllSold(GoodsEnum goods)
        {
            if (AllSold != null)
                AllSold(this, goods);
        }

        public Pet[] PetsArray
        {
            get
            {
                return Pets;
            }
        }
        public Item[] ItemsArray
        {
            get
            {
                return Items;
            }
        }
    }
}
