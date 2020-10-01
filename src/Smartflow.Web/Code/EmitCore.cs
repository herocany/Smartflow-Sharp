using EmitMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Smartflow.Web.Code
{
    public static class EmitCore
    {
        /// <summary>
        /// 强制换
        /// </summary>
        /// <typeparam name="From"></typeparam>
        /// <typeparam name="To"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static To Convert<From, To>(From entity)
        {
            ObjectsMapper<From, To> mapper =
                ObjectMapperManager.DefaultInstance.GetMapper<From, To>(IgnoreCaseRule.NewInstance);

            return mapper.Map(entity);
        }
    }
}
