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

namespace FCMFirstTest
{
    class Mes
    {
        public string to;
        public Noti notification;
        public Mes(string to,Noti notification){
            this.to = to;
            this.notification = notification;
        }
    }
    class Noti {
        public string title;
        public string text;
        public Noti(string body,string text) {
            this.title = body;
            this.text = body;
        }
    }
}