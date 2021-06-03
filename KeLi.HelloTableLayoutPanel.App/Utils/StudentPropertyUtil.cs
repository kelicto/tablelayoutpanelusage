using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using KeLi.HelloTableLayoutPanel.App.Models;

namespace KeLi.HelloTableLayoutPanel.App.Utils
{
    public static class StudentPropertyUtil
    {
        private static Label _lblCategory;

        private static int _groupCount;

        public static void AddControl(this TableLayoutPanel tlp, StudentProperty studentProperty)
        {
            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));

            if (tlp.RowCount == 1)
            {
                // Refresh data.
                _lblCategory = null;

                var lblGroup = CreateLabel("Group", ContentAlignment.MiddleCenter);

                tlp.Controls.Add(lblGroup, 0, tlp.RowCount - 1);

                var lblParameterName = CreateLabel("Parameter", ContentAlignment.MiddleCenter);

                tlp.Controls.Add(lblParameterName, 1, tlp.RowCount - 1);

                var lblValue = CreateLabel("Value", ContentAlignment.MiddleCenter);

                tlp.Controls.Add(lblValue, 2, tlp.RowCount - 1);

                var lblUnit = CreateLabel("Unit", ContentAlignment.MiddleCenter);

                tlp.Controls.Add(lblUnit, 3, tlp.RowCount - 1);
            }

            tlp.RowCount++;

            if (_lblCategory == null || _lblCategory.Text != studentProperty.Group.GroupName)
            {
                _lblCategory = CreateLabel(studentProperty.Group.GroupName, ContentAlignment.MiddleCenter);
                _lblCategory.Margin = new Padding(0);

                tlp.Controls.Add(_lblCategory, 0, tlp.RowCount - 1);

                _groupCount = 1;
            }

            else
            {
                _groupCount++;

                tlp.SetRowSpan(_lblCategory, _groupCount);
            }

            // Label
            {
                var labParameterName = CreateLabel(studentProperty.PropertyName, ContentAlignment.MiddleLeft);

                tlp.Controls.Add(labParameterName, 1, tlp.RowCount - 1);
            }

            // Value
            {
                var ctrlValue = new Control();

                if (studentProperty.DataType == InputType.Label)
                    ctrlValue = CreateLabel(studentProperty.Text, ContentAlignment.MiddleLeft);

                else if (studentProperty.DataType == InputType.Text)
                    ctrlValue = CreateTextBox(studentProperty.Text, studentProperty.ReadOnly);

                else if (studentProperty.DataType == InputType.Check)
                    ctrlValue = CreateCheckBox(studentProperty.PropertyName, (bool)studentProperty.Text, studentProperty.ReadOnly);

                else if (studentProperty.DataType == InputType.Select)
                    ctrlValue = CreateComboBox(Convert.ToInt32(studentProperty.Text), studentProperty.Data);

                tlp.Controls.Add(ctrlValue, 2, tlp.RowCount - 1);
            }

            // Unit
            {
                var unit = CreateLabel(studentProperty.UnitName, ContentAlignment.MiddleLeft);

                tlp.Controls.Add(unit, 3, tlp.RowCount - 1);
            }
        }

        public static Label CreateLabel(object text, ContentAlignment aligen)
        {
            return new Label
            {
                Text = text.ToString(),
                Anchor = AnchorStyles.Left,
                Dock = DockStyle.Fill,
                TextAlign = aligen
            };
        }

        public static TextBox CreateTextBox(object text, bool readOnly)
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

        public static Button CreateButton(object text, bool enable)
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

        public static CheckBox CreateCheckBox(object text, bool check, bool readOnly = false)
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

        public static ComboBox CreateComboBox(int index, object data)
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