namespace Monopoly
{
    internal class Field
    {
        public string Name { get; }

        public Monopoly.FieldType FieldType { get; }

        public int OwnerId { get; private set; } = 0;

        public bool Flag { get; } = false;

        public Field(string name, Monopoly.FieldType fieldType)
        {
            Name = name;
            FieldType = fieldType;
        }

        public void SetOwner(int buyerId)
        {
            OwnerId = buyerId;
        }
    }
}
