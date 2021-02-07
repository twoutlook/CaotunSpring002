using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using DreamAITek.T001.Adapter;
using NS5Comp.Components;
using Microsoft.EntityFrameworkCore;
using NS4.Data;
using NS4.Models;
using System.Linq.Dynamic.Core;

namespace NS5
{
    //https://www.telerik.com/blogs/using-a-code-behind-approach-to-blazor-components
    public  class A007SalesBase :AppComponentBaseV2
  
    {

        [Inject]
        public A007Adapter QueryAdapter { get; set; }

        [Inject]
        public IPageHelperA000 PageHelper { get; set; }


        [Parameter]
        //public int Page // 原範例,已更換變量 //頁面上的分頁
        //{
        //    get => QueryAdapter.f.PageHelper.Page;
        //    set
        //    {
        //        QueryAdapter.f.PageHelper.Page = value;
        //    }
        //}

        public int Page // 原範例,已更換變量 //頁面上的分頁
        {
            get {
         //       PreGetPage();
                return PageHelper.Page; 
            }
            set
            {
                PageHelper.Page = value;
        //        PreSetPage();
            }
        }

        private void PreGetPage()
        {
            PageHelper.Page = QueryAdapter.f.PageHelper.Page;
        }
        private void PreSetPage()
        {
             QueryAdapter.f.PageHelper.Page= PageHelper.Page ;
        }


        //  protected WrapperA Wrapper { get; set; } // 整個頁面的剛性骨架

        //    protected ICollection<Object> Objs { get; set; } // 技術梗,適時使用泛型

        protected List<Sales> Items { get; set; } // 本頁面要顯示的內容     // === 要調整的地方 ===

        protected string IsDisabled(bool condition) => !QueryAdapter.f.Loading && condition ? "" : "disabled";// 原範例,已更換變量

       // protected int _lastPage = -1;// 原範例,不動

        protected override void OnAfterRender(bool firstRender) // 原範例,已調整導航
        {
            // Ensure we're on the same, er, right page.
            //if (_lastPage < 1)
            //{
            //    Nav.NavigateTo(QueryAdapter.f.PageHelper.BaseUrl + "1");
            //    return;
            //}

            if (_lastPage < 1)
            {
                Nav.NavigateTo(PRE + "/1");
                return;
            }

            // Normalize the page values.
            //if (QueryAdapter.f.PageHelper.PageCount > 0)
            //{
            //    if (Page < 1)
            //    {
            //        Nav.NavigateTo(QueryAdapter.f.PageHelper.BaseUrl + "1");
            //        return;
            //    }
            //    if (Page > QueryAdapter.f.PageHelper.PageCount)
            //    {
            //        Nav.NavigateTo(QueryAdapter.f.PageHelper.BaseUrl + QueryAdapter.f.PageHelper.PageCount);
            //        return;
            //    }
            //}

            if (PageHelper.PageCount > 0)
            {
                if (Page < 1)
                {
                    Nav.NavigateTo( PRE + "/1");
                    return;
                }
                if (Page >= PageHelper.PageCount)
                {
                    Nav.NavigateTo(PRE+"/" + PageHelper.PageCount);
                    return;
                }
            }

            base.OnAfterRender(firstRender);
        }

        protected async override Task OnParametersSetAsync() // 原範例,不動
        {
            // Make sure the page really chagned.
            if (Page != _lastPage)
            {
                _lastPage = Page;
                await ReloadAsync();
            }
            await base.OnParametersSetAsync();
        }

        protected string TITLE { get; set; }// "銷售/出庫";
        private string PRE = "A007";
        private string ENT = "Sales";
        private string TABLE_CONFIG;


        protected async Task ReloadAsync()
        {
            var qa = QueryAdapter;
            var f = QueryAdapter.f;
       //     var p = QueryAdapter.f.PageHelper;
            var db = DbFactory.CreateDbContext();

            if (f.Loading || Page < 1)
            {
                return;
            }

            f.Loading = true; // --- start

            string strWhere = qa.GetWhereString();
            string strOrderBy = qa.GetSortString() + qa.GetSortString2();

      //      p.TotalItemCount = await db.Sales.Where(strWhere).CountAsync();

            //DOING
            PageHelper.TotalItemCount = await db.Sales.Where(strWhere).CountAsync();

            //Items = db.Sales.Where(strWhere).OrderBy(strOrderBy).Skip(p.Skip).Take(p.PageSize).ToList();
            Items = db.Sales.Where(strWhere).OrderBy(strOrderBy).Skip(PageHelper.Skip).Take(PageHelper.PageSize).ToList();

            //      p.PageItems = Items.Count;

            //DOING
            PageHelper.PageItems = Items.Count;

            f.Loading = false;// --- end
        }





        protected override async Task OnInitializedAsync()
        {
            await ReloadAsync();
        }

        protected override void OnInitialized()
        {
            PageHelper.PageSize = 9;

            TITLE = "銷售 / 出庫";
            TABLE_CONFIG = Configuration["TABLE_CONFIG"];
            QueryAdapter.Start(typeof(Sales), PRE, ENT, TABLE_CONFIG);

            //1. 指定導航網址
            //QueryAdapter.f.PageHelper.BaseUrl = "/" + PRE + "/";

            ////2. 讀取欄位定義, 如果不存在,以標準模板建立使用
            //Type type = typeof(Sales); //注意模型還未放到指定的 namespace   // === 要調整的地方 ===
            //PropertyInfo[] properties = type.GetProperties();
            //QueryAdapter.ReadJson(type, PRE, ENT);

            ////3. 設定默認排序    取第一個欄位, 正向排序
            //QueryAdapter.defaultSortStr = QueryAdapter.f.FieldMappers[0].Id + "_1";

            ////4. 設置篩選欄位, *** 要處理可篩選欄位不足 4 個 的情況
            //QueryAdapter.UpdateFMapper(type);
        }
    }
}
