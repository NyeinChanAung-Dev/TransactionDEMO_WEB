using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        //[HttpGet]
        //public async Task<IActionResult> _EVoucherList(short Status = 1, int page = 1, int pageSize = 10)
        //{
        //    GetEVoucherListingRequest _requestData = new GetEVoucherListingRequest()
        //    {
        //        Status = Status,
        //        PageNumber = page,
        //        PageSize = pageSize
        //    };

        //    string tokenString = getTokenString();
        //    var Url = string.Format("api/evoucher/getevoucherlist?status={0}&page={1}&pageSize={2}", Status, page, pageSize);

        //    PagedListModel<GetEVoucherListingResponse> result = await APIRequest<PagedListModel<GetEVoucherListingResponse>>.Get(Url, tokenString);
        //    var model = new PagingModel<GetEVoucherListingResponse>();
        //    if (result.TotalCount > 0 && result.TotalPages > 0)
        //    {
        //        var pagedList = new StaticPagedList<GetEVoucherListingResponse>(result.Results, page, pageSize, result.TotalCount);
        //        model.Results = pagedList;
        //        model.TotalPages = result.TotalPages;
        //        model.TotalCount = result.TotalCount;
        //    }
        //    else
        //    {
        //        model.Results = null;
        //        model.TotalPages = 0;
        //        model.TotalCount = 0;
        //    }

        //    return PartialView("_EVoucherList", model);
        //}

    }
}
