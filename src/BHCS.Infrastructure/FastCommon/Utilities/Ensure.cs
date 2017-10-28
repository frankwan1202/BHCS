using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BHCS.Infrastructure.FastCommon.Utilities
{
    public static class Ensure
    {
        public static void NotNull<T>(T obj,string errorMsg,Exception innerException=null)
        {
            if (obj == null) throw new EnsureException(errorMsg, innerException);
        }

        public static void NotNull<T>(IList<T> list,string errorMsg, Exception innerException=null)
        {
            if (list == null||list.Count()==0) throw new EnsureException(errorMsg, innerException);
        }

        public static void NotNull(Guid value, string errorMsg, Exception innerException = null)
        {
            if (value == null || value == Guid.Empty) throw new EnsureException(errorMsg, innerException);
        }

        public static void NotNullOrWhiteSpace(string str,string errorMsg,Exception innerException=null)
        {
            if (string.IsNullOrWhiteSpace(str)) throw new EnsureException(errorMsg, innerException);
        }

        public static void GrandThan(double maxValue,double minValue,string errorMsg,bool hasAllowEqual=true)
        {
            if (hasAllowEqual && maxValue < minValue) throw new EnsureException(errorMsg);
            if (!hasAllowEqual && maxValue <= minValue) throw new EnsureException(errorMsg);
        }

        public static void MustBeNull<T>(IList<T> list,string errorMsg)
        {
            if (list != null && list.Count != 0) throw new EnsureException(errorMsg);
        }

        public static void MustBeEqual(int left,int right,string errorMsg)
        {
            if (left != right) throw new EnsureException(errorMsg);
        }

        public static void MustBeEqual(string left, string right, string errorMsg)
        {
            if (!string.Equals(left , right)) throw new EnsureException(errorMsg);
        }

        public static void MustBeNoEqual(int left, int right, string errorMsg)
        {
            if (left == right) throw new EnsureException(errorMsg);
        }

        public static void MustBeTrue(bool value,string errorMsg)
        {
            if (!value) throw new EnsureException(errorMsg);
        }

        public static void MustBeFalse(bool value, string errorMsg)
        {
            if (value) throw new EnsureException(errorMsg);
        }

        public static void MustBeNum(string numStr,string errorMsg)
        {
            if (!Regex.IsMatch(numStr, @"^[0-9]*$")) throw new EnsureException(errorMsg);
        }

    }

    public class EnsureException:System.Exception
    {
        public EnsureException(string message,Exception innerException=null):base(message,innerException)
        { }
    }
}
