using Ecommerce.Models.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models.ViewModel
{
    public class ShowDpHub
    {
        public int DpHubId { get; set; }
        public string DpHubName { get; set; }
        public ShowDpHubAddressModel HubAddress { get; set; }
    }
}
