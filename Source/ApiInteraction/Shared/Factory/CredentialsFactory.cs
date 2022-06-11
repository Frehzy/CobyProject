using Shared.Data;
using Shared.Factory.Dto;
using Shared.Factory.InternalModel;

namespace Shared.Factory;

internal class CredentialsFactory
{
    public static Credentials Create(ICredentials credentials) =>
        new(credentials.Id);

    public static CredentialsDto CreateDto(ICredentials credentials) =>
        new(credentials.Id);

    public static Credentials Create(CredentialsDto credentials) =>
        new(credentials.Id);
}