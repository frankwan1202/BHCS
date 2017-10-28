using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Reflection;
using System.Data;
using System.Threading;
using BHCS.Infrastructure.FastDbCommon.Querying.Model;

namespace BHCS.Infrastructure.FastDbCommon.Querying.Infrastructure
{

    /// <summary>
    /// 实体帮助类
    /// </summary>
    public class EntityUtils
    {
        /// <summary>
        /// 根据字段名返回实体字段
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="fieldName"></param>
        /// <returns>返回null 就表示字段不存在</returns>
        public static Field GetField<TEntity>(string fieldName)
             where TEntity : class
        {
            Field[] fields = EntityCache.GetFields<TEntity>();

            foreach (Field field in fields)
            {
                if (string.Compare(fieldName, field.PropertyName, true) == 0)
                    return field;
            }

            return null;
        }
        
        /// <summary>
        /// 实体赋值
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="toEntity">被赋值实体</param>
        /// <param name="fromEntity">来源实体</param>
        /// <returns>返回null 就表示fromEntity==null</returns>
        public static void SetValue<TEntity>(TEntity toEntity, TEntity fromEntity)
             where TEntity : class
        {
            if (null == fromEntity)
                toEntity = null;
            else
            {

                if (null == toEntity)
                    toEntity = DbUtils.Create<TEntity>();

                //toEntity.Attach();

                PropertyInfo[] pis = toEntity.GetType().GetTypeInfo().GetProperties();
                foreach (PropertyInfo pi in pis)
                {
                    DbUtils.SetPropertyValue(toEntity, pi, DbUtils.GetPropertyValue<TEntity>(fromEntity, pi.Name));
                }

            }

        }

        private static readonly MethodInfo Object_ToString = typeof(object).GetTypeInfo().GetMethod("ToString");
        private static readonly MethodInfo Reader_Read = typeof(IDataReader).GetTypeInfo().GetMethod("Read");
        private static readonly MethodInfo Reader_GetValues = typeof(IDataRecord).GetTypeInfo().GetMethod("GetValues", new Type[] { typeof(object[]) });
        private static readonly MethodInfo Convert_IsDBNull = typeof(DbUtils.DBConvert).GetTypeInfo().GetMethod("IsDBNull", new Type[] { typeof(object) });

        private static readonly MethodInfo Convert_ToInt16 = typeof(DbUtils.DBConvert).GetTypeInfo().GetMethod("ToInt16", new Type[] { typeof(object) });
        private static readonly MethodInfo Convert_ToUInt16 = typeof(DbUtils.DBConvert).GetTypeInfo().GetMethod("ToUInt16", new Type[] { typeof(object) });
        private static readonly MethodInfo Convert_ToInt32 = typeof(DbUtils.DBConvert).GetTypeInfo().GetMethod("ToInt32", new Type[] { typeof(object) });
        private static readonly MethodInfo Convert_ToUInt32 = typeof(DbUtils.DBConvert).GetTypeInfo().GetMethod("ToUInt32", new Type[] { typeof(object) });
        private static readonly MethodInfo Convert_ToInt64 = typeof(DbUtils.DBConvert).GetTypeInfo().GetMethod("ToInt64", new Type[] { typeof(object) });
        private static readonly MethodInfo Convert_ToUInt64 = typeof(DbUtils.DBConvert).GetTypeInfo().GetMethod("ToUInt64", new Type[] { typeof(object) });
        private static readonly MethodInfo Convert_ToBoolean = typeof(DbUtils.DBConvert).GetTypeInfo().GetMethod("ToBoolean", new Type[] { typeof(object) });
        private static readonly MethodInfo Convert_ToDateTime = typeof(DbUtils.DBConvert).GetTypeInfo().GetMethod("ToDateTime", new Type[] { typeof(object) });
        private static readonly MethodInfo Convert_ToDecimal = typeof(DbUtils.DBConvert).GetTypeInfo().GetMethod("ToDecimal", new Type[] { typeof(object) });
        private static readonly MethodInfo Convert_ToDouble = typeof(DbUtils.DBConvert).GetTypeInfo().GetMethod("ToDouble", new Type[] { typeof(object) });
        private static readonly MethodInfo Convert_ToFloat = typeof(DbUtils.DBConvert).GetTypeInfo().GetMethod("ToFloat", new Type[] { typeof(object) });
        private static readonly MethodInfo Convert_ToGuid = typeof(DbUtils.DBConvert).GetTypeInfo().GetMethod("ToGuid", new Type[] { typeof(object) });
        private static readonly MethodInfo Convert_ToByteArr = typeof(DbUtils.DBConvert).GetTypeInfo().GetMethod("ToByteArr", new Type[] { typeof(object) });

        private static readonly MethodInfo Convert_ToNullInt16 = typeof(DbUtils.DBConvert).GetTypeInfo().GetMethod("ToNInt16", new Type[] { typeof(object) });
        private static readonly MethodInfo Convert_ToNullUInt16 = typeof(DbUtils.DBConvert).GetTypeInfo().GetMethod("ToNUInt16", new Type[] { typeof(object) });
        private static readonly MethodInfo Convert_ToNullInt32 = typeof(DbUtils.DBConvert).GetTypeInfo().GetMethod("ToNInt32", new Type[] { typeof(object) });
        private static readonly MethodInfo Convert_ToNullUInt32 = typeof(DbUtils.DBConvert).GetTypeInfo().GetMethod("ToNUInt32", new Type[] { typeof(object) });
        private static readonly MethodInfo Convert_ToNullInt64 = typeof(DbUtils.DBConvert).GetTypeInfo().GetMethod("ToNInt64", new Type[] { typeof(object) });
        private static readonly MethodInfo Convert_ToNullUInt64 = typeof(DbUtils.DBConvert).GetTypeInfo().GetMethod("ToNUInt64", new Type[] { typeof(object) });
        private static readonly MethodInfo Convert_ToNullBoolean = typeof(DbUtils.DBConvert).GetTypeInfo().GetMethod("ToNBoolean", new Type[] { typeof(object) });
        private static readonly MethodInfo Convert_ToNullDateTime = typeof(DbUtils.DBConvert).GetTypeInfo().GetMethod("ToNDateTime", new Type[] { typeof(object) });
        private static readonly MethodInfo Convert_ToNullDecimal = typeof(DbUtils.DBConvert).GetTypeInfo().GetMethod("ToNDecimal", new Type[] { typeof(object) });
        private static readonly MethodInfo Convert_ToNullDouble = typeof(DbUtils.DBConvert).GetTypeInfo().GetMethod("ToNDouble", new Type[] { typeof(object) });
        private static readonly MethodInfo Convert_ToNullFloat = typeof(DbUtils.DBConvert).GetTypeInfo().GetMethod("ToNFloat", new Type[] { typeof(object) });
        private static readonly MethodInfo Convert_ToNullGuid = typeof(DbUtils.DBConvert).GetTypeInfo().GetMethod("ToNGuid", new Type[] { typeof(object) });
        private delegate T ReadEntityInvoker<T>(IDataReader dr);
        private static Dictionary<string,  DynamicMethod> m_CatchMethod;
        private static void ConvertValue(ILGenerator ilg, Type pi)//PropertyInfo pi
        {
            TypeCode code = Type.GetTypeCode(pi);
            switch (code)
            {
                case TypeCode.Int16:
                    ilg.Emit(OpCodes.Call, Convert_ToInt16);
                    return;
                case TypeCode.UInt16:
                    ilg.Emit(OpCodes.Call, Convert_ToUInt16);
                    return;
                case TypeCode.Int32:
                    ilg.Emit(OpCodes.Call, Convert_ToInt32);
                    return;
                case TypeCode.UInt32:
                    ilg.Emit(OpCodes.Call, Convert_ToUInt32);
                    return;
                case TypeCode.Int64:
                    ilg.Emit(OpCodes.Call, Convert_ToInt64);
                    return;
                case TypeCode.UInt64:
                    ilg.Emit(OpCodes.Call, Convert_ToUInt64);
                    return;
                case TypeCode.Boolean:
                    ilg.Emit(OpCodes.Call, Convert_ToBoolean);
                    return;
                case TypeCode.String:
                    ilg.Emit(OpCodes.Callvirt, Object_ToString);
                    return;
                case TypeCode.DateTime:
                    ilg.Emit(OpCodes.Call, Convert_ToDateTime);
                    return;
                case TypeCode.Decimal:
                    ilg.Emit(OpCodes.Call, Convert_ToDecimal);
                    return;
                case TypeCode.Double:
                    ilg.Emit(OpCodes.Call, Convert_ToDouble);
                    return;
                case TypeCode.Single:
                    ilg.Emit(OpCodes.Call, Convert_ToFloat);
                    return;
            }
            Type type = Nullable.GetUnderlyingType(pi);
            if (type != null)
            {
                code = Type.GetTypeCode(type);
                switch (code)
                {
                    case TypeCode.Int16:
                        ilg.Emit(OpCodes.Call, Convert_ToNullInt16);
                        return;
                    case TypeCode.UInt16:
                        ilg.Emit(OpCodes.Call, Convert_ToNullUInt16);
                        return;
                    case TypeCode.Int32:
                        ilg.Emit(OpCodes.Call, Convert_ToNullInt32);
                        return;
                    case TypeCode.UInt32:
                        ilg.Emit(OpCodes.Call, Convert_ToNullUInt32);
                        return;
                    case TypeCode.Int64:
                        ilg.Emit(OpCodes.Call, Convert_ToNullInt64);
                        return;
                    case TypeCode.UInt64:
                        ilg.Emit(OpCodes.Call, Convert_ToNullUInt64);
                        return;
                    case TypeCode.Boolean:
                        ilg.Emit(OpCodes.Call, Convert_ToNullBoolean);
                        return;
                    case TypeCode.DateTime:
                        ilg.Emit(OpCodes.Call, Convert_ToNullDateTime);
                        return;
                    case TypeCode.Decimal:
                        ilg.Emit(OpCodes.Call, Convert_ToNullDecimal);
                        return;
                    case TypeCode.Double:
                        ilg.Emit(OpCodes.Call, Convert_ToNullDouble);
                        return;
                    case TypeCode.Single:
                        ilg.Emit(OpCodes.Call, Convert_ToNullFloat);
                        return;
                }
                if (type.Name == "Guid")
                {
                    ilg.Emit(OpCodes.Call, Convert_ToNullGuid);
                    return;
                }
            }
            if (pi.Name == "Guid")
            {
                ilg.Emit(OpCodes.Call, Convert_ToGuid);
                return;
            }
            if (pi.Name == "Byte[]")
            {
                ilg.Emit(OpCodes.Call, Convert_ToByteArr);
                return;
            }
            throw new Exception(string.Format("不支持\"{0}\"类型的转换！", pi.Name));
        }
        static readonly Dictionary<Type, DbType> typeMap;
        static EntityUtils()
        {
            typeMap = new Dictionary<Type, DbType>();
            typeMap[typeof(byte)] = DbType.Byte;
            typeMap[typeof(sbyte)] = DbType.SByte;
            typeMap[typeof(short)] = DbType.Int16;
            typeMap[typeof(ushort)] = DbType.UInt16;
            typeMap[typeof(int)] = DbType.Int32;
            typeMap[typeof(uint)] = DbType.UInt32;
            typeMap[typeof(long)] = DbType.Int64;
            typeMap[typeof(ulong)] = DbType.UInt64;
            typeMap[typeof(float)] = DbType.Single;
            typeMap[typeof(double)] = DbType.Double;
            typeMap[typeof(decimal)] = DbType.Decimal;
            typeMap[typeof(bool)] = DbType.Boolean;
            typeMap[typeof(string)] = DbType.String;
            typeMap[typeof(char)] = DbType.StringFixedLength;
            typeMap[typeof(Guid)] = DbType.Guid;
            typeMap[typeof(DateTime)] = DbType.DateTime;
            typeMap[typeof(DateTimeOffset)] = DbType.DateTimeOffset;
            typeMap[typeof(byte[])] = DbType.Binary;
            typeMap[typeof(byte?)] = DbType.Byte;
            typeMap[typeof(sbyte?)] = DbType.SByte;
            typeMap[typeof(short?)] = DbType.Int16;
            typeMap[typeof(ushort?)] = DbType.UInt16;
            typeMap[typeof(int?)] = DbType.Int32;
            typeMap[typeof(uint?)] = DbType.UInt32;
            typeMap[typeof(long?)] = DbType.Int64;
            typeMap[typeof(ulong?)] = DbType.UInt64;
            typeMap[typeof(float?)] = DbType.Single;
            typeMap[typeof(double?)] = DbType.Double;
            typeMap[typeof(decimal?)] = DbType.Decimal;
            typeMap[typeof(bool?)] = DbType.Boolean;
            typeMap[typeof(char?)] = DbType.StringFixedLength;
            typeMap[typeof(Guid?)] = DbType.Guid;
            typeMap[typeof(DateTime?)] = DbType.DateTime;
            typeMap[typeof(DateTimeOffset?)] = DbType.DateTimeOffset;

            FastExpandoDescriptionProvider provider = new FastExpandoDescriptionProvider();
            TypeDescriptor.AddProvider(provider, typeof(FastExpando));
        }
        private const string LinqBinary = "System.Data.Linq.Binary";
        public static Func<IDataReader, object> GetDeserializer(Type type, IDataReader reader, int startBound, int length, bool returnNullIfFirstMissing)
        {
            if (type == typeof(object)
                || type == typeof(FastExpando))
            {
                return GetDynamicDeserializer(reader, startBound, length, returnNullIfFirstMissing);
            }

            if (!(typeMap.ContainsKey(type) || type.FullName == LinqBinary))
            {
                return GetTypeDeserializer(type, reader, startBound, length, returnNullIfFirstMissing);
            }
            return GetStructDeserializer(type, startBound);
        }
        private static Func<IDataReader, object> GetStructDeserializer(Type type, int index)
        {
            if (type == typeof(char))
            {
                return r => DbUtils.ReadChar(r.GetValue(index));
            }
            if (type == typeof(char?))
            {
                return r => DbUtils.ReadNullableChar(r.GetValue(index));
            }
            if (type.FullName == LinqBinary)
            {
                return r => Activator.CreateInstance(type, r.GetValue(index));
            }
            if (type == typeof(bool))
            {
                return r =>
                {
                    var val = r.GetValue(index);
                    return val == DBNull.Value ? false : (val.GetType() == type ? val : Convert.ToBoolean(val));
                };
            }
            if (type == typeof(bool?))
            {
                return r =>
                {
                    var val = r.GetValue(index);
                    return val == DBNull.Value ? null : (val.GetType() == type ? val : Convert.ToBoolean(val));
                };
            }
            return r =>
            {
                var val = r.GetValue(index);
                return val is DBNull ? null : val;
            };
        }
        class PropInfo
        {
            public string Name { get; set; }
            public MethodInfo Setter { get; set; }
            public Type Type { get; set; }
        }
        static List<PropInfo> GetSettableProps(Type t)
        {
            return t.GetTypeInfo()
                  .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                  .Select(p => new PropInfo
                  {
                      Name = p.Name,
                      Setter = p.DeclaringType == t ? p.GetSetMethod(true) : p.DeclaringType.GetTypeInfo().GetProperty(p.Name).GetSetMethod(true),
                      Type = p.PropertyType
                  })
                  .Where(info => info.Setter != null)
                  .ToList();
        }
        static List<FieldInfo> GetSettableFields(Type t)
        {
            return t.GetTypeInfo().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).ToList();
        }
        private static void EmitInt32(ILGenerator il, int value)
        {
            switch (value)
            {
                case -1: il.Emit(OpCodes.Ldc_I4_M1); break;
                case 0: il.Emit(OpCodes.Ldc_I4_0); break;
                case 1: il.Emit(OpCodes.Ldc_I4_1); break;
                case 2: il.Emit(OpCodes.Ldc_I4_2); break;
                case 3: il.Emit(OpCodes.Ldc_I4_3); break;
                case 4: il.Emit(OpCodes.Ldc_I4_4); break;
                case 5: il.Emit(OpCodes.Ldc_I4_5); break;
                case 6: il.Emit(OpCodes.Ldc_I4_6); break;
                case 7: il.Emit(OpCodes.Ldc_I4_7); break;
                case 8: il.Emit(OpCodes.Ldc_I4_8); break;
                default:
                    if (value >= -128 && value <= 127)
                    {
                        il.Emit(OpCodes.Ldc_I4_S, (sbyte)value);
                    }
                    else
                    {
                        il.Emit(OpCodes.Ldc_I4, value);
                    }
                    break;
            }
        }

        static readonly MethodInfo
                enumParse = typeof(System.Enum).GetTypeInfo().GetMethod("Parse", new Type[] { typeof(Type), typeof(string), typeof(bool) }),
                getItem = typeof(IDataRecord).GetTypeInfo().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .Where(p => p.GetIndexParameters().Any() && p.GetIndexParameters()[0].ParameterType == typeof(int))
                    .Select(p => p.GetGetMethod()).First();

        public static Func<IDataReader, object> GetTypeDeserializer(Type type, IDataReader reader, int startBound = 0, int length = -1, bool returnNullIfFirstMissing = false)
        {
            string name = string.Format("Deserialize{0}", Guid.NewGuid());
            var dm = new DynamicMethod(name, typeof(object), new[] { typeof(IDataReader) }, true);

            var il = dm.GetILGenerator();
            il.DeclareLocal(typeof(int));
            il.DeclareLocal(type);
            bool haveEnumLocal = false;
            il.Emit(OpCodes.Ldc_I4_0);
            il.Emit(OpCodes.Stloc_0);
            var properties = GetSettableProps(type);
            var fields = GetSettableFields(type);
            if (length == -1)
            {
                length = reader.FieldCount - startBound;
            }

            if (reader.FieldCount <= startBound)
            {
                throw new ArgumentException("reader.FieldCount <= startBound", "splitOn");
            }

            var names = new List<string>();

            for (int i = startBound; i < startBound + length; i++)
            {
                names.Add(reader.GetName(i));
            }

            var setters = (
                            from n in names
                            let prop = properties.FirstOrDefault(p => string.Equals(p.Name, n, StringComparison.Ordinal))
                                  ?? properties.FirstOrDefault(p => string.Equals(p.Name, n, StringComparison.OrdinalIgnoreCase))
                            let field = prop != null ? null : (fields.FirstOrDefault(p => string.Equals(p.Name, n, StringComparison.Ordinal))
                                ?? fields.FirstOrDefault(p => string.Equals(p.Name, n, StringComparison.OrdinalIgnoreCase)))
                            select new { Name = n, Property = prop, Field = field }
                          ).ToList();

            int index = startBound;

            if (type.GetTypeInfo().IsValueType)
            {
                il.Emit(OpCodes.Ldloca_S, (byte)1);
                il.Emit(OpCodes.Initobj, type);
            }
            else
            {
                il.Emit(OpCodes.Newobj, type.GetTypeInfo().GetConstructor(Type.EmptyTypes));
                il.Emit(OpCodes.Stloc_1);
            }
            il.BeginExceptionBlock();
            if (type.GetTypeInfo().IsValueType)
            {
                il.Emit(OpCodes.Ldloca_S, (byte)1);
            }
            else
            {
                il.Emit(OpCodes.Ldloc_1);
            }
            bool first = true;
            var allDone = il.DefineLabel();
            foreach (var item in setters)
            {
                if (item.Property != null || item.Field != null)
                {
                    il.Emit(OpCodes.Dup);
                    Label isDbNullLabel = il.DefineLabel();
                    Label finishLabel = il.DefineLabel();

                    il.Emit(OpCodes.Ldarg_0);
                    EmitInt32(il, index);
                    il.Emit(OpCodes.Dup);
                    il.Emit(OpCodes.Stloc_0);
                    il.Emit(OpCodes.Callvirt, getItem);

                    Type memberType = item.Property != null ? item.Property.Type : item.Field.FieldType;

                    if (memberType == typeof(char) || memberType == typeof(char?))
                    {
                        il.EmitCall(OpCodes.Call, typeof(DbUtils).GetTypeInfo().GetMethod(
                            memberType == typeof(char) ? "ReadChar" : "ReadNullableChar", BindingFlags.Static | BindingFlags.Public), null);
                    }
                    //else if (memberType == typeof(bool) || memberType == typeof(bool?))
                    //{
                    //    il.EmitCall(OpCodes.Call, typeof(SqlMapper).GetMethod(
                    //        memberType == typeof(bool) ? "ReadBoolean" : "ReadNullableBoolean", BindingFlags.Static | BindingFlags.Public), null);
                    //}
                    else
                    {
                        il.Emit(OpCodes.Dup);
                        il.Emit(OpCodes.Isinst, typeof(DBNull));
                        il.Emit(OpCodes.Brtrue_S, isDbNullLabel);
                        var nullUnderlyingType = Nullable.GetUnderlyingType(memberType);
                        var unboxType = nullUnderlyingType != null && nullUnderlyingType.GetTypeInfo().IsEnum ? nullUnderlyingType : memberType;
                        if (unboxType.GetTypeInfo().IsEnum)
                        {
                            if (!haveEnumLocal)
                            {
                                il.DeclareLocal(typeof(string));
                                haveEnumLocal = true;
                            }

                            Label isNotString = il.DefineLabel();
                            il.Emit(OpCodes.Dup);
                            il.Emit(OpCodes.Isinst, typeof(string));
                            il.Emit(OpCodes.Dup);
                            il.Emit(OpCodes.Stloc_2);
                            il.Emit(OpCodes.Brfalse_S, isNotString);
                            il.Emit(OpCodes.Pop);
                            il.Emit(OpCodes.Ldtoken, unboxType);
                            il.EmitCall(OpCodes.Call, typeof(Type).GetTypeInfo().GetMethod("GetTypeFromHandle"), null);
                            il.Emit(OpCodes.Ldloc_2);
                            il.Emit(OpCodes.Ldc_I4_1);
                            il.EmitCall(OpCodes.Call, enumParse, null);
                            il.Emit(OpCodes.Unbox_Any, unboxType);
                            if (nullUnderlyingType != null)
                            {
                                il.Emit(OpCodes.Newobj, memberType.GetTypeInfo().GetConstructor(new[] { nullUnderlyingType }));
                            }
                            if (item.Property != null)
                            {
                                il.Emit(OpCodes.Callvirt, item.Property.Setter);
                            }
                            else
                            {
                                il.Emit(OpCodes.Stfld, item.Field);
                            }
                            il.Emit(OpCodes.Br_S, finishLabel);
                            il.MarkLabel(isNotString);
                        }
                        if (memberType.FullName == LinqBinary)
                        {
                            il.Emit(OpCodes.Unbox_Any, typeof(byte[]));
                            il.Emit(OpCodes.Newobj, memberType.GetTypeInfo().GetConstructor(new Type[] { typeof(byte[]) }));
                        }
                        else if (memberType == typeof(bool) || memberType == typeof(bool?))
                        {
                            il.EmitCall(OpCodes.Call, typeof(Convert).GetTypeInfo().GetMethod("ToBoolean", new Type[] { typeof(object) }), null);
                        }
                        else
                        {
                            ConvertValue(il, properties.First(d => String.Equals(d.Name, item.Name, StringComparison.CurrentCultureIgnoreCase)).Type);
                        }
                        if (nullUnderlyingType != null && (nullUnderlyingType.GetTypeInfo().IsEnum || nullUnderlyingType == typeof(bool)))
                        {
                            il.Emit(OpCodes.Newobj, memberType.GetTypeInfo().GetConstructor(new[] { nullUnderlyingType }));
                        }
                    }
                    if (item.Property != null)
                    {
                        il.Emit(type.GetTypeInfo().IsValueType ? OpCodes.Call : OpCodes.Callvirt, item.Property.Setter);
                    }
                    else
                    {
                        il.Emit(OpCodes.Stfld, item.Field);
                    }
                    il.Emit(OpCodes.Br_S, finishLabel);
                    il.MarkLabel(isDbNullLabel);
                    il.Emit(OpCodes.Pop);
                    il.Emit(OpCodes.Pop);

                    if (first && returnNullIfFirstMissing)
                    {
                        il.Emit(OpCodes.Pop);
                        il.Emit(OpCodes.Ldnull);
                        il.Emit(OpCodes.Stloc_1);
                        il.Emit(OpCodes.Br, allDone);
                    }
                    il.MarkLabel(finishLabel);
                    first = false;
                }
                index += 1;
            }
            il.Emit(type.GetTypeInfo().IsValueType ? OpCodes.Pop : OpCodes.Stloc_1);
            il.MarkLabel(allDone);
            il.BeginCatchBlock(typeof(Exception));
            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Ldarg_0);
            il.EmitCall(OpCodes.Call, typeof(DbUtils).GetTypeInfo().GetMethod("ThrowDataException"), null);
            il.EndExceptionBlock();
            il.Emit(OpCodes.Ldloc_1);
            if (type.GetTypeInfo().IsValueType)
            {
                il.Emit(OpCodes.Box, type);
            }
            il.Emit(OpCodes.Ret);
            return (Func<IDataReader, object>)dm.CreateDelegate(typeof(Func<IDataReader, object>));
        }
        static MethodInfo GetOperator(Type from, Type to)
        {
            if (to == null) return null;
            MethodInfo[] fromMethods, toMethods;
            return ResolveOperator(fromMethods = from.GetTypeInfo().GetMethods(BindingFlags.Static | BindingFlags.Public), from, to, "op_Implicit")
                ?? ResolveOperator(toMethods = to.GetTypeInfo().GetMethods(BindingFlags.Static | BindingFlags.Public), from, to, "op_Implicit")
                ?? ResolveOperator(fromMethods, from, to, "op_Explicit")
                ?? ResolveOperator(toMethods, from, to, "op_Explicit");

        }
        static MethodInfo ResolveOperator(MethodInfo[] methods, Type from, Type to, string name)
        {
            for (int i = 0; i < methods.Length; i++)
            {
                if (methods[i].Name != name || methods[i].ReturnType != to) continue;
                var args = methods[i].GetParameters();
                if (args.Length != 1 || args[0].ParameterType != from) continue;
                return methods[i];
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        public class FastExpando : System.Dynamic.DynamicObject, IDictionary<string, object>
        {
            IDictionary<string, object> data;
            public IDictionary<string, object> Data
            {
                get { return data; }
                set { data = value; }
            }
            public static FastExpando Attach(IDictionary<string, object> data)
            {
                return new FastExpando { data = data };
            }
            public override bool TrySetMember(System.Dynamic.SetMemberBinder binder, object value)
            {
                data[binder.Name] = value;
                return true;
            }
            public override bool TryGetMember(System.Dynamic.GetMemberBinder binder, out object result)
            {
                return data.TryGetValue(binder.Name, out result);
            }
            public override IEnumerable<string> GetDynamicMemberNames()
            {
                return data.Keys;
            }
            void IDictionary<string, object>.Add(string key, object value)
            {
                throw new NotImplementedException();
            }
            bool IDictionary<string, object>.ContainsKey(string key)
            {
                return data.ContainsKey(key);
            }
            ICollection<string> IDictionary<string, object>.Keys
            {
                get { return data.Keys; }
            }
            bool IDictionary<string, object>.Remove(string key)
            {
                throw new NotImplementedException();
            }
            bool IDictionary<string, object>.TryGetValue(string key, out object value)
            {
                return data.TryGetValue(key, out value);
            }
            ICollection<object> IDictionary<string, object>.Values
            {
                get { return data.Values; }
            }
            object IDictionary<string, object>.this[string key]
            {
                get
                {
                    return data[key];
                }
                set
                {
                    if (!data.ContainsKey(key))
                    {
                        throw new NotImplementedException();
                    }
                    data[key] = value;
                }
            }
            void ICollection<KeyValuePair<string, object>>.Add(KeyValuePair<string, object> item)
            {
                throw new NotImplementedException();
            }
            void ICollection<KeyValuePair<string, object>>.Clear()
            {
                throw new NotImplementedException();
            }

            bool ICollection<KeyValuePair<string, object>>.Contains(KeyValuePair<string, object> item)
            {
                return data.Contains(item);
            }

            void ICollection<KeyValuePair<string, object>>.CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
            {
                data.CopyTo(array, arrayIndex);
            }
            int ICollection<KeyValuePair<string, object>>.Count
            {
                get { return data.Count; }
            }
            bool ICollection<KeyValuePair<string, object>>.IsReadOnly
            {
                get { return true; }
            }
            bool ICollection<KeyValuePair<string, object>>.Remove(KeyValuePair<string, object> item)
            {
                throw new NotImplementedException();
            }
            IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
            {
                return data.GetEnumerator();
            }
            IEnumerator IEnumerable.GetEnumerator()
            {
                return data.GetEnumerator();
            }
        }

        class FastExpandoDescriptionProvider : TypeDescriptionProvider
        {
            public FastExpandoDescriptionProvider() : base() { }

            public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
            {
                return new FastExpandoCustomTypeDescriptor(objectType, instance);
            }
        }

        class FastExpandoCustomTypeDescriptor : CustomTypeDescriptor
        {
            public FastExpandoCustomTypeDescriptor(Type objectType, object instance)
                : base()
            {
                if (instance != null)
                {
                    var tmp = (FastExpando)instance;
                    var names = tmp.GetDynamicMemberNames();
                    foreach (var name in names)
                    {
                        customFields.Add(new DynamicPropertyDescriptor(name, instance));
                    }
                }
            }
            List<PropertyDescriptor> customFields = new List<PropertyDescriptor>();
            public override PropertyDescriptorCollection GetProperties()
            {
                return new PropertyDescriptorCollection(customFields.ToArray());
            }

            public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
            {
                return new PropertyDescriptorCollection(customFields.ToArray());
            }
        }
        class DynamicPropertyDescriptor : PropertyDescriptor
        {
            Type propertyType = typeof(object);
            public DynamicPropertyDescriptor(string name, object instance)
                : base(name, null)
            {
                var obj = (IDictionary<string, object>)instance;
                propertyType = obj[name].GetType();
            }

            public override bool CanResetValue(object component)
            {
                return false;
            }

            public override Type ComponentType
            {
                get
                {
                    return typeof(FastExpando);
                }
            }

            public override object GetValue(object component)
            {
                IDictionary<string, object> obj = (IDictionary<string, object>)component;
                return obj[Name];
            }

            public override bool IsReadOnly
            {
                get
                {
                    return false;
                }
            }

            public override Type PropertyType
            {
                get
                {
                    return propertyType;
                }
            }

            public override void ResetValue(object component)
            {
                throw new NotImplementedException();
            }

            public override void SetValue(object component, object value)
            {
                IDictionary<string, object> obj = (IDictionary<string, object>)component;
                obj[Name] = value;
            }

            public override bool ShouldSerializeValue(object component)
            {
                return false;
            }
        }

        private static Func<IDataReader, object> GetDynamicDeserializer(IDataRecord reader, int startBound, int length, bool returnNullIfFirstMissing)
        {
            var fieldCount = reader.FieldCount;
            if (length == -1)
            {
                length = fieldCount - startBound;
            }

            if (fieldCount <= startBound)
            {
                throw new ArgumentException("fieldCount <= startBound", "splitOn");
            }
            return
                 r =>
                 {
                     IDictionary<string, object> row = new Dictionary<string, object>(length);
                     for (var i = startBound; i < startBound + length; i++)
                     {
                         var tmp = r.GetValue(i);
                         tmp = tmp == DBNull.Value ? null : tmp;
                         row[r.GetName(i)] = tmp;
                         if (returnNullIfFirstMissing && i == startBound && tmp == null)
                         {
                             return null;
                         }
                     }
                     return FastExpando.Attach(row);
                 };
        }
        private static int collect;
        private const int COLLECT_PER_ITEMS = 1000, COLLECT_HIT_COUNT_MIN = 0;
        public class CacheInfo
        {
            public Func<IDataReader, object> Deserializer { get; set; }
            public Func<IDataReader, object>[] OtherDeserializers { get; set; }
            public Action<IDbCommand, object> ParamReader { get; set; }
            private int hitCount;
            public int GetHitCount() { return Interlocked.CompareExchange(ref hitCount, 0, 0); }
            public void RecordHit() { Interlocked.Increment(ref hitCount); }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<T> ReaderToEnumerable<T>(IDataReader reader)
        {
            var info = new CacheInfo
            {
                Deserializer = GetDeserializer(typeof(T), reader, 0, -1, false)
            };
            while (reader.Read())
            {
                dynamic next = info.Deserializer(reader);
                yield return (T)next;
            }
        }
    }
}
