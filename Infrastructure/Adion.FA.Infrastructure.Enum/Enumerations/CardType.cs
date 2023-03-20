namespace Adion.FA.Infrastructure.Enums.Enumerations
{
    public class CardType : EnumerationBase
    {
        public static CardType Amex = new(1, nameof(Amex));
        public static CardType Visa = new(2, nameof(Visa));
        public static CardType MasterCard = new(3, nameof(MasterCard));

        public CardType(int id, string name)
            : base(id, name)
        {
        }
    }
}
