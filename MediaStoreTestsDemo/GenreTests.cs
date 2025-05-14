using MediaStoreTestsDemo.Repository;

namespace MediaStoreTestsDemo
{
    public class GenreTests : BaseApiTests<Genre>        
    {
        protected override Repository<Genre> Repository {  get; set; } = new GenreRepository();
        protected override Factory<Genre> Factory { get; set; } = new GenreFactory();
    }
}