using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NS4.Data;
using NS4RazorLib.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NS4
{

    //https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.components.componentbase?view=aspnetcore-5.0
    // NOTE by Mark, 2021-02-03
    // 共有的, 先做在這裡
    public  class AppComponentBase: ComponentBase
    {
        [Inject]
        public IConfiguration Configuration { get; set; }

        [Inject]
        public IDbContextFactory<NS2Context> DbFactory { get; set; }
        [Inject]
        public NavigationManager Nav { get; set; }


        protected WrapperA Wrapper { get; set; } // 整個頁面的剛性骨架

        //      protected override void OnAfterRender(bool firstRender) { }

        protected int _lastPage = -1;// 原範例,不動

        //protected override async Task OnInitializedAsync()
        //{
        //    await ReloadAsync();
        //}
    }
}
