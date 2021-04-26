using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CampaignModule.Domain.Timer;
using CampaignModule.Utilities;
using Newtonsoft.Json;

namespace CampaignModule.Core.Repositories
{
    public class TimerRepository : BaseOperationService, ITimerRepository<TimerItem>
    {
        public async Task<string> CreateAsync(TimerItem entity)
        {
            var timer = await base.GetValuesFromFolder<TimerItem>(Constants.TimerConstant.StorePath);

            if (timer == null)
                timer = new TimerItem();

            timer.Hour += entity.Hour;

            if (timer.Hour > 24)
                throw new Exception(Constants.General.TimeCanNoLongerThanFull);

            var jsonable = JsonConvert.SerializeObject(timer);

            var createResponse = await base.WriteJson(Constants.TimerConstant.StorePath, jsonable);

            if (createResponse)
                return ResponseHelper.GetInstance().GetResponse(Constants.TimerConstant.IncreaseTimeMessage, new object[] { timer.Hour });
            else
                throw new Exception(Constants.General.CreateErrorMessage);
        }
    }
}
