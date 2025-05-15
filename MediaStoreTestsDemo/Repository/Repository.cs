using MediaStoreTestsDemo.Extensions;
using RestSharp;
using RestSharp.Authenticators.OAuth2;

namespace MediaStoreTestsDemo.Repository
{
    public abstract class Repository<T>
        where T : ApiModel
    {
        protected abstract string Path { get; set; }
        protected RestClientOptions RestClientOptions { get; set; }
        protected RestClient RestClient { get; set; }

        protected Repository()
        {
            RestClientOptions = GetOptions();
            RestClient = new RestClient(RestClientOptions);
        }

        public RestResponse<List<T>> GetAll()
        {
            var getAllRequest = new RestRequest(Path, Method.Get);                   
         
            var getAllResponse = RestClient.Execute<List<T>>(getAllRequest);

            return getAllResponse;
        }

        public RestResponse<T> GetById(T apiEntity)
        {
            return GetById(apiEntity.GetKey());
        }

        public RestResponse<T> GetById(object id)
        {
            var getRequest = new RestRequest($"{Path}/{id.ToString()}", Method.Get);

            var getResponse = RestClient.Execute<T>(getRequest);

            return getResponse;
        }

        public RestResponse<T> Create(T apiEntity)
        {
            var postRequest = new RestRequest($"{Path}", Method.Post);
            postRequest.AddBody(apiEntity);

            var postResponse = RestClient.Execute<T>(postRequest);

            return postResponse;
        }

        protected virtual RestClientOptions GetOptions() {
            var options = new RestClientOptions("http://localhost:60715/");
            options.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(Environment.GetEnvironmentVariable("mediastore_token"), "Bearer");

            return options;
        }
    }
}
