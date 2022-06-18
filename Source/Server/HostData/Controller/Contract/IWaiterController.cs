﻿using HostData.Domain.Contracts.Models;
using Shared.Factory.Dto;

namespace HostData.Controller.Contract;

public interface IWaiterController
{
    public Task<List<WaiterDto>> GetWaiters();

    public Task<WaiterDto> GetWaiterById(dynamic waiterId);

    [Obsolete("ТОЛЬКО ДЛЯ СЕРВЕРА")]
    public Task<WaiterModel> GetWaiterByPassword(dynamic password);

    public Task<WaiterDto> CreateWaiter(dynamic credentials, dynamic name, dynamic password);

    public Task<WaiterDto> RemoveWaiter(dynamic credentials, dynamic waiterId);

    public Task<WaiterDto> AddPermissionOnWaiterById(dynamic credentials, dynamic waiterId, dynamic permissionId);

    public Task<WaiterDto> RemovePermissionOnWaiterById(dynamic credentials, dynamic waiterId, dynamic permissionId);
}