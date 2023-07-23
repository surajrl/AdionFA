namespace AdionFA.Domain.Model
{
    public sealed class Metadata
    {
        public Metadata()
        {
        }

        public Metadata(
            string code,
            string name,
            string value,
            int id = 0)
        {
            Id = id;

            Code = code;
            Name = name;
            Value = value;
        }

        public int Id { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
