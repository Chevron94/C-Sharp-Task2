using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace Task2
{
    enum GoodsEnum {cat,fish,mouse,parrot,tortoise,aquarium,brush,cell,feed,toys}
    
    class Goods
    {
        public Image Image { get; set; }
        public Label CostLable { get; set; }
        public Label CountLable { get; set; }
        public int Count { get; set; }
        public GoodsEnum GoodsType { get; set; }
        public int Cost { get; set; }
        public PictureBox Preview { get; set; }
        public bool Sold(int count)
        {
            if (count > Count)
                return false;
            else
            {
                Count -= count;
                CountLable.Text = Count.ToString();
                return true;
            }
        }
        public void Add(int count)
        {
            Count += count;
            CountLable.Text = Count.ToString();
            CountLable.ForeColor = Color.GreenYellow;
        }
    }

}
