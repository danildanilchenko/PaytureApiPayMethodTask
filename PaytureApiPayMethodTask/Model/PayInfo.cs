using System.Net;
using System.Text.RegularExpressions;

namespace PaytureApiPayMethodTask.Model
{
    internal class PayInfo
    {
        private string pan;
        private string eMonth;
        private string eYear;
        private string orderId;
        private string amount;
        private string secureCode;
        private string cardHolder;
        private Regex regex;
        private MatchCollection matchCollection;

        public void SetPan(string pan)
        {
            regex = new Regex("^[0-9]{13,19}");
            matchCollection = regex.Matches(pan);

            if (matchCollection.Count > 0)
                this.pan = matchCollection[0].ToString();
            else
                throw new ArgumentException("PAN does not meet requirements.");
        }
        public void SetEMonth(int month)
        {
            if (month is > 0 and < 13)
                eMonth = month.ToString();
            else
                throw new ArgumentException("Wrong month.");
        }
        public void SetEYear(int year)
        {
            regex = new Regex("[0-9]{2}$");
            matchCollection = regex.Matches(year.ToString());

            if (matchCollection.Count > 0)
                eYear = matchCollection[0].ToString();
            else
                throw new ArgumentException("Year must be two-digit number.");
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
        public void SetSecureCode(int secureCode)
        {
            regex = new Regex("^[0-9]{3,4}");
            matchCollection = regex.Matches(secureCode.ToString());

            if (matchCollection.Count > 0)
                this.secureCode = matchCollection[0].ToString();
            else
                throw new ArgumentException("Wrong secure code.");
        }
        public void SetCardHolder(string cardHolder)
        {
            regex = new Regex("[A-z]{1,}");
            matchCollection = regex.Matches(cardHolder);

            if (matchCollection.Count > 1)
                this.cardHolder = matchCollection[0].ToString() + " " + matchCollection[1].ToString();
            else
                throw new ArgumentException("Unable to recognize cardholder name.");
        }
        public string ToParametersString()
        {
            return WebUtility.UrlEncode("PAN=" + pan + ";") +
                WebUtility.UrlEncode("EMonth=" + eMonth + ";") +
                WebUtility.UrlEncode("EYear=" + eYear + ";") +
                WebUtility.UrlEncode("CardHolder=" + cardHolder + ";") +
                WebUtility.UrlEncode("SecureCode=" + secureCode + ";") +
                WebUtility.UrlEncode("OrderId=" + orderId + ";") +
                WebUtility.UrlEncode("Amount=" + amount);
        }
    }
}
