using System.Collections.Generic;
using TimeZoneBack.Models;

namespace TimeZoneBack.ViewModels
{
    public class HomeVM
    {
        public List<Arrival> Arrivals { get; set; }
        public List<PopularItem> PopularItems { get; set; }
    }
}
