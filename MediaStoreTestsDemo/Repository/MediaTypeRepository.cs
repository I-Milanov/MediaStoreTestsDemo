namespace MediaStoreTestsDemo.Repository
{
    public class MediaTypeRepository : Repository<MediaType>
    {
        protected override string Path { get; set; } = "api/MediaTypes";
    }
}
