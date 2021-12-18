using System;
using System.Collections.Generic;
using System.Text;

namespace ElarabyCA.Application.Common.Helpers
{
    public static class Messages
    {
        public static class SparePart
        {
            public static string CannotDeleteSparePartHasInventory  => "ServerMessages.SparePart.CannotDeleteSparePartHasInventory";
        }

        public static class Store
        {
            public static string CannotDeleteStoreHasInventory => "ServerMessages.Store.CannotDeleteStoreHasInventory";
        }

        public static class InventoryTransaction
        {
            public static string AmountIsGreateThanCurrentBalance => "ServerMessages.InventoryTransaction.AmountIsGreateThanCurrentBalance";
        }

        public static class SMSMessage
        {
            public static string CannotDeleteSMSMessageHasOrder => "ServerMessages.SMSMessage.CannotDeleteSMSMessageHasOrder";
        }
    }
}

