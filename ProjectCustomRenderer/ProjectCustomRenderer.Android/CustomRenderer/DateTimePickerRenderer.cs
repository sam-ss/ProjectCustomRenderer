using Android.Widget;
using Xamarin.Forms.Platform.Android;
using Android.App;
using Xamarin.Forms;
using Android.Views;
using Android.Icu.Util;

using ProjectCustomRenderer.CustomViews;
using ProjectCustomRenderer.Droid.CustomRenderer;

[assembly: ExportRendererAttribute(typeof(DateTimePicker), typeof(DateTimePickerRenderer))]
namespace ProjectCustomRenderer.Droid.CustomRenderer
{
    public class DateTimePickerRenderer : ViewRenderer<DateTimePicker, Android.Views.View>
    {
        Android.Widget.DatePicker datePicker;
        Android.Widget.TimePicker timePicker;

        protected override void OnElementChanged(ElementChangedEventArgs<DateTimePicker> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || this.Element == null)
                return;


            MainActivity activity = Forms.Context as MainActivity;

            LayoutInflater inflater = (LayoutInflater)(activity.LayoutInflater);
            Android.Views.View dateLayout = Android.Views.View.Inflate(activity, Resource.Layout.DateTimePicker, null);

            Android.Widget.Button date_button = dateLayout.FindViewById<Android.Widget.Button>(Resource.Id.date_button);
            Android.Widget.Button time_button = dateLayout.FindViewById<Android.Widget.Button>(Resource.Id.time_button);

            datePicker = dateLayout.FindViewById<Android.Widget.DatePicker>(Resource.Id.date_picker);
            timePicker = dateLayout.FindViewById<Android.Widget.TimePicker>(Resource.Id.time_picker);

            date_button.Click += Date_button_Click;
            time_button.Click += TimePicker_Click;

            SetNativeControl(dateLayout);



            //Android.Views.View dateLayout = inflater.Inflate(Resource.Layout.DateTimePicker, null, false) as LinearLayout;

            //AlertDialog alertDialog = new AlertDialog.Builder(activity).Create();

            //Android.Widget.DatePicker datePicker = dateLayout.FindViewById<Android.Widget.DatePicker>(Resource.Id.date_picker);
            //Android.Widget.TimePicker timePicker = dateLayout.FindViewById<Android.Widget.TimePicker>(Resource.Id.time_picker);

            //Calendar calendar = new GregorianCalendar(datePicker.Year,
            //                   datePicker.Month,
            //                   datePicker.DayOfMonth,
            //                   timePicker.Hour,
            //                   timePicker.Minute);

            //alertDialog.SetView(dateLayout);

            //SetNativeControl(alertDialog.);







            //datePicker = new Android.Widget.DatePicker(this.Context);

            //this.SetNativeControl(datePicker);




            //MainActivity activity = Forms.Context as MainActivity;

            //DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            //{
            //    //_dateDisplay.Text = time.ToLongDateString();
            //});
            //frag.Show(activity.FragmentManager, DatePickerFragment.TAG);

        }

        private void TimePicker_Click(object sender, EventArgs e)
        {
            try
            {
                datePicker.Visibility = ViewStates.Gone;
                timePicker.Visibility = ViewStates.Visible;
            }
            catch (Exception ex)
            {

            }
        }

        private void Date_button_Click(object sender, EventArgs e)
        {
            try
            {
                timePicker.Visibility = ViewStates.Gone;
                datePicker.Visibility = ViewStates.Visible;
            }
            catch (Exception ex)
            {

            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
        }

        private void UpdateDateFromModel(bool animate)
        {
            var date = new DateTime(Element.DateTicks);
            if (datePicker.DateTime != date)
                //datePicker.SetDate(date.ToNSDate(), animate);
                datePicker.DateTime = date;

            //Control.Text = date.ToString(Element.Format);
        }

        private void OnStarted(object sender, EventArgs eventArgs)
        {
            ((IElementController)Element).SetValueFromRenderer(VisualElement.IsFocusedProperty, true);
        }

        private void OnEnded(object sender, EventArgs eventArgs)
        {
            ((IElementController)Element).SetValueFromRenderer(VisualElement.IsFocusedProperty, false);
        }

        private void UpdateMaximumDate()
        {
            datePicker.MaxDate = Element.MaximumDate.Millisecond;
        }

        private void UpdateMinimumDate()
        {
            datePicker.MinDate = Element.MinimumDate.Millisecond;
        }

        private void UpdateTextColor()
        {
            //Control.SetBackgroundColor((Xamarin.Forms.Color)Element.TextColor);
        }

    }
}