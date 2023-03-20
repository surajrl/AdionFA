using Adion.FA.Infrastructure.I18n.Enums;
using Adion.FA.TransferObject.Base;
using Adion.FA.TransferObject.MetaTrader;
using System;
using System.Collections.Generic;

namespace Adion.FA.TransferObject.Project
{
    public class ProjectDTO : EntityBaseDTO
    {
        #region Properties

        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        public int ProjectStepId { get; set; }
        public ProjectStepDTO ProjectStep { get; set; }

        #endregion

        #region Navegation Properties

        public IList<ProjectConfigurationDTO> ProjectConfigurations { get; set; }

        public ICollection<ExpertAdvisorDTO> ExpertAdvisors { get; set; }

        #endregion

        #region Not Mapped

        public DateTime? ProcessLastDate { get; set; }

        public long ProcessId { get; set; }

        #endregion

        #region Validators

        #region CurrencyPairAndCurrencyPeriodMustBeSameValidation

        public static ResponseDTO CurrencyPairAndCurrencyPeriodMustBeSameValidation(
            int cCurrencyPairId, int cCurrencyPeriodId,
            int mdCurrencyPairId, int mdCurrencyPeriodId)
        {
            var vr = new ResponseDTO { IsSuccess = false };

            if (cCurrencyPairId > 0 && cCurrencyPeriodId > 0 && mdCurrencyPairId > 0 && mdCurrencyPeriodId > 0)
            {
                if (cCurrencyPairId != mdCurrencyPairId || cCurrencyPeriodId != mdCurrencyPeriodId)
                {
                    vr.MessageResource = (int)MessageResourceEnum.CurrencyPairAndCurrencyPeriodMustBeSame;
                }
                else
                {
                    vr.IsSuccess = true;
                }
            }
            else
            {
                vr.MessageResource = (int)MessageResourceEnum.CurrencyPairAndCurrencyPeriodMustBeSame;
            }

            return vr;
        }

        #endregion

        #endregion
    }
}
