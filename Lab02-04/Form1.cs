using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab02_04
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lvAccounts.View = View.Details;
            lvAccounts.FullRowSelect = true;
            lvAccounts.GridLines = true;

            // Thêm các cột vào ListView
            lvAccounts.Columns.Add("STT", 50);
            lvAccounts.Columns.Add("Số tài khoản", 150);
            lvAccounts.Columns.Add("Tên khách hàng", 200);
            lvAccounts.Columns.Add("Địa chỉ", 250);
            lvAccounts.Columns.Add("Số tiền", 100);
        }

        private void btnAddOrUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra thông tin đầu vào
                if (string.IsNullOrWhiteSpace(txtAccountNumber.Text) ||
                    string.IsNullOrWhiteSpace(txtCustomerName.Text) ||
                    string.IsNullOrWhiteSpace(txtAddress.Text) ||
                    string.IsNullOrWhiteSpace(txtBalance.Text))
                {
                    throw new Exception("Vui lòng nhập đầy đủ thông tin tài khoản!");
                }

                // Kiểm tra số tiền có hợp lệ không
                if (!decimal.TryParse(txtBalance.Text, out decimal balance))
                {
                    throw new Exception("Số tiền không hợp lệ!");
                }

                // Kiểm tra tài khoản đã tồn tại trong ListView chưa
                ListViewItem existingItem = null;
                foreach (ListViewItem item in lvAccounts.Items)
                {
                    if (item.SubItems[1].Text == txtAccountNumber.Text)
                    {
                        existingItem = item;
                        break;
                    }
                }

                if (existingItem != null) // Cập nhật
                {
                    existingItem.SubItems[2].Text = txtCustomerName.Text;
                    existingItem.SubItems[3].Text = txtAddress.Text;
                    existingItem.SubItems[4].Text = balance.ToString();
                    MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK);
                }
                else // Thêm mới
                {
                    ListViewItem newItem = new ListViewItem((lvAccounts.Items.Count + 1).ToString());
                    newItem.SubItems.Add(txtAccountNumber.Text);
                    newItem.SubItems.Add(txtCustomerName.Text);
                    newItem.SubItems.Add(txtAddress.Text);
                    newItem.SubItems.Add(balance.ToString());
                    lvAccounts.Items.Add(newItem);
                    MessageBox.Show("Thêm mới dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK);
                }

                UpdateTotalBalance();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvAccounts.SelectedItems.Count == 0)
                {
                    throw new Exception("Vui lòng chọn tài khoản cần xóa!");
                }

                ListViewItem selectedItem = lvAccounts.SelectedItems[0];
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    lvAccounts.Items.Remove(selectedItem);
                    MessageBox.Show("Xóa tài khoản thành công!", "Thông báo", MessageBoxButtons.OK);
                    UpdateTotalBalance();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lvAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvAccounts.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = lvAccounts.SelectedItems[0];
                txtAccountNumber.Text = selectedItem.SubItems[1].Text;
                txtCustomerName.Text = selectedItem.SubItems[2].Text;
                txtAddress.Text = selectedItem.SubItems[3].Text;
                txtBalance.Text = selectedItem.SubItems[4].Text;
            }
        }
        private void UpdateTotalBalance()
        {
            decimal total = 0;
            foreach (ListViewItem item in lvAccounts.Items)
            {
                total += decimal.Parse(item.SubItems[4].Text);
            }
            txtBalance.Text = $"{total}";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
