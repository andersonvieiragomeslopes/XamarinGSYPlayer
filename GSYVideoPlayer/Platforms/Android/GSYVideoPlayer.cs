using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Views;
using Android.Widget;
using Com.Shuyu.Gsyvideoplayer.Utils;
using Com.Shuyu.Gsyvideoplayer.Video;
using Plugin.GSYVideoPlayer.Platforms.Android;
using Plugin.GSYVideoPlayer.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SharedGSYVideoPlayer), typeof(GSYVideoPlayer))]

namespace Plugin.GSYVideoPlayer.Platforms.Android
{
    public class GSYVideoPlayer : Xamarin.Forms.Platform.Android.AppCompat.ViewRenderer<SharedGSYVideoPlayer, StandardGSYVideoPlayer>
    {
        Context context;
        ViewGroup mainpage;
        OrientationUtils orientationUtils;
        StandardGSYVideoPlayer player;
        ImageView imageView;
        bool fullscreen = false;
        public GSYVideoPlayer(Context context) : base(context)
        {
            this.context = context;

        }
        protected override void OnElementChanged(ElementChangedEventArgs<SharedGSYVideoPlayer> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                if (Control == null)
                {
                    player = new StandardGSYVideoPlayer(context);
                    var activity = (Activity)base.Context;

                    orientationUtils = new OrientationUtils(activity, player);
                    player.SetThumbPlay(true);

                    InitializePlayer(e.NewElement.Source, e.NewElement.AutoPlay, e.NewElement.Thumbnail, e.NewElement.Title);

                    SetNativeControl(player);

                    player.BackButton.Click += (sender, e) =>
                    {

                        try
                        {
                            player.SetUp("", true, "");
                            if (activity.RequestedOrientation != ScreenOrientation.Portrait)
                                activity.RequestedOrientation = ScreenOrientation.Portrait;
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

                            if (!fullscreen)
                            {
                                fullscreen = true;
                                activity.Window.AddFlags(WindowManagerFlags.Fullscreen);
                            }
                            else
                            {
                                fullscreen = false;
                                activity.Window.ClearFlags(WindowManagerFlags.Fullscreen);

                            }
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
            //OnSizeAllocated you can implent this after
            var activity = (Activity)base.Context;

            player.SetUp("", true, "");            
            if (activity.RequestedOrientation != ScreenOrientation.Portrait)
                activity.RequestedOrientation = ScreenOrientation.Portrait;
            //activity.OnBackPressed();
            player.OnVideoPause();
            player.Release();
            player.Dispose();
            //base.Dispose(disposing);

        }
        private void InitializePlayer(string Source, bool AutoPlay = true, string Thumbnail = "", string Title = "")
        {
            imageView = new ImageView(context);            
            imageView.SetScaleType(ImageView.ScaleType.CenterCrop);           
            //imageView.SetImageURI(Android.Net.Uri.Parse(Thumbnail));
            //player.ThumbImageView = imageView;
            player.VerticalScrollBarEnabled = false;
            player.SetUp(Source, true, Title);
            player.Looping = AutoPlay;
            if (AutoPlay)
                player.StartPlayLogic();

        }        
    }
}