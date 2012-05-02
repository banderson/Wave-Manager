using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WaveManagerUtil
{
    public static class NumericHelper
    {
        public static int toMegabytes(this long bytes)
        {
            // 1024*1024 = 1048576
            return (int)(bytes/1048576);
        }

        public static int Lesser(int first, int second)
        {
            return (first < second)
                ? first
                : second;
        }
    }
}
