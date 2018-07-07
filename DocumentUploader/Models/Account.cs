using System.Collections.Generic;

namespace DocumentUploader.Models
{

    public enum SubscriptionLevel
    {
        Gold, Silver, Bronze
    }

    public class Account
    {

        public string AccountId { get; set; }
        public SubscriptionLevel Subscription { get; set; }

        public List<User> Users { get; set; }

        public Account()
        {
            Users = new List<User>();
 
        }

    }
}
