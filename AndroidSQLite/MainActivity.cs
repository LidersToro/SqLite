using System;
using Android.App;
using Android.OS;
using Android.Widget;
using System.Collections.Generic;
using AndroidSQLite.Resources.Model;
using AndroidSQLite.Resources.DataHelper;
using AndroidSQLite.Resources;
using Android.Util;

namespace AndroidSQLite
{
    [Activity(Label = "AndroidSQLite", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        ListView lstData;
        List<Person> lstSource = new List<Person>();
        DataBase db;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //Create DataBase
            db = new DataBase();
            db.createDataBase();
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            Log.Info("DB_PATH", folder);

            lstData = FindViewById<ListView>(Resource.Id.listView);

            var edtFecha = FindViewById<EditText>(Resource.Id.edtFecha);
            var edtCantidad = FindViewById<EditText>(Resource.Id.edtCantidad);
            var edtId_cliente = FindViewById<EditText>(Resource.Id.edtId_cliente);
            var edtId_producto = FindViewById<EditText>(Resource.Id.edtId_producto);

            var btnAdd = FindViewById<Button>(Resource.Id.btnAdd);
            var btnEdit = FindViewById<Button>(Resource.Id.btnEdit);
            var btnDelete = FindViewById<Button>(Resource.Id.btnDelete);

            //LoadData
            LoadData();

            //Event
            btnAdd.Click += delegate
            {
                Person person = new Person() {
                    Fecha = edtFecha.Text,
                    Cantidad = int.Parse(edtCantidad.Text),
                    Id_cliente = int.Parse(edtId_cliente.Text),
                    Id_producto = int.Parse(edtId_producto.Text)
                };
                db.insertIntoTablePerson(person);
                LoadData();
            };

            btnEdit.Click += delegate {
                Person person = new Person()
                {
                    Id = int.Parse(edtFecha.Tag.ToString()),
                    Fecha = edtFecha.Text,
                    Cantidad = int.Parse(edtCantidad.Text),
                    Id_cliente = int.Parse(edtId_cliente.Text),
                    Id_producto = int.Parse(edtId_producto.Text)
                };
                db.updateTablePerson(person);
                LoadData();
            };

            btnDelete.Click += delegate {
                Person person = new Person()
                {
                    Id = int.Parse(edtFecha.Tag.ToString()),
                    Fecha = edtFecha.Text,
                    Cantidad = int.Parse(edtCantidad.Text),
                    Id_cliente = int.Parse(edtId_cliente.Text),
                    Id_producto = int.Parse(edtId_producto.Text),
                };
                db.deleteTablePerson(person);
                LoadData();
            };

            lstData.ItemClick += (s,e) =>{
                //Set background for selected item
                for(int i = 0; i < lstData.Count; i++)
                {
                    if (e.Position == i)
                        lstData.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.DarkGray);
                    else
                        lstData.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.Transparent);

                }

                //Binding Data
                var txtFecha = e.View.FindViewById<TextView>(Resource.Id.textView1);
                var txtCantidad = e.View.FindViewById<TextView>(Resource.Id.textView2);
                var txtId_cliente = e.View.FindViewById<TextView>(Resource.Id.textView3);
                var txtId_producto = e.View.FindViewById<TextView>(Resource.Id.textView4);
                edtFecha.Text = txtFecha.Text;
                edtFecha.Tag = e.Id;

                edtCantidad.Text = txtCantidad.Text;

                edtId_cliente.Text = txtId_cliente.Text;

                edtId_producto.Text = txtId_producto.Text;

            };

        }

        private void LoadData()
        {
            lstSource = db.selectTablePerson();
            var adapter = new ListViewAdapter(this, lstSource);
            lstData.Adapter = adapter;
        }
    }
}