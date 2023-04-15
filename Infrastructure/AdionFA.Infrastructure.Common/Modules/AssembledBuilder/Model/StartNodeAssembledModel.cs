namespace AdionFA.Infrastructure.Common.AssembledBuilder.Model
{
    public class StartNodeAssembledModel : NodeAssembledModel
    {
        public override string Label { get; set; }

        public override string Name => TypeNodeName;
    }
}
