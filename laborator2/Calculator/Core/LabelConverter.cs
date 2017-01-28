using System;
using System.Globalization;
using System.Text;
using static System.Double;

namespace Core
{
    public class LabelConverter
    {
        private readonly StringBuilder text = new StringBuilder("0");
        private readonly CultureInfo culture = new CultureInfo("en-US");
        private readonly string format = "#,##0.###############";
        private bool forcedNumber;

        public delegate void ChangedEventHandler(string text);

        public event ChangedEventHandler OnChange;

        public double Number
        {
            get { return Parse(text.ToString(), culture); }
        }

        public string Text
        {
            get { return Number.ToString(format, culture) + (text[text.Length - 1] == '.' ? "." : ""); }
        }

        public void AddDigit(string digit)
        {
            if (forcedNumber)
            {
                Clear();
            }

            if (CanAdd())
            {
                if (text.ToString() == "0")
                {
                    text.Clear();
                }

                text.Append(digit);
                FireChange();
            }
        }

        public void AddDot()
        {
            if (forcedNumber)
            {
                Clear();
            }

            if (CanAdd() && !text.ToString().Contains("."))
            {
                text.Append(".");
                FireChange();
            }
        }

        public void Clear()
        {
            text.Clear();
            text.Append("0");
            forcedNumber = false;
            FireChange();
        }

        public void SetNumber(double number)
        {
            text.Clear();
            string textNumer = number.ToString(culture);
            text.Append(textNumer.Substring(0, Math.Min(textNumer.Length, 15)));
            forcedNumber = true;
            FireChange();
        }

        private bool CanAdd()
        {
            return text.Length < 15;
        }

        private void FireChange()
        {
            if (OnChange != null)
            {
                OnChange.Invoke(Text);
            }
        }
    }
}
