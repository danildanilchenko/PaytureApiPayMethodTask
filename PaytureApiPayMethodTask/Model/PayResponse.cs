using System.Xml.Serialization;

namespace PaytureApiPayMethodTask.Model
{
    [XmlRoot("Pay")]
    public class PayResponse
    {
        [XmlAttribute("Success")]
        public string success { get; set; }
        [XmlAttribute("OrderId")]
        public string orderId { get; set; }
        [XmlAttribute("Key")]
        public string key { get; set; }
        [XmlAttribute("Amount")]
        public int amount { get; set; }
        [XmlAttribute("ErrCode")]
        public string errorCode { get; set; }
        public override string ToString()
        {
            return "Success: " + success +
                "\nOrderId: " + orderId +
                "\nKey: " + key +
                "\nAmount: " + amount +
                "\nErrorCode: " + errorCode ?? new string("none");
        }
    }
}
