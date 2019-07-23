﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace SuperM.Dapper.PageModel
{
    /// <summary>
    /// 分页集合
    /// </summary>
    /// <typeparam name="T">元素类型</typeparam>
    [Serializable]
    public class PaginationList<T> : IPaginationsBase
    {
        /// <summary>
        /// 初始化分页集合
        /// </summary>
        public PaginationList() : this(0)
        {
        }

        /// <summary>
        /// 初始化分页集合
        /// </summary>
        /// <param name="data">内容</param>
        public PaginationList(IEnumerable<T> data = null)
            : this(0, data)
        {
        }

        /// <summary>
        /// 初始化分页集合
        /// </summary>
        /// <param name="totalCount">总行数</param>
        /// <param name="data">内容</param>
        public PaginationList(int totalCount, IEnumerable<T> data = null)
            : this(1, 20, totalCount, data)
        {
        }


        /// <summary>
        /// 初始化分页集合
        /// </summary>
        /// <param name="page">页索引</param>
        /// <param name="pageSize">每页显示行数</param>
        /// <param name="totalCount">总行数</param>
        /// <param name="order">排序条件</param>
        /// <param name="data">内容</param>
        public PaginationList(int page, int pageSize, int totalCount, IEnumerable<T> data = null)
        {
            Data = data?.ToList() ?? new List<T>();
            var pager = new Pagination(page, pageSize, totalCount);
            TotalCount = pager.TotalCount;
            PageCount = pager.GetPageCount();
            Page = pager.Page;
            PageSize = pager.PageSize;
        }

        /// <summary>
        /// 初始化分页集合
        /// </summary>
        /// <param name="pager">查询对象</param>
        /// <param name="data">内容</param>
        public PaginationList(IPagination pager, IEnumerable<T> data = null)
            : this(pager.Page, pager.PageSize, pager.TotalCount, data)
        {
        }

        /// <summary>
        /// 页索引，即第几页，从1开始
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 每页显示行数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总行数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public List<T> Data { get; }

        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="index">索引</param>
        public T this[int index]
        {
            get => Data[index];
            set => Data[index] = value;
        }

        /// <summary>
        /// 添加元素
        /// </summary>
        /// <param name="item">元素</param>
        public void Add(T item)
        {
            Data.Add(item);
        }

        /// <summary>
        /// 添加元素集合
        /// </summary>
        /// <param name="collection">元素集合</param>
        public void AddRange(IEnumerable<T> collection)
        {
            Data.AddRange(collection);
        }

        /// <summary>
        /// 清空
        /// </summary>
        public void Clear()
        {
            Data.Clear();
        }

        /// <summary>
        /// 转换分页集合
        /// </summary>
        /// <typeparam name="TResult">目标元素类型</typeparam>
        /// <param name="converter">转换方法</param>
        public PaginationList<TResult> Convert<TResult>(Func<T, TResult> converter)
        {
            return Convert(this.Data.Select(converter));
        }

        /// <summary>
        /// 转换分页集合
        /// </summary>
        /// <param name="data">内容</param>
        public PaginationList<TResult> Convert<TResult>(IEnumerable<TResult> data)
        {
            return new PaginationList<TResult>(Page, PageSize, TotalCount, data);
        }
    }
}
