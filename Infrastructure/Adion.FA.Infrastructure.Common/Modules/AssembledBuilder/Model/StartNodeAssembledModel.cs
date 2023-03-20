namespace Adion.FA.Infrastructure.Common.Infrastructures.AssembledBuilder.Model
{
    public class StartNodeAssembledModel : NodeAssembledModel
    {
        public override string Label { get; set; }

        public override string Name => TypeNodeName;
    }
}
