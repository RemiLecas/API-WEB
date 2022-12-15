using System.ComponentModel.DataAnnotations.Schema;

namespace APIDs.Entities
{
    public class Creatures
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public CardColor? Color { get; set; }
    }
}
