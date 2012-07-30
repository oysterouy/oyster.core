using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oyster.Core.Orm;
using Oyster.Core.Cache;
using Oyster.Core.Db;

namespace System
{
    public partial class OyEngine
    {
        public static CacheEngine C
        {
            get
            {
                return CacheEngine.Instance;
            }
        }
        public static DbEngine D
        {
            get
            {
                return DbEngine.Instance;
            }
        }
    }
}
