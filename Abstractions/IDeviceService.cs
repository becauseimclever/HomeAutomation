using System.Threading.Tasks;

namespace BecauseImClever.HomeAutomation.Abstractions
{
    public interface IDeviceService
    {
        Task DoTheThing(string value);
    }
}
