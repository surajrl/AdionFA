using Adion.FA.UI.Station.Infrastructure.Model.Core;
using System.Threading.Tasks;

namespace Adion.FA.UI.Station.Infrastructure.Contracts.AppServices
{
    public interface ISecurityServiceAgent
    {
        Task<CoreUserVM> GetUserByUserName(string username);
    }
}
