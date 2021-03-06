﻿using System;
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
        OrientationUtils orientationUtils;
        StandardGSYVideoPlayer player;
        ImageView imageView;
        bool fullscreen = false;
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

                    orientationUtils = new OrientationUtils(activity, player);
                    player.SetThumbPlay(true);

                    InitializePlayer(e.NewElement.Source, e.NewElement.AutoPlay, e.NewElement.Thumbnail, e.NewElement.Title);

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

                            if (!fullscreen)
                            {
                                fullscreen = true;
                                activity.Window.AddFlags(WindowManagerFlags.Fullscreen);

                                //var uiOptions = SystemUiFlags.HideNavigation | SystemUiFlags.ImmersiveSticky | SystemUiFlags.Fullscreen | SystemUiFlags.LayoutFullscreen | SystemUiFlags.LayoutHideNavigation;
                               // activity.Window.DecorView.SystemUiVisibility = (StatusBarVisibility)uiOptions;
                            }
                            else
                            {
                                fullscreen = false;
                                activity.Window.ClearFlags(WindowManagerFlags.Fullscreen);
                                //activity.Window.DecorView.SystemUiVisibility = (StatusBarVisibility)(SystemUiFlags.LayoutFullscreen | SystemUiFlags.LayoutHideNavigation);

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

            player.SetUp("", true, "");
            if(MainActivity.Instance.RequestedOrientation!= ScreenOrientation.Portrait)
            MainActivity.Instance.RequestedOrientation = ScreenOrientation.Portrait;

            base.Dispose(disposing);

        }
        private void InitializePlayer(string Source, bool AutoPlay = true, string Thumbnail = "", string Title="")
        {            
            imageView = new ImageView(context);

            imageView.SetScaleType(ImageView.ScaleType.CenterCrop);

            imageView.SetImageURI(Android.Net.Uri.Parse(Thumbnail));
            player.ThumbImageView = imageView;
            player.VerticalScrollBarEnabled = false;
            player.SetUp(Source, true, Title);
            player.Looping = AutoPlay;
            if(AutoPlay)
            player.StartPlayLogic();

        }

    }
}