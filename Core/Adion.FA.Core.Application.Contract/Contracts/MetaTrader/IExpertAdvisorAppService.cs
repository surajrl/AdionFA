﻿using Adion.FA.TransferObject.Base;
using Adion.FA.TransferObject.MetaTrader;

namespace Adion.FA.Core.Application.Contract.Contracts.MetaTrader
{
    public interface IExpertAdvisorAppService : IAppContractBase
    {
        ResponseDTO CreateExpertAdvisor(ExpertAdvisorDTO advisor);
        ExpertAdvisorDTO GetExpertAdvisor(int? advisorId, int? projectId = null, bool includeGraph = false);
    }
}
