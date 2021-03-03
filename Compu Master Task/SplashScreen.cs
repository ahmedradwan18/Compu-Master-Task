using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Gr.Net.MaroulisLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Compu_Master_Task
{
    [Activity(Label = "SplashScreen",Theme ="@style/Theme.AppCompat.Light.NoActionBar", MainLauncher = true)]
    public class SplashScreen : Activity
    {
        Context mContext;

        ISharedPreferences prefs;
        string state;
        View easySplashScreenView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            mContext = Android.App.Application.Context;

            // check if the user is loggedIn
            prefs = PreferenceManager.GetDefaultSharedPreferences(mContext);
            state = prefs.GetString("State_Key", "");

            // when user is already loggedIn he will go to home directly
            if (state == "LoggedIn")
            {

                easySplashScreenView = new EasySplashScreen(this)
                    .WithFullScreen()
                    .WithTargetActivity(Java.Lang.Class.FromType(typeof(MainActivity)))
                  .WithSplashTimeOut(2500)
                                  .WithBeforeLogoText("Compu Master Task")

                  .WithLogo(Resource.Drawable.logoo)
                  .Create();

            }

            else {
                easySplashScreenView = new EasySplashScreen(this)
                        .WithFullScreen()
                        .WithTargetActivity(Java.Lang.Class.FromType(typeof(Login)))
                      .WithSplashTimeOut(3000)
                                      .WithBeforeLogoText("Compu Master Task")

                      .WithLogo(Resource.Drawable.logoo)
                      .Create();

            }

                  SetContentView(easySplashScreenView);


    }
    }
}