
using ExcelDataReader;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Linq;
using NS2Workbench.Data;
using NS2Workbench.Models;

//using Excel = Microsoft.Office.Interop.Excel;
//https://stackoverflow.com/questions/15285880/how-to-reference-microsoft-office-interop-excel-dll/29742906
namespace NS7Workbench
{
    public class Account
    {
        public int ID { get; set; }
        public double Balance { get; set; }
    }
    class Program
    {
        //static void AddPart()
        //{
        //    using (var db = new NS2Context())
        //    {
        //        // Note: This sample requires the database to be created before running.

        //        // Create
        //        Console.WriteLine("Inserting a new Part");
        //        Part part = new Part
        //        {
        //            Code = "aaa",
        //            Name = "測試",
        //            Category = "產品",
        //            Uom = "Each"

        //        };
        //        db.Add(part);
        //        //  db.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
        //        db.SaveChanges();
        //        //  var cnt = db.Part.C
        //        var cnt = db.Part.Count();
        //    }
        //}

        static void ResetPart()
        {
            using (var db = new NS2Context())
            {
                //VotingContext.Votes.RemoveRange(VotingContext.Votes);
                db.Part.RemoveRange(db.Part);

                //foreach (var part in db.Part)
                //{
                //    db.Remove(part);
                //}
                db.SaveChanges();
            }
        }
        static void AddPart(List<Part> partList)
        {
            using (var db = new NS2Context())
            {
                db.AddRange(partList);

                db.SaveChanges();

            }
        }

        static void AddSales(List<Sales> xList)
        {
            using (var db = new NS2Context())
            {
                db.AddRange(xList);

                db.SaveChanges();

            }
        }
        static void Reading供应商(ExcelWorksheet s1)
        {
            Console.WriteLine("\n===" + s1.Name + "=== ");

            int rowCnt = s1.Dimension.End.Row;

            //數據是從A3開始
            for (int row = 3; row <= rowCnt; row++)
            {
                string c = "A" + row;
                if (s1.Cells[c] == null) return;

                if (s1.Cells[c].Text != "")
                {
                    string strA = "A" + row;
                    string strB = "B" + row;
                    string txtA = s1.Cells[strA].Text;
                    string txtB = s1.Cells[strB].Text;
                    //Console.WriteLine("s1: " + vA + " " + s1.Cells[vA].Text);
                    //Console.WriteLine(row + " " + txtA + " " + txtB);
                    Console.WriteLine(s1.Name + " " + row + " " + txtA + " " + txtB);
                    //s1.Cells[vA]
                }
            }
        }
        static void Reading客户(ExcelWorksheet s1)
        {
            Console.WriteLine("\n===" + s1.Name + "=== ");

            //   int colCount = s1.Dimension.End.Column;  //get Column Count
            int rowCnt = s1.Dimension.End.Row;
            //數據是從A3開始
            for (int row = 3; row <= rowCnt; row++)
            {
                string c = "A" + row;
                if (s1.Cells[c] == null) return;



                if (s1.Cells[c].Text != "")
                {
                    string vA = "A" + row;
                    string strA = "A" + row;
                    string strB = "B" + row;
                    string txtA = s1.Cells[strA].Text;
                    string txtB = s1.Cells[strB].Text;
                    //Console.WriteLine("s1: " + vA + " " + s1.Cells[vA].Text);
                    Console.WriteLine(s1.Name + " " + row + " " + txtA + " " + txtB);
                    //s1.Cells[vA]
                }
            }
        }

        //class Sales
        //{
        //    //    public string SalesNum { get; set; }
        //    public string SalesNum { get; set; }
        //    public DateTime SalesDate { get; set; }
        //    public string PartNum { get; set; }
        //    public int Qty { get; set; }
        //    public decimal UnitPrice { get; set; }
        //    public decimal SubTotal { get; set; }
        //    public string Currency { get; set; }
        //    public string CustomerCode { get; set; }
        //    public string FromWH { get; set; }
        //}
        static List<Sales> Readings仓(ExcelWorksheet s1)
        {
            Console.WriteLine("\n===" + s1.Name + "=== ");

            int rowCnt = s1.Dimension.End.Row;

            List<Sales> salesList = new();
            //數據是從A4開始
            for (int row = 4; row <= rowCnt; row++)
            {
                string c = "A" + row;
                if (s1.Cells[c] == null) break;

                //取出SO, 先不含期初
                if (s1.Cells[c].Text != "" && (s1.Cells[c].Text.StartsWith("CN") || s1.Cells[c].Text.StartsWith("TW") || s1.Cells[c].Text.StartsWith("HK")))
                {
                    //string vA = "A" + row;
                    //Console.WriteLine("s1: " + vA + " " + s1.Cells[vA].Text);
                    string strA = "A" + row;
                    string strB = "B" + row;
                    string strC = "C" + row;
                    string strM = "M" + row;
                    string strN = "N" + row;
                    string strO = "O" + row;
                    string strP = "P" + row;
                    string strR = "R" + row;
                    string txtA = s1.Cells[strA].Text;
                    string txtB = s1.Cells[strB].Text;
                    string txtC = s1.Cells[strC].Text;
                    string txtM = s1.Cells[strM].Text;
                    string txtN = s1.Cells[strN].Text;
                    string txtO = s1.Cells[strO].Text;
                    string txtP = s1.Cells[strP].Text;
                    string txtR = s1.Cells[strR].Text;

                    DateTime dtB = DateTime.ParseExact(txtB, "yyyy/M/d", CultureInfo.InvariantCulture);
                    int intM = int.Parse(txtM);
                    decimal decimalN = decimal.Parse(txtN);
                    decimal decimalO = decimal.Parse(txtO);

                    var sales = new Sales()
                    {
                        SalesNum = txtA,
                        SalesDate = dtB,
                        PartNum = txtC,
                        Qty = intM,
                        UnitPrice = decimalN,
                        SubTotal = decimalO,
                        Currency = txtP,
                        CustomerCode = txtR,
                        FromWh = s1.Name
                    };
                    salesList.Add(sales);
                    //  Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(ent));
                    //Console.WriteLine(s1.Name + " 出库 " + row + " " + txtA + " " + txtB + " " + txtM + " " + txtN + " " + txtO + " " + txtP + " " + txtR);
                }
            }
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(salesList));
            return salesList;
        }

        static List<Part> Reading产品(ExcelWorksheet s1)
        {
            NumberStyles style;
            CultureInfo provider;

            Console.WriteLine("\n===" + s1.Name + "=== ");
            List<Part> partList = new();
            int rowCnt = s1.Dimension.End.Row;

            //數據是從A3開始
            for (int row = 3; row <= rowCnt; row++)
            {
                string cellA = "A" + row;
                string cellB = "B" + row;
                if (s1.Cells[cellA] == null) break;
                if (s1.Cells[cellB] == null) break;

                if (s1.Cells[cellA].Text != "" && s1.Cells[cellB].Text != "")
                {
                    //string vA = "A" + row;
                    //Console.WriteLine("s1: " + vA + " " + s1.Cells[vA].Text);
                    string strA = "A" + row;
                    string strB = "B" + row;
                    string strC = "C" + row;
                    string strD = "D" + row;
                    string strE = "E" + row;
                    string strF = "F" + row;
                    string strG = "G" + row;
                    string strH = "H" + row;
                    string strI = "I" + row;
                    string txtA = s1.Cells[strA].Text;
                    string txtB = s1.Cells[strB].Text;
                    string txtC = s1.Cells[strC].Text;
                    string txtD = s1.Cells[strD].Text;
                    string txtE = s1.Cells[strE].Text;
                    //string txtF = s1.Cells[strF] == null ? "0.0" : s1.Cells[strF].Text;

                    string txtF = s1.Cells[strF].Text == "" ? "0.0" : s1.Cells[strF].Text;
                    string txtG = s1.Cells[strG] == null ? "" : s1.Cells[strG].Text;
                    string txtH = s1.Cells[strH].Text;
                    string txtI = s1.Cells[strI] == null ? "" : s1.Cells[strI].Text;
                    Console.WriteLine(s1.Name + " " + row + " " + txtA + " " + txtB);


                    style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands;
                    provider = new CultureInfo("zh-CN");
                    decimal decimalF = Decimal.Parse(txtF, style, provider);


                    var part = new Part
                    {
                        Code = txtA,
                        Name = txtB,
                        Brand = txtC,
                        Spec = txtD,
                        Uom = txtE,
                        UnitPrice = decimalF,
                        SupplierCode = txtG,
                        Category = txtH,
                        Remarks = txtI,


                    };
                    partList.Add(part);
                }
            }
            return partList;

        }


        static void Readings材料(ExcelWorksheet s1)
        {
            Console.WriteLine("\n===" + s1.Name + "=== ");

            int rowCnt = s1.Dimension.End.Row;

            //數據是從B4開始, A的單號目前沒有數據
            for (int row = 4; row <= rowCnt; row++)
            {
                string c = "B" + row;
                if (s1.Cells[c] == null) return;

                if (s1.Cells[c].Text != "")
                {
                    //string vA = "A" + row;
                    //Console.WriteLine("s1: " + vA + " " + s1.Cells[vA].Text);
                    string strA = "A" + row;
                    string strB = "B" + row;
                    string txtA = s1.Cells[strA].Text;
                    string txtB = s1.Cells[strB].Text;
                    Console.WriteLine(s1.Name + " " + row + " " + txtA + " " + txtB);

                }
            }


        }

        static void Main(string[] args)
        {

          //  AutoToolsAdapters.DoAutoPages();
            AutoToolsRazor.Go();

        }
        static void NS_Excel()
        {


            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var enc = Encoding.GetEncoding(1251);
            //    Console.WriteLine(enc);
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Start to read Excel info...");



            string filePath = @"D:\2021\Ken_project\D0130.xlsx";
            //string filePath = @"D:\2021\Ken_project\Test1.xlsx";

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var s东莞仓 = package.Workbook.Worksheets["东莞仓"];
                var s台湾仓 = package.Workbook.Worksheets["台湾仓"];
                var s武汉仓 = package.Workbook.Worksheets["武汉仓"];
                var s材料库存表 = package.Workbook.Worksheets["材料库存表"];
                var s产品信息表 = package.Workbook.Worksheets["产品信息表"];
                var s客户信息表 = package.Workbook.Worksheets["客户信息表"];
                var s供应商信息表 = package.Workbook.Worksheets["供应商信息表"];
                //  Reading客户

                AddSales(Readings仓(s东莞仓));
                AddSales(Readings仓(s台湾仓));
                AddSales(Readings仓(s武汉仓));
                // Readings材料(s材料库存表);

                //List<Part> partList = Reading产品(s产品信息表);
                //AddPart(partList);

                //AddPart(Reading产品(s产品信息表));

                //Reading客户(s客户信息表);
                //Reading供应商(s供应商信息表);


                //https://suresh-kamrushi.medium.com/c-reading-excel-file-using-epplus-package-95cfb0ed98c5

            }
        }

        static void XXXMain(string[] args)
        {
            Console.WriteLine("Hello World!");
            var x = new AppLogger.Logger();
            x.Log("My NuGet package!");

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var enc = Encoding.GetEncoding(1251);
            Console.WriteLine(enc);
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            string filePath = @"D:\2021\Ken_project\进销存账.xlsx";
            //string filePath = @"D:\2021\Ken_project\Test1.xlsx";

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                // Auto-detect format, supports:
                //  - Binary Excel files (2.0-2003 format; *.xls)
                //  - OpenXml Excel files (2007 format; *.xlsx, *.xlsb)
                // No data is available for encoding 1252. For information on defining a custom encoding, see the documentation for the Encoding.RegisterProvider method.
                //https://stackoverflow.com/questions/50858209/system-notsupportedexception-no-data-is-available-for-encoding-1252
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    // Choose one of either 1 or 2:

                    // 1. Use the reader methods
                    do
                    {
                        while (reader.Read())
                        {
                            // reader.GetDouble(0);
                            var x0 = reader.GetString(0);
                            var x1 = reader.GetString(1);
                            //   var x2 = reader.GetDateTime(2);
                            var x2 = "";
                            var x3 = reader.GetString(3);
                            //  Console.WriteLine(x0 + "," + x1 + "," + x2 + "," + x3 + "");
                            x.Log(x0 + "," + x1 + "," + x2 + "," + x3 + "");

                        }
                    } while (reader.NextResult());

                    // 2. Use the AsDataSet extension method
                    // var result = reader.AsDataSet();

                    // The result of each spreadsheet is in result.Tables
                }
            }
        }
    }
}