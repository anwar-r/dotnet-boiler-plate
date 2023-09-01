namespace Data.Exeptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string entity) : base($"{entity} - Entity Not Found") { }
    }
}
