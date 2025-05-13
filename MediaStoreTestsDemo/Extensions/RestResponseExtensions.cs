using RestSharp;

namespace MediaStoreTestsDemo.Extensions
{
    public static class RestResponseExtensions
    {
        public static RestResponse<TResultType> AssertResponseStatusSuccessful<TResultType>(this RestResponse<TResultType> response)
         where TResultType : IEquatable<TResultType>, new()
        {
            if ((int)response.StatusCode <= 200 && (int)response.StatusCode >= 299)
            {
                throw new AssertionException($"Request's status code was not successful - {response.StatusCode}. URL = {response.ResponseUri}");
            }

            return response;
        }

        public static RestResponse<TResultType> AssertResultEquals<TResultType>(this RestResponse<TResultType> response, TResultType result)
            where TResultType : IEquatable<TResultType>, new()
        {
            if (!response.Data.Equals(result))
            {
                throw new AssertionException($"Request's Data was not equal to {result}. URL = {response.ResponseUri}");
            }

            return response;
        }

        public static RestResponse<TResultType> AssertJsonSchema<TResultType>(this RestResponse<TResultType> response, string schema)
            where TResultType : IEquatable<TResultType>, new()
        {
            // Json schmea validation

            return response;
        }
    }
}
