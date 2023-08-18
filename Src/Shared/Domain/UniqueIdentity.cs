namespace UserService.Shared.Domain
{
    public class UniqueIdentity : Identifier<Guid>
    {
        public UniqueIdentity(Guid? value) : base(value ?? Guid.NewGuid())
        {

        }
    }
}
