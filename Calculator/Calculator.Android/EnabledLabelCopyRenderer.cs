using Android.Content;
using Calculator.Controls;
using Calculator.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(EnabledCopyLabel), typeof(EnabledLabelCopyRenderer))]
namespace Calculator.Droid
{
    public class EnabledLabelCopyRenderer : LabelRenderer
    {
        public EnabledLabelCopyRenderer(Context context) : base(context)
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            Control.SetTextIsSelectable(true);
        }
    }
}