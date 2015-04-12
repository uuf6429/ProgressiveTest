using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;

namespace CS.TestUtils
{
    public partial class ProgressForm : Form
    {
        public ProgressForm(string title)
        {
            InitializeComponent();
            this.Text = title;
        }

        enum HitTest
        {
            Caption = 2,
            Transparent = -1,
            Nowhere = 0,
            Client = 1,
            Left = 10,
            Right = 11,
            Top = 12,
            TopLeft = 13,
            TopRight = 14,
            Bottom = 15,
            BottomLeft = 16,
            BottomRight = 17,
            Border = 18
        }

        [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 0x84) // WM_NCHITTEST
            {
                var result = (HitTest)m.Result.ToInt32();
                if (result == HitTest.Top || result == HitTest.Bottom)
                    m.Result = new IntPtr((int)HitTest.Caption);
                if (result == HitTest.TopLeft || result == HitTest.BottomLeft)
                    m.Result = new IntPtr((int)HitTest.Left);
                if (result == HitTest.TopRight || result == HitTest.BottomRight)
                    m.Result = new IntPtr((int)HitTest.Right);
            }
        }
    }
}
