using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StripeMauiApp.Behaviors
{
    public class FastEntryBehaviour : Behavior<Entry>
    {
        public static readonly BindableProperty MaskProperty =
            BindableProperty.Create(nameof(Mask), typeof(string), typeof(FastEntryBehaviour), default(string));

        public string Mask
        {
            get => (string)GetValue(MaskProperty);
            set => SetValue(MaskProperty, value);
        }

        public static readonly BindableProperty MaxLengthProperty =
            BindableProperty.Create(nameof(MaxLength), typeof(int), typeof(FastEntryBehaviour), 0);

        public int MaxLength
        {
            get => (int)GetValue(MaxLengthProperty);
            set => SetValue(MaxLengthProperty, value);
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += OnTextChanged;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= OnTextChanged;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is not Entry entry || string.IsNullOrEmpty(e.NewTextValue)) return;

            string raw = new string(e.NewTextValue.Where(char.IsDigit).ToArray());

            if (MaxLength > 0 && raw.Length > MaxLength)
                raw = raw.Substring(0, MaxLength);

            if (!string.IsNullOrEmpty(Mask))
                entry.Text = ApplyMask(Mask, raw);
        }

        private string ApplyMask(string mask, string raw)
        {
            var result = new StringBuilder();
            int rawIndex = 0;

            foreach (char c in mask)
            {
                if (c == '#' && rawIndex < raw.Length)
                {
                    result.Append(raw[rawIndex]);
                    rawIndex++;
                }
                else if (c != '#')
                {
                    result.Append(c);
                }

                if (rawIndex >= raw.Length)
                    break;
            }

            return result.ToString();
        }
    }
}