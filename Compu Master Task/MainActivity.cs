using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Views;
using Android.Content;
using System;
using Compu_Master_Task.Helper;
using System.Collections.Generic;
using Android.Preferences;

namespace Compu_Master_Task
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme",NoHistory =true)]
    public class MainActivity : AppCompatActivity
    {
        ISharedPreferences prefs;
        ISharedPreferencesEditor editor;
        DbHelper dbHelper;
        CustomAdapter adapter;
        Context mContext;

        ListView listTask;
        Button gotoAddNewTaskBtn;
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {

                case Resource.Id.action_logout:
                    dbHelper.deleteAll();
                    prefs = PreferenceManager.GetDefaultSharedPreferences(mContext);
                    editor = prefs.Edit();
                    editor.PutString("State_Key", "");
                    editor.Apply();

                    Intent intent = new Intent(this, typeof(Login));
                    StartActivity(intent);

                    return true;
            
            }
            return base.OnOptionsItemSelected(item);
        }



      
        public void LoadTaskList()
        {

            List<String> taskList = dbHelper.getTaskList();
            List<String> taskTimeList = dbHelper.getTaskTimeList();
            adapter = new CustomAdapter(dbHelper,this, taskList,taskTimeList);
            listTask.Adapter = adapter;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {

            MenuInflater.Inflate(Resource.Menu.menu_item, menu);

            return base.OnCreateOptionsMenu(menu);
        }


       


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            dbHelper = new DbHelper(this);
            mContext = Android.App.Application.Context;
            listTask = FindViewById<ListView>(Resource.Id.listTask);
            gotoAddNewTaskBtn = FindViewById<Button>(Resource.Id.gotoAddTaskBtn);

            gotoAddNewTaskBtn.Click += gotoAddTask;
            // load data
            LoadTaskList();
             
        }

        private void gotoAddTask(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(AddTask));
            StartActivity(intent);

        }
    }
}