using Microsoft.EntityFrameworkCore;

namespace Order.API.Models
{
    //bu classın ayrı bir tablo olmasını istemediğimiz order içinde olmasını istediğimiz için Owned olarak işaretlendi
    [Owned]
    public class Address
    {
        public string Line { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
    }
}
