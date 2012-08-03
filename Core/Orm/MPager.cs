using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oyster.Core.Orm
{
    public class Mpager
    {
        public int PageIndex = 1;
        public int PageSize = 20;
        public int TotalCount = 0;
        public int PageCount
        {
            get
            {
                if (TotalCount % PageSize > 0)
                {
                    return TotalCount / PageSize + 1;
                }
                return TotalCount / PageSize;
            }
        }

        public override string ToString()
        {
            return string.Format("this page index is {0},pagesize is {1}."
                , new string[] { PageIndex.ToString(), PageSize.ToString() });
        }
    }
}
