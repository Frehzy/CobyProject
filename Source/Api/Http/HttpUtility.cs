namespace Api.Http;

internal static class HttpUtility
{
    public static Uri CreateUri(string ip, int port, string path) =>
        new(string.Format("http://{0}:{1}/{2}", ip, port, path));

    public static Uri CreateUri(string ip, int port, string path, params object[] param) =>
        new(string.Format("http://{0}:{1}/{2}/{3}", ip, port, path, string.Join("/", param)));
}