using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.Core;

namespace AdionFA.Core.API.Contracts.Security
{
    public interface ISecurityAPI
    {
        // User

        CoreUserDTO GetUserByUserName(string username);
        ResponseDTO CreateUser(CoreUserDTO user);
    }
}
