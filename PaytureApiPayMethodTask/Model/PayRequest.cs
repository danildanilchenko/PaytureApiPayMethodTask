using System.Text.RegularExpressions;

namespace PaytureApiPayMethodTask.Model
{
    internal class PayRequest
    {
        private string key;
        private string orderId;
        private string amount;
        private PayInfo payInfo;
        private Regex regex;
        private MatchCollection matchCollection;

        public void SetKey(string key)
        {
            if (!string.IsNullOrWhiteSpace(key))
                this.key = key;
            else
                throw new ArgumentException("Key is empty.");
        }
        public void SetOrderId(string orderId)
        {
            regex = new Regex("[0-9A-Za-z-]{1,50}");
            matchCollection = regex.Matches(orderId);

            if (matchCollection.Count > 0)
                this.orderId = matchCollection[0].ToString();
            else
                throw new ArgumentException("OrderId does not meet requirements.");
        }
        public void SetAmount(int amount)
        {
            regex = new Regex("^[0-9]{0,2147483647}");
            matchCollection = regex.Matches(amount.ToString());

            if (matchCollection.Count > 0)
                this.amount = matchCollection[0].ToString();
            else
                throw new ArgumentException("Amount exceeds limits.");
        }
        public void SetPayInfo(PayInfo payInfo)
        {
            if (payInfo != null)
                this.payInfo = payInfo;
            else
                throw new ArgumentException("PayInfo cannot be null.");
        }
        public string ToParametersString()
        {
            return "Key=" + key + "&OrderId=" + orderId + "&Amount=" + amount + "&PayInfo=" + payInfo.ToParametersString();
        }
    }
}
