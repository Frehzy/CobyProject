using System.Net;

namespace Api.Http;

internal class Response<T>
{
    public HttpStatusCode StatusCode { get; private set; }

    public Uri Uri { get; private set; }

    public T Content { get; private set; }

    public Response(HttpStatusCode statusCode, Uri uri, T contect)
    {
        StatusCode = statusCode;
        Uri = uri;
        Content = contect;
    }
}