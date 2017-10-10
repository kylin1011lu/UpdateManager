using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace UpdateManager
{
    public class ListViewCombox
    {
        MyListView _listView;
        ComboBox _combox;
        int _showColumn = 0;
        ListViewItem.ListViewSubItem _selectedSubItem;

        /// <summary>
        /// 列表combox
        /// </summary>
        /// <param name="listView">listView控件</param>
        /// <param name="combox">要呈现的combox控件</param>
        /// <param name="showColumn">要在哪一列显示combox(从0开始)</param>
        public ListViewCombox(MyListView listView, ComboBox combox, int showColumn)
        {
            _listView = listView;

            _listView.VScroll += combox_Leave;
            _listView.MouseWheel += combox_Leave;
            _combox = combox;
            _showColumn = showColumn;
            BindComboxEvent();
        }

        /// <summary>
        /// 定位combox
        /// </summary>
        /// <param name="x">点击的x坐标</param>
        /// <param name="y">点击的y坐标</param>
        public void Location(int x, int y)
        {
            ListViewItem item = _listView.GetItemAt(x, y);
            if (item != null)
            {
                _selectedSubItem = item.GetSubItemAt(x, y);
                if (_selectedSubItem != null)
                {
                    int clickColumn = item.SubItems.IndexOf(_selectedSubItem);
                    if (clickColumn == 0)
                    {
                        _combox.Visible = false;
                    }
                    else if (clickColumn == _showColumn)
                    {
                        int padding = 0;
                        Rectangle rect = _selectedSubItem.Bounds;
                        rect.X += _listView.Left + 2;
                        rect.Y += _listView.Top + padding;
                        rect.Width = _listView.Columns[clickColumn].Width + padding;
                        if (_combox != null)
                        {
                            _combox.Items.Clear();
                            if (_selectedSubItem.Tag != null)
                            {
                                _combox.Items.AddRange((string[])_selectedSubItem.Tag);
                            }
                            _combox.Bounds = rect;
                            _combox.Text = _selectedSubItem.Text;
                            _combox.Visible = true;
                            
                            _combox.BringToFront();
                            _combox.Focus();
                        }
                    }
                }
            }
        }

        private void BindComboxEvent()
        {
            if (_combox != null)
            {
                _combox.SelectedIndexChanged += combox_SelectedIndexChanged;
                _combox.Leave += combox_Leave;
            }
        }

        private void combox_Leave(object sender, EventArgs e)
        {
            if (_selectedSubItem != null && _combox.Visible)
            {
                _selectedSubItem.Text = _combox.Text;
                _combox.Items.Clear();
                _combox.Text ="";
                _combox.Visible = false;

                _listView.Focus();
            }
        }

        private void combox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_selectedSubItem != null)
            {
                _selectedSubItem.Text = _combox.Text;
                _combox.Visible = false;
            }
        }
    }
}
