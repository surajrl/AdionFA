using AdionFA.Core.Application.Contract.Contracts;
using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.Core;

namespace AdionFA.Core.Application.Contracts.Security
{
    public interface ISecurityAppService : IAppContractBase
    {
        #region User
        CoreUserDTO GetUserByUserName(string username);
        ResponseDTO CreateUser(CoreUserDTO user);
        #endregion
    }
}
