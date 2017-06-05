using System;
using System.ComponentModel;

#if __UNIFIED__

using UIKit;
using Foundation;

#else
using MonoTouch.UIKit;
using MonoTouch.Foundation;
#endif
#if __UNIFIED__

using RectangleF = CoreGraphics.CGRect;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using ProjectCustomRenderer.CustomViews;
using ProjectCustomRenderer.iOS.CustomRenderer;



#else
using nfloat = System.Single;
using nint = System.Int32;
using nuint = System.UInt32;
#endif

[assembly: ExportRendererAttribute(typeof(DateTimePicker), typeof(DateTimePickerRenderer))]
namespace ProjectCustomRenderer.iOS.CustomRenderer
{
    internal class NoCaretField : UITextField
    {
        public NoCaretField() : base(new RectangleF())
        {
        }

        public override RectangleF GetCaretRectForPosition(UITextPosition position)
        {
            return new RectangleF();
        }
    }

    public class DateTimePickerRenderer : ViewRenderer<DateTimePicker, UITextField>
    {
        private UIDatePicker _picker;

        protected override void OnElementChanged(ElementChangedEventArgs<DateTimePicker> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                var entry = new NoCaretField { BorderStyle = UITextBorderStyle.None };
                entry.Started += OnStarted;
                entry.Ended += OnEnded;

                _picker = new UIDatePicker { Mode = UIDatePickerMode.DateAndTime, TimeZone = new NSTimeZone("UTC") };

                _picker.ValueChanged += HandleValueChanged;

                var width = UIScreen.MainScreen.Bounds.Width;
                var toolbar = new UIToolbar(new RectangleF(0, 0, width, 44)) { BarStyle = UIBarStyle.Default, Translucent = true };
                var spacer = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace);
                var doneButton = new UIBarButtonItem(UIBarButtonSystemItem.Done, (o, a) => entry.ResignFirstResponder());

                toolbar.SetItems(new[] { spacer, doneButton }, false);

                entry.InputView = _picker;
                entry.InputAccessoryView = toolbar;
                entry.Font = UIFont.SystemFontOfSize(Element.FontSize);

                SetNativeControl(entry);
                //   remove shortcurbar(udo / redo / copy / paste)
                Control.InputAssistantItem.LeadingBarButtonGroups = null;
                Control.InputAssistantItem.TrailingBarButtonGroups = null;
            }

            if (e.NewElement != null)
            {
                UpdateDateFromModel(false);
                UpdateMaximumDate();
                UpdateMinimumDate();
                UpdateTextColor();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == DateTimePicker.DateTicksProperty.PropertyName ||
                e.PropertyName == DateTimePicker.FormatProperty.PropertyName)
                UpdateDateFromModel(true);
            else if (e.PropertyName == DateTimePicker.MinimumDateProperty.PropertyName)
                UpdateMinimumDate();
            else if (e.PropertyName == DateTimePicker.MaximumDateProperty.PropertyName)
                UpdateMaximumDate();
            else if (e.PropertyName == DateTimePicker.TextColorProperty.PropertyName ||
                e.PropertyName == VisualElement.IsEnabledProperty.PropertyName)
                UpdateTextColor();
        }

        private void HandleValueChanged(object sender, EventArgs e)
        {
            if (Element != null)
            {
                var date = _picker.Date.ToDateTime();
                ((IElementController)Element).SetValueFromRenderer(DateTimePicker.DateTicksProperty, date.Ticks);
            }
        }

        private void OnEnded(object sender, EventArgs eventArgs)
        {
            ((IElementController)Element).SetValueFromRenderer(VisualElement.IsFocusedProperty, false);
        }

        private void OnStarted(object sender, EventArgs eventArgs)
        {
            ((IElementController)Element).SetValueFromRenderer(VisualElement.IsFocusedProperty, true);
        }

        private void UpdateDateFromModel(bool animate)
        {
            var date = new DateTime(Element.DateTicks);
            if (_picker.Date.ToDateTime() != date)
                _picker.SetDate(date.ToNSDate(), animate);

            Control.Text = date.ToString(Element.Format);
        }

        private void UpdateMaximumDate()
        {
            _picker.MaximumDate = Element.MaximumDate.ToNSDate();
        }

        private void UpdateMinimumDate()
        {
            _picker.MinimumDate = Element.MinimumDate.ToNSDate();
        }

        private void UpdateTextColor()
        {
            Control.TextColor = Element.TextColor.ToUIColor();
        }
    }
}