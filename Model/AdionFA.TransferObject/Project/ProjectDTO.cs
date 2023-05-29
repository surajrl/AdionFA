using AdionFA.Infrastructure.I18n.Enums;
using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.MetaTrader;
using System;
using System.Collections.Generic;

namespace AdionFA.TransferObject.Project
{
    public class ProjectDTO : EntityBaseDTO
    {
        public int ProjectId { get; set; }

        public string ProjectName { get; set; }


        // Navigation

        public IList<ProjectConfigurationDTO> ProjectConfigurations { get; set; }

        public ICollection<ExpertAdvisorDTO> ExpertAdvisors { get; set; }

        // Not Mapped

        public DateTime? ProcessLastDate { get; set; }

        public long ProcessId { get; set; }

        // Validators

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
    }
}
