namespace AdionFA.Infrastructure.Common.AssembledBuilder.Model
{
    public class AssembledBuilderModel
    {
        public NodeAssembledModel UPNode { get; set; }
        public NodeAssembledModel DOWNNode { get; set; }

        #region Start & End

        public static StartNodeAssembledModel Start(string label = null)
        {
            return new StartNodeAssembledModel
            {
                Label = label,
                Type = Enums.NodeAssembledTypeEnum.Start
            };
        }
        public static EndNodeAssembledModel End(string label = null)
        {
            return new EndNodeAssembledModel
            {
                Label = label,
                Type = Enums.NodeAssembledTypeEnum.End
            };
        }

        #endregion Start & End
    }
}
