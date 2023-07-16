using AdionFA.TransferObject.Base;
using System.Collections.Generic;

namespace AdionFA.TransferObject.Project
{
    public class ProjectDTO : EntityBaseDTO
    {
        public int ProjectId { get; set; }

        public string ProjectName { get; set; }


        // Navigation

        public IList<ProjectConfigurationDTO> ProjectConfigurations { get; set; }

        // Validators

        public static ResponseDTO SymbolAndTimeframeMustBeSameValidation(
            int configSymbolId, int configTimeframeId,
            int hdSymbolId, int hdTimeframeId)
        {
            var response = new ResponseDTO { IsSuccess = false };

            if (configSymbolId > 0 && configTimeframeId > 0 && hdSymbolId > 0 && hdTimeframeId > 0)
            {
                if (configSymbolId != hdSymbolId || configTimeframeId != hdTimeframeId)
                {
                    response.IsSuccess = false;
                }
                else
                {
                    response.IsSuccess = true;
                }
            }
            else
            {
                response.IsSuccess = false;
            }

            return response;
        }
    }
}
