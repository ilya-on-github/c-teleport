using System.Net;
using Moq;
using RestSharp;

namespace Distances.Tests
{
    public class TestRestResponseBuilder<T>
    {
        private T _data;
        private HttpStatusCode _statusCode;

        public TestRestResponseBuilder<T> WithData(T data)
        {
            _data = data;
            return this;
        }

        public TestRestResponseBuilder<T> WithStatusCode(HttpStatusCode statusCode)
        {
            _statusCode = statusCode;
            return this;
        }

        public Mock<IRestResponse<T>> Build()
        {
            var mock = new Mock<IRestResponse<T>>();

            mock.Setup(x => x.StatusCode).Returns(_statusCode);
            mock.Setup(x => x.Data).Returns(_data);

            return mock;
        }
    }
}