using System;
using System.Collections.Generic;
using System.Text;

namespace CampaignModule.Utilities
{
    public class ResponseHelper
    {
        private static ResponseHelper instance;

        protected ResponseHelper()
        {
        }

        public static ResponseHelper GetInstance()
        {
            // Uses lazy initialization.

            if (instance == null)
            {
                instance = new ResponseHelper();
            }

            return instance;
        }

        public string GetResponse(string baseMessage, object[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                baseMessage = baseMessage.Replace($"[PARAM_{i+1}]", args[i].ToString());
            }

            return baseMessage;
        }
    }
}
