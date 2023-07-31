using ControlzEx.Theming;
using MaterialDesignThemes.Wpf;
using System;
using Theme = MaterialDesignThemes.Wpf.Theme;

namespace AdionFA.UI.Infrastructure.Extensions
{
    internal static class BaseThemeExtension
    {
        public static string GetMahAppsBaseColorScheme(this BaseTheme baseTheme)
        {
            return baseTheme switch
            {
                BaseTheme.Light => ThemeManager.BaseColorLightConst,
                BaseTheme.Dark => ThemeManager.BaseColorDarkConst,
                BaseTheme.Inherit => Theme.GetSystemTheme() switch
                {
                    BaseTheme.Dark => ThemeManager.BaseColorDarkConst,
                    _ => ThemeManager.BaseColorLightConst
                },
                _ => throw new InvalidOperationException()
            };
        }
    }
}
