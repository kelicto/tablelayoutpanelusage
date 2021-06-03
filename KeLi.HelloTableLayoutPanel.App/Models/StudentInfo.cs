using System;

namespace KeLi.HelloTableLayoutPanel.App.Models
{
    public class StudentInfo
    {
        public string StudentId { get; set; }

        public string StudentName { get; set; }

        public DateTime Birthday { get; set; }

        public string Address { get; set; }

        public StudentPropertyCollection Properties { get; set; }
    }
}