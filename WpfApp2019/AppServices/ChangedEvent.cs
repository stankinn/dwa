using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp2019.Model;

namespace WpfApp2019.AppServices
{
    internal class PathChangedEvent : PubSubEvent<string>
    {
    }
    internal class DirectoryChangedEvent : PubSubEvent<string>
    {
    }

    internal class TNameChangedEvent : PubSubEvent<Table>
    {
    }
    internal class GVisibilityChangedEvent : PubSubEvent<GridVisible>
    {
    }
    internal class ServerChangedEvent : PubSubEvent<ServerNameModel>
    {
    }
    internal class DatabaseChangedEvent : PubSubEvent<DatabaseModel>
    {
    }
    internal class DbConnectionChangedEvent : PubSubEvent<DbConnection>
    {
    }
    internal class SearchChangedEvent : PubSubEvent<SearchParameters>
    {
    }
}
