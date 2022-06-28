using System.Xml.Serialization;
using PaytureApiPayMethodTask.Model;

namespace PaytureApiPayMethodTask
{
    internal class Program
    {
        private static string payBaseUrl = "https://sandbox3.payture.com/api/Pay?";

        static async Task Main()
        {
            string orderId = Guid.NewGuid().ToString();
            int amount = 12000;

            PayInfo payInfo = new PayInfo();
            payInfo.SetEMonth(12);
            payInfo.SetEYear(25);
            payInfo.SetPan("5218851946955484");
            payInfo.SetOrderId(orderId);
            payInfo.SetAmount(amount);
            payInfo.SetSecureCode(123);
            payInfo.SetCardHolder("Ivan Ivanov");

            PayRequest payRequest = new PayRequest();
            payRequest.SetKey("Merchant");
            payRequest.SetOrderId(orderId);
            payRequest.SetAmount(amount);
            payRequest.SetPayInfo(payInfo);

            Console.WriteLine(DateTime.Now + ":\nSending request...");

            try
            {
                string response = (await new HttpClient().GetAsync(payBaseUrl + payRequest.ToParametersString())).Content.ReadAsStringAsync().Result;
                Console.WriteLine($"\n" + DateTime.Now + ":\nRequest sent.");
                XmlSerializer serializer = new XmlSerializer(typeof(PayResponse));
                using (StringReader reader = new StringReader(response))
                {
                    var result = serializer.Deserialize(reader);
                    Console.WriteLine("\n" + DateTime.Now + ":\nResponse received. Response:\n\n" + result?.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now + ":\nError on sending request: " + ex.Message);
            }

            Console.WriteLine("\nPress any key to finish...");
            Console.ReadKey();
        }
    }
}
