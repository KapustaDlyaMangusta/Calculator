using System;
using System.ComponentModel;
using Calculator;
using Calculator.iOS;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(EnabledLabelCopy), typeof(CustomLabelRenderer))]
namespace Calculator.iOS
{
    public class CustomLabelRenderer : ViewRenderer<Label, UITextView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    SetNativeControl(new UITextView());
                }

                UpdateText();
            }

            base.OnElementChanged(e);
        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == nameof(Label.Text))
            {
                UpdateText();
            }
        }
        void UpdateText()
        {
            if (string.IsNullOrWhiteSpace(Element?.Text) && Element?.FormattedText?.Spans.Count == 0)
            {
                Control.Text = string.Empty;
                return;
            }

            NSError error = null;
            if (!string.IsNullOrWhiteSpace(Element?.Text))
            {
                Control.AttributedText = new NSAttributedString(NSData.FromString(Element.Text),
                                                           new NSAttributedStringDocumentAttributes { DocumentType = NSDocumentType.PlainText },
                                                           ref error);
            }

            if (Element?.FormattedText?.Spans.Count > 0)
            {
                foreach (var item in Element?.FormattedText.Spans)
                {
                    Control.AttributedText = new NSAttributedString(NSData.FromString(item.Text),
                                                           new NSAttributedStringDocumentAttributes { DocumentType = NSDocumentType.PlainText },
                                                           ref error);
                }
            }

            switch (Element.FontAttributes)
            {
                case FontAttributes.None:
                    Control.Font = UIFont.SystemFontOfSize(new nfloat(Element.FontSize));
                    break;
                case FontAttributes.Bold:
                    Control.Font = UIFont.BoldSystemFontOfSize(new nfloat(Element.FontSize));
                    break;
                case FontAttributes.Italic:
                    Control.Font = UIFont.ItalicSystemFontOfSize(new nfloat(Element.FontSize));
                    break;
                default:
                    Control.Font = UIFont.BoldSystemFontOfSize(new nfloat(Element.FontSize));
                    break;
            }

            Control.BackgroundColor = Element.BackgroundColor.ToUIColor();
            Control.TextColor = Element.TextColor.ToUIColor();
            Control.TextAlignment = Element.HorizontalTextAlignment switch
            {
                TextAlignment.Center => UITextAlignment.Center,
                TextAlignment.End => UITextAlignment.Right,
                _ => UITextAlignment.Left
            };

            Control.Selectable = true;
            Control.InputView = new UIView();
            Control.Editable = true;
            Control.ScrollEnabled = false;
            Control.ShouldInteractWithUrl += delegate { return true; };
        }
    }
}
