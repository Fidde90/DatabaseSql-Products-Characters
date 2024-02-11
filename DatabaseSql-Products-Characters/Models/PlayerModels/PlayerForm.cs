namespace DatabaseSql_Products_Characters.Models
{
    public class PlayerForm
    {
        public string CharacterName { get; set; } = null!;

        public int Age { get; set; }

        public string Gender { get; set; } = null!;

        public string Kind { get; set; } = null!;

        public string Race { get; set; } = null!;
    }
}
