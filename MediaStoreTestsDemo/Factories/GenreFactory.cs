namespace MediaStoreTestsDemo
{
    public class GenreFactory : Factory<Genre>
    {
        public override Genre Build(int idToIncrease)
        {
            return new Genre()
            {
                GenreId = idToIncrease + 1,
                Name = "Test1234Random"
            };
        }
    }
}
