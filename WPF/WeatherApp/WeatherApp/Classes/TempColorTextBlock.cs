using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WeatherApp.Classes
{
    class TempColorTextBlock : TextBlock
    {
        public string TempProperty
        {
            get 
            { 
                return (string)GetValue(TempPropertyProperty); 
            }
            set 
            { 
                SetValue(TempPropertyProperty, value); 
            }
        }

        // Using a DependencyProperty as the backing store for TempProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TempPropertyProperty =
            DependencyProperty.Register("TempProperty", typeof(string), typeof(TempColorTextBlock), new PropertyMetadata("", ChangeForegroundColor));

        //콜백함수
        public static void ChangeForegroundColor(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            TempColorTextBlock TempColorTxtBlock = source as TempColorTextBlock;

            // Text가 바인딩 되어있으므로 맨 뒤의 " ℃" 빼고 int로 변환
            int Temp = int.Parse(TempColorTxtBlock.TempProperty.Substring(0, TempColorTxtBlock.TempProperty.Length - 2));

            if (Temp > 20)
            {
                TempColorTxtBlock.Foreground = System.Windows.Media.Brushes.Red;
            }
            else 
            {
                TempColorTxtBlock.Foreground = System.Windows.Media.Brushes.Blue;
            }
        }
    }
}
