using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
/***
 * 
 * 要注意 ApplicationDbContext<<< changed
 * 目前是在 Inventory.Data;
 */

namespace CaotunSpring.C000.Adapter
{
    // NOTE by Mark, 2021-01-21, 
    // *** 不要直接改名, 要copy/paste改新增的,再刪掉或註釋掉舊的,以避免VS2019自動去改其它的引用
    // public class Q998DynamicAdapter
    // public class Q997DynamicAdapter
    //public class Q029Adapter
    //public class C000Adapter : IAdapter
    public class C000Adapter : IAdapterV7

    {
        //private IHostingEnvironment Environment;

        //   private string TABLE_CONFIG_ROOT = @"C:\ZZZ_TABLES_CONFIG_7777\";
        private string TABLE_CONFIG_ROOT = @"D:\2021\Lab\Lab0130\NS_TABLE_CONFIG\";



        private int MAX_ITEM_COL_CNT = 12;
        private int FIX_FITER_CNT = 8;// 這是指在 search, 最上兩個ROW, 各有 4 個 text filter control


//        public IFiltersA000 f;
        public IFiltersV7 f;

        public bool IsLoading { get; set; }

        public string defaultSortStr;


   //     public bool Loading { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        //private bool _loading = true;
        //public bool Loading { get => _loading; set => _loading = value; }

      //  bool IsLoading { get; set; }



        // NOTE by Mark, 2021-01-24
        // 將兩段組合在一起
        //
        // 1. 取得10筆泛型模型記錄
        // 2. 寫入到本頁的特定模型



        public void UpdateFMapper(Type type)
        {
            f.FMapper = new List<int>(); //WATCH , by Mark, 2021-02-02


            //Type type = typeof(VInasn);
            //PropertyInfo[] properties = type.GetProperties();
            int k = 0;
            f.FILTER_FILED_CNT = 0;
            foreach (var x in f.FieldMappers)
            {
                //  x.Name
                //foreach (var y in typeof(VInasn).GetProperties())
                foreach (var y in type.GetProperties())
                {
                    if (y.Name == x.Id)
                    {
                        if (y.PropertyType.Name.ToString() == "String")
                        {
                            //QueryAdapter.f.FMapper.Add(k);
                            f.FMapper.Add(k);
                            f.FILTER_FILED_CNT++;

                        }

                    }

                }
                k++;
            }

            // Note by Mark, 2021-01-24, 這是處理可篩選的欄位不足4個的時候
            // 另外在search componet 要禁制掉補 -1 的
            // FIX WHEN FILTER_FILED_CNT<=4
            for (int i = f.FILTER_FILED_CNT; i < FIX_FITER_CNT; i++)
            {
                f.FMapper.Add(-1);
            }

        }

        //https://stackoverflow.com/questions/21533506/find-a-specified-generic-dbset-in-a-dbcontext-dynamically-when-i-have-an-entity
        //var type = context.Model.FindEntityType(ENT);


        public void Start(Type type, string PRE, string ENT, string TABLE_CONFIG)
        {
            // NOTE by Mark, 2021-01-31
            //https://docs.microsoft.com/en-us/dotnet/architecture/blazor-for-web-forms-developers/config
            TABLE_CONFIG_ROOT = TABLE_CONFIG;

            // TODO 要如何取 Configuration?
            //TABLE_CONFIG_ROOT = Configuration["TABLE_CONFIG"];


            // NOTE by Mark, 2021-02-04
            // 因為不使用 filter 裡的 PageHelper
            //1. 指定導航網址
            // f.PageHelper.BaseUrl = "/" + PRE + "/";



            //2. 
            ReadJson(type, PRE, ENT);
            //3. 設定默認排序    取第一個欄位, 正向排序
            defaultSortStr = f.FieldMappers[0].Id + "_1";

            //4. 設置篩選欄位, *** 要處理可篩選欄位不足 4 個 的情況
            UpdateFMapper(type);
        }


        // ENT 應該就 type Name
        public void Init(string PRE, Type type,  string TABLE_CONFIG)
        {
            // NOTE by Mark, 2021-01-31
            //https://docs.microsoft.com/en-us/dotnet/architecture/blazor-for-web-forms-developers/config
            TABLE_CONFIG_ROOT = TABLE_CONFIG;


            //var typeName = type.Name;
            //var guess = false;
            //if (typeName == ENT)
            //{
            //     guess = true;
            //}

            //2. 
            ReadJson(type, PRE, type.Name);
            //3. 設定默認排序    取第一個欄位, 正向排序
            defaultSortStr = f.FieldMappers[0].Id + "_1";

            //4. 設置篩選欄位, *** 要處理可篩選欄位不足 4 個 的情況
            UpdateFMapper(type);
        }



        //https://docs.microsoft.com/en-us/aspnet/core/fundamentals/static-files?view=aspnetcore-5.0
        public void ReadJson(Type type, string PRE, string ENT)
        {
            try
            {
                //同一個ENT 可能有不同的顯示方式,用前綴區分
                //var str = System.IO.File.ReadAllText(@"D:\ZZZ\ENT2\" + PRE + ENT + ".json");


                var str = System.IO.File.ReadAllText(TABLE_CONFIG_ROOT + PRE + ENT + ".json");
                var array = JsonConvert.DeserializeObject<List<A000FieldMapper>>(str);


                // NOTE by Mark, 2021-01-25
                // 改用法
                // Id = id;   所有的欄位名
                // Name = name;自定義顯示名
                // Index = -1;大於0顯示, 小於或等於0不顯示
                //         f.FieldMappers = new(); //WATCH, by Mark, 2021-02-02
                f.FieldMappers = new List<A000FieldMapper>();


                array.Sort(delegate (A000FieldMapper x, A000FieldMapper y)
                {
                    //https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.sort?view=net-5.0
                    return x.Index.CompareTo(y.Index);
                });


                foreach (var item in array)
                {
                    if (item.Index > 0)
                        f.FieldMappers.Add(item);
                }

            }
            catch
            {
                //  Type type = typeof(VCmdMst);
                PropertyInfo[] properties = type.GetProperties();

                //  var auto = new List<FieldMapper>();
                //                f.FieldMappers = new(); WATCH, by Mark 2021-02-02
                f.FieldMappers = new List<A000FieldMapper>();



                //     // NOTE by Mark, 2021-01-25
                //foreach (PropertyInfo property in properties.Take(MAX_ITEM_COL_CNT))// DOING
                //{
                //    string y = property.Name;

                //    f.FieldMappers.Add(new A000FieldMapper { Id = y, Name = y, Index = -1 });
                //}

                // NOTE by Mark, 2021-01-25
                // 全部欄位都要輸出, 但是改變 Index, 仿 BASIC 10, 20 ...
                int k = 0;
                foreach (PropertyInfo property in properties)// DOING
                {
                    k += 1;
                    string y = property.Name;
                    int k2 = k * 10;
                    k2 = k <= MAX_ITEM_COL_CNT ? k2 : (-1) * k2;

                    // 加了 顯示 會擠到LAYOUT,
                    string strName = y;
                    if (y.Length > 10)
                    {
                        strName = y.Substring(0, 8) + "__";
                    }
                    //f.FieldMappers.Add(new A000FieldMapper { Id = y, Name = "顯示" + y, Index = k2 });
                    f.FieldMappers.Add(new A000FieldMapper { Id = y, Name = strName, Index = k2 });
                }
                WriteJson(PRE, ENT);
            }
        }

        public void WriteJson(string PRE, string ENT)
        {

            // string json = JsonConvert.SerializeObject(_data.ToArray(), Formatting.Indented);

            //        string json = JsonConvert.SerializeObject(QueryAdapter.f.FieldMappers.ToArray());
            string json = JsonConvert.SerializeObject(f.FieldMappers.ToArray(), Formatting.Indented);

            //write string to file
            //System.IO.File.WriteAllText(@"D:\ZZZ\ENT2\" + PRE + ENT + ".json", json);
            System.IO.File.WriteAllText(TABLE_CONFIG_ROOT + PRE + ENT + ".json", json);
        }


        // 使用最簡約的繼承時, 不能有參數

        //   public IConfiguration Configuration;
        public C000Adapter()
        {
            //https://docs.microsoft.com/en-us/aspnet/core/blazor/components/lifecycle?view=aspnetcore-5.0#stateful-reconnection-after-prerendering
            //   _cache = memoryCache;
            f = new FiltersV7(); // 
                                   //  f = filter;
      //      f.PageHelper.BaseUrl = "/Base_TOCHANGE/";    // *** 這裡要改 
      //      defaultSortStr = "TOCHANGE_1";   // *** 這裡要改 




        }

        string GetContains(string col, string val)
        {
            return String.Format(" and {0}.Contains(\"{1}\")", col, val);
        }

        public string GetWhereString()
        {
            string strWhere = " 1==1 "; // 使用傳統的做法

            for (int i = 0; i < 9; i++)
            {
                if (f.FilterContains[i] != null)
                {
                    // NOTE by Mark, 2021-01-20
                    // 在前端, 可以和 control 挷定
                    // 那就在這裡處理空白
                    f.FilterContains[i] = f.FilterContains[i].Trim();
                    if (f.FilterContains[i] != "")
                        strWhere += GetContains(f.FilterContainsCol[i], f.FilterContains[i]);
                }
            }
            return strWhere;
        }

        public string GetWhere()
        {
            string strWhere = " 1==1 "; // 使用傳統的做法

            for (int i = 0; i < 9; i++)
            {
                if (f.FilterContains[i] != null)
                {
                    // NOTE by Mark, 2021-01-20
                    // 在前端, 可以和 control 挷定
                    // 那就在這裡處理空白
                    f.FilterContains[i] = f.FilterContains[i].Trim();
                    if (f.FilterContains[i] != "")
                        strWhere += GetContains(f.FilterContainsCol[i], f.FilterContains[i]);
                }
            }
            return strWhere;
        }

        public string GetOrderBy()
        {
            return GetSortString() + GetSortString2();

        }// Field_1 => Field ,  Field_2 => Field desc


        public string GetSortString() // Field_1 => Field ,  Field_2 => Field desc
        {
            if (f.SortStr == null)
            {
                // BUG
                // NOTE by Mark, 2021-01-23
                // 不知為何, 會殘留上個頁面的值?
                // 在這裡, 再強制一
                defaultSortStr = f.FieldMappers[0].Id + "_1";

                f.SortStr = defaultSortStr;


            }
            string[] str = f.SortStr.Split('_');
            string strOrderBy = str[0];

            // BUG 仍會使用到殘存的 f.SortStr
            // TODO 要核對看看是否 是合法欄位

            if (str[1] == "2") strOrderBy += " desc";
            return strOrderBy;
        }

        public string GetSortString2() // Field_1 => Field ,  Field_2 => Field desc
        {
            if (f.SortStr2 == null || f.SortStr2 == "")
            {
                return "";
            }

            string[] str = f.SortStr2.Split('_');
            string strOrderBy = "," + str[0];
            if (str[1] == "2") strOrderBy += " desc";
            return strOrderBy;
        }

        public IFiltersV7 GetFilter()
        {
            return f;
        }
    }
}
