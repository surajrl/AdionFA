using Adion.FinancialAutomat.UI.Station.Modules.Infrastructure;
using Adion.FinancialAutomat.UI.Station.Modules.Infrastructure.Contracts;
using Adion.FinancialAutomat.UI.Station.Modules.Tools.Services;
using Dragablz;
using DynamicData.Binding;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive.Disposables;
using System.Text;

namespace Adion.FinancialAutomat.UI.Station.Modules.Tools.ViewModels
{
    public class ToolsViewModel : AbstractNotifyPropertyChanged, IDisposable
    {
        #region Services
        public readonly IToolsService TraderService;
        public readonly IDataReferenceService DataReferenceService;
        private readonly IDialogCoordinator DialogCoordinator;
        #endregion

        public event PropertyChangedEventHandler ThemePropertyChanged;
        public IInterTabClient InterTabClient { get; }

        #region Dispose
        private readonly IDisposable _cleanUp;
        public void Dispose()
        {
            _cleanUp.Dispose();
        }
        #endregion
    }
}
