using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1
{
    /// <summary>
    /// 测试
    /// </summary>
    [Description("test1111")]
    [Comment("测试")]
    public class hc_user
    {
        /// <summary>
        /// id
        /// </summary>
        [Description("主键")]
        [Comment("主键")]
        public int id { get; set; }
        [Comment("名称")]
        public string name { get; set; }
    }
}
