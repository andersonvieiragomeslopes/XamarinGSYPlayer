using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using XamarinGSYPlayer.Controls;
using ARelativeLayout = Android.Widget.RelativeLayout;

namespace XamarinGSYPlayer.Droid.Renderers
{
    [Preserve(AllMembers = true)]

    public class GSYPlayerRenderer : Xamarin.Forms.Platform.Android.AppCompat.ViewRenderer<GSYPlayerControlView, ARelativeLayout>
    {
        Context context;
        ARelativeLayout frameLayout;
        ViewGroup mainpage;
        public GSYPlayerRenderer(Context context) : base(context)
        {
            this.context = context;

        }
        protected override void OnElementChanged(ElementChangedEventArgs<GSYPlayerControlView> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                if (Control == null)
                {
                    InitializePlayer(e.NewElement.Souce);

                    frameLayout = new ARelativeLayout(context);
                    ARelativeLayout.LayoutParams layoutParams =
                            new ARelativeLayout.LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent);
                    layoutParams.AddRule(LayoutRules.CenterInParent);


                }
            }
        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
        }
        protected override void Dispose(bool disposing)
        {
            //mPlayerView?.Dispose();
           // mPlayerView = null;
            base.Dispose(disposing);

        }
        private void InitializePlayer(string Source)
        {

        }

    }
}