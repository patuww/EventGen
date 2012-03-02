using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace JCI.Security.EventGen.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEventGenAdminService" in both code and config file together.
    [ServiceContract]
    public interface IEventGenAdminService
    {
        [OperationContract]
        void DoWork();
    }
}
