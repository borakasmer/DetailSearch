using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace DetaylıAra.Controllers
{
    public class SearchModel
    {
        public List<Customers> CustomerList { get; set; }
        public List<Employees> EmployeeList { get; set; }
        public List<Suppliers> SupplierList { get; set; }
        public string SearchKey { get; set; }
       
    }

    public static class HtmlHelperExtensions
    {
        public static string UppercaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }
    }
}
