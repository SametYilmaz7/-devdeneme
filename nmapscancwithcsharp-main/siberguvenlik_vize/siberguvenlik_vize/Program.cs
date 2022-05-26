using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http.Json;
using System.Text.RegularExpressions;


namespace siberguvenlik_vize
{
    class Program
    {
        static void Main(string[] args)
        {
            string address;
            int port;

            while (true) //konsolda cikis yazılmadığı sürece program devam edecek. 
            {
                //Tarama işleminde kullanılacak alanlar için kullanıcıdan parametre alıyoruz

                Console.WriteLine("Cikmak icin 'cikis' yaziniz.");

                Console.Write("İnternet Adresini Giriniz: ");
                address = Console.ReadLine();

                if (address == "cikis") break; //cikis yazılırsa program sonlanıyor.

                Console.Write("Adresin portunu giriniz: ");
                port = Convert.ToInt32(Console.ReadLine());

                //Burada bir process oluşturuyoruz ve nmap.exe uygulamasını çalıştırıyoruz girilecek komutları "Arguments" kısmında belirtiyoruz ve processi başlatıyoruz.
                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "nmap.exe";
                startInfo.Arguments = "-p " + port + " --script http-sql-injection " + address; //Kullanıcının girdiği ip parametresini burada kullanıyoruz. Bu komut sayesinde uygulamada yazılması gereken komutları yazıyoruz.

                process.StartInfo = startInfo;
                process.Start();
                process.WaitForExit();//Process işlemi bitene kadar herhangi bir kod çalışmasın diye bu fonksiyonu kullanıyoruz.


                Console.WriteLine("*******************************************************************************************");
                Console.WriteLine("*************************Tarama islemi tamamlandi.*************************");
                Console.WriteLine("*******************************************************************************************");

                JArray result = new JArray(   //Bu kısımda girdigimiz verileri array'e kaydediyorum
                   new JObject(
                            new JProperty("Nmap command", startInfo.Arguments)));
                   new JObject(
                            new JProperty("Url",));

                // Bu kısımda array'e kaydettigim verileri ayarlanan dosya yoluna json dosyasını oluşturuyor

                File.WriteAllText(@"C:\Users\samet\OneDrive\Masaüstü\deneme.json", result.ToString());
                using (StreamWriter dosya = File.CreateText(@"C:\Users\samet\OneDrive\Masaüstü\deneme.json"))
                using (JsonTextWriter yazdir = new JsonTextWriter(dosya))
                {
                    result.WriteTo(yazdir);
                }
            }






        }
    }
}