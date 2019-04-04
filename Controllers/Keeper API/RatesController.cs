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
using MP.Models.Keeper;

namespace MP.Keeper.Controllers {
    [ApiVersion("1.0")]
    [ApiController]
    [Route("/api/{version:apiVersion}/keeper/rates")]
    public class RatesController : ControllerBase {
        private IKeeperRepository _rep;
        private IUrlHelper _urlHelper;

        public RatesController(IUrlHelper urlHelper, IKeeperRepository rep) {
            _urlHelper = urlHelper;
            _rep = rep;
        }

        // GET: /api/1.0/keeper/rates/latests
        [HttpGet("latests")]
        public async Task<IActionResult> GetRate() {
            var rateFromRepo = _rep.GetLastRateForCurrency("RUB");
            if (rateFromRepo == null) {
                return NotFound();
            }

            var data = new {
                Rate = rateFromRepo.Rate,
                Currency = "rub",
                UpdatedAt = rateFromRepo.CreatedAt
            };

            // var rate = Mapper.Map<RateDto>(rateFromRepo);
            return Ok(data);
        }

        // GET: /api/1.0/keeper/rates/5
        [HttpGet("{id}", Name = "GetRate")]
        public async Task<IActionResult> GetRate([FromRoute] Guid id) {
            var rateFromRepo = _rep.GetRateById(id);
            if (rateFromRepo == null) {
                return NotFound();
            }

            var rate = Mapper.Map<RateDto>(rateFromRepo);
            return Ok(rate);
        }

        // GET: /api/1.0/keeper/rates
        [HttpGet(Name = "GetRates")]
        public async Task<IActionResult> GetRates([FromQuery] RatesResourceParameters parameters) {
            var ratesFromRepo = _rep.GetCurrencyRates(parameters);
             
            var paginationMetadata = new {
                totalCount = ratesFromRepo.TotalCount,
                pageSize = ratesFromRepo.PageSize,
                currentPage = ratesFromRepo.CurrentPage,
                totalPages = ratesFromRepo.TotalPages,
                // previousPageLink = previousPageLink,
                // nextPageLink = nextPageLink
            };
            
            Response.Headers.Add("X-Pagination",
                Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));
            
            var rates = Mapper.Map<IEnumerable<RateDto>>(ratesFromRepo);
            return Ok(rates);
        }

        private string CreateRatesResourceUri(RatesResourceParameters parameters, ResourceUriType type) {
            switch (type) {
            case ResourceUriType.PreviousPage:
                return _urlHelper.Link("GetRates",
                    new {
                        pageNumber = parameters.PageNumber - 1,
                        pageSize = parameters.PageSize,
                        searchQuery = parameters.SearchQuery,
                        isoCode = parameters.IsoCode
                    });
            case ResourceUriType.NextPage:
                return _urlHelper.Link("GetRates",
                    new {
                        pageNumber = parameters.PageNumber + 1,
                        pageSize = parameters.PageSize,
                        searchQuery = parameters.SearchQuery,
                        isoCode = parameters.IsoCode
                    });
            default:
                return _urlHelper.Link("GetRates",
                    new {
                        pageNumber = parameters.PageNumber,
                        pageSize = parameters.PageSize,
                        searchQuery = parameters.SearchQuery,
                        isoCode = parameters.IsoCode
                    });
            }
        }

        // POST: /api/1.0/keeper/rates
        [HttpPost]
        public async Task<IActionResult> AddRate ([FromBody] RateForCreationDto rateDto) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            // Если нет, то создаем новую
            var rateEntity = Mapper.Map<CurrencyRate>(rateDto);

            rateEntity.Id = Guid.NewGuid();
            _rep.AddRateForCurrency(rateEntity);

            if (!_rep.Save()) {
                return StatusCode(500, "A problem happened with handling your request.");
            }

            var dataToReturn = Mapper.Map<RateDto>(rateEntity);
            return CreatedAtRoute("GetRate",
                new { id = dataToReturn.Id },
                dataToReturn);
        }
    }
}