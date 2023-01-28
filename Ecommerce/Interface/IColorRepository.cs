using Ecommerce.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Interface
{
    public interface IColorRepository
    {
        public bool AddColor(AddColorModel model);
        public bool EditColor(EditColorModel model);
        public bool DeleteColor(DeleteColorModel model);
        public List<ShowAllColor> ShowAllColor();
    }
}
