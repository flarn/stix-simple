namespace Stix.Core
{
    public class EntityNotFoundException : ApplicationException
    {
        public EntityNotFoundException(string entityId)
        {
            EntityId = entityId;
        }

        public string EntityId { get; }
    }
}
