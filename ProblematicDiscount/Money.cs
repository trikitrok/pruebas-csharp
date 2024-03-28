namespace ProblematicDiscount
{
    public record Money
    {
        public static readonly Money OneThousand = new Money(1000);

        public static readonly Money OneHundred = new Money(100);

        private readonly decimal _value;

        public Money(int value) => this._value = value;

        public Money(decimal value) => this._value = value;

        public virtual Money ReduceBy(int p)
        {
            return new Money(_value * (100m - p) / 100m);
        }

        public virtual bool MoreThan(Money other)
        {
            return this._value.CompareTo(other._value) > 0;
        }
    }

}
