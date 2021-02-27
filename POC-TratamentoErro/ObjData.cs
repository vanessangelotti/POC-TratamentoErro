using System;
using System.Collections.Generic;
using System.Text;

namespace POC_TratamentoErro
{
    public class ObjData
    {
        public int ContractId { get; set; }
        public int TransactionId { get; set; }
        //ToDo: colocar como um array "VehicleAccounts"
        public int VehicleAccount { get; set; }
        public Guid OriginBlockId { get; set; }
        public int AutomaticBlockUnblock { get; set; }
        public Uri fileUrl { get; set; }

    }
}
