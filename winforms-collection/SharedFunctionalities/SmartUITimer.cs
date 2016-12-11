using System;
using System.Timers;
using System.Windows.Forms;

namespace SharedFunctionalities {
    public class SmartUiTimer : SmartTimer {

        private readonly Control _ui;

        public SmartUiTimer(Control ui) {
            this._ui = ui;
        }

        public override void Start(Action<object, ElapsedEventArgs, SmartTimer> handler, Action after) {
            base.Start((object sender, ElapsedEventArgs e, SmartTimer timer) => { HandleInvoke(handler, sender, e, timer); }, () => { HandleInvoke(after); });
        }


        private void HandleInvoke(Action<object, ElapsedEventArgs, SmartTimer> act, object sender, ElapsedEventArgs e, SmartTimer timer) {
            if (IsFormAccessiable()) {
                if (_ui.InvokeRequired) {
                    if (IsFormAccessiable()) {
                        _ui.BeginInvoke((MethodInvoker)(() => { act.Invoke(sender, e, timer); }));
                    }
                } else {
                    act.Invoke(sender, e, timer);
                }
            }
        }

        private bool IsFormAccessiable() {
            if (_ui == null || _ui.Disposing || _ui.IsDisposed || (!_ui.IsHandleCreated)) {
                return false;
            } else {
                return true;
            }
        }

        private void HandleInvoke(Action act) {
            if (!IsFormAccessiable() || act == null) {
                return;
            }
            if (_ui.InvokeRequired) {
                _ui.BeginInvoke((MethodInvoker)(() => { act.Invoke(); }));
            } else {
                act.Invoke();
            }
        }
    }
}
