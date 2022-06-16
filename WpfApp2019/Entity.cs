using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateTestCase
{
    public class EntityAttribute
    {
        public string Name { get; set; }
        public string Type { get; set; }

        public string DataType { get; set; }

    }
    public class Entity
    {
        public string Name { get; set; }

        public List<EntityAttribute> Attributes;

        public Entity(string Name)
        {
            this.Name = Name;
            Attributes = new List<EntityAttribute> {};
        }

        public List<EntityAttribute> GetAttributes(string Type)
        {
            return Attributes.Where(attr => attr.Type == Type).ToList();
        }
        public EntityAttribute GetAttributeByName(string Name)
        {
            return Attributes.Where(attr => attr.Name == Name).First();
        }

        public List<EntityAttribute> GetAllAttributes()
        {
            return Attributes;
        }
    }
}
