using Prism.Events;
using WpfDemo.ViewModels;

namespace WpfDemo.PubSubEvents
{
    public class CustomerDeletePubSubEvent : PubSubEvent<CustomerViewModel>
    {
    }
}
