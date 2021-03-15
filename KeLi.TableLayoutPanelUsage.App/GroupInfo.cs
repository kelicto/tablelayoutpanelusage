namespace KeLi.TableLayoutPanelUsage.App
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