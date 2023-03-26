namespace AdionFA.Infrastructure.Enums.Model
{
    public sealed class Metadata
    {
        public Metadata()
        {

        }

        public Metadata(string code, string name, string description, string symbol, string value, int id = 0)
        {
            Id = id;

            Code = code;
            Name = name;
            Description = description;
            
            Symbol = symbol;
            Value = value;
        }

        public int Id { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string Symbol { get; set; }
        public string Value { get; set; }
    }
}
