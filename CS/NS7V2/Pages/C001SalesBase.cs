using CaotunSpring.C000;
using CaotunSpring.C000.Adapter;
using Microsoft.AspNetCore.Components;
using NS4.Models;
using System.Threading.Tasks;

namespace NS7V2
{
    public class C001SalesBase : AppComponentBaseV7
    {
        [Inject] // 1. 注入 Adapter, 每頁要有各自的, 不能混用
        public C001Adapter Adapter { get; set; }

        protected override void OnInitialized() // 2. 初值化
        {
            // (1) 所有頁面共同的, 和Adapter無關的: (a)Routing (b)Title (c)每頁筆數 
            base.OnInitialized();

            // (2) 本頁獨特的Adapter
            Adapter.Init(PRE, typeof(Sales), TABLE_CONFIG);
        }

        protected override async Task ReloadAsync() // 3. 更新顯示內容
        {
            if (Adapter.IsLoading || Page < 1)
            { return; }

            Adapter.IsLoading = true;
            Items = await GetItemsAsync(nameof(Sales), Adapter);
            Adapter.IsLoading = false;
        }
    }
}
