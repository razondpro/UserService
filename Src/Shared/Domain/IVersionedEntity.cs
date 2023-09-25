namespace UserService.Shared.Domain
{
    public interface IVersionedEntity
    {
        public int Version { get; set; }
    }
}