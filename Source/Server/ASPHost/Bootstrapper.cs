﻿using Nancy;
using Nancy.Bootstrapper;

namespace ASPHost;

public class Bootstrapper : DefaultNancyBootstrapper
{
    protected override IEnumerable<ModuleRegistration> Modules =>
        GetType().Assembly.GetTypes().Where(x => x.BaseType.Equals(typeof(NancyModule))).Select(x => new ModuleRegistration(x));
}