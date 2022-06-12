﻿using Shared.Data;

namespace Api.Operations.TableOper;

public interface ITableOperation
{
    public IReadOnlyList<ITable> GetTables();

    public IReadOnlyList<ITable> ChangeTable(ICredentials credentials, IReadOnlyList<ITable> tables, ref ISession session);
}