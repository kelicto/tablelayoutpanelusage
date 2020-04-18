/*
 * MIT License
 *
 * Copyright(c) 2020 KeLi
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

/*
             ,---------------------------------------------------,              ,---------,
        ,----------------------------------------------------------,          ,"        ,"|
      ,"                                                         ,"|        ,"        ,"  |
     +----------------------------------------------------------+  |      ,"        ,"    |
     |  .----------------------------------------------------.  |  |     +---------+      |
     |  | C:\>FILE -INFO                                     |  |  |     | -==----'|      |
     |  |                                                    |  |  |     |         |      |
     |  |                                                    |  |  |/----|`---=    |      |
     |  |              Author: KeLi                          |  |  |     |         |      |
     |  |              Email: kelistudy@163.com              |  |  |     |         |      |
     |  |              Creation Time: 04/19/2020 01:00:00 PM |  |  |     |         |      |
     |  | C:\>_                                              |  |  |     | -==----'|      |
     |  |                                                    |  |  |   ,/|==== ooo |      ;
     |  |                                                    |  |  |  // |(((( [66]|    ,"
     |  `----------------------------------------------------'  |," .;'| |((((     |  ,"
     +----------------------------------------------------------+  ;;  | |         |,"
        /_)_________________________________________________(_/  //'   | +---------+
           ___________________________/___  `,
          /  oooooooooooooooo  .o.  oooo /,   \,"-----------
         / ==ooooooooooooooo==.o.  ooo= //   ,`\--{)B     ,"
        /_==__==========__==_ooo__ooo=_/'   /___________,"
*/

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