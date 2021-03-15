using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace KeLi.TableLayoutPanelUsage.App
{
    public static class TableLayoutPanelUtil
    {
        private static Label _lblCategory;

        private static int _groupCount;

        public static void AddController(this TableLayoutPanel tlpDefault, StudentProperty stuProp)
        {
            tlpDefault.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));

            if (tlpDefault.RowCount == 1)
            {
                // Refresh data.
                _lblCategory = null;

                var lblGroup = tlpDefault.AddLabel("Group", ContentAlignment.MiddleCenter);

                tlpDefault.Controls.Add(lblGroup, 0, tlpDefault.RowCount - 1);

                var lblName = tlpDefault.AddLabel("Parameter", ContentAlignment.MiddleCenter);

                tlpDefault.Controls.Add(lblName, 1, tlpDefault.RowCount - 1);

                var lblValue = tlpDefault.AddLabel("Value", ContentAlignment.MiddleCenter);

                tlpDefault.Controls.Add(lblValue, 2, tlpDefault.RowCount - 1);

                var lblUnit = tlpDefault.AddLabel("Unit", ContentAlignment.MiddleCenter);

                tlpDefault.Controls.Add(lblUnit, 3, tlpDefault.RowCount - 1);
            }

            tlpDefault.RowCount++;

            if (_lblCategory == null || _lblCategory.Text != stuProp.Group.GroupName)
            {
                _lblCategory = tlpDefault.AddLabel(stuProp.Group.GroupName, ContentAlignment.MiddleCenter);

                _lblCategory.Margin = new Padding(0);

                tlpDefault.Controls.Add(_lblCategory, 0, tlpDefault.RowCount - 1);

                _groupCount = 1;
            }

            else
            {
                _groupCount++;

                tlpDefault.SetRowSpan(_lblCategory, _groupCount);
            }

            // Label
            var labName = tlpDefault.AddLabel(stuProp.PropertyName, ContentAlignment.MiddleLeft);

            tlpDefault.Controls.Add(labName, 1, tlpDefault.RowCount - 1);

            // Value
            var ctrlValue = new Control();

            switch (stuProp.DataType)
            {
                case InputType.Label:
                {
                    ctrlValue = tlpDefault.AddLabel(stuProp.Text, ContentAlignment.MiddleLeft);

                    break;
                }

                case InputType.Text:
                {
                    ctrlValue = tlpDefault.AddTextBox(stuProp.Text, stuProp.ReadOnly);

                    break;
                }

                case InputType.Check:
                {
                    ctrlValue = tlpDefault.AddCheckBox(stuProp.PropertyName, (bool)stuProp.Text, stuProp.ReadOnly);

                    break;
                }

                case InputType.Select:
                {
                    ctrlValue = tlpDefault.AddComboBox(Convert.ToInt32(stuProp.Text), stuProp.Data);

                    break;
                }
            }

            tlpDefault.Controls.Add(ctrlValue, 2, tlpDefault.RowCount - 1);

            // Unit
            var unit = tlpDefault.AddLabel(stuProp.UnitName, ContentAlignment.MiddleLeft);

            tlpDefault.Controls.Add(unit, 3, tlpDefault.RowCount - 1);
        }

        public static Label AddLabel(this TableLayoutPanel tlpDefault, object text, ContentAlignment aligen)
        {
            return new Label
            {
                Text = text.ToString(),
                Anchor = AnchorStyles.Left,
                Dock = DockStyle.Fill,
                TextAlign = aligen
            };
        }

        public static TextBox AddTextBox(this TableLayoutPanel tlpDefault, object text, bool readOnly)
        {
            return new TextBox
            {
                Text = text.ToString(),
                Anchor = AnchorStyles.Left,
                Dock = DockStyle.Fill,
                TextAlign = HorizontalAlignment.Left,
                ReadOnly = readOnly
            };
        }

        public static Button AddButton(this TableLayoutPanel tlpDefault, object text, bool enable)
        {
            return new Button
            {
                Text = text.ToString(),
                Anchor = AnchorStyles.Left,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Enabled = enable
            };
        }

        public static CheckBox AddCheckBox(this TableLayoutPanel tlpDefault, object text, bool check, bool readOnly = false)
        {
            return new CheckBox
            {
                Text = text.ToString(),

                Anchor = AnchorStyles.Left,

                Dock = DockStyle.Fill,

                Checked = check,

                Enabled = readOnly
            };
        }

        public static ComboBox AddComboBox(this TableLayoutPanel tlpDefault, int index, object data)
        {
            var items = data.ToString().Replace(" ", string.Empty).Split(',').ToList();

            items.Insert(0, items[index]);
            items.RemoveAt(index + 1);

            return new ComboBox
            {
                Anchor = AnchorStyles.Left,

                Dock = DockStyle.Fill,

                DataSource = items,

                DropDownStyle = ComboBoxStyle.DropDownList
            };
        }
    }
}