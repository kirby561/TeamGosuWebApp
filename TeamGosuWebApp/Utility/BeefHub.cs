using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace TeamGosuWebApp.Utility {
    public class BeefHub : Hub {
        public async Task OnBeefLadderUpdated()
            => await Clients.All.SendAsync("OnBeefLadderUpdated");
    }
}
