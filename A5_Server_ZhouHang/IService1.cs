using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace A5_Server_ZhouHang
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
         List<string> readTable(string tableName);

        [OperationContract]
         List<string> listPurchases(string cid);

        [OperationContract]
         void placeOrder(string customerId, string[] productIds, int[] quantities);

        // TODO: Add your service operations here
    }

}
