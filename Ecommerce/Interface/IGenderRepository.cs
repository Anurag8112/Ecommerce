using Ecommerce.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Interface
{
    public interface IGenderRepository
    {
        public bool AddGender(AddGenderModel model);
        public bool EditGender(EditGenderModel model);
        public List<ShowGendersModel> ShowGenders();
        public bool DeleteGender(DeleteGenderModel model);
    }
}
