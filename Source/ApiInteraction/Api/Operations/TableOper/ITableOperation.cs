using Shared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Operations.TableOper;

public interface ITableOperation
{
    public IReadOnlyList<ITable> GetTables();
}