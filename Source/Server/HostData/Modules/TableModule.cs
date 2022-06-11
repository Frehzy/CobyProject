using HostData.Cache.Config;
using HostData.Cache.Orders;
using HostData.Cache.Tables;
using HostData.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostData.Modules;

public class TableModule : BaseModule
{
    private readonly IConfigCache _configCache;
    private readonly TableController _tableController;

    public TableModule(ITableCache tableCache, IConfigCache configCache) : base()
    {
        _configCache = configCache;
        _tableController = new(tableCache);

        Get("/tables", parameters =>
        {
            return Execute(Context, () => _tableController.GetTables());
        });
    }
}