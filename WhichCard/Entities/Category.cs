using System;
namespace WhichCard.Entities
{
    /// <summary>
    /// Represents a shopping or reward category
    /// </summary>
    public class Category
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public Icon Icon { get; set; }
    }

    /// <summary>
    /// Represents an Icon to represent a category
    /// </summary>
    public class Icon
    {
        public string Name { get; set; }
    }
}
