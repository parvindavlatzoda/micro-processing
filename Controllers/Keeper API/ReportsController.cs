using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MP.Helpers;
using MP.Keeper.Helpers;
using MP.Keeper.Models;
using MP.Keeper.Services;

namespace MP.Keeper.Controllers {
    [ApiVersion("1.0")]
    [ApiController]
    [Route("/api/{version:apiVersion}/keeper/reports")]
    public class ReportsController : ControllerBase {
        private IKeeperRepository _rep;
        private IUrlHelper _urlHelper;
        

        public ReportsController(IUrlHelper urlHelper, IKeeperRepository rep) {
            _urlHelper = urlHelper;
            _rep = rep;
        }

        // GET: /api/1.0/keeper/reports
        [HttpGet(Name = "GetReports")]
        public async Task<IActionResult> GetReports([FromRoute]ReportsResourceParameters parameters) {
            var transactionsFromRepo = _rep.GetReportTransactions(parameters);
            
            var previousPageLink = transactionsFromRepo.HasPrevious ?
                CreateReportsResourceUri(parameters,
                    ResourceUriType.PreviousPage) : null;
            
            var nextPageLink = transactionsFromRepo.HasNext ?
                CreateReportsResourceUri(parameters,
                    ResourceUriType.NextPage) : null;

            var paginationMetadata = new {
                totalCount = transactionsFromRepo.TotalCount,
                pageSize = transactionsFromRepo.PageSize,
                currentPage = transactionsFromRepo.CurrentPage,
                totalPages = transactionsFromRepo.TotalPages,
                previousPageLink = previousPageLink,
                nextPageLink = nextPageLink
            };
            
            Response.Headers.Add("X-Pagination",
                Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));
            
            var clients = Mapper.Map<IEnumerable<RubReportDto>>(transactionsFromRepo);
            return Ok(clients);
        }

        private string CreateReportsResourceUri(ReportsResourceParameters parameters, ResourceUriType type) {
            switch (type) {
            case ResourceUriType.PreviousPage:
                return _urlHelper.Link("GetReports",
                    new {
                        firsLetter = parameters.FirstLetter,
                        pageNumber = parameters.PageNumber - 1,
                        pageSize = parameters.PageSize,
                        searchQuery = parameters.SearchQuery
                    });
            case ResourceUriType.NextPage:
                return _urlHelper.Link("GetReports",
                    new {
                        firsLetter = parameters.FirstLetter,
                        pageNumber = parameters.PageNumber + 1,
                        pageSize = parameters.PageSize,
                        searchQuery = parameters.SearchQuery
                    });
            default:
                return _urlHelper.Link("GetReports",
                    new {
                        firsLetter = parameters.FirstLetter,
                        pageNumber = parameters.PageNumber,
                        pageSize = parameters.PageSize,
                        searchQuery = parameters.SearchQuery
                    });
            }
        }

        // POST: /api/1.0/keeper/reports
        // [HttpPost]
        // public async Task<IActionResult> AddTransactionToReport([FromBody]) {

        // }
    }
}