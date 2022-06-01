using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostData.Modules;

public class HelloModule : NancyModule
{
    public HelloModule() : base("/")
    {
        Get("/", parameters => "Hello World");

        Get("/hello", parameters => "Helloqweqweqw");
    }
}