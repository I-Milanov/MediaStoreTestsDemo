namespace MediaStoreTestsDemo.Repository
{
    public class GenreRepository : Repository<Genre>
    {
        protected override string Path { get; set; } = "api/Genres";
    }
}
