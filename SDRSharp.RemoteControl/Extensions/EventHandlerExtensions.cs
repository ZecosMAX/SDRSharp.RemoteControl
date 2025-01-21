using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDRSharp.RemoteControl.Extensions
{
    public static class EventHandlerExtensions
    {
        public static void SafeInvoke(this EventHandler? eventHandler, object sender, EventArgs args, Action<Exception>? exceptionCallback = null)
        {
            try
            {
                eventHandler?.Invoke(sender, args);
            }
            catch (Exception ex)
            {
                try
                {
                    exceptionCallback?.Invoke(ex);
                } catch { }
            }
        }
    }
}
