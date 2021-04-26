using CampaignModule.Domain.Timer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CampaignModule.Service.Timer
{
    public class TimerService : ITimerService
    {
        private readonly ITimerRepository<TimerItem> _timerRepository;

        public TimerService(ITimerRepository<TimerItem> timerRepository)
        {
            this._timerRepository = timerRepository;
        }

        public async Task<string> CreateAsync(List<string> commands)
        {
            if (commands.Count != 2)
                throw new Exception("Command parameters aren't as expected.");

            var hour = Convert.ToInt32(commands[1]);

            var timerEntity = new TimerItem() { Hour = hour };

            return await _timerRepository.CreateAsync(timerEntity);
        }
    }
}
