using swk5.ufo.dal;
using swk5.ufo.server;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace ufo.commander.Helper
{
    class NameToBrushConverter : IValueConverter
    {
        private ICommanderBL commander = BLFactory.GetCommander();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string input = value as string;
            Artist a = commander.GetArtistByName(input);
            if (a != null)
            {
                switch (a.CategoryName)
                {
                    case "Akrobatik&Tanz":
                        return Brushes.Plum;

                    case "Clownerie&Pantomime":
                        return Brushes.AntiqueWhite;

                    case "Comedy&Jonglage":
                        return Brushes.LightGreen;

                    case "Feuerperformances":
                        return Brushes.Red;

                    case "Figuren-undObjektheater":
                        return Brushes.DarkKhaki;

                    case "Kinderprogramm":
                        return Brushes.LightSkyBlue;

                    case "Local Art":
                        return Brushes.AntiqueWhite;

                    case "Luftakrobatik":
                        return Brushes.Aquamarine;

                    case "Musik,Samba,Percussion":
                        return Brushes.MediumOrchid;

                    case "Straßentheater":
                        return Brushes.LightSteelBlue;

                    case "Walkact, Stelzen, Stehstill":
                        return Brushes.LightPink;
                }
            }
            else
            {
                return Brushes.Gray;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
