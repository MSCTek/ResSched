using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Plugin.CurrentActivity;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ResSched.Controls.IconLabel), typeof(ResSched.Android.CustomRenderers.IconLabelRenderer))]

namespace ResSched.Android.CustomRenderers
{
    public class IconLabelRenderer : LabelRenderer
    {
        Context CurrentContext => CrossCurrentActivity.Current.Activity; 
        public IconLabelRenderer(Context context) : base(context)
        {
            AutoPackage = false;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || Element == null)
            {
                return;
            }
            var label = (TextView)Control;
            Typeface font = Typeface.CreateFromAsset(CurrentContext.Assets, "fa-regular-400.ttf");
            label.Typeface = font;
        }
    }
}