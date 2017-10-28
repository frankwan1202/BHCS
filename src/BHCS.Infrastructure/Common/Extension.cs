using BHCS.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace BHCS
{
    public static class Extension
    {
        //public static Pager<T> ToPage<T>(this DbContext context,string strsql,string orderBy,int pageIndex,int pageSize)
        //{
        //    if (string.IsNullOrWhiteSpace(strsql)) throw new ArgumentNullException("sql 语句不能为空！");
        //    if (string.IsNullOrWhiteSpace(orderBy)) throw new ArgumentNullException("orderby 列名不能为空！");
        //    if (pageIndex <= 0) throw new InvalidOperationException("Page index 必须大于零！");
        //    if (pageSize <= 0) throw new InvalidOperationException("Page size 必须大于零！");

        //    var pageStartIndex = pageIndex == 1 ? 0 : (pageIndex - 1) * pageSize;
        //    string pagingSql = $"{strsql} order by {orderBy} limit {pageStartIndex},{pageSize}";
        //    string countSql = $"select count(0) from ({strsql}) t";

        //    var list = context.Model.<T>(pagingSql, parameters).ToList();
        //    totalRow = context.Database.SqlQuery<int>(countSql, parameters).First();
        //    return list;
        //}

        public static string ToText(this AccountState accountState)
        {
            switch (accountState)
            {
                case AccountState.Disabled: return "已禁用";
                case AccountState.Enabled: return "已启用";
                default: return "未知";
            }
        }

        public static string ToMD5WithLower(this string str)
        {
            if (string.IsNullOrWhiteSpace(str)) return "";

            byte[] data = Encoding.UTF8.GetBytes(str);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] OutBytes = md5.ComputeHash(data);

            string OutString = "";
            for (int i = 0; i < OutBytes.Length; i++)
            {
                OutString += OutBytes[i].ToString("x2");
            }
            // return OutString.ToUpper();
            return OutString.ToLower();

        }
    }
}