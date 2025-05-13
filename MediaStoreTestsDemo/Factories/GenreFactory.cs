namespace MediaStoreTestsDemo
{
    public static class GenreFactory
    {
        public static Genre BuildFull(int idToIncrease)
        {
            var genre = Build(idToIncrease);
            genre.Name = "RandomName125342";

            return genre;
        }

        public static Genre Build(int idToIncrease)
        {
            return new Genre()
            {
                GenreId = idToIncrease + 1,
            };
        }
    }
}
