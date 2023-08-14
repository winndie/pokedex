using System;
namespace Pokedex.Models
{
	public class Variation
	{
        public string Name { get; set; }
        public List<Variation> Variations { get; set; }

        public Variation(string name, List<Variation> variations)
        {
            Name = name;
            Variations = variations;
        }
    }
}

