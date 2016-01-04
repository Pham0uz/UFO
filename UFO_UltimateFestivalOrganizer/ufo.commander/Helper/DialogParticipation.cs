﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ufo.commander.Helper
{
    public static class DialogParticipation
    {
        private static readonly IDictionary<object, DependencyObject> _contextRegistrationIndex = new Dictionary<object, DependencyObject>();

        public static readonly DependencyProperty RegisterProperty = DependencyProperty.RegisterAttached(
            "Register", typeof(object), typeof(DialogParticipation), new PropertyMetadata(default(object), RegisterPropertyChangedCallback));

        private static void RegisterPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            //TODO tidy up old val?

            _contextRegistrationIndex[dependencyPropertyChangedEventArgs.NewValue] = dependencyObject;
        }

        public static void SetRegister(DependencyObject element, object context)
        {
            element.SetValue(RegisterProperty, context);
        }

        public static object GetRegister(DependencyObject element)
        {
            return element.GetValue(RegisterProperty);
        }

        internal static bool IsRegistered(object context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            return _contextRegistrationIndex.ContainsKey(context);
        }

        internal static DependencyObject GetAssociation(object context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            return _contextRegistrationIndex[context];
        }
    }
}
