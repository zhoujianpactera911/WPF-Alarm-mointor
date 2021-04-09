using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhaoxi.Industrial.Base
{
    public static class ExtendClass
    {
        public static float ByteArrayToFloat(this byte[] value)
        {
            float fValue = 0f;
            uint nRest = ((uint)value[2]) * 256
                + ((uint)value[3]) +
                65536 * (((uint)value[0]) * 256 + ((uint)value[1]));
            unsafe
            {
                float* ptemp;
                ptemp = (float*)(&nRest);
                fValue = *ptemp;
            }
            return fValue;
        }

        public static Int16 ByteArrayToInt16(this byte[] value)
        {
            short s = 0;
            short s0 = (short)(value[0] & 0xff);// 最低位 
            short s1 = (short)(value[1] & 0xff);
            s1 <<= 8;
            s = (short)(s0 | s1);
            return s;
        }
    }
}
