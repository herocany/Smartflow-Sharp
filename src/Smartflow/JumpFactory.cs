using Smartflow.Components;
using Smartflow.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow
{
    internal sealed class JumpFactory
    {
        private static readonly Dictionary<string, IJump> mapService = new Dictionary<string, IJump>();

        static JumpFactory()
        {
            mapService.Add(Utils.CONST_REJECT_TRANSITION_ID, new VetoService());
            mapService.Add(Utils.CONST_BACK_TRANSITION_ID, new BackService());
        }

        public static IJump Create(string command)
        {
            return mapService.ContainsKey(command) ? mapService[command] : new JumpService();
        }
    }
}
