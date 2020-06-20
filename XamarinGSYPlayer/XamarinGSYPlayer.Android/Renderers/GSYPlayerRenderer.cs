using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Shuyu.Gsyvideoplayer.Utils;
using Com.Shuyu.Gsyvideoplayer.Video;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinGSYPlayer.Controls;
using XamarinGSYPlayer.Droid.Renderers;
[assembly: ExportRenderer(typeof(GSYPlayerControlView), typeof(GSYPlayerRenderer))]

namespace XamarinGSYPlayer.Droid.Renderers
{
    [Preserve(AllMembers = true)]
    public class GSYPlayerRenderer : Xamarin.Forms.Platform.Android.AppCompat.ViewRenderer<GSYPlayerControlView, StandardGSYVideoPlayer>
    {
        Context context;
        ViewGroup mainpage;
        Com.Shuyu.Gsyvideoplayer.Utils.OrientationUtils orientationUtils;
        StandardGSYVideoPlayer player;
        ImageView imageView;
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
                    player = new StandardGSYVideoPlayer(context);
                     var activity = (Activity)base.Context;

                    orientationUtils = new Com.Shuyu.Gsyvideoplayer.Utils.OrientationUtils(activity, player);

                    InitializePlayer(e.NewElement.Souce);

                    SetNativeControl(player);
                    
                    player.BackButton.Click += (sender, e) =>
                    {

                        try
                        {
                            player.SetUp("", true, "");
                            player.OnBackFullscreen();
                            activity.OnBackPressed();


                        }
                        catch (Exception ex)
                        {

                        }
                    };
                    player.FullscreenButton.Click += (sender, e) =>
                    {

                        try
                        {
                            orientationUtils.ResolveByClick();

                            player.StartWindowFullscreen(context, true, true);

                        }
                        catch (Exception ex)
                        {

                        }
                    };
                    mainpage = ((ViewGroup)player.Parent);

                }

            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
        }
        protected override void Dispose(bool disposing)
        {


            player.SetUp("", true, "");
            if(MainActivity.Instance.RequestedOrientation!= ScreenOrientation.Portrait)
            MainActivity.Instance.RequestedOrientation = ScreenOrientation.Portrait;

            base.Dispose(disposing);

        }
        private void InitializePlayer(string Source)
        {
            imageView = new ImageView(context);

            imageView.SetScaleType(ImageView.ScaleType.CenterCrop);
            imageView.SetImageURI(Android.Net.Uri.Parse("https://hexoblogstorage.blob.core.windows.net/blogres/images/xamarin-custom-navbar-icon-text/navbardiff.png"));
            player.ThumbImageView = imageView;
            player.SetThumbPlay(true);
            player.VerticalScrollBarEnabled = false;
            player.SetUp(Source, true, "Video");
            //player.StartPlayLogic();

        }

    }
}