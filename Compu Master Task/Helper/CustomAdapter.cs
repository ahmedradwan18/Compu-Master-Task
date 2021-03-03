using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compu_Master_Task.Helper
{
    public class CustomAdapter : BaseAdapter
    {

        private DbHelper dbHelper;
        private MainActivity mainActivity;
        private List<String> taskList;
        private List<String> taskTimeList;

        public CustomAdapter(DbHelper dbHelper, MainActivity mainActivity, List<string> taskList,List<string> taskTimeList)
        {
            this.dbHelper = dbHelper;
            this.mainActivity = mainActivity;
            this.taskList = taskList;
            this.taskTimeList = taskTimeList;
          }

        public override int Count {


            get {
                return taskList.Count;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return position; 
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LayoutInflater layoutInflater = (LayoutInflater)mainActivity.GetSystemService(Context.LayoutInflaterService);
            View view = layoutInflater.Inflate(Resource.Layout.row, null);
            TextView txtTask = view.FindViewById<TextView>(Resource.Id.task_title);
            TextView txtTaskDate = view.FindViewById<TextView>(Resource.Id.task_date);
            Button btnDelete = view.FindViewById<Button>(Resource.Id.btnDelete);

            DateTime now = DateTime.Now;

            txtTask.Text = taskList[position];
            txtTaskDate.Text = taskTimeList[position];



            btnDelete.Click += delegate
            {

                // reload data after deleting
                String task = taskList[position];
                dbHelper.DeleteTask(task);
                mainActivity.LoadTaskList();
            };
            return view;


        }
    }
}