using Shared.Factory.Dto;

namespace HostData.Controller.Contract;

public interface ICredentialsController
{
    public Task<CredentialsDto> CreateCredentials(dynamic password);
}