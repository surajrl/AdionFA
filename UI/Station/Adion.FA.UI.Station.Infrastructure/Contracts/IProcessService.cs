namespace Adion.FA.UI.Station.Infrastructure.Contracts.Services
{
    public interface IProcessService
    {
        void StartProcessProject(int? projectId);
        bool CanStartProcessProject(int? projectId);

        void StartProcessWekaJava();

        void EndAllProcessProject(bool? includeStation);
        bool AnyProcessProject();
        bool CanEndAllProcessProject(bool? includeStation);
    }
}
