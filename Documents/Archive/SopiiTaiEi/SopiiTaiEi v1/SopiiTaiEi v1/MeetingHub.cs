using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SopiiTaiEi1
{
    public class MeetingHub : Hub
    {
        //static List<string> attendeeNames = new List<string>();

        //public void FillAttendeeList(string name)
        //{
        //    attendeeNames.Add(name);

        //}
        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("NewAttendee");
        }
        //public async Task FillGlobalArray()
        //{
        //    await Clients.All.SendAsync("ReceiveGlobalArray", attendeeNames);
        //}
        public async Task FillTempArray(string[] TempArray)
        {
            await Clients.All.SendAsync("ReceiveTempArray", TempArray);
        }
        public async Task SendCell(string id)
        {
            await Clients.All.SendAsync("ReceiveCell", id);
        }
    }
}
