namespace KeLi.HelloTableLayoutPanel.App.Models
{
    public class GroupInfo
    {
        public GroupInfo(int groupIndex, string groupName)
        {
            GroupIndex = groupIndex;
            GroupName = groupName;
        }

        public int GroupIndex { get; set; }

        public string GroupName { get; set; }
    }
}