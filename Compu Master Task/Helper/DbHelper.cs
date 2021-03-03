using Android.App;
using Android.Content;
using Android.Database;
using Android.Database.Sqlite;
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
    public class DbHelper : SQLiteOpenHelper
    {

        private static String DB_NAME = "TodoTasks";
        public static String DB_TABLE = "Task";
        public static String DB_COLUMN = "TaskName";
        public static String DB_DATE_COLUMN = "TaskDate";
        private static int DB_VER = 1;


        public DbHelper(Context context) : base(context, DB_NAME, null, DB_VER)
        {

        }
        public override void OnCreate(SQLiteDatabase db)
        {

            String query = $"CREATE TABLE {DbHelper.DB_TABLE} (ID INTEGER PRIMARY KEY AUTOINCREMENT , {DbHelper.DB_COLUMN} TEXT NOT NULL , {DbHelper.DB_DATE_COLUMN} DATE NOT NULL );)";
            db.ExecSQL(query);
        }

        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {

            String query = $"DELETE TABLE IF EXISTS {DbHelper.DB_TABLE}";
            db.ExecSQL(query);
            OnCreate(db);
        }

        public void InsertNewTask(String task,String date) {
            SQLiteDatabase db = this.WritableDatabase;
            ContentValues values = new ContentValues();
            values.Put("TaskName", task);
            values.Put("TaskDate", date);
            db.InsertWithOnConflict(DB_TABLE, null, values, Android.Database.Sqlite.Conflict.Replace);
            db.Close();
        
        }

        public void DeleteTask(String task) {
            SQLiteDatabase db = this.WritableDatabase;
            db.Delete(DB_TABLE, DB_COLUMN + " = ? ", new string[] { task });
            db.Close();


        }


        public void deleteAll()
        {
            SQLiteDatabase db = this.WritableDatabase;
            db.Delete(DB_TABLE, null, null);
            db.Close();
        }


        public List<String> getTaskTimeList() {
            List<String> taskTimeList = new List<string>();

            SQLiteDatabase db = this.WritableDatabase;
            String count = "SELECT count(*) FROM Task";
            ICursor mcursor = db.RawQuery(count, null);
            mcursor.MoveToFirst();
            int icount = mcursor.GetInt(0);
            if (icount > 0)
            {
                SQLiteDatabase dbb = this.ReadableDatabase;
                ICursor cursor = dbb.Query(DB_TABLE, new string[] { DB_DATE_COLUMN }, null, null, null, null, null);
                while (cursor.MoveToNext())
                {
                    int index = cursor.GetColumnIndex(DB_DATE_COLUMN);
                    taskTimeList.Add(cursor.GetString(index));

                }
            }
            return taskTimeList;

        }public List<String> getTaskList() {
            List<String> taskList = new List<string>();

            SQLiteDatabase db = this.WritableDatabase;
            String count = "SELECT count(*) FROM Task";
            ICursor mcursor = db.RawQuery(count, null);
            mcursor.MoveToFirst();
            int icount = mcursor.GetInt(0);
            if (icount > 0)
            {
                SQLiteDatabase dbb = this.ReadableDatabase;
                ICursor cursor = dbb.Query(DB_TABLE, new string[] { DB_COLUMN }, null, null, null, null, null);
                while (cursor.MoveToNext())
                {
                    int index = cursor.GetColumnIndex(DB_COLUMN);
                    taskList.Add(cursor.GetString(index));

                }
            }
            return taskList;

        }
    }
}