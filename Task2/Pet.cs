using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace Task2
{
    delegate void AllDiedEventHandler(object Sender, GoodsEnum goods);
    class Pet:Goods
    {
        public event AllDiedEventHandler AllDied;
        public Pet(PictureBox pic, string picture, GoodsEnum en, Label _costL, Label _countL ,int _count, int _cost)
        {
            Preview = pic;
            Count = _count;
            Cost = _cost;
            string filename = picture;
            Image = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + filename);
            Preview.Image = Image;
            GoodsType = en;
            CostLable = _costL;
            CostLable.Text = Cost.ToString()+" руб";
            CountLable = _countL;
            CountLable.Text = Count.ToString();
        }
        public void Die()
        {
            Count --;
            CountLable.Text = Count.ToString();
            CountLable.ForeColor = Color.DarkGray;
            if (Count == 0)
                OnAllDied(GoodsType);
        }

        protected void OnAllDied(GoodsEnum goods)
        {
            if (AllDied != null)
                AllDied(this, goods);
        }
    }
}
