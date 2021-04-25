using CampaignModule.Domain.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CampaignModule.Utilities
{
    public class CommandHelper
    {
        private static CommandHelper instance;

        protected CommandHelper()
        {
        }

        public static CommandHelper GetInstance()
        {
            if (instance == null)
            {
                instance = new CommandHelper();
            }

            return instance;
        }

        public List<string> PrepareCommandBase(string commandInput)
        {
            var partOfCommands = commandInput.Trim().Split(" ").ToList();

            return partOfCommands;
        }
    }
}
