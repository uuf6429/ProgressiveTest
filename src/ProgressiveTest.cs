using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;

namespace CS.TestUtils
{
    public class ProgressiveTestHandler : IDisposable
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
            f = new ProgressForm(title);
            t = new Thread(() =>
            {
                f.ShowDialog();
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

        #region Implement IDisposable

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(Boolean disposing)
        {
            if (disposing)
            {
                this.Close();
                if (f != null)
                {
                    f.Dispose();
                    f = null;
                }
            }
        }

        #endregion
    }

    public class ProgressiveTest
    {
        #region Singleton Pattern

        protected static ProgressiveTestHandler mainHandler;

        public ProgressiveTestHandler Progress
        {
            get
            {
                if (mainHandler == null)
                {
                    Application.EnableVisualStyles();
                    mainHandler = new ProgressiveTestHandler();
                }
                return mainHandler;
            }
        }

        #endregion
    }
}
