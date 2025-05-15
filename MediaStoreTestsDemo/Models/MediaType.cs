namespace MediaStoreTestsDemo
{
    public class MediaType : ApiModel, IEquatable<MediaType?>
    {
        [Key]
        public int MediaTypeId { get; set; }

        public string Name { get; set; }

        public override bool Equals(object? obj)
        {
            return Equals(obj as MediaType);
        }

        public bool Equals(MediaType? other)
        {
            return other is not null &&
                   MediaTypeId == other.MediaTypeId &&
                   Name == other.Name;
        }

        public static bool operator ==(MediaType? left, MediaType? right)
        {
            return EqualityComparer<MediaType>.Default.Equals(left, right);
        }

        public static bool operator !=(MediaType? left, MediaType? right)
        {
            return !(left == right);
        }
    }
}
