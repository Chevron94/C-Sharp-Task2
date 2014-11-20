using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Task2
{
    class Item:Goods
    {
        public Item(PictureBox pic, string picture, GoodsEnum en, Label _costL, Label _countL ,int _count, int _cost)
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

    }
}
