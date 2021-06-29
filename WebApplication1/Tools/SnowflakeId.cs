using System;
using Yitter.IdGenerator;

namespace WebApplication1
{
    public class SnowflakeId
    {
        static SnowflakeId snowflake;

        static object o = new object();
        public static SnowflakeId Instance
        {
            get
            {
                if (snowflake == null)
                {
                    lock (o)
                    {
                        if (snowflake == null)
                            snowflake = new SnowflakeId();
                    }
                }

                return snowflake;
            }
        }

        public SnowflakeId()
        {
            var random = new Random();
            var value = random.Next(0, 63);
            var options = new IdGeneratorOptions((ushort)value);
            YitIdHelper.SetIdGenerator(options);
        }


        /// <summary>
        /// 获取长整形的ID
        /// </summary>
        /// <returns></returns>
        public long GetId()
        {
            return YitIdHelper.NextId();
        }
    }
}
