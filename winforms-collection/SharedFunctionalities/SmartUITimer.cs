using System;
using System.Timers;
using System.Windows.Forms;

namespace SharedFunctionalities {
    public class SmartUITimer : SmartTimer {

        private Control ui;

        public SmartUITimer(Control ui) {
            this.ui = ui;
        }

        public override void start(Action<object, ElapsedEventArgs, SmartTimer> handler, Action after) {
            base.start((object sender, ElapsedEventArgs e, SmartTimer timer) => { handleInvoke(handler, sender, e, timer); }, () => { handleInvoke(after); });
        }


        private void handleInvoke(Action<object, ElapsedEventArgs, SmartTimer> act, object sender, ElapsedEventArgs e, SmartTimer timer) {
            if (isFormAccessiable()) {
                if (ui.InvokeRequired) {
                    if (isFormAccessiable()) {
                        ui.BeginInvoke((MethodInvoker)(() => { act.Invoke(sender, e, timer); }));
                    }
                } else {
                    act.Invoke(sender, e, timer);
                }
            }
        }

        private bool isFormAccessiable() {
            if (ui == null || ui.Disposing || ui.IsDisposed || (!ui.IsHandleCreated)) {
                return false;
            } else {
                return true;
            }
        }

        private void handleInvoke(Action act) {
            if (!isFormAccessiable() || act == null) {
                return;
            }
            if (ui.InvokeRequired) {
                ui.BeginInvoke((MethodInvoker)(() => { act.Invoke(); }));
            } else {
                act.Invoke();
            }
        }
    }
}
