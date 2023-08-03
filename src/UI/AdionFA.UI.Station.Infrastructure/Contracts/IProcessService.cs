namespace AdionFA.UI.Infrastructure.Contracts.Services
{
    public interface IProcessService
    {
        void StartWeka();

        void EndWeka();

        bool AnyProcessProject();
    }
}
