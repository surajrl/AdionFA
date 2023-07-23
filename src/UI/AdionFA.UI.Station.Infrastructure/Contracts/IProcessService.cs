namespace AdionFA.UI.Infrastructure.Contracts.Services
{
    public interface IProcessService
    {
        void StartProcessProject(int? projectId);

        void StartProcessWeka();

        bool AnyProcessProject();
    }
}
