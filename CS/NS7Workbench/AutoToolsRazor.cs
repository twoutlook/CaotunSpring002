using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NS7Workbench
{
    class AutoToolsRazor
    {
        static string NS7V2WorkDir = @"D:\2021\Lab\Lab0130\Lab0130\NS7V2\Pages\";
        static string NS7WorkDir = @"D:\2021\Lab\Lab0130\Lab0130\NS7\Pages\";
        static string WorkDir = @"D:\2021\Lab\Lab0130\Lab0130\NS7\Pages\";
        static string SrcPre = "A015";
        static String SrcEnt = "Sales";
        static string SrcRazor = SrcPre + SrcEnt + ".razor";// "A015Sales.razor";
        static string SrcBase = SrcPre + SrcEnt + "Base.cs";
        static string TgtRazor = "";
        static string TgtBase = "";

        public AutoToolsRazor()
        {

        }
        public static void Go()
        {
            WorkDir = NS7V2WorkDir;
            SrcPre = "C001";
            SrcEnt = "Sales";
            AutoPages("C002", "Part");
            AutoPages("C003", "Sales");
            AutoPages("C004", "Part");

        }


        /***
         * D:\2021\Lab\Lab0130\Lab0130\NS7\Pages\
         * A015Sales.razor
         * A015SalesBase.cs
         * 
         */
        static (string, string) GetFiles(string Pre, string Ent)
        {
            string Razor = WorkDir + Pre + Ent + ".razor";// "A015Sales.razor";
            string Base = WorkDir + Pre + Ent + "Base.cs";
            return (Razor, Base);

        }
        public static void AutoPages(string TgtPre, string TgtEnt)
        {
            (SrcRazor, SrcBase) = GetFiles(SrcPre, SrcEnt);
            (TgtRazor, TgtBase) = GetFiles(TgtPre, TgtEnt);


            if (!File.Exists(SrcRazor))
            {
                Console.WriteLine(SrcRazor + " SrcRazor 不存在 , 請給 原型");
                return;
            }
            if (!File.Exists(SrcRazor))
            {
                Console.WriteLine(SrcRazor + " SrcBase 不存在 , 請給 原型");
                return;
            }
            Console.WriteLine(String.Format(@"{0} {1} 原型可用", SrcPre, SrcEnt));
            if (File.Exists(TgtRazor))
            {
                Console.WriteLine(TgtRazor + " SrcRazor 已存在 , 不做  AutoPages ");
                return;
            }
            if (File.Exists(TgtBase))
            {
                Console.WriteLine(TgtBase + " SrcBase 已存在 , 不做  AutoPages");
                return;
            }
            Console.WriteLine(String.Format(@"{0} {1} 可以 AutoPages", TgtPre, TgtEnt));

            string txt1 = File.ReadAllText(SrcRazor);
            txt1 = txt1.Replace(SrcPre, TgtPre).Replace(SrcEnt, TgtEnt);

            string txt2 = File.ReadAllText(SrcBase);
            txt2 = txt2.Replace(SrcPre, TgtPre).Replace(SrcEnt, TgtEnt);

            try
            {
                File.WriteAllText(TgtRazor, txt1, Encoding.UTF8);
                File.WriteAllText(TgtBase, txt2, Encoding.UTF8);
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
