using ApiModule.Api;
using ApiModule.Attributes;
using Shared.Exceptions;

namespace ApiModule.Operations;

public class IntegrationModule : IIntegrationModule
{
    private bool disposedValue;

    public IntegrationModule()
    {
        var q = GetLicenceModuleAttrubute();
    }

    private async Task CheckLicence(IEnumerable<LicenceModuleAttribute> attributes)
    {
        if (attributes.Distinct().Count() != 1)
            throw new InvalidLicenceModuleException("Multiple use different ModuleLicenceId");
    }

    private IEnumerable<LicenceModuleAttribute> GetLicenceModuleAttrubute()
    {
        var typesWithMyAttribute =
            from a in AppDomain.CurrentDomain.GetAssemblies()
            from t in a.GetTypes()
            let attributes = t.GetCustomAttributes(typeof(LicenceModuleAttribute), true)
            where attributes != null && attributes.Length > 0
            select new { Type = t, Attributes = attributes.Cast<LicenceModuleAttribute>() };
        return typesWithMyAttribute.SelectMany(x => x.Attributes);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {

            }
            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}