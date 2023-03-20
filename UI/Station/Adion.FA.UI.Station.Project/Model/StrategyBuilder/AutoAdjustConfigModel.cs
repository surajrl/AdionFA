using Adion.FA.UI.Station.Infrastructure.Model.Base;

namespace Adion.FA.UI.Station.Project.Model.StrategyBuilder
{
    public class AutoAdjustConfigModel : ConfigurationBaseVM
    {
        public bool IsDefault => !AdjustNTotalTree && !AdjustMinTransactionCountIS && !AdjustMinTransactionCountOS;

        #region Weka
        private bool adjustDepthWeka;
        public bool AdjustDepthWeka
        {
            get => adjustDepthWeka;
            set => SetProperty(ref adjustDepthWeka, value);
        }

        public bool adjustMaxRatioTree;
        public bool AdjustMaxRatioTree
        {
            get => adjustMaxRatioTree;
            set => SetProperty(ref adjustMaxRatioTree, value);
        }

        public bool adjustNTotalTree;
        public bool AdjustNTotalTree
        {
            get => adjustNTotalTree;
            set => SetProperty(ref adjustNTotalTree, value);
        }
        #endregion

        #region StrategyBuilder
        private bool adjustMinTransactionCountIS;
        public bool AdjustMinTransactionCountIS
        {
            get => adjustMinTransactionCountIS;
            set => SetProperty(ref adjustMinTransactionCountIS, value);
        }

        private bool adjustMinPercentSuccessIS;
        public bool AdjustMinPercentSuccessIS
        {
            get => adjustMinPercentSuccessIS;
            set => SetProperty(ref adjustMinPercentSuccessIS, value);
        }


        private bool adjustMinTransactionCountOS;
        public bool AdjustMinTransactionCountOS
        {
            get => adjustMinTransactionCountOS;
            set => SetProperty(ref adjustMinTransactionCountOS, value);
        }

        private bool adjustMinPercentSuccessOS;
        public bool AdjustMinPercentSuccessOS
        {
            get => adjustMinPercentSuccessOS;
            set => SetProperty(ref adjustMinPercentSuccessOS, value);
        }


        private bool adjustVariationTransaction;
        public bool AdjustVariationTransaction
        {
            get => adjustVariationTransaction;
            set => SetProperty(ref adjustVariationTransaction, value);
        }


        private bool adjustProgressiveness;
        public bool AdjustProgressiveness
        {
            get => adjustProgressiveness;
            set => SetProperty(ref adjustProgressiveness, value);
        }
        #endregion
    }
}
