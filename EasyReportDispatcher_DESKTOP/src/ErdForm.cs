using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyReportDispatcher_DESKTOP.src
{
    public class ErdForm: Form
    {

        protected override void OnShown(EventArgs e)
        {
            UI_Utils.registerExitESC(this);
            base.OnShown(e);
        }

    }
}
