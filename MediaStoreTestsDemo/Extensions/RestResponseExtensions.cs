using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RestSharp;
using Newtonsoft.Json.Schema;

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

        /// <summary>
        /// Use the method the assert the response schema
        /// <example><code>response.AssertSchema(new FileInfo("path/to/schema.json"))</code></example>
        /// </summary>
        /// <typeparam name="TResultType">Response model</typeparam>
        /// <param name="response">Tesponse</param>
        /// <param name="schemaFile"></param>
        /// <returns>Same response</returns>
        /// <exception cref="ArgumentNullException">If response or schema are null</exception>
        /// <exception cref="FileNotFoundException">If schema file could not be found</exception>
        /// <exception cref="InvalidOperationException">If json is invalid</exception>
        public static RestResponse<TResultType> AssertSchema<TResultType>(this RestResponse<TResultType> response, string schemaFilePath)
            where TResultType : IEquatable<TResultType>, new()
        {
           return response.AssertSchema(new FileInfo(schemaFilePath));
        }

        /// <summary>
        /// Use the method the assert the response schema
        /// <example><code>response.AssertSchema(new FileInfo("path/to/schema.json"))</code></example>
        /// </summary>
        /// <typeparam name="TResultType">Response model</typeparam>
        /// <param name="response">Tesponse</param>
        /// <param name="schemaFile"></param>
        /// <returns>Same response</returns>
        /// <exception cref="ArgumentNullException">If response or schema are null</exception>
        /// <exception cref="FileNotFoundException">If schema file could not be found</exception>
        /// <exception cref="InvalidOperationException">If json is invalid</exception>
        public static RestResponse<TResultType> AssertSchema<TResultType>(this RestResponse<TResultType> response, FileInfo schemaFile)
            where TResultType : IEquatable<TResultType>, new()
        {
            if (response == null)
            { 
                throw new ArgumentNullException(nameof(response));
            }

            if (schemaFile == null)
            { 
                throw new ArgumentNullException(nameof(schemaFile));
            }

            if (!schemaFile.Exists)
            {
                throw new FileNotFoundException($"Schema file not found: {schemaFile.FullName}");
            }

            string schemaJson = File.ReadAllText(schemaFile.FullName);
            JSchema schema = JSchema.Parse(schemaJson);

            try
            {
                JToken jsonResponse = JToken.Parse(response.Content);

                IList<string> validationErrors = new List<string>();
                bool isValid = jsonResponse.IsValid(schema, out validationErrors);
                if (!isValid)
                {
                    string errorMessages = string.Join(Environment.NewLine, validationErrors);
                    Assert.IsTrue(isValid, $"JSON Schema validation failed:{Environment.NewLine}{errorMessages}");
                }

                return response;
            }
            catch (JsonReaderException ex)
            {
                throw new InvalidOperationException($"Invalid JSON in response: {ex.Message}", ex);
            }
        }
    }
}
