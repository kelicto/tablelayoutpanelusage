namespace KeLi.HelloTableLayoutPanel.App.Models
{
    public class StudentProperty
    {
        public int PropertyIndex { get; set; }

        public string PropertyName { get; set; }

        public object Text { get; set; }

        public object Data { get; set; }

        public string UnitName { get; set; }

        public InputType DataType { get; set; }

        public bool ReadOnly { get; set; }

        public string Tag { get; set; }

        public GroupInfo Group { get; set; }

        public ProjectInfo Project { get; set; }

        public CustuomInfo Custom { get; set; }

        public string Note { get; set; }
    }
}