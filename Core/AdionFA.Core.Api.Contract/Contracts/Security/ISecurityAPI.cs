using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.Core;

namespace AdionFA.Core.API.Contracts.Security
{
    public interface ISecurityAPI
    {
        #region User

        CoreUserDTO GetUserByUserName(string username);
        ResponseDTO CreateUser(CoreUserDTO user);

        #endregion
    }
}
