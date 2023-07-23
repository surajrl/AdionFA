namespace AdionFA.Domain.Entities.Base
{
    public abstract class ReferenceDataBase : EntityBase
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
