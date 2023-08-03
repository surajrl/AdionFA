using MahApps.Metro.Controls.Dialogs;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AdionFA.UI.Infrastructure.Helpers
{
    public static class MessageHelper
    {
        private static readonly IDialogCoordinator Dialog = new DialogCoordinator();
        private static readonly MetroDialogSettings _setting;

        static MessageHelper()
        {
            _setting = new MetroDialogSettings
            {
                DialogTitleFontSize = 16,
                DialogMessageFontSize = 12,
                ColorScheme = MetroDialogColorScheme.Theme,
                AffirmativeButtonText = "Yes",
                NegativeButtonText = "No",
            };
        }

        public static async void ShowMessage(object context, string title, string message)
        {
            await Dialog.ShowMessageAsync(context,
                title, $"{message ?? string.Empty}",
                settings: _setting);
        }

        public static async Task<MessageDialogResult> ShowMessageInputAsync(object context, string title, string message)
        {
            return await Dialog.ShowMessageAsync(context,
                title, $"{message ?? string.Empty}",
                MessageDialogStyle.AffirmativeAndNegative,
                settings: _setting).ConfigureAwait(false);
        }

        public static async void ShowMessages(object context, string title, string[] messages)
        {
            var megs = messages?.ToArray() ?? Array.Empty<string>();
            await Dialog.ShowMessageAsync(context,
                title, string.Join(Environment.NewLine, messages.Select(m => $"{m}")),
                settings: _setting);
        }
    }
}
