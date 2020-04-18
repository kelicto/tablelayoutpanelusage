using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using KeLi.TableLayoutPanelUsage.App.Properties;

using Newtonsoft.Json;

namespace KeLi.TableLayoutPanelUsage.App
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            dgvStudent.AutoGenerateColumns = false;

            var stus = GetStudents();

            dgvStudent.DataSource = stus;

            tlpStudent.SuspendLayout();

            var properties = GetSortedStudentPropertyList(stus);

            tlpStudent.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

            tlpStudent.Height = properties.Count * 31 + 25;

            properties.ForEach(g => tlpStudent.AddController(g));

            tlpStudent.ResumeLayout();
        }

        private static List<StudentInfo> GetStudents()
        {
            var text = Encoding.UTF8.GetString(Resources.StudentInfo);

            return JsonConvert.DeserializeObject<List<StudentInfo>>(text);
        }

        private static List<StudentProperty> GetSortedStudentPropertyList(List<StudentInfo> stus)
        {
            var tmpStus = stus.Select(s => s.PropertyList).FirstOrDefault(f => f != null);

            return tmpStus.OrderBy(o => o.Group.GroupIndex).ThenBy(o => o.PropertyIndex).ToList();
        }

        private void DgvStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tlpStudent.Controls.Clear();

            tlpStudent.RowCount = 1;

            tlpStudent.SuspendLayout();

            var stuId = dgvStudent.SelectedRows[0].Cells[dgiStudentId.Name].Value.ToString();

            var stus = GetStudents().Where(f => f.StudentId == stuId).ToList();

            var properties = GetSortedStudentPropertyList(stus);

            properties.ForEach(f => tlpStudent.AddController(f));

            tlpStudent.ResumeLayout();
        }
    }
}