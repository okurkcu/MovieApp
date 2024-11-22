using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DAL;

namespace BLL.Models
{
    public class MoviesModel
    {
        public Movie Record { get; set; }

        public string Name => Record.Name;

        public string ReleaseDate => Record.ReleaseDate.HasValue ? Record.ReleaseDate.Value.ToString("MM/dd/yyyy") : "Release date is not set";

        //public string TotalRevenue => Record.TotalRevenue.HasValue ? Record.TotalRevenue.Value.ToString("N0") : string.Empty;

        public string TotalRevenue => Record.TotalRevenue.HasValue ? Record.TotalRevenue.Value.ToString("N0") : "0";

        public string Director => Record.Director?.Name;
    }
}
