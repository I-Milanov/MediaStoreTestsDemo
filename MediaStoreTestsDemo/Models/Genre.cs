
namespace MediaStoreTestsDemo
{
    public class Genre : IEquatable<Genre?>
    {
        public int GenreId { get; set; }

        public string Name { get; set; }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Genre);
        }

        public bool Equals(Genre? other)
        {
            return other is not null &&
                   GenreId == other.GenreId &&
                   Name == other.Name;
        }

        public static bool operator ==(Genre? left, Genre? right)
        {
            return EqualityComparer<Genre>.Default.Equals(left, right);
        }

        public static bool operator !=(Genre? left, Genre? right)
        {
            return !(left == right);
        }
    }
}
