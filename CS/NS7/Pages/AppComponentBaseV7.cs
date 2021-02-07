using DreamAITek.T001.Adapter;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NS2.Data;
using NS5Comp.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NS7
{

    //https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.components.componentbase?view=aspnetcore-5.0
    // NOTE by Mark, 2021-02-03
    // 共有的, 先做在這裡
    public  class AppComponentBaseV7: ComponentBase
    {
        [Inject]
        public IConfiguration Configuration { get; set; }

        [Inject]
        public IDbContextFactory<NS2Context> DbFactory { get; set; }
        [Inject]
        public NavigationManager Nav { get; set; }

    

        [Inject]
        public IPageHelperV7 PageHelper { get; set; }


        [Parameter]

        public int Page // 原範例,已更換變量 //頁面上的分頁
        {
            get
            {
                return PageHelper.Page;
            }
            set
            {
                PageHelper.Page = value;
            }
        }


        public string PageRouteBase { get; set; }

        // 2. 如何給 PRE??? TODO
        protected override void OnAfterRender(bool firstRender) // 原範例,已調整導航
        {
          //  var x = PageRouteBase;

            var x = PageHelper.BaseUrl;
            if (_lastPage < 1)
            {
                Nav.NavigateTo(x + "1");
                return;
            }

            if (PageHelper.PageCount > 0)
            {
                if (Page < 1)
                {
                    Nav.NavigateTo(x + "1");
                    return;
                }
                if (Page >= PageHelper.PageCount)
                {
                    // NOTE by Mark, 2021-02-04
                    // 故意將頁面走到超出的 199
                    // 是會進到這裡兩次
                    // 有點浪費, 但功能正常,
                    // 反應速度可以的
                    Nav.NavigateTo(x + "" + PageHelper.PageCount);
                    return;
                }
            }

            base.OnAfterRender(firstRender);
        }




        //     protected WrapperV3 Wrapper { get; set; } // 整個頁面的剛性骨架
        protected WrapperV7 Wrapper { get; set; } // 整個頁面的剛性骨架

        //      protected override void OnAfterRender(bool firstRender) { }

        protected int _lastPage = -1;// 原範例,不動

        //NOTE by Mark, 2021-02-04
        //The virtual keyword is used to modify a method, property, indexer, or event declaration and allow for it 
        //to be overridden in a derived class
        //https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/virtual


        protected virtual Task ReloadAsync()
        {
            return Task.CompletedTask;
        }




        // 7. working!
        protected override async Task OnInitializedAsync()
        {
            await ReloadAsync();
        }


        // 3. 調用 ReloadAsync,是否能仍移動 Base, override ReloadAsync
        //           Page  _lastPage
        // stage 1:    0      -1
        // stage 2:    1       0
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
  

        protected string TABLE_CONFIG;
        protected string PRE;
        protected string TITLE;
        protected override void OnInitialized()
        {
            //var guess=this.GetType().Name.Substring(0, 4);
            PRE = this.GetType().Name.Substring(0, 4);
            PageHelper.PageSize = 5; //DOING
            PageHelper.BaseUrl = "/" + PRE + "/";
            TITLE = ""+this.GetType().Name.Substring(4).ToUpper();

            //   TITLE = "銷售";
            TABLE_CONFIG = Configuration["TABLE_CONFIG"];
         //   QueryAdapter.Start(typeof(Part), PRE, ENT, TABLE_CONFIG);

        }
    }
}
