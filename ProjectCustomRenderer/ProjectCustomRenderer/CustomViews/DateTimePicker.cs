using System;
using Xamarin.Forms;

namespace ProjectCustomRenderer.CustomViews
{
    public class DateTimePicker : View
    {
        public DateTimePicker()
        {
        }

        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create(nameof(FontSize),
                                    typeof(int),
                                    typeof(DateTimePicker),
                                    12);

        public static readonly BindableProperty FormatProperty =
            BindableProperty.Create(nameof(Format),
                                    typeof(string),
                                    typeof(DatePicker),
                                    "dd/M/yyyy : hh:mm");

        public static readonly BindableProperty DateTicksProperty =
            BindableProperty.Create(nameof(DateTicks),
                                    typeof(long),
                                    typeof(DateTimePicker),
                                    DateTime.Now.Ticks,
                                    propertyChanged: OnDateTicksPropertyChanged);

        public static readonly BindableProperty MinimumDateProperty =
            BindableProperty.Create(nameof(MinimumDate),
                                    typeof(DateTime),
                                    typeof(DatePicker),
                                    new DateTime(1900, 1, 1),
                                    validateValue: ValidateMinimumDate, coerceValue: CoerceMinimumDate);

        public static readonly BindableProperty MaximumDateProperty =
            BindableProperty.Create(nameof(MaximumDate),
                                    typeof(DateTime),
                                    typeof(DatePicker),
                                    new DateTime(2100, 12, 31),
                                    validateValue: ValidateMaximumDate, coerceValue: CoerceMaximumDate);

        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create(nameof(TextColor),
                                    typeof(Color),
                                    typeof(DatePicker),
                                    Color.Black);

        #region Properties

        public int FontSize
        {
            get { return (int)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        public long DateTicks
        {
            get { return (long)GetValue(DateTicksProperty); }
            set { SetValue(DateTicksProperty, value); }
        }

        public string Format
        {
            get
            {
                return
                    (string)GetValue(FormatProperty);
            }
            set
            {
                SetValue(FormatProperty, value);
            }
        }

        public DateTime MaximumDate
        {
            get { return (DateTime)GetValue(MaximumDateProperty); }
            set { SetValue(MaximumDateProperty, value); }
        }

        public DateTime MinimumDate
        {
            get { return (DateTime)GetValue(MinimumDateProperty); }
            set { SetValue(MinimumDateProperty, value); }
        }

        public Color TextColor
        {
            get { return (Color)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }

        #endregion Properties

        public event EventHandler<DateChangedEventArgs> DateSelected;

        private static object CoerceDate(BindableObject bindable, object value)
        {
            var picker = (DateTimePicker)bindable;
            var dateValue = ((DateTime)value).Date;

            if (dateValue > picker.MaximumDate)
                dateValue = picker.MaximumDate;

            if (dateValue < picker.MinimumDate)
                dateValue = picker.MinimumDate;

            return dateValue;
        }

        private static object CoerceMaximumDate(BindableObject bindable, object value)
        {
            var dateValue = ((DateTime)value).Date;
            var picker = (DateTimePicker)bindable;
            var date = new DateTime(picker.DateTicks);
            if (date > dateValue)
                date = dateValue;

            return dateValue;
        }

        private static object CoerceMinimumDate(BindableObject bindable, object value)
        {
            var dateValue = ((DateTime)value).Date;
            var picker = (DateTimePicker)bindable;
            var date = new DateTime(picker.DateTicks);
            if (date < dateValue)
                date = dateValue;

            return dateValue;
        }

        private static void OnDateTicksPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var datePicker = (DateTimePicker)bindable;
            var newDate = new DateTime((long)newValue);
            var oldDate = new DateTime((long)oldValue);
            var selected = datePicker.DateSelected;
            if (selected != null)
                selected(datePicker, new DateChangedEventArgs(oldDate, newDate));
        }

        private static bool ValidateMaximumDate(BindableObject bindable, object value)
        {
            return (DateTime)value >= ((DateTimePicker)bindable).MinimumDate;
        }

        private static bool ValidateMinimumDate(BindableObject bindable, object value)
        {
            return (DateTime)value <= ((DateTimePicker)bindable).MaximumDate;
        }
    }
}
