namespace UserService.Shared.Domain;
public abstract class Identifier<T>
{
    private readonly T value;

    protected Identifier(T value)
    {
        this.value = value;
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

        return Equals(id.value, value);
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
        return value?.GetHashCode() ?? 0;
    }
}
