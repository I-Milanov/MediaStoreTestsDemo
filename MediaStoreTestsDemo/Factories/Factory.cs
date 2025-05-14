namespace MediaStoreTestsDemo
{
    public abstract class Factory<T>
        where T : ApiModel
    {
        public abstract T Build(int idToIncrease);
     }
}
