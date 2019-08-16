using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lime
{
    public class TextHelper
    {
        /// <summary>
        /// 查找中间文本 例如 123 ABC 234取ABC:XtoYGetTo(text,"123","234",0)
        /// </summary>
        /// <param name="all"></param>
        /// <param name="r"></param>
        /// <param name="l"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string XtoYGetTo(string all, string r, string l, int t)
        {

            int A = all.IndexOf(r, t);
            int B = all.IndexOf(l, A + 1);
            if (A < 0 || B < 0)
            {
                return null;
            }
            else
            {
                A = A + r.Length;
                B = B - A;
                if (A < 0 || B < 0)
                {
                    return null;
                }
                return all.Substring(A, B);
            }
        }
    }
}
