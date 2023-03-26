using AdionFA.UI.Station.Infrastructure.Model.Core;
using System.Threading.Tasks;

namespace AdionFA.UI.Station.Infrastructure.Contracts.AppServices
{
    public interface ISecurityServiceAgent
    {
        Task<CoreUserVM> GetUserByUserName(string username);
    }
}
