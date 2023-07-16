namespace AdionFA.Domain.Entities.Base
{
    public class ReferenceDataBase : EntityBase
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
    }
}
