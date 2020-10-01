using EmitMapper.MappingConfiguration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Smartflow.Web.Code
{
    public class IgnoreCaseRule: DefaultMapConfig
    {
        private readonly static  DefaultMapConfig newInstance;
        public static DefaultMapConfig NewInstance
        {
            get
            {
                return newInstance;
            }
        }

        static IgnoreCaseRule()
        {
            newInstance = new IgnoreCaseRule();
        }


        protected override bool MatchMembers(string m1, string m2)
        {
            return base.MatchMembers(m1.ToLower(), m2.ToLower());
        }
    }
}
