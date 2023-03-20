/*using Adion.FA.UI.Station.Modules.GRPC;
using Grpc.Core;
using Ninject;
using Prism.Ioc;
using System.Threading.Tasks;

namespace Adion.FA.UI.Station.Modules.Infrastructure.Services
{
    public class ProjectRPCServerService : ProjectRPCServicePartial.ProjectRPCServicePartialBase
    {
        public override Task<ProjectResponse> LoadProject(ProjectRequest request, ServerCallContext context)
        {
            ContainerLocator.Current.Resolve<IKernel>().Get<IApplicationCommands>().LoadedProjectCommand.Execute(request.ProjectId);
            return Task.FromResult(new ProjectResponse
            {
                IsSuccess = request.ProjectId > 0 ? true : false
            }); ;
        }
    }

    public static class ProjectRPCClientService 
    {
        private static Channel channel;

        public static bool LoadProjectRequest(int projectId, bool isLoadProject)
        {
            var respoonse = GetInstance().LoadProject(new ProjectRequest 
            {
                ProjectId = projectId,
                IsLoadSuccess = isLoadProject 
            });
            channel.ShutdownAsync().Wait();
            return respoonse?.IsSuccess ?? false;
        }

        private static ProjectRPCServicePartial.ProjectRPCServicePartialClient GetInstance()
        {
            if ((channel?.State ?? ChannelState.Shutdown) == ChannelState.Shutdown)
            {
                channel = new Channel($"localhost:{(int)PortEnum.ServerRPCPort}", ChannelCredentials.Insecure);
            }

            return new ProjectRPCServicePartial.ProjectRPCServicePartialClient(channel);
        }
    }
}
*/