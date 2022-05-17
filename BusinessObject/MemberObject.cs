using System.Collections.Generic;

namespace BusinessLogic
{
    public class MemberObject
    {
        public MemberObject()
        {
            Orders = new HashSet<OrderObject>();
        }

        public int MemberId { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Password { get; set; }

        public virtual ICollection<OrderObject> Orders { get; set; }
    }
}