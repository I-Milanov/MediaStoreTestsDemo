using MediaStoreTestsDemo.Repository;

namespace MediaStoreTestsDemo
{
    public class MediaTypeTests : BaseApiTests<MediaType>        
    {
        protected override Repository<MediaType> Repository {  get; set; } = new MediaTypeRepository();
        protected override Factory<MediaType> Factory { get; set; } = new MediaTypeFactory();
    }
}