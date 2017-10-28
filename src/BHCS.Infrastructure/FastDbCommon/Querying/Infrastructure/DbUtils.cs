using BHCS.Infrastructure.FastDbCommon.Querying.Enum;
using BHCS.Infrastructure.FastDbCommon.Querying.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BHCS.Infrastructure.FastDbCommon.Querying.Infrastructure
{
    public static class DbUtils
    {
        public static int paramCount = 0;

        private static Dictionary<MemberInfo, Object> _micache1 = new Dictionary<MemberInfo, Object>();
        private static Dictionary<MemberInfo, Object> _micache2 = new Dictionary<MemberInfo, Object>();

        /// <summary>
        /// 格式化sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="leftToken"></param>
        /// <param name="rightToken"></param>
        /// <returns></returns>
        internal static string FormatSQL(string sql, char leftToken, char rightToken)
        {
            if (sql == null)
            {
                return string.Empty;
            }

            sql = sql.Replace("{0}", leftToken.ToString()).Replace("{1}", rightToken.ToString());

            return sql;
        }

        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object ConvertValue(Type type, object value)
        {
            if (DBNull.Value ==value  || (value == null))
            {
                return null;
            }
            if (CheckStruct(type))
            {
                string data = value.ToString();
                return SerializationManager.Deserialize(type, data);
            }
            Type type2 = value.GetType();
            if (type == type2)
            {
                return value;
            }
            if (((type == typeof(Guid)) || (type == typeof(Guid?))) && (type2 == typeof(string)))
            {
                if (string.IsNullOrEmpty(value.ToString()))
                {
                    return null;
                }
                return new Guid(value.ToString());
            }
            if (((type == typeof(DateTime)) || (type == typeof(DateTime?))) && (type2 == typeof(string)))
            {
                if (string.IsNullOrEmpty(value.ToString()))
                {
                    return null;
                }
                return Convert.ToDateTime(value);
            }
            if (type.GetTypeInfo().IsEnum)
            {
                try
                {
                    return System.Enum.Parse(type, value.ToString(), true);
                }
                catch
                {
                    return System.Enum.ToObject(type, value);
                }
            }
            if (((type == typeof(bool)) || (type == typeof(bool?))))
            {
                bool tempbool = false;
                if (bool.TryParse(value.ToString(), out tempbool))
                {
                    return tempbool;
                }
                else
                {
                    //处理  Request.Form  的 checkbox  如果没有返回值就是没有选中false  
                    if (string.IsNullOrEmpty(value.ToString()))
                        return false;
                    else
                    {
                        if (value.ToString() == "0")
                        {
                            return false;
                        }
                        return true;
                    }
                }

            }

            if (type.GetTypeInfo().IsGenericType)
            {
                type = type.GetTypeInfo().GetGenericArguments()[0];
            }

            return Convert.ChangeType(value, type);
        }

        /// <summary>
        /// 转换数据类型
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TResult ConvertValue<TResult>(object value)
        {
            if (DBNull.Value==value || value == null)
                return default(TResult);

            object obj = ConvertValue(typeof(TResult), value);
            if (obj == null)
            {
                return default(TResult);
            }
            return (TResult)obj;
        }

        public static int GetNewParamCount()
        {
            if (paramCount >= 9999)
            {
                paramCount = 0;
            }
            paramCount++;
            return paramCount;
        }

        /// <summary>
        /// 生成唯一字符串
        /// </summary>
        /// <returns></returns>
        public static string MakeUniqueKey(Field field)//string prefix,
        {
            //TODO 此处应该根据数据库类型来附加@、?、:
            //return string.Concat("@", field.tableName, "_", field.Name, "_", GetNewParamCount()).Replace(".","_");
            //如遇Oracle超过30字符Bug，把field.tableName去掉即可
            return string.Concat("@", field.Name, GetNewParamCount());
            //byte[] data = new byte[16];
            //new RNGCryptoServiceProvider().GetBytes(data);
            //string keystring = keyReg.Replace(Convert.ToBase64String(data).Trim(), string.Empty);

            //if (keystring.Length > 16)
            //    return keystring.Substring(0, 16).ToLower();

            //return keystring.ToLower();

            //return string.Concat(prefix, Guid.NewGuid().ToString().Replace("-", ""));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="op"></param>
        /// <returns></returns>
        public static string ToString(QueryOperator op)
        {
            switch (op)
            {
                case QueryOperator.Add:
                    return " + ";
                case QueryOperator.BitwiseAND:
                    return " & ";
                case QueryOperator.BitwiseNOT:
                    return " ~ ";
                case QueryOperator.BitwiseOR:
                    return " | ";
                case QueryOperator.BitwiseXOR:
                    return " ^ ";
                case QueryOperator.Divide:
                    return " / ";
                case QueryOperator.Equal:
                    return " = ";
                case QueryOperator.Greater:
                    return " > ";
                case QueryOperator.GreaterOrEqual:
                    return " >= ";
                case QueryOperator.IsNULL:
                    return " IS NULL ";
                case QueryOperator.IsNotNULL:
                    return " IS NOT NULL ";
                case QueryOperator.Less:
                    return " < ";
                case QueryOperator.LessOrEqual:
                    return " <= ";
                case QueryOperator.Like:
                    return " LIKE ";
                case QueryOperator.Modulo:
                    return " % ";
                case QueryOperator.Multiply:
                    return " * ";
                case QueryOperator.NotEqual:
                    return " <> ";
                case QueryOperator.Subtract:
                    return " - ";
            }

            throw new NotSupportedException("Unknown QueryOperator: " + op.ToString() + "!");
        }

        ///// <summary>
        ///// 获取自定义特性，带有缓存功能，避免因.Net内部GetCustomAttributes没有缓存而带来的损耗
        ///// </summary>
        ///// <typeparam name="TAttribute"></typeparam>
        ///// <param name="member"></param>
        ///// <param name="inherit"></param>
        ///// <returns></returns>
        //public static TAttribute[] GetCustomAttributes<TAttribute>(this MemberInfo member, Boolean inherit)
        //{
        //    if (member == null) return new TAttribute[0];

        //    // 根据是否可继承，分属两个缓存集合
        //    var cache = inherit ? _micache1 : _micache2;

        //    Object obj = null;
        //    if (cache.TryGetValue(member, out obj)) return (TAttribute[])obj;
        //    lock (cache)
        //    {
        //        if (cache.TryGetValue(member, out obj)) return (TAttribute[])obj;

        //        var atts = CustomAttributeExtensions.GetCustomAttributes(member,typeof(TAttribute), inherit) as TAttribute[];
        //        var att = atts == null ? new TAttribute[0] : atts;
        //        cache[member] = att;
        //        return att;
        //    }
        //}

        ///// <summary>获取自定义属性</summary>
        ///// <typeparam name="TAttribute"></typeparam>
        ///// <param name="member"></param>
        ///// <param name="inherit"></param>
        ///// <returns></returns>
        //public static TAttribute GetCustomAttribute<TAttribute>(this MemberInfo member, Boolean inherit)
        //{
        //    var atts = member.GetCustomAttributes<TAttribute>(inherit);
        //    if (atts == null || atts.Length < 1) return default(TAttribute);
        //    return atts[0];
        //}
        
        /// <summary>
        /// 快速实例化一个T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Create<T>()
        {
            return (T)Create(typeof(T))();
        }

        /// <summary>
        /// 快速实例化一个FastCreateInstanceHandler
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static FastCreateInstanceHandler Create(Type type)
        {
            return DynamicCalls.GetInstanceCreator(type);
        }

        /// <summary>
        /// 设置属性值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="property"></param>
        /// <param name="value"></param>
        public static void SetPropertyValue(object obj, PropertyInfo property, object value)
        {
            if (property.CanWrite)
            {
                FastPropertySetHandler propertySetter = DynamicCalls.GetPropertySetter(property);
                value = ConvertValue(property.PropertyType, value);
                propertySetter(obj, value);
            }
        }

        /// <summary>
        /// 设置属性值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public static void SetPropertyValue(object obj, string propertyName, object value)
        {
            SetPropertyValue(obj.GetType(), obj, propertyName, value);
        }

        /// <summary>
        /// 设置属性值
        /// </summary>
        /// <param name="type"></param>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public static void SetPropertyValue(Type type, object obj, string propertyName, object value)
        {
            PropertyInfo property = type.GetTypeInfo().GetProperty(propertyName);
            if (property != null)
            {
                SetPropertyValue(obj, property, value);
            }
        }

        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static object GetPropertyValue<TEntity>(TEntity entity, string propertyName)
        {
            PropertyInfo property = entity.GetType().GetTypeInfo().GetProperty(propertyName);
            if (property != null)
            {
                return property.GetValue(entity, null);
            }

            return null;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("此方法仅供内部使用", false)]
        public static char ReadChar(object value)
        {
            if (value == null || value is DBNull) throw new ArgumentNullException("value");
            string s = value as string;
            if (s == null || s.Length != 1) throw new ArgumentException("A single-character was expected", "value");
            return s[0];
        }

        /// <summary>
        /// Internal use only
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("This method is for internal usage only", false)]
        public static char? ReadNullableChar(object value)
        {
            if (value == null || value is DBNull) return null;
            string s = value as string;
            if (s == null || s.Length != 1) throw new ArgumentException("A single-character was expected", "value");
            return s[0];
        }

        public static void ThrowDataException(Exception ex, int index, IDataReader reader)
        {
            string name = "(n/a)", value = "(n/a)";
            if (reader != null && index >= 0 && index < reader.FieldCount)
            {
                name = reader.GetName(index);
                object val = reader.GetValue(index);
                if (val == null || val is DBNull)
                {
                    value = "<null>";
                }
                else
                {
                    value = Convert.ToString(val) + " - " + Type.GetTypeCode(val.GetType());
                }
            }
            throw new DataException(string.Format("Error parsing column {0} ({1}={2})", index, name, value), ex);
        }

        /// <summary>
        /// CheckStuct
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static bool CheckStruct(Type type)
        {
            return ((type.GetTypeInfo().IsValueType && !type.GetTypeInfo().IsEnum) && (!type.GetTypeInfo().IsPrimitive && !type.GetTypeInfo().IsSerializable));
        }

        internal static class DBConvert
        {
            public static bool IsDBNull(object value)
            {
                return object.Equals(DBNull.Value, value);
            }
            public static short ToInt16(object value)
            {
                if (value is short)
                {
                    return (short)value;
                }
                try
                {
                    return Convert.ToInt16(value);
                }
                catch
                {
                    return 0;
                }
            }
            public static ushort ToUInt16(object value)
            {
                if (value is ushort)
                {
                    return (ushort)value;
                }
                try
                {
                    return Convert.ToUInt16(value);
                }
                catch
                {
                    return 0;
                }
            }
            public static int ToInt32(object value)
            {
                if (value is int)
                {
                    return (int)value;
                }
                try
                {
                    return Convert.ToInt32(value);
                }
                catch
                {
                    return 0;
                }
            }
            public static uint ToUInt32(object value)
            {
                if (value is uint)
                {
                    return (uint)value;
                }
                try
                {
                    return Convert.ToUInt32(value);
                }
                catch
                {
                    return 0;
                }
            }
            public static long ToInt64(object value)
            {
                if (value is long)
                {
                    return (long)value;
                }
                try
                {
                    return Convert.ToInt64(value);
                }
                catch
                {
                    return 0;
                }
            }
            public static ulong ToUInt64(object value)
            {
                if (value is long)
                {
                    return (ulong)value;
                }
                try
                {
                    return Convert.ToUInt64(value);
                }
                catch
                {
                    return 0;
                }
            }
            public static bool ToBoolean(object value)
            {
                if (value == null)
                {
                    return false;
                }
                if (value is bool)
                {
                    return (bool)value;
                }
                if (value.Equals("1") || value.Equals("-1"))
                {
                    value = "true";
                }
                else if (value.Equals("0"))
                {
                    value = "false";
                }

                try
                {
                    return Convert.ToBoolean(value);
                }
                catch
                {
                    return false;
                }
            }
            public static DateTime ToDateTime(object value)
            {
                if (value is DateTime)
                {
                    return (DateTime)value;
                }
                try
                {
                    return Convert.ToDateTime(value);
                }
                catch
                {
                    return DateTime.MinValue;
                }
            }
            public static decimal ToDecimal(object value)
            {
                if (value is decimal)
                {
                    return (decimal)value;
                }
                try
                {
                    return Convert.ToDecimal(value);
                }
                catch
                {
                    return 0;
                }
            }
            public static double ToDouble(object value)
            {
                if (value is double)
                {
                    return (double)value;
                }
                try
                {
                    return Convert.ToDouble(value);
                }
                catch
                {
                    return 0;
                }
            }
            //2015-09-22
            public static float ToFloat(object value)
            {
                if (value is Single || value is float)
                {
                    return (float)value;
                }
                try
                {
                    return Convert.ToSingle(value);
                }
                catch
                {
                    return 0;
                }
            }
            public static Guid ToGuid(object value)
            {
                if (value is Guid)
                {
                    return (Guid)value;
                }
                try
                {
                    return Guid.Parse(value.ToString());
                }
                catch
                {
                    return new Guid();
                }
            }
            public static byte[] ToByteArr(object value)
            {
                var arr = value as byte[];
                return arr;
            }

            public static Nullable<short> ToNInt16(object value)
            {
                if (value is short)
                {
                    return new Nullable<short>((short)value);
                }
                try
                {
                    return new Nullable<short>(Convert.ToInt16(value));
                }
                catch
                {
                    return new Nullable<short>();
                }
            }
            public static Nullable<ushort> ToNUInt16(object value)
            {
                if (value is ushort)
                {
                    return new Nullable<ushort>((ushort)value);
                }
                try
                {
                    return new Nullable<ushort>(Convert.ToUInt16(value));
                }
                catch
                {
                    return new Nullable<ushort>();
                }
            }
            public static Nullable<int> ToNInt32(object value)
            {
                if (value is int)
                {
                    return new Nullable<int>((int)value);
                }
                try
                {
                    return new Nullable<int>(Convert.ToInt32(value));
                }
                catch
                {
                    return new Nullable<int>();
                }
            }
            public static Nullable<uint> ToNUInt32(object value)
            {
                if (value is uint)
                {
                    return new Nullable<uint>((uint)value);
                }
                try
                {
                    return new Nullable<uint>(Convert.ToUInt32(value));
                }
                catch
                {
                    return new Nullable<uint>();
                }
            }
            public static Nullable<long> ToNInt64(object value)
            {
                if (value is long)
                {
                    return new Nullable<long>((long)value);
                }
                try
                {
                    return new Nullable<long>(Convert.ToInt64(value));
                }
                catch
                {
                    return new Nullable<long>();
                }
            }
            public static Nullable<ulong> ToNUInt64(object value)
            {
                if (value is long)
                {
                    return new Nullable<ulong>((ulong)value);
                }
                try
                {
                    return new Nullable<ulong>(Convert.ToUInt64(value));
                }
                catch
                {
                    return new Nullable<ulong>();
                }
            }
            public static Nullable<bool> ToNBoolean(object value)
            {
                if (value is bool)
                {
                    return new Nullable<bool>((bool)value);
                }
                try
                {
                    return new Nullable<bool>(Convert.ToBoolean(value));
                }
                catch
                {
                    return new Nullable<bool>();
                }
            }
            public static Nullable<DateTime> ToNDateTime(object value)
            {
                if (value is DateTime)
                {
                    return new Nullable<DateTime>((DateTime)value);
                }
                try
                {
                    return new Nullable<DateTime>(Convert.ToDateTime(value));
                }
                catch
                {
                    return new Nullable<DateTime>();
                }
            }
            public static Nullable<decimal> ToNDecimal(object value)
            {
                if (value is decimal)
                {
                    return new Nullable<decimal>((decimal)value);
                }
                try
                {
                    return new Nullable<decimal>(Convert.ToDecimal(value));
                }
                catch
                {
                    return new Nullable<decimal>();
                }
            }
            public static Nullable<double> ToNDouble(object value)
            {
                if (value is double)
                {
                    return new Nullable<double>((double)value);
                }
                try
                {
                    return new Nullable<double>(Convert.ToDouble(value));
                }
                catch
                {
                    return new Nullable<double>();
                }
            }
            public static Nullable<float> ToNFloat(object value)
            {
                if (value is Single || value is float)
                {
                    return new Nullable<float>((float)value);
                }
                try
                {
                    return new Nullable<float>(Convert.ToSingle(value));
                }
                catch
                {
                    return new Nullable<float>();
                }
            }
            public static Nullable<Guid> ToNGuid(object value)
            {
                if (value is Guid)
                {
                    return new Nullable<Guid>((Guid)value);
                }
                try
                {
                    return new Nullable<Guid>(Guid.Parse(value.ToString()));
                }
                catch
                {
                    return new Nullable<Guid>();
                }
            }
        }
    }
}
