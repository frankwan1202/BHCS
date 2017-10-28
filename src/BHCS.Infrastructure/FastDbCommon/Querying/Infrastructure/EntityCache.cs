using BHCS.Infrastructure.FastDbCommon.Querying.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using BHCS.Infrastructure.FastDbCommon.Persistents.Attributes;

namespace BHCS.Infrastructure.FastDbCommon.Querying.Infrastructure
{
    /// <summary>
    /// 实体信息缓存
    /// </summary>
    public class EntityCache
    {
        /// <summary>
        /// 保存实体列表
        /// </summary>
        private static Dictionary<string, object> _entityList = new Dictionary<string, object>();

        /// <summary>
        /// lock object
        /// </summary>
        private static readonly object LockObj = new object();


        /// <summary>
        /// 清空所有缓存
        /// </summary>
        public static void Reset()
        {
            _entityList.Clear();
        }

        /// <summary>
        /// 清理具体实体的缓存
        /// </summary>
        public static void Reset<TEntity>()
            where TEntity : class
        {
            var typestring = typeof(TEntity).ToString();
            if (_entityList.ContainsKey(typestring))
                _entityList.Remove(typestring);
        }

        /// <summary>
        /// 返回表名
        /// </summary>
        /// <returns></returns>
        public static string GetTableName<TEntity>()
            where TEntity : class
        {
            //return getTEntity<TEntity>().GetTableName();

            var table = typeof(TEntity).GetTypeInfo().GetCustomAttribute<TableAttribute>(true);
            if (table == null)
            {
                return typeof(TEntity).Name;
            }
            else
            {
                return table.Table;
            }
        }
        /// <summary>
        /// 返回用户名
        /// </summary>
        /// <returns></returns>
        public static string GetUserName<TEntity>()
            where TEntity : class
        {
            //return getTEntity<TEntity>().GetUserName();

            var table = typeof(TEntity).GetTypeInfo().GetCustomAttribute<TableAttribute>(true);
            if (table == null)
            {
                return string.Empty;
            }
            else
            {
                return table.Schema;
            }
        }
        /// <summary>
        /// 返回T
        /// </summary>
        /// <returns></returns>
        private static TEntity getTEntity<TEntity>()
            where TEntity : class
        {
            //var typestring = typeof(TEntity).ToString();

            //if (_entityList.ContainsKey(typestring))
            //    return (TEntity)_entityList[typestring];

            //lock (LockObj)
            //{
            //    if (_entityList.ContainsKey(typestring))
            //        return (TEntity)_entityList[typestring];

            //    var t = DataUtils.Create<TEntity>();
            //    _entityList.Add(typestring, t);
            //    return t;
            //}

            throw new NotImplementedException();
        }


        /// <summary>
        /// 获取主键字段
        /// </summary>
        /// <returns></returns>
        public static Field[] GetPrimaryKeyFields<TEntity>()
            where TEntity : class
        {
            //return getTEntity<TEntity>().GetPrimaryKeyFields();

            throw new NotImplementedException();
        }

        /// <summary>
        /// 返回所有字段
        /// </summary>
        /// <returns></returns>
        public static Field[] GetFields<TEntity>()
            where TEntity : class
        {
            //return getTEntity<TEntity>().GetFields();

            throw new NotImplementedException();
        }


        /// <summary>
        /// 返回第一个字段
        /// </summary>
        /// <returns></returns>
        public static Field GetFirstField<TEntity>()
            where TEntity : class
        {
            var fields = GetFields<TEntity>();
            if (null != fields && fields.Length > 0)
                return fields[0];
            return null;
        }


        /// <summary>
        /// 返回标识字段
        /// </summary>
        /// <returns></returns>
        public static Field GetIdentityField<TEntity>()
            where TEntity : class
        {
            //return getTEntity<TEntity>().GetIdentityField();

            throw new NotImplementedException();
        }

        /// <summary>
        /// 是否只读
        /// </summary>
        /// <returns></returns>
        public static bool IsReadOnly<TEntity>()
            where TEntity : class
        {
            //return getTEntity<TEntity>().IsReadOnly();

            throw new NotImplementedException();
        }


        /// <summary>
        /// 标识列的名称（Oracle）
        /// </summary>
        /// <returns></returns>
        public static string GetSequence<TEntity>()
            where TEntity : class
        {
            //return getTEntity<TEntity>().GetSequence();

            throw new NotImplementedException();
        }
    }
}
