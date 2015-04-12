using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;

namespace GlobalCapitalTests.TestUtils.Framework
{
    public class ProgressiveTestHandler
    {
        #region Properties & Fields

        protected Thread t;
        protected ProgressForm f;

        public string Title
        {
            set
            {
                SetControlPropertyThreadSafe(f, "Text", value);
            }
        }

        public int Progress
        {
            set
            {
                SetControlPropertyThreadSafe(f.progressBar1, "Value", value);
            }
        }

        public string Message
        {
            set
            {
                SetControlPropertyThreadSafe(f.label1, "Text", value);
            }
        }

        #endregion

        #region Public Methods

        public void Open(string title = "Test Progress")
        {
            t = new Thread(() =>
            {
                Application.EnableVisualStyles();
                using (f = new ProgressForm(title)) f.ShowDialog();
            });
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }

        public void Close()
        {
            if (t != null) t.Abort();
            t = null;
        }

        #endregion

        #region Utilities

        private delegate void SetControlPropertyThreadSafeDelegate(Control control, string propertyName, object propertyValue);

        public static void SetControlPropertyThreadSafe(Control control, string propertyName, object propertyValue)
        {
            if (control != null)
            {
                if (control.InvokeRequired)
                {
                    control.Invoke(new SetControlPropertyThreadSafeDelegate(SetControlPropertyThreadSafe), new object[] { control, propertyName, propertyValue });
                }
                else
                {
                    control.GetType().InvokeMember(propertyName, BindingFlags.SetProperty, null, control, new object[] { propertyValue });
                }
            }
        }

        #endregion
    }

    public class ProgressiveTest
    {
        protected static ProgressiveTestHandler mainHandler = new ProgressiveTestHandler();

        public ProgressiveTestHandler Progress
        {
            get
            {
                return mainHandler;
            }
        }
    }
}
