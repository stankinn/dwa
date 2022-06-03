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
            Attributes = new List<EntityAttribute> { new EntityAttribute(){ Name = "Attribute1", Type ="A", DataType="Int" },
                                                     new EntityAttribute(){ Name = "Attribute2", Type ="A", DataType="String"  },
                                                     new EntityAttribute(){ Name = "Attribute3", Type ="B", DataType="Date" },
                                                     new EntityAttribute(){ Name = "Attribute4", Type ="A", DataType="String" },
                                                     new EntityAttribute(){ Name = "SpecialAtttribute", Type ="C", DataType="String" }
                                             };
        }

        public List<EntityAttribute> GetAttributes(string Type)
        {
            return Attributes.Where(attr => attr.Type == Type).ToList();
        }
        public EntityAttribute GetAttributeByName(string Name)
        {
            return Attributes.Where(attr => attr.Name == Name).First();
        }


    }
}
