using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelGuide.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelGuide.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ThemeCustomSwitch : CustomSwitch
    {
        public ThemeCustomSwitch()
        {
            InitializeComponent();
            SwitchPanUpdate += (sender, e) =>
            {
                Color fromColorGradient1 = IsToggled ? Color.Transparent : Color.Transparent;
                Color toColorGradient1 = IsToggled ? Color.Transparent : Color.Transparent;

                Color fromColorGradient2 = IsToggled ? Color.Transparent : Color.Transparent;
                Color toColorGradient2 = IsToggled ? Color.Transparent : Color.Transparent;

                double t = e.Percentage * 0.01;

                Flex.TranslationX = -(e.TranslateX + e.XRef);
                if (IsToggled)
                {
                    if (e.Percentage >= 50)
                    {
                        MoonImg.Opacity = (e.Percentage - 50) * 2 * 0.01;
                        DarkImg.Opacity = (e.Percentage - 50) * 2 * 0.01;
                    }
                    else
                    {
                        LightImg.Opacity = (100 - (e.Percentage * 2)) * 0.01;
                        DarkImg.Opacity = 0;
                        MoonImg.Opacity = 0;
                    }
                }
                else
                {
                    if (e.Percentage <= 50)
                    {
                        MoonImg.Opacity = (100 - (e.Percentage * 2)) * 0.01;
                        DarkImg.Opacity = (100 - (e.Percentage * 2)) * 0.01;
                        LightImg.Opacity = 0;
                    }
                    else
                    {
                        LightImg.Opacity = (e.Percentage - 50) * 2 * 0.01;
                    }
                }

                Background = new LinearGradientBrush(new GradientStopCollection
                {
                    new GradientStop
                    {
                        Color =  ColorAnimationUtil.ColorAnimation(fromColorGradient1, toColorGradient1, t),
                        Offset = 0
                    },
                    new GradientStop
                    {
                        Color = ColorAnimationUtil.ColorAnimation(fromColorGradient2, toColorGradient2, t),
                        Offset = 1
                    }
                });

                CornerRadius = 20;
            };
        }
    }
}