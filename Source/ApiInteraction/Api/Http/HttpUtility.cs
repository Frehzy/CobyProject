namespace Api.Http;

internal static class HttpUtility
{
    public static Uri CreateUri(string ip, int port, string path) =>
        new(string.Format("http://{0}:{1}/{2}", ip, port, path));
}