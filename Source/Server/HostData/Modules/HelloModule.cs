using Nancy;

namespace HostData.Modules;

public class HelloModule : NancyModule
{
    public HelloModule() : base("/")
    {
        Get("/", parameters => "Hello World111");

        Get("/hello", parameters => "Helloqweqweqw");
    }
}