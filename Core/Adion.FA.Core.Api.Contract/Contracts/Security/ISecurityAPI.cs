using Adion.FA.TransferObject.Base;
using Adion.FA.TransferObject.Core;

namespace Adion.FA.Core.API.Contracts.Security
{
    public interface ISecurityAPI
    {
        #region User

        CoreUserDTO GetUserByUserName(string username);
        ResponseDTO CreateUser(CoreUserDTO user);

        #endregion
    }
}
