using Core.Entities;

namespace Web.ViewModels.Contact
{
    public class ContactIndexVM
    {
        public Location Location { get; set; }
        public List<ContactComponent> ContactComponents { get; set; }
    }
}
