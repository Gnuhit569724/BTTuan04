using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private int GetSelectedRow(string studentID)
        {
            for (int i = 0; i < dgvStudent.Rows.Count; i++)
            {
                if (!dgvStudent.Rows[i].IsNewRow && dgvStudent.Rows[i].Cells[0].Value != null)
                {
                    if (dgvStudent.Rows[i].Cells[0].Value.ToString() == studentID)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        private void InsertUpdate(int selectedRow)
        {
            dgvStudent.Rows[selectedRow].Cells[0].Value = txtStudentID.Text;
            dgvStudent.Rows[selectedRow].Cells[1].Value = txtFullName.Text;
            dgvStudent.Rows[selectedRow].Cells[2].Value = optFemale.Checked ? "Nữ" : "Nam";
            dgvStudent.Rows[selectedRow].Cells[3].Value = float.Parse(txtAverageScore.Text).ToString();
            dgvStudent.Rows[selectedRow].Cells[4].Value = cmbFaculty.Text;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtStudentID.Text == "" || txtFullName.Text == "" || txtAverageScore.Text == "") throw new Exception("Vui lòng nhập đầy đủ thông tin sinh viên!");
                int selectedRow = GetSelectedRow(txtStudentID.Text);
                if (selectedRow == -1) // TH Thêm mới
                {
                    selectedRow = dgvStudent.Rows.Add();
                    InsertUpdate(selectedRow);
                    MessageBox.Show("Thêm mới dữ liệu thành công!", "Thông Bảo", MessageBoxButtons.OK);
                }
                else
                {
                    InsertUpdate(selectedRow);
                    MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông Báo", MessageBoxButtons.OK);
                }
                UpdateGenderCount();
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
                int selectedRow = GetSelectedRow(txtStudentID.Text);
                if (selectedRow == -1)
                {
                    throw new Exception("Không tìm thấy MSSV cần xóa!");
                }
                else
                {
                    DialogResult dr = MessageBox.Show("Bạn có muốn xóa ?", "YES/NO", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        dgvStudent.Rows.RemoveAt(selectedRow);
                        MessageBox.Show("Xóa sinh viên thành công!", "Thông Báo", MessageBoxButtons.OK);
                    }
                }
                UpdateGenderCount();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbFaculty.SelectedIndex = 0;
            optFemale.Checked = true; 
            UpdateGenderCount(); 
        }
        private void UpdateGenderCount()
        {
            int maleCount = 0;
            int femaleCount = 0;

            foreach (DataGridViewRow row in dgvStudent.Rows)
            {
                if (!row.IsNewRow && row.Cells[2].Value != null)
                {
                    string gender = row.Cells[2].Value.ToString().Trim();
                    if (gender == "Nam")
                        maleCount++;
                    else if (gender == "Nữ")
                        femaleCount++;
                }
            }

            txtMaleCount.Text = $"{maleCount}";
            txtFemaleCount.Text = $"{femaleCount}";
        }

        private void dgvStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow selectedRow = dgvStudent.Rows[e.RowIndex];

            // Hiển thị dữ liệu từ hàng được chọn lên các ô bên trái
            txtStudentID.Text = selectedRow.Cells[0].Value?.ToString() ?? "";
            txtFullName.Text = selectedRow.Cells[1].Value?.ToString() ?? "";
            txtAverageScore.Text = selectedRow.Cells[3].Value?.ToString() ?? "";
            cmbFaculty.Text = selectedRow.Cells[4].Value?.ToString() ?? "";

            // Xác định giới tính và chọn radio button phù hợp
            string gender = selectedRow.Cells[2].Value?.ToString();
            if (gender == "Nam")
            {
                optMale.Checked = true;
            }
            else if (gender == "Nữ")
            {
                optFemale.Checked = true;
            }
        }
    }
}
