namespace HostData.Cache.Entities;

public class LicenceAction
{
    public List<string> TerminalsId { get; } = new();

    public int ReservedLicenceCount { get; private set; } = 0;

    public int MaxReservedLicence { get; set; }

    public LicenceAction(int maxReservedLicence)
    {
        MaxReservedLicence = maxReservedLicence;
    }

    public void ReservedLicence(string terminalId)
    {
        if (ReservedLicenceCount + 1 > MaxReservedLicence)
            throw new ArgumentOutOfRangeException(nameof(ReservedLicenceCount));

        TerminalsId.Add(terminalId);
        ReservedLicenceCount++;
    }

    public void DisposeLicence(string terminalId)
    {
        if (ReservedLicenceCount - 1 < 0)
            throw new ArgumentOutOfRangeException(nameof(ReservedLicenceCount));

        TerminalsId.Remove(terminalId);
        ReservedLicenceCount--;
    }
}