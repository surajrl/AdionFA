namespace AdionFA.Domain.Entities.Base
{
    public class WekaBuilderConfigurationBase : EntityBase
    {
        public int WekaNTotal { get; set; }
        public int WekaStartDepth { get; set; }
        public int WekaEndDepth { get; set; }
        public decimal WekaMaxRatio { get; set; }
    }
}
