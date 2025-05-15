namespace MediaStoreTestsDemo
{
    public class MediaTypeFactory : Factory<MediaType>
    {
        public override MediaType Build(int idToIncrease)
        {
            return new MediaType()
            {
                MediaTypeId = idToIncrease + 1,
                Name = "Test1234Random"
            };
        }
    }
}
