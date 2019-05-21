using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidSQLite.Resources.Model;
using Java.Lang;

namespace AndroidSQLite.Resources
{
    public class ViewHolder : Java.Lang.Object
    {
        public TextView txtFecha { get; set; }
        public TextView txtCantidad { get; set; }
        public TextView txtId_cliente { get; set; }
        public TextView txtId_producto { get; set; }
    }
    public class ListViewAdapter:BaseAdapter
    {
        private Activity activity;
        private List<Person> lstPerson;
        public ListViewAdapter(Activity activity, List<Person> lstPerson)
        {
            this.activity = activity;
            this.lstPerson = lstPerson;
        }

        public override int Count
        {
            get
            {
                return lstPerson.Count;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return lstPerson[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? activity.LayoutInflater.Inflate(Resource.Layout.list_view_dataTemplate, parent, false);

            var txtFecha = view.FindViewById<TextView>(Resource.Id.textView1);
            var txtCantidad = view.FindViewById<TextView>(Resource.Id.textView2);
            var txtId_cliente = view.FindViewById<TextView>(Resource.Id.textView3);
            var txtId_producto = view.FindViewById<TextView>(Resource.Id.textView4);

            txtFecha.Text = lstPerson[position].Fecha;
            txtCantidad.Text = ""+lstPerson[position].Cantidad;
            txtId_cliente.Text = ""+lstPerson[position].Id_cliente;
            txtId_producto.Text = ""+lstPerson[position].Id_producto;

            return view;
        }
    }
}