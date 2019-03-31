using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MP.Data.Keeper;
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

        // GET: /api/1.0/keeper/reports/5
        [HttpGet("{id}", Name = "GetRubReport")]
        public async Task<IActionResult> GetReport([FromRoute] Guid id) {
            var reportFromRepo = _rep.GetReportTransaction(id);
            if (reportFromRepo == null) {
                return NotFound();
            }

            var report = Mapper.Map<RubReportDto>(reportFromRepo);
            return Ok(report);
        }

        // GET: /api/1.0/keeper/reports
        [HttpGet(Name = "GetRubReports")]
        public async Task<IActionResult> GetRubReports([FromQuery] ReportsResourceParameters parameters) {
            var transactionsFromRepo = _rep.GetReportTransactions(parameters);
             
            var paginationMetadata = new {
                totalCount = transactionsFromRepo.TotalCount,
                pageSize = transactionsFromRepo.PageSize,
                currentPage = transactionsFromRepo.CurrentPage,
                totalPages = transactionsFromRepo.TotalPages,
                // previousPageLink = previousPageLink,
                // nextPageLink = nextPageLink
            };
            
            Response.Headers.Add("X-Pagination",
                Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));
            
            var clients = Mapper.Map<IEnumerable<RubReportDto>>(transactionsFromRepo);
            return Ok(clients);
        }

        private string CreateReportsResourceUri(ReportsResourceParameters parameters, ResourceUriType type) {
            switch (type) {
            case ResourceUriType.PreviousPage:
                return _urlHelper.Link("GetRubReports",
                    new {
                        pageNumber = parameters.PageNumber - 1,
                        pageSize = parameters.PageSize,
                        searchQuery = parameters.SearchQuery
                    });
            case ResourceUriType.NextPage:
                return _urlHelper.Link("GetRubReports",
                    new {
                        pageNumber = parameters.PageNumber + 1,
                        pageSize = parameters.PageSize,
                        searchQuery = parameters.SearchQuery
                    });
            default:
                return _urlHelper.Link("GetRubReports",
                    new {
                        pageNumber = parameters.PageNumber,
                        pageSize = parameters.PageSize,
                        searchQuery = parameters.SearchQuery
                    });
            }
        }

        // POST: /api/1.0/keeper/reports
        [HttpPost]
        public async Task<IActionResult> AddTransactionToReport([FromBody] RubReportForCreationDto reportDto) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            
            var reportEntity = Mapper.Map<RubReport>(reportDto);

            reportEntity.Id = Guid.NewGuid();
            _rep.AddTransactionToReport(reportEntity);

            if (!_rep.Save()) {
                return StatusCode(500, "A problem happened with handling your request.");
            }

            var reportToReturn = Mapper.Map<RubReportDto>(reportEntity);
            return CreatedAtRoute("GetRubReport",
                new { id = reportToReturn.Id },
                reportToReturn);
        }
    }
}