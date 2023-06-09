﻿namespace AdionFA.UI.Station.Infrastructure.Contracts.Services
{
    public interface IProcessService
    {
        void StartProcessProject(int? projectId);

        void StartProcessWekaJava();

        void EndAllProcessProject(bool? includeStation);

        bool AnyProcessProject();
    }
}
