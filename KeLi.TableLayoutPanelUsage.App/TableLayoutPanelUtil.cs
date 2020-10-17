/*
 * MIT License
 *
 * Copyright(c) 2020 KeLi
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

/*
             ,---------------------------------------------------,              ,---------,
        ,----------------------------------------------------------,          ,"        ,"|
      ,"                                                         ,"|        ,"        ,"  |
     +----------------------------------------------------------+  |      ,"        ,"    |
     |  .----------------------------------------------------.  |  |     +---------+      |
     |  | C:\>FILE -INFO                                     |  |  |     | -==----'|      |
     |  |                                                    |  |  |     |         |      |
     |  |                                                    |  |  |/----|`---=    |      |
     |  |              Author: KeLi                          |  |  |     |         |      |
     |  |              Email: kelicto@protonmail.com         |  |  |     |         |      |
     |  |              Creation Time: 04/19/2020 01:00:00 PM |  |  |     |         |      |
     |  | C:\>_                                              |  |  |     | -==----'|      |
     |  |                                                    |  |  |   ,/|==== ooo |      ;
     |  |                                                    |  |  |  // |(((( [66]|    ,"
     |  `----------------------------------------------------'  |," .;'| |((((     |  ,"
     +----------------------------------------------------------+  ;;  | |         |,"
        /_)_________________________________________________(_/  //'   | +---------+
           ___________________________/___  `,
          /  oooooooooooooooo  .o.  oooo /,   \,"-----------
         / ==ooooooooooooooo==.o.  ooo= //   ,`\--{)B     ,"
        /_==__==========__==_ooo__ooo=_/'   /___________,"
*/

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