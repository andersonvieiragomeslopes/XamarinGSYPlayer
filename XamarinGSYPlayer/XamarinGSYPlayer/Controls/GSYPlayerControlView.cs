using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinGSYPlayer.Controls
{
    public class GSYPlayerControlView: View
    {
        public static readonly BindableProperty SourceProperty =
            BindableProperty.Create(nameof(Souce), typeof(string), typeof(GSYPlayerControlView), null);

        public string Souce {
            set { SetValue(SourceProperty, value); }
            get { return (string)GetValue(SourceProperty); }
        }
    }
}
