using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compu_Master_Task
{
    [Activity(Label = "Login", Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
    public class Login : Activity
    {
        ISharedPreferences prefs;
        ISharedPreferencesEditor editor;
        EditText username;
        EditText password;
        Button loginButton;
        Context mContext;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.login);
             mContext = Android.App.Application.Context;
            username = FindViewById<EditText>(Resource.Id.usernameEdtTxt);
            password = FindViewById<EditText>(Resource.Id.passwordEdtTxt);
            loginButton = FindViewById<Button>(Resource.Id.loginBtn);



            loginButton.Click += LoginClicked;
        }



        private void LoginClicked(object sender, EventArgs e)
        {

            // static data because we have not a signup 
            if (username.Text == "Admin" && password.Text == "12345") {

                 prefs = PreferenceManager.GetDefaultSharedPreferences(mContext);
                 editor = prefs.Edit();
                 editor.PutString("State_Key", "LoggedIn");
                 editor.Apply();

                username.Text = "";
                password.Text = "";

                Intent intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
                Toast.MakeText(this, "Login Successfull", ToastLength.Short).Show();
            
            }

            else Toast.MakeText(this, "Invalid Login", ToastLength.Short).Show();

        }
    }
}