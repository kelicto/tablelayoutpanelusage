using System.Collections.ObjectModel;

namespace KeLi.TableLayoutPanelUsage.App
{
    public class StudentPropertyCollection : KeyedCollection<string, StudentProperty>
    {
        protected override string GetKeyForItem(StudentProperty item)
        {
            return item.PropertyName;
        }
    }
}