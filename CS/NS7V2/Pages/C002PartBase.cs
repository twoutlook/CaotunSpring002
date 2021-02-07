using CaotunSpring.C000;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using NS4.Models;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using CaotunSpring.C000.Adapter;
using System;

namespace NS7V2
{
    public class C002PartBase : AppComponentBaseV7

    {
        // 1. 注入 Adapter, 每頁要有各自的, 不能混用
        [Inject]
        public C002Adapter Adapter { get; set; }

        // 2. 本地 Item List, 存放本頁面要顯示的內容, 使用 Entity Type
        //public List<Part> Items { get; set; }
        public List<Object> Items { get; set; }

        // 3. 初值化
        protected override void OnInitialized()
        {
            // (1) 所有頁面共同的, 和Adapter無關的: (a)Routing (b)Title (c)每頁筆數 
            base.OnInitialized();

            // (2) 本頁獨特的Adapter
            Adapter.Init(PRE, typeof(Part), TABLE_CONFIG);
        }

        // 4. 更新本頁要顯示的內容
        protected override async Task ReloadAsync()
        {
            //var qa = Adapter;
            //var db = DbFactory.CreateDbContext();

            if (Adapter.IsLoading || Page < 1) return;

            Adapter.IsLoading = true; // --- start

            // (1) 總筆數
         //   PageHelper.TotalItemCount = await db.Part.Where(qa.GetWhere()).CountAsync();

            // (2) 分頁的內容
            //Items = db.Part.Where(qa.GetWhere()).OrderBy(qa.GetOrderBy())
            //       .Skip(PageHelper.Skip).Take(PageHelper.PageSize).ToList();

            Items =await GetItemsAsync(nameof(Part), Adapter);

            // (3) 分頁的筆數
        //    PageHelper.PageItems = Items.Count;

            Adapter.IsLoading = false;// --- end
        }

    }
}
