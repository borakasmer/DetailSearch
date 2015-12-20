using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using DAL;
namespace DetaylıAra.Controllers
{
    public class HomeController : Controller
    {
        SearchModel viewModel = null;
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public async Task<PartialViewResult> Search(string searchKey)
        {
            var tasks = new Task[3];
            int i = 0;
            viewModel = new SearchModel();
            viewModel.SearchKey = searchKey;
            List<Task> TaskList = GetSeachResult(searchKey, viewModel);
            foreach (Task tsk in TaskList)
            {
                tasks[i] = tsk;
                i++;
            }
            await Task.WhenAll(tasks);

            return PartialView("ResultView", viewModel);
        }

        private List<Task> GetSeachResult(string search,SearchModel model)
        {
            //NorthContext dbContext = new NorthContext();
            List<Task> Tasks = new List<Task>();
            var taskCustomer = Task.Factory.StartNew(() =>
            {
                using (NorthContext dbContext = new NorthContext())
                {
                    model.CustomerList = dbContext.Customers.Where(cus => cus.ContactName.Contains(search)).ToList();                    
                }
            });
            Tasks.Add(taskCustomer);
            var taskSupplier = Task.Factory.StartNew(() =>
            {
                using (NorthContext dbContext = new NorthContext())
                {
                    model.SupplierList = dbContext.Suppliers.Where(sup => sup.ContactName.Contains(search)).ToList();                    
                }
            });
            Tasks.Add(taskSupplier);
            var taskEmployee = Task.Factory.StartNew(() =>
            {
                using (NorthContext dbContext = new NorthContext())
                {
                    model.EmployeeList = dbContext.Employees.Where(emp => emp.FirstName.Contains(search)).ToList();                    
                }
            });
            Tasks.Add(taskEmployee);
            return Tasks;
        }
    }
}