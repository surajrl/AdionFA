﻿using AdionFA.UI.Station.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdionFA.UI.Station.Project.Model.Weka
{
    public class REPTreeOutputModelVM : ViewModelBase
    {
        private int _seed;
        public int Seed
        {
            get => _seed;
            set => SetProperty(ref _seed, value);
        }

        private string _treeOutput;
        public string TreeOutput
        {
            get => _treeOutput;
            set => SetProperty(ref _treeOutput, value);
        }

        private bool? _winningStrategy;
        public bool? WinningStrategy
        {
            get => _winningStrategy;
            set => SetProperty(ref _winningStrategy, value);
        }

        private int _totalWinningStrategy;
        public int TotalWinningStrategy
        {
            get => _totalWinningStrategy;
            set => SetProperty(ref _totalWinningStrategy, value);
        }

        private int _totalWinningStrategyUP;
        public int TotalWinningStrategyUP
        {
            get => _totalWinningStrategyUP;
            set => SetProperty(ref _totalWinningStrategyUP, value);
        }

        private int _totalWinningStrategyDOWN;
        public int TotalWinningStrategyDOWN
        {
            get => _totalWinningStrategyDOWN;
            set => SetProperty(ref _totalWinningStrategyDOWN, value);
        }

        private int _winningNodes;
        public int WinningNodes
        {
            get => _winningNodes;
            set => SetProperty(ref _winningNodes, value);
        }

        private ObservableCollection<REPTreeNodeModelVM> _nodeOutput;
        public ObservableCollection<REPTreeNodeModelVM> NodeOutput
        {
            get => _nodeOutput;
            set => SetProperty(ref _nodeOutput, value);
        }

        public int _counterProgressBar;
        public int CounterProgressBar
        {
            get => _counterProgressBar;
            set => SetProperty(ref _counterProgressBar, value);
        }

        private bool _isSuccess;
        public bool IsSuccess
        {
            get => _isSuccess;
            set => SetProperty(ref _isSuccess, value);
        }

        private string _message;
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }
    }
}
