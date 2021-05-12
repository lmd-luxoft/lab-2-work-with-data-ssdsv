namespace Monopoly
{
    internal class Field
    {
        public static readonly Field None = new Field(string.Empty, FieldType.NONE);

        public string Name { get; }

        public FieldType FieldType { get; }

        public int OwnerId { get; private set; } = 0;

        public Field(string name, FieldType fieldType)
        {
            Name = name;
            FieldType = fieldType;
        }

        public void SetOwner(int buyerId)
        {
            OwnerId = buyerId;
        }

        public bool IsOwned() => OwnerId != 0;

        protected bool Equals(Field other)
        {
            return Name == other.Name && FieldType == other.FieldType && OwnerId == other.OwnerId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Field) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int) FieldType;
                hashCode = (hashCode * 397) ^ OwnerId;
                return hashCode;
            }
        }
    }
}
