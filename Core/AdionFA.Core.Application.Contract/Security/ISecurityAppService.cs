using AdionFA.TransferObject.Base;
using AdionFA.TransferObject.Core;

namespace AdionFA.Core.Application.Contracts.Security
{
    public interface ISecurityAppService : IAppContractBase
    {
        CoreUserDTO GetUserByUserName(string username);
        ResponseDTO CreateUser(CoreUserDTO user);
    }
}
