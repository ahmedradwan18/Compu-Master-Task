using Android.App;
using Android.Content;
using Android.Icu.Text;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Compu_Master_Task.Helper;
using Java.Sql;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using static Android.Provider.SyncStateContract;

namespace Compu_Master_Task
{
    [Activity(Label = "AddTask",NoHistory =true)]
    public class AddTask : Activity
    {
        DbHelper dbHelper;
        public const int NOTIFY_ID = 1100;

        public const string URGENT_CHANNEL = "CompuMasterTask";

        Button addNewTask;
        EditText taskEdtTxt;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.addTask);

            addNewTask = FindViewById<Button>(Resource.Id.addTaskBtn);
            taskEdtTxt= FindViewById<EditText>(Resource.Id.addTaskEdt);
            dbHelper = new DbHelper(this);

            addNewTask.Click += addNewTaskmethod;
        }

        [Obsolete]
        private void addNewTaskmethod(object sender, EventArgs e)
        {
            // insert task into database
            String task = taskEdtTxt.Text;
            DateTime now = DateTime.Now;


            dbHelper.InsertNewTask(task,now.ToString("F"));


            // Creating a Notification Channel for Android 8.0 Oreo 
            string chanName = "Urgent";
            var importance = NotificationImportance.High;
            NotificationChannel chan = new NotificationChannel(URGENT_CHANNEL, chanName, importance);

            chan.EnableVibration(true);
            chan.LockscreenVisibility = NotificationVisibility.Public;


            // Submit the notification channel object to the notification manager
            NotificationManager notificationManager =
            (NotificationManager)GetSystemService(NotificationService);
            notificationManager.CreateNotificationChannel(chan);

            // Posting to a Notifications Channel
            Notification.Builder builder = new Notification.Builder(this)
            .SetContentTitle("Attention!")
                             .SetSmallIcon(Resource.Drawable.logoo)
            .SetContentText("A new Task has created successfully")
            .SetChannelId(URGENT_CHANNEL);

            notificationManager.Notify(NOTIFY_ID, builder.Build());

            Intent i = new Intent(this, typeof(MainActivity));
            StartActivity(i);

          
        }


    }
}