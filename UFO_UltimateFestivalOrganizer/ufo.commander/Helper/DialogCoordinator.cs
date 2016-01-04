using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ufo.commander.Helper
{
    public static class DialogCoordinator
    {
        public static Task<string> ShowDialog(object context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (!DialogParticipation.IsRegistered(context))
                throw new InvalidOperationException(
                    "Context is not registered. Consider using DialogParticipation.Register in XAML to bind in the DataContext.");

            var association = DialogParticipation.GetAssociation(context);
            var metroWindow = Window.GetWindow(association) as MetroWindow;

            if (metroWindow == null)
                throw new InvalidOperationException("Control is not inside a MetroWindow.");

            return metroWindow.ShowInputAsync("From a VM", "This dialog was shown from a VM, without knoweldge of Window");
        }
    }


}
