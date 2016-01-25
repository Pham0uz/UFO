using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ufo.commander.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notifies the property changed.
        /// </summary>
        /// <param name="propName">The name of the property.</param>
        protected void RaisePropertyChangedEvent(string propName)
        {
            ValidatePropertyName(propName);
            //if (PropertyChanged != null)
            //    PropertyChanged(this, new PropertyChangedEventArgs(propName));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        protected void ValidatePropertyName(string propName)
        {
            if (TypeDescriptor.GetProperties(this)[propName] == null)
            {
                throw new ArgumentException("Invalid property name: " + propName);
            }
        }
    }
}
