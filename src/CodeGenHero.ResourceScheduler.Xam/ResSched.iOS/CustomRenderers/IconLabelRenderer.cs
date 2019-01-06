using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using ResSched.Controls;
using ResSched.iOS.CustomRenderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(IconLabel), typeof(IconLabelRenderer))]

namespace ResSched.iOS.CustomRenderers
{
    public class IconLabelRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || Element == null)
            {
                return;
            }
            try
            {
                if (Control.Text.Length == 0) return;
                //4.7
                //var myFont = UIFont.FromName("fontawesome", Control.Font.PointSize);
                //5
                var myFont = UIFont.FromName("Font Awesome 5 Free Regular", Control.Font.PointSize);
                Control.Font = myFont;
                Element.FontFamily = myFont.FamilyName;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
            }
        }
    }
}