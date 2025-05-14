using System.Net;
using MediaStoreTestsDemo.Extensions;
using MediaStoreTestsDemo.Repository;

namespace MediaStoreTestsDemo
{
    public abstract class BaseApiTests<T>
        where T : ApiModel
    {
        protected abstract Repository<T> Repository { get; set; }
        protected abstract Factory<T> Factory { get; set; }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
        }

        [Test]
        public void ResponseWasCorrect_When_GetAllEntities()
        {
            var getResponse = Repository.GetAll();

            Assert.AreEqual(getResponse.StatusCode, HttpStatusCode.OK);
        }
   
        [Test]
        public void EntityCreatedSuccessful_When_SendPostRequest()
        {
            var lastId = Repository.GetAll().Data.Last().GetKey<int, T>();
            var entity = Factory.Build(lastId);

            var postResponse = Repository.Create(entity);

            Assert.Multiple(() =>
            {
                Assert.That(HttpStatusCode.OK, Is.EqualTo(postResponse.StatusCode));
                Assert.AreEqual(HttpStatusCode.OK, postResponse.StatusCode);
                Assert.AreEqual(entity, postResponse.Data);
            });
        }
    }
}