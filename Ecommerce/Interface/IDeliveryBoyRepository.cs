using Ecommerce.Models.ViewModel;

namespace Ecommerce.Interface
{
    public interface IDeliveryBoyRepository
    {
        public bool AddDeliveryBoy(AddDeliveryBoyModel model);
        public bool RemoveDeliveryBoy();
        public bool ChangeDeliveryHub();
    }
}
