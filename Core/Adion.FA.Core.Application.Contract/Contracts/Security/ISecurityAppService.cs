using Adion.FA.Core.Application.Contract.Contracts;
using Adion.FA.TransferObject.Base;
using Adion.FA.TransferObject.Core;

namespace Adion.FA.Core.Application.Contracts.Security
{
    public interface ISecurityAppService : IAppContractBase
    {
        #region User
        CoreUserDTO GetUserByUserName(string username);
        ResponseDTO CreateUser(CoreUserDTO user);
        #endregion
    }
}
