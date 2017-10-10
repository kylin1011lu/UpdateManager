using System;
using System.Windows.Forms;

namespace UpdateManager
{
    public partial class MyListView : ListView
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int ShowScrollBar(IntPtr hWnd, int iBar, int bShow);
        const int SB_HORZ = 0;
        const int SB_VERT = 1;

        public event EventHandler HScroll;
        public event EventHandler VScroll;

        const int WM_HSCROLL = 0x0114;
        const int WM_VSCROLL = 0x0115;
        protected override void WndProc(ref Message m)
        {
            if (this.View == View.List || this.View == View.Details)
            {
                ShowScrollBar(this.Handle, SB_HORZ, 0);
            }

            if (m.Msg == WM_HSCROLL)
            {
                OnHScroll(this, new EventArgs());
            }
            else if (m.Msg == WM_VSCROLL)
            {
                OnVScroll(this, new EventArgs());
            }
            base.WndProc(ref m);
        }
        virtual protected void OnHScroll(object sender, EventArgs e)
        {
            if (HScroll != null)
                HScroll(this, e);
        }
        virtual protected void OnVScroll(object sender, EventArgs e)
        {
            if (VScroll != null)
                VScroll(this, e);
        }    
    }
}
