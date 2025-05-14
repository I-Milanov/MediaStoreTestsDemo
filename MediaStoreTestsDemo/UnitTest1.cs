using System.Net;
using MediaStoreTestsDemo.Extensions;
using RestSharp;
using RestSharp.Authenticators.OAuth2;

namespace MediaStoreTestsDemo
{
    public class Tests
    {
        private RestClient _restClient;

        public List<Genre> GetAllGenres()
        {
            var getGenresRequest = new RestRequest("api/Genres/", Method.Get);
            var getGeresResponse = _restClient.Execute<List<Genre>>(getGenresRequest);

            return getGeresResponse.Data;
        }

        public Genre CreateGenre()
        {
            // Get genre with higer id
            var lastId = GetAllGenres().Last().GenreId;

            // Create new object Genre with random data and correct id
            var genre = new GenreFactory().Build(lastId);

            // Prepare post request
            var postGenreRequest = new RestRequest($"api/Genres/", Method.Post);
            // add body
            postGenreRequest.AddBody(genre);

            // Send Request
            var postGenreResponse = _restClient.Execute<Genre>(postGenreRequest);
            return postGenreResponse.Data;
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            // Create options to control the rest Client
            var options = new RestClientOptions("http://localhost:60715/");
            // Add correct authentication
            options.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator($"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJiZWxsYXRyaXhVc2VyIiwianRpIjoiNjEyYjIzOTktNDUzMS00NmU0LTg5NjYtN2UxYmRhY2VmZTFlIiwibmJmIjoxNTE4NTI0NDg0LCJleHAiOjE1MjM3MDg0ODQsImlzcyI6ImF1dG9tYXRldGhlcGxhbmV0LmNvbSIsImF1ZCI6ImF1dG9tYXRldGhlcGxhbmV0LmNvbSJ9.Nq6OXqrK82KSmWNrpcokRIWYrXHanpinrqwbUlKT_cs", "Bearer");

            _restClient = new RestClient(options);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _restClient.Dispose();
        }

        [Test]
        public void ResponseWasCorrect_When_GetAllGenres()
        {
            // Prepare get all genres request
            var getGenresRequest = new RestRequest("api/Genres/", Method.Get);

            // execute the request
            var getGeresResponse = _restClient.Execute<List<Genre>>(getGenresRequest);

            // Assert result from request
            Assert.AreEqual(getGeresResponse.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public void CorrectGenreRetrieved_When_GetGenreById()
        {
            // Prepare Request for exact genre
            var genreId = 1;
            var getGenreRequest = new RestRequest($"api/Genres/{genreId}", Method.Get);
            var getGenreResponse = _restClient.Execute<Genre>(getGenreRequest);
            var getGenreData = getGenreResponse.Data;

            // Проверки
            Assert.That(1, Is.EqualTo(getGenreData.GenreId));
            Assert.That("Rock", Is.EqualTo(getGenreData.Name));
            Assert.That(HttpStatusCode.OK, Is.EqualTo(getGenreResponse.StatusCode));
        }

        [Test]
        public void GenreCreatedSuccessful_When_SendPostRequest()
        {
            // Get genre with higer id
            var getGenresRequest = new RestRequest("api/Genres/", Method.Get);
            var getGeresResponse = _restClient.Execute<List<Genre>>(getGenresRequest);
            var lastId = getGeresResponse.Data.Last().GenreId;

            // Create new object Genre with random data and correct id
            var genre = new Genre();
            genre.Name = "Random12341251235";
            genre.GenreId = lastId+1;
  
            // Prepare post request
            var postGenreRequest = new RestRequest($"api/Genres/", Method.Post);
            // add body
            postGenreRequest.AddBody(genre);

            // Send Request
            var postGenreResponse = _restClient.Execute<Genre>(postGenreRequest);
            var postGenreData = postGenreResponse.Data;

            // Assertions
            Assert.That(HttpStatusCode.OK, Is.EqualTo(postGenreResponse.StatusCode));
            Assert.AreEqual(HttpStatusCode.OK, postGenreResponse.StatusCode);
            Assert.AreEqual(genre, postGenreData);         
        }

        [Test]
        public void CreateGenreTestShorter()
        {
            // Get genre with higer id
            var lastId = GetAllGenres().Last().GenreId;

            // Create new object Genre with random data and correct id
            var genre = new GenreFactory().Build(lastId);

            // Prepare post request
            var postGenreRequest = new RestRequest($"api/Genres/", Method.Post);
            // add body
            postGenreRequest.AddBody(genre);

            // Send Request
            var postGenreResponse = _restClient.Execute<Genre>(postGenreRequest);
            var postGenreData = postGenreResponse.Data;

            postGenreResponse
                .AssertResponseStatusSuccessful()
                .AssertResultEquals(genre);
        }

        [Test]
        public void GenreDeletedSuccessful_When_SendDeleteRequest()
        {
            var genre = CreateGenre();

            var deleteRequest = new RestRequest($"api/Genres/{genre.GenreId}", Method.Delete);
            var deleteResponse = _restClient.Execute<Genre>(deleteRequest);

            var allGeres = GetAllGenres();

            deleteResponse.AssertResponseStatusSuccessful();

            //Assert.That(1, Is.EqualTo(getGenreData.GenreId));
            //Assert.That("Rock", Is.EqualTo(getGenreData.Name));
            //Assert.That(HttpStatusCode.OK, Is.EqualTo(getGenreResponse.StatusCode));
        }

        [Test]
        public void PutGenre()
        {
            var genre = CreateGenre();

            var putRequest = new RestRequest($"api/Genres/{genre.GenreId}", Method.Put);
            genre.Name = "New" + genre.Name;
            putRequest.AddBody(genre);
            var putResponse = _restClient.Execute<Genre>(putRequest);

            var getGenreRequest = new RestRequest($"api/Genres/{genre.GenreId}", Method.Get);
            var getGenreResponse = _restClient.Execute<Genre>(getGenreRequest);

            Assert.That(HttpStatusCode.OK, Is.EqualTo(putResponse.StatusCode));

            //Assert.That(1, Is.EqualTo(getGenreData.GenreId));
            //Assert.That("Rock", Is.EqualTo(getGenreData.Name));
            //Assert.That(HttpStatusCode.OK, Is.EqualTo(getGenreResponse.StatusCode));
        }

        public string GetGenreJsonSchema()
        {
            return "{\r\n    \"$schema\": \"https://json-schema.org/draft/2020-12/schema\",\r\n    \"items\": {\r\n        \"properties\": {\r\n            \"genreId\": {\r\n                \"type\": \"integer\"\r\n            },\r\n            \"name\": {\r\n                \"type\": \"string\"\r\n            },\r\n            \"tracks\": {\r\n                \"type\": \"array\"\r\n            }\r\n        },\r\n        \"required\": [\r\n            \"genreId\",\r\n            \"name\",\r\n            \"tracks\"\r\n        ],\r\n        \"type\": \"object\"\r\n    },\r\n    \"type\": \"array\"\r\n}";
        }

    }
}