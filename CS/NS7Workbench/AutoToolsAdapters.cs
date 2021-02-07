using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NS7Workbench
{
    class AutoToolsAdapters
    {
        public AutoToolsAdapters()
        {

        }
        static string WorkDir = @"D:\2021\Lab\Lab0130\Lab0130\CS7Comps\C000Adapters\";


        static string SrcPre = "C001";
        static String SrcEnt = "Adapter";
        //       static string SrcRazor = SrcPre + SrcEnt + ".razor";// "A015Sales.razor";
        static string SrcBase = SrcPre + SrcEnt + ".cs"; //C001Adapter.cs
                                                         //       static string TgtRazor = "";
        static string TgtBase = "";

        /***
         * D:\2021\Lab\Lab0130\Lab0130\NS7\Pages\
         * A015Sales.razor
         * A015SalesBase.cs
         * 
         */
        //static (string, string) GetFiles(string Pre, string Ent)
        //{
        //    string Razor = WorkDir + Pre + Ent + ".razor";// "A015Sales.razor";
        //    string Base = WorkDir + Pre + Ent + "Base.cs";
        //    return (Razor, Base);

        //}
        public static void DoAutoPages()
        {
            //  AutoPages("C001", "C002");
            string pre;
            for (int i = 2; i <= 99; i++)
            {
                pre ="C"+ i.ToString("000");
                Console.WriteLine(pre);
                AutoPages("C001", pre);
            }

            //    services.AddScoped<A001Adapter>();
            AutoPagesAddScoped();
        }
        static string GetSrcFile(string Pre)
        {
            string Base = WorkDir + Pre + "Adapter.cs";
            return Base;

        }
        static string GetTgtFile(string Pre)
        {
            string Base = WorkDir + @"Adapters\" + Pre + "Adapter.cs";
            return Base;

        }
        public static void AutoPagesAddScoped()
        {
            StringBuilder sb = new();
            string strPageHelper = @"services.AddScoped<IPageHelperV7, PageHelperV7>();";
            sb.Append(strPageHelper);
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);


            string pre;
            for (int i = 0; i <= 99; i++)
            {
                pre = "C" + i.ToString("000");
                Console.WriteLine(pre);
                sb.Append(String.Format(@" services.AddScoped<{0}Adapter>();", pre));
                sb.Append(Environment.NewLine);
            }
            string strFile = WorkDir + "ForStartup.txt";
            try
            {
                
                File.WriteAllText(strFile, sb.ToString(), Encoding.UTF8);
                Console.WriteLine("done," + strFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        public static void AutoPages(string SrcPre, string TgtPre)
        {
            string srcFile = GetSrcFile(SrcPre);
            string tgtFile = GetTgtFile(TgtPre);



            if (!File.Exists(srcFile))
            {
                Console.WriteLine(srcFile + " srcFile 不存在 , 請給 原型");
                return;
            }
      //      Console.WriteLine(String.Format(@"{0} 原型可用", srcFile));

            if (File.Exists(tgtFile))
            {
                Console.WriteLine(tgtFile + " tgtFile 已存在 , 不做  AutoPages ");
                return;
            }
      
       //     Console.WriteLine(String.Format(@"{0}  可以 AutoPages", tgtFile));

            string txt1 = File.ReadAllText(srcFile);
            txt1 = txt1.Replace(SrcPre, TgtPre);

            try
            {
                File.WriteAllText(tgtFile, txt1, Encoding.UTF8);
                Console.WriteLine("done,"+ tgtFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        static void MakePage(string PRE, string ENT, string TITLE)
        {
            string basePath = @"D:\2021\Lab\Hot\BlazorServerDbContextExample\BlazorServerEFCoreSample\MyFileGenTool\Gen\";

            //NOTE by Mark, 2021-01-25, 直接寫入工作目錄
            string basePath2 = @"D:\2021\Lab\Hot\BlazorServerDbContextExample\BlazorServerEFCoreSample\Inventory\A000\Pages\";

            string path = basePath + "A999V2Outbill.razor"; //這是提供原型
                                                            //     string path2 =basePath2 + "A005V2Outbill.razor";
                                                            // This text is added only once to the file.


            if (!File.Exists(path))
            {
                Console.WriteLine(path + " 不存在 , 請給 原型");
                return;
            }

            string str = File.ReadAllText(path);
            string key0 = "出庫單";
            string key1 = "A999";
            string key2 = "V2Outbill";

            string pathFinal = basePath + PRE + ENT + ".razor";
            string path2Final = basePath2 + PRE + ENT + ".razor";



            var strFinal = str.Replace(key0, TITLE).Replace(key1, PRE).Replace(key2, ENT);

            File.WriteAllText(pathFinal, strFinal, Encoding.UTF8);
            File.WriteAllText(path2Final, strFinal, Encoding.UTF8);


            //   Console.WriteLine(str);
        }
    }
}
