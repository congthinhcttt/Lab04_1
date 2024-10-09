using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab04_1.QuanLySinhVien
{
    public partial class frmFalculty : Form
    {
        private Model1 dbContext;
        public frmFalculty()
        {
            InitializeComponent();
            dbContext = new Model1();
        }

        private void LoadData()
        {
            dataGridView1.DataSource = dbContext.Faculties.Select(f => new
            {
                f.FacultyID,
                f.FacultyName,
                f.TotalProfessor
            }).ToList();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var selectedFaculty = dataGridView1.CurrentRow.DataBoundItem as dynamic;
            if (selectedFaculty != null)
            {
                txtMaKhoa.Text = selectedFaculty.FacultyID.ToString();
                txtTenKhoa.Text = selectedFaculty.FacultyName;
                txtTong.Text = selectedFaculty.TotalProfessor?.ToString() ?? "";
            }
        }
        private void ClearFields()
        {
            txtMaKhoa.Clear();
            txtTenKhoa.Clear();
            txtTong.Clear();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            var faculty = new Faculty
            {
                FacultyName = txtTenKhoa.Text,
                TotalProfessor = int.TryParse(txtTong.Text, out int professors) ? professors : (int?)null
            };

            dbContext.Faculties.Add(faculty);
            dbContext.SaveChanges();
            LoadData();
            ClearFields();
        }

        private void frmFalculty_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            var selectedFaculty = dataGridView1.CurrentRow.DataBoundItem as dynamic;
            if (selectedFaculty != null)
            {
                var faculty = dbContext.Faculties.Find(selectedFaculty.FacultyID);
                if (faculty != null)
                {
                    faculty.FacultyName = txtTenKhoa.Text;
                    faculty.TotalProfessor = int.TryParse(txtTong.Text, out int professors) ? professors : (int?)null;

                    dbContext.SaveChanges();
                    LoadData();
                    ClearFields();
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            var selectedFaculty = dataGridView1.CurrentRow.DataBoundItem as dynamic;
            if (selectedFaculty != null)
            {
                var faculty = dbContext.Faculties.Find(selectedFaculty.FacultyID);
                if (faculty != null)
                {
                    dbContext.Faculties.Remove(faculty);
                    dbContext.SaveChanges();
                    LoadData();
                    ClearFields();
                }
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn Đang Muốn Thoát", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Close();
            }
        }
    }
}
