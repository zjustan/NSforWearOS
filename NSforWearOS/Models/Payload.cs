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

namespace NSforWearOS.Models
{
    public class Root<T> where T : IPayload
    {
        public T payload { get; set; }
    }
    public interface IPayload { }

}