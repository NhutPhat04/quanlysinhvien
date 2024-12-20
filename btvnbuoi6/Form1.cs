using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using btvnbuoi6.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace btvnbuoi6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                Model1 context = new Model1();
                List<Faculty> listFalcultys = context.Faculties.ToList(); //l y các khoa
                List<Student> listStudent = context.Students.ToList(); //l y sinh viên
                FillFalcultyCombobox(listFalcultys);
                BindGrid(listStudent);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        private void FillFalcultyCombobox(List<Faculty> listFalcultys)
        {
            this.cbmkhoa.DataSource = listFalcultys;
            this.cbmkhoa.DisplayMember = "FacultyName";
            this.cbmkhoa.ValueMember = "FacultyID";
        }
        //Hàm binding gridView t list sinh viên
        private void BindGrid(List<Student> listStudent)
        {
            dataGridView1.Rows.Clear();
            foreach (var item in listStudent)
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells[0].Value = item.StudentID;
                dataGridView1.Rows[index].Cells[1].Value = item.FullName;
                dataGridView1.Rows[index].Cells[2].Value = item.Faculty.FacultyName;
                dataGridView1.Rows[index].Cells[3].Value = item.AverageScore;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtMSSV.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value?.ToString();
                txthoten.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value?.ToString();
                cbmkhoa.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value?.ToString();
                txtDTB.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value?.ToString();
            }
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            try
            {
                using (Model1 context = new Model1())
                {
                    Student newStudent = new Student
                    {
                        StudentID = (txtMSSV.Text),
                        FullName = txthoten.Text,
                        AverageScore = double.Parse(txtDTB.Text),
                        FacultyID = int.Parse(cbmkhoa.SelectedValue.ToString())
                    };

                    context.Students.Add(newStudent);
                    context.SaveChanges();

                    // Refresh DataGridView
                    BindGrid(context.Students.ToList());
                    MessageBox.Show("Thêm sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            try
            {
                using (Model1 context = new Model1())
                {
                    string studentID = txtMSSV.Text;
                    Student dbStudent = context.Students.FirstOrDefault(s => s.StudentID == studentID);

                    if (dbStudent != null)
                    {
                        dbStudent.FullName = txthoten.Text;
                        dbStudent.AverageScore = double.Parse(txtDTB.Text);
                        dbStudent.FacultyID = (int)cbmkhoa.SelectedValue;

                        context.SaveChanges();

                        // Refresh DataGridView
                        BindGrid(context.Students.ToList());
                        MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy sinh viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            try
            {
                using (Model1 context = new Model1())
                {
                    string studentID = txtMSSV.Text;
                    Student dbStudent = context.Students.FirstOrDefault(s => s.StudentID == studentID);

                    if (dbStudent != null)
                    {
                        context.Students.Remove(dbStudent);
                        context.SaveChanges();

                        // Refresh DataGridView
                        BindGrid(context.Students.ToList());
                        MessageBox.Show("Xóa sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy sinh viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    

    }
}
