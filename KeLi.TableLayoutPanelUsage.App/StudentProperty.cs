namespace KeLi.TableLayoutPanelUsage.App
{
    public class StudentProperty
    {
        public StudentProperty(int propertyIndex, string propertyName, object text, object data, InputType type, GroupInfo groupInfo, string unitName, bool readOnly = true)
        {
            PropertyIndex = propertyIndex;
            PropertyName = propertyName;
            Text = text;
            Data = data;
            DataType = type;
            Group = groupInfo;
            UnitName = unitName;
            ReadOnly = readOnly;
        }

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

        public override string ToString()
        {
            return PropertyName;
        }
    }
}