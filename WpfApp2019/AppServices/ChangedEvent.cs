using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp2019.Model;

namespace WpfApp2019.AppServices
{
    internal class PathChangedEvent : PubSubEvent<PathText>
    {
    }
    internal class TNameChangedEvent : PubSubEvent<Table>
    {
    }
    internal class GVisibilityChangedEvent : PubSubEvent<GridVisible>
    {
    }
}
