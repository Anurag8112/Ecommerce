using Ecommerce.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Interface
{
    public interface IHubRepository
    {
        public bool AddDPHub(AddDpHubModel model);
        public bool RemoveDpHub(DeleteDpHub model);
        public bool EditDpHub(EditDpHub model);
        public List<ShowDpHub> ShowDpHub();
    }
}
