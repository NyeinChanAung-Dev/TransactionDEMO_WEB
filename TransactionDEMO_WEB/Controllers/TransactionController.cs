using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TransactionDEMO_WEB.APIRepo;
using TransactionDEMO_WEB.Helper;
using TransactionDEMO_WEB.Models;
using X.PagedList;

namespace TransactionDEMO_WEB.Controllers
{
    public class TransactionController : Controller
    {
        public IActionResult Index(int page = 1, int pageSize = 10)
        {
            ViewBag.pagename = "tranIndexPage";
            ViewBag.page = page;
            ViewBag.pageSize = pageSize;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> _GetAllTransactions(int page = 1, int pageSize = 10)
        {
            var Url = string.Format("api/transactions/alltransactions?page={0}&pageSize={1}", page, pageSize);

            PagedListModel<ResponseTransaction> result = await APIRequest<PagedListModel<ResponseTransaction>>.Get(Url);
            var model = new PagingModel<ResponseTransaction>();
            if (result.TotalCount > 0 && result.TotalPages > 0)
            {
                var pagedList = new StaticPagedList<ResponseTransaction>(result.Results, page, pageSize, result.TotalCount);
                model.Results = pagedList;
                model.TotalPages = result.TotalPages;
                model.TotalCount = result.TotalCount;
            }
            else
            {
                model.Results = null;
                model.TotalPages = 0;
                model.TotalCount = 0;
            }

            return PartialView("_GetAllTransactions", model);
        }

        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                string extension = Path.GetExtension(file.FileName);
                List<Transaction> transactionsList = new List<Transaction>();

                if (file.Length > 0)
                {
                    List<string> errors = FileHelper.ValidateUploadFile(file, extension);

                    if (errors.Any())
                    {
                        return Json(errors);
                    }

                    if (extension == ".xml")
                    {
                        transactionsList = FileHelper.ExtractXML(file);
                    }

                    else if (extension == ".csv")
                    {
                        transactionsList = FileHelper.ExtractCSV(file);
                    }

                    errors = TransactionHelper.ValidateTransactions(transactionsList);

                    if (errors.Any())
                    {
                        return Json(errors);
                    }

                    //send to api
                    string Url = "api/transactions/transactions";
                    List<Transaction> result = await APIRequest<List<Transaction>>.Post(Url, transactionsList);
                    if(result.Count() > 0)
                    {
                        return Json("Success");
                    }
                    else
                    {
                        return Json("Error");
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

    }
}
