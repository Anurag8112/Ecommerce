using Ecommerce.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Interface
{
    public interface ISizeRepository
    {

        public bool AddSize(AddSizeModel model);
        public bool EditSize(EditSizeModel model);
        public bool DeleteSize(DeleteSizeModel model);
        public List<ShowSizeModel> ShowAllSizes();
    }
}
