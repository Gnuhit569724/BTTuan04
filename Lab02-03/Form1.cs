using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab02_03
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeSeats();
        }

        private void button11_Click(object sender, EventArgs e)
        {

        }

        private int totalPrice = 0;

        private void InitializeSeats()
        {
            int tag = 1;
            foreach (Control control in this.Controls)
            {
                if (control is Button btn && btn.Name.StartsWith("button"))
                {
                    btn.Tag = tag.ToString(); // Gán Tag tương ứng
                    btn.BackColor = Color.White; // Màu mặc định là trắng
                    tag++;
                }
            }
        }

        private void btnChooseASeat(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.BackColor == Color.White) btn.BackColor = Color.Blue;
            else if (btn.BackColor == Color.Blue) btn.BackColor = Color.White;
            else if (btn.BackColor == Color.Yellow) MessageBox.Show("Ghế đã được bán!!");
        }

        private void button16_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private int GetSeatPrice(Button btn)
        {
            int seatNumber = int.Parse(btn.Tag.ToString());
            if (seatNumber >= 1 && seatNumber <= 5) return 30000;
            if (seatNumber >= 6 && seatNumber <= 10) return 40000;
            if (seatNumber >= 11 && seatNumber <= 15) return 50000;
            if (seatNumber >= 16 && seatNumber <= 20) return 80000;
            return 0;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            totalPrice = 0;
            foreach (Control control in this.Controls)
            {
                if (control is Button btn && btn.BackColor == Color.Blue)
                {
                    btn.BackColor = Color.Yellow; // Đổi sang màu vàng (đã bán)
                    totalPrice += GetSeatPrice(btn); // Cộng giá ghế
                }
            }
            lblTotalPrice.Text = totalPrice.ToString(); // Hiển thị tổng tiền
        }

        private void button17_Click(object sender, EventArgs e)
        {
            foreach (Control control in this.Controls)
            {
                if (control is Button btn && btn.BackColor == Color.Blue)
                {
                    btn.BackColor = Color.White; // Đổi lại màu trắng
                }
            }
            totalPrice = 0;
            lblTotalPrice.Text = totalPrice.ToString(); // Reset tổng tiền
        }

    }
}