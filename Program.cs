using System.Data.SqlClient;
using System.Net;
using System.Text;


namespace ChinaMall
{
    class Program
    {

        static void Main(string[] args)
        {


            //معلومات الاتصال بقواعد بيانات sql sever
            // QGGL79F
            //City mall cash DESKTOP-EDBMTC2\MOH2008
            //[Unit_Price1]
            //kinder house: DESKTOP-T35EOL8\sqlexpress
            //   Console.WriteLine("input Database name: ");
        

            string? DatabaseName;
            DatabaseName = "ACC_DB_2024_9";
            // Console.WriteLine("input server name: ");
            // serverName = "DESKTOP-T35EOL8\sqlexpress";

            var cs = @"Server=الرئيسي\MOH2008" + ";Database=" + DatabaseName + ";Trusted_Connection=True;";
            //تعليمة sql
            // EndUser
            //SELECT [Materials].Mat_Arb_Name,[Materials].Mat_ID,[Materials].[Mat_Current_Quantity],[Units].Unit_Price1,[Units].Unit_Price3,[Units].Unit_Price4,[Units].Unit_Cost_Price,[Units].Unit_Price2,[Units].[Unit_Name]  FROM[Materials]  join[Units] on [Materials].Mat_ID=[Units].Mat_ID 
            var stm1 = "SELECT [Materials].Mat_Arb_Name,[Materials].Mat_ID,[Materials].[Mat_Current_Quantity]"
+ " ,[Units].Unit_Price3,[Units].[Unit_Name] ,ISNULL(Mat_Unit_BarCode.BarCode,'') as BarCode1,[Materials].[Mat_Unlinked_Unit_Qty]  "
 + " FROM[Materials] "
 + " join[Units] on [Materials].Mat_ID=[Units].Mat_ID "
 + " left join Mat_Unit_BarCode on Mat_Unit_BarCode.Mat_ID=Materials.Mat_ID  ";
            var stm2 = "SELECT       [Equal_Currency]   FROM .[Currency] where Sym_Currency='$'";
            var csv = new StringBuilder();
            using (SqlConnection con = new SqlConnection(cs))
            {

                con.Open();
                var newLine = "";
                using (SqlCommand cmd2 = new SqlCommand(stm2, con))
                {
                    using SqlDataReader rdr2 = cmd2.ExecuteReader();

                    while (rdr2.Read())
                    {

                        //Suggestion made by KyleMit
                        newLine = string.Format("{0},{1},{2},{3},{4},{5},{6}", "اسم المادة", "كود", "الكمية", "سعر", "الواحدة", "باركود", "الكمية");
                        csv.AppendLine(newLine);


                    }
                }
                using (SqlCommand cmd1 = new SqlCommand(stm1, con))

                {

                    using SqlDataReader rdr1 = cmd1.ExecuteReader();
                    while (rdr1.Read())
                    {
                        try
                        {
                            String name = rdr1.GetString(0);
                           name = name.Replace(",", " ")      // استبدال الفاصلة
           .Replace("\"", " ")     // استبدال علامات الاقتباس المزدوجة
           .Replace("\n", " ")     // استبدال السطر الجديد
           .Replace("\r", " ")     // استبدال العودة إلى بداية السطر
           .Replace("\'", " ")     // استبدال العودة إلى بداية السطر
           .Replace("\t", " ")     // استبدال علامات التبويب
           .Replace(";", " ")      // استبدال الفاصلة المنقوطة
           .Replace("\\", " ")     // استبدال الشرطة المائلة
           .Replace("/", " ")      // استبدال الشرطة المائلة الأمامية
           .Replace("*", " ")      // استبدال علامة النجمة
           .Replace("?", " ")      // استبدال علامة السؤال
           .Replace("#", " ")      // استبدال علامة #
           .Replace("%", " ");     // استبدال علامة %

                            //Suggestion made by KyleMit
                            newLine = string.Format("{0},{1},{2},{3},{4},{5},{6}", name, rdr1.GetInt64(1), rdr1.GetDouble(2), rdr1.GetDouble(3), rdr1.GetString(4), rdr1.GetString(5), rdr1.GetDouble(6));
                            // Console.WriteLine(newLine);
                            csv.AppendLine(newLine);
                        }
                        catch (Exception err)
                        {
                            Console.WriteLine(err.Message);
                        }
                    }

                }


                File.WriteAllText("mm101.csv", csv.ToString(), Encoding.UTF8);
                Console.WriteLine("finish");
                try

                {
                    using (WebClient client = new WebClient())
                    {
                        string myFile = @"mm101.csv";
                        client.Credentials = CredentialCache.DefaultCredentials;
                        //إرسال ملف الإكسل كapi
                        byte[] result = client.UploadFile(@"https://css4dev.com/ChineseCenter/ControlPanel/MohtasebFileConnect.php", "POST", myFile);
                        string s = Encoding.UTF8.GetString(result, 0, result.Length);
                        Console.WriteLine(s);
                        client.Dispose();
                    }
                }
                catch (Exception err)

                {
                    Console.WriteLine(err.Message);

                }

            }

        }
    }
}