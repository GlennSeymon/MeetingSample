using MeetingSample.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingSample.WebAPI.Interface
{
    public interface IMeetingService
    {
        Meeting Get(int meetCode);
    }
}
