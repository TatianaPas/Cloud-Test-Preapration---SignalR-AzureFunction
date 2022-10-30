using Microsoft.AspNetCore.SignalR;

namespace SignalRSamlple.Hubs
{
    public class DeathlyHallowHub :Hub
    {
        public Dictionary<string, int> GetRaceStatus()
        {
            return StaticData.DealthyHallowRace;
        }
    }
}
