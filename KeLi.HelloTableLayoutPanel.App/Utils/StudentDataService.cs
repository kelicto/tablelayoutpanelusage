using System.Collections.Generic;
using System.Linq;
using System.Text;

using KeLi.HelloTableLayoutPanel.App.Models;
using KeLi.HelloTableLayoutPanel.App.Properties;

using Newtonsoft.Json;

namespace KeLi.HelloTableLayoutPanel.App.Utils
{
    public class StudentDataService
    {
        public List<StudentInfo> GetStudentInfos()
        {
            var text = Encoding.UTF8.GetString(Resources.StudentInfo);

            return JsonConvert.DeserializeObject<List<StudentInfo>>(text);
        }

        public List<StudentProperty> GetSortedStudentProperties(StudentInfo studentInfo)
        {
            return studentInfo.Properties.OrderBy(o => o.Group.GroupIndex).ThenBy(o => o.PropertyIndex).ToList();
        }
    }
}
