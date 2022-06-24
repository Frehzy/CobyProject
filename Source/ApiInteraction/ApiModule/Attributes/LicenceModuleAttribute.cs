namespace ApiModule.Attributes;

[AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false)]
public sealed class LicenceModuleAttribute : Attribute
{
    public int ModuleLicenceId { get; private set; }

    public LicenceModuleAttribute(int moduleLicenceId)
    {
        ModuleLicenceId = moduleLicenceId;
    }
}