namespace MSDevsHK.Website.Data.DocumentDB.Entities
{
    abstract class Entity
    {
        public string Id { get; set; }
        public string Type { get; set; }

        public static readonly string TypeUser = "user";
    }
}