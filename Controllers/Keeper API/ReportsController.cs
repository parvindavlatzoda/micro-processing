using System;
using System.Collections.Generic;
using System.Text;
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
            
            // Идемпотентность
            // Транзакция существует?
            var reportFromRepo = _rep.GetReportTransaction(reportDto.ServiceUpgId,
                reportDto.QpayTransactionId,
                reportDto.ServiceProviderTransactionId);

            if (reportFromRepo != null) {
                // Обновить данные транзакции
                reportFromRepo.QpayPayedAt = reportDto.QpayPayedAt;
                reportFromRepo.Status = reportDto.Status;

                if (!_rep.Save()) {
                    return StatusCode(500, "A problem happened with handling your request.");
                }

                var reportToReturn = Mapper.Map<RubReportDto>(reportFromRepo);
                return CreatedAtRoute("GetRubReport",
                    new { id = reportToReturn.Id },
                    reportToReturn);

            } else {
                // Если нет, то создаем новую
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

        // GET: /api/1.0/keeper/reports/csv
        [Route("csv")]
        [HttpGet]
        [Produces("text/csv")]
        public async Task<IActionResult> GenerateCsvFile([FromQuery] int? serviceId = null,
            [FromQuery] DateTime? from = null,
            [FromQuery] DateTime? to = null) {
            var data = "Created at,QPay ID,Gateway ID,Provider ID,TJS,RUB,Rate,Service,Terminal\n";
            var parameters = new ReportsResourceParameters {
                
            };

            //var transactionsFromRepo = _rep.GetReportTransactions(parameters);
            var transactionsFromRepo = _rep.GetReportTransactions(from, to, serviceId);

            
            foreach (var transaction in transactionsFromRepo) {
                data += transaction.QpayCreatedAt;
                data += "," + transaction.QpayTransactionId;
                data += "," + transaction.GatewayTransactionId;
                data += "," + transaction.ServiceProviderTransactionId;
                data += "," + transaction.AmountInTjs;
                data += "," + transaction.AmountInRub;
                data += "," + transaction.RubRate;

                switch (transaction.ServiceUpgId) {
                    case 323:
                        data += ",Megafon RT (IBT RUB)" ; 
                    break;
                    case 325:
                        data += ",Tcell (IBT RUB)" ; 
                    break;
                    case 326:
                        data += ",Babilon-M (IBT RUB)" ; 
                    break;
                    case 324:
                        data += ",Beeline RT (IBT RUB)" ; 
                    break;
                }
                
                data += "," + transaction.TerminalNumber;
                data += "\n";
            }

            var bytes = UnicodeEncoding.UTF8.GetBytes(data);
            return File(bytes, "text/csv");
        }
    

        ///api/{version:apiVersion}/keeper/reports/aggregated
        [Route("aggregated")]
        [HttpGet]
        [Produces("text/csv")]
        public async Task<IActionResult> GenerateAggregatedReport([FromQuery] DateTime from,
            [FromQuery] DateTime to,
            [FromQuery] int? serviceId = null) {
                var reports = _rep.GetAggregatedReport(from, to, serviceId);
                
                var data = "Date,Sum in TJS,Sum in RUB,Quantity\n";
                
                foreach (var report in reports) {
                    data += report.Date;
                    data += "," + report.AmountInTjs;
                    data += "," + report.AmountInRub;
                    data += "," + report.Quantity + "\n";
                }
                
                var bytes = UnicodeEncoding.UTF8.GetBytes(data);
                return File(bytes, "text/csv");
        }
    }
}