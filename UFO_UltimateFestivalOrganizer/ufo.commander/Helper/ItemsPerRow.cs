using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ufo.commander.Helper
{
    public static class ItemsPerRow
    {
        /// <summary>
        /// Handles property changed event for the ItemsPerRow property, constructing
        /// the required ItemsPerRow elements on the grid which this property is attached to.
        /// </summary>
        private static void OnItemsPerRowPropertyChanged(DependencyObject d,
                            DependencyPropertyChangedEventArgs e)
        {
            Grid grid = d as Grid;
            int itemsPerRow = (int)e.NewValue;

            // construct the required row definitions
            grid.LayoutUpdated += (s, e2) =>
            {
                var childCount = grid.Children.Count;

                // add the required number of row definitions
                int rowsToAdd = (childCount - grid.RowDefinitions.Count) / itemsPerRow;
                for (int row = 0; row < rowsToAdd; row++)
                {
                    grid.RowDefinitions.Add(new RowDefinition());
                }

                // set the row property for each chid
                for (int i = 0; i < childCount; i++)
                {
                    var child = grid.Children[i] as FrameworkElement;
                    Grid.SetRow(child, i / itemsPerRow);
                }
            };
        }
    }
}
