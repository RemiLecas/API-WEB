namespace APIDs.Entities
{
    public class Spell
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }
        public CardColor Colors { get; set; }
    }
}
