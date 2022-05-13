using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace TravelGuide.Converters
{
    public abstract class BaseEntryValidatorBehavior : BaseBehavior<Entry>
    {
        /// <summary>
        /// Дефоутен цвят на placeholder-a
        /// </summary>
        private const string grayTextColor = "#9f9f9f";

        /// <summary>
        /// Регекс за валидация
        /// </summary>
        private Regex regexPattern;

        /// <summary>
        /// Шаблон за валидация
        /// </summary>
        protected abstract string Pattern { get; }

        /// <summary>
        /// Дефоутен цвят на placeholder-a
        /// </summary>
        protected virtual Color DefaultColor
        {
            get
            {
                Application.Current.Resources.TryGetValue("GrayTextColor", out var color);

                if (color != null)
                    return (Color)color;

                return Color.FromHex(grayTextColor);
            }
        }

        /// <summary>
        /// При закачане
        /// </summary>
        /// <param name="entry"></param>
        protected override void OnAttachedTo(Entry entry)
        {
            regexPattern = new Regex(Pattern);
            entry.TextChanged += Entry_TextChanged;

            base.OnAttachedTo(entry);
        }

        /// <summary>
        /// При откачане
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.NewTextValue) && !regexPattern.IsMatch(e.NewTextValue))
            {
                AssociatedObject.PlaceholderColor = Color.Red;
                return;
            }

            AssociatedObject.PlaceholderColor = DefaultColor;
        }

        /// <summary>
        /// Обработчик на събитието
        /// </summary>
        /// <param name="entry"></param>
        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= Entry_TextChanged;
            regexPattern = null;

            base.OnDetachingFrom(entry);
        }
    }
}
