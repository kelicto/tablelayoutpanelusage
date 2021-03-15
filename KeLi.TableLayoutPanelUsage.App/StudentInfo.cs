using System;

namespace KeLi.TableLayoutPanelUsage.App
{
    public class StudentInfo
    {
        public string StudentId { get; set; }

        public string StudentName { get; set; }

        public DateTime Birthday { get; set; }

        public string Address { get; set; }

        public StudentPropertyCollection PropertyList { get; set; }
    }
}