namespace UserService.Shared.Domain
{
    public abstract class Identifier<T>
    {
        public T Value { get; private init; }

        protected Identifier(T value)
        {
            Value = value;
        }

        public bool Equals(Identifier<T> id)
        {
            if (id is null)
            {
                return false;
            }

            if (GetType() != id.GetType())
            {
                return false;
            }

            return Equals(id.Value, Value);
        }

        public override bool Equals(object? obj)
        {
            if (obj is Identifier<T> otherId)
            {
                return Equals(otherId);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Value?.GetHashCode() ?? 0;
        }
    }

}