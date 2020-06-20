using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinGSYPlayer.Controls
{
    public class GSYPlayerControlView : View
    {
        public static readonly BindableProperty SourceProperty =
            BindableProperty.Create(nameof(Source), typeof(string), typeof(GSYPlayerControlView), "");

        public static readonly BindableProperty ThumbnailProperty =
   BindableProperty.Create(nameof(Thumbnail), typeof(string), typeof(GSYPlayerControlView), ""); 

        public static readonly BindableProperty TitleProperty =
BindableProperty.Create(nameof(Title), typeof(string), typeof(GSYPlayerControlView), "");

        public static readonly BindableProperty AutoPlayProperty =
   BindableProperty.Create(nameof(AutoPlay), typeof(bool), typeof(GSYPlayerControlView), true); 

        public static readonly BindableProperty LoopingProperty =
BindableProperty.Create(nameof(Looping), typeof(bool), typeof(GSYPlayerControlView), true);

        public string Source {
            set { SetValue(SourceProperty, value); }
            get { return (string)GetValue(SourceProperty); }
        }


        public string Thumbnail {
            set { SetValue(ThumbnailProperty, value); }
            get { return (string)GetValue(ThumbnailProperty); }
        }
        public string Title {
            set { SetValue(TitleProperty, value); }
            get { return (string)GetValue(TitleProperty); }
        }
        public bool AutoPlay {
            set { SetValue(AutoPlayProperty, value); }
            get { return (bool)GetValue(AutoPlayProperty); }
        }
        public bool Looping {
            set { SetValue(LoopingProperty, value); }
            get { return (bool)GetValue(LoopingProperty); }
        }
    }
}
