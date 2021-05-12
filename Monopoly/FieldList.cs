using System.Collections.Generic;
using System.Linq;

namespace Monopoly
{
    internal class FieldList
    {
        private List<Field> fields = new List<Field>();

        public FieldList()
        {
        }

        public void Add(string name, FieldType fieldType)
        {
            fields.Add(new Field(name, fieldType));
        }

        public IReadOnlyCollection<Field> GetAll() => fields.AsReadOnly();

        public Field GetByName(string name)
        {
            return fields.FirstOrDefault(x => x.Name == name) ?? Field.None;
        }
    }
}
