using System;
using System.Linq;
using System.Windows.Forms;

using KeLi.HelloTableLayoutPanel.App.Utils;

namespace KeLi.HelloTableLayoutPanel.App
{
    public partial class MainFrm : Form
    {
        private readonly StudentDataService _service;

        public MainFrm()
        {
            _service = new StudentDataService();

            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            dgvStudent.AutoGenerateColumns = false;

            var studentInfos = _service.GetStudentInfos();

            dgvStudent.DataSource = studentInfos;

            if (!studentInfos.Any())
                return;

            tlpStudent.SuspendLayout();

            var properties = _service.GetSortedStudentProperties(studentInfos.First());

            tlpStudent.Height = properties.Count * 31 + 25;
            properties.ForEach(g => tlpStudent.AddController(g));

            tlpStudent.ResumeLayout();
        }

        private void DgvStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tlpStudent.Controls.Clear();
            tlpStudent.RowCount = 1;

            tlpStudent.SuspendLayout();

            var studentId = dgvStudent.SelectedRows[0].Cells[dgiStudentId.Name].Value.ToString();
            var studentInfo = _service.GetStudentInfos().FirstOrDefault(f => f.StudentId == studentId);
            var properties = _service.GetSortedStudentProperties(studentInfo);

            properties.ForEach(f => tlpStudent.AddController(f));

            tlpStudent.ResumeLayout();
        }
    }
}