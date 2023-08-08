namespace UserService.Shared.Domain
{
    public abstract class Entity : IEquatable<Entity>
    {
        public UniqueIdentity Id { get; private init; }

        protected Entity(UniqueIdentity? id)
        {
            Id = id ?? new UniqueIdentity(null);
        }

        public override bool Equals(object? obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            if (obj is not Entity entity)
            {
                return false;
            }

            return Id == entity.Id;
        }

        public bool Equals(Entity? other)
        {
            if (other is null)
            {
                return false;
            }

            if (GetType() != other.GetType())
            {
                return false;
            }

            return Id == other.Id;
        }

        public static bool operator ==(Entity? left, Entity? right)
        {
            if (left is null && right is null)
            {
                return true;
            }

            if (left is null || right is null)
            {
                return false;
            }

            return left.Equals(right);
        }

        public static bool operator !=(Entity? left, Entity? right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

    }
}