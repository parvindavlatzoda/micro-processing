using System;
using System.Collections.Generic;
using System.Linq;
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
    [Route("/api/{version:apiVersion}/keeper/currencies")]
    public class CurrenciesController : ControllerBase {
        private IKeeperRepository _rep;
        private IUrlHelper _urlHelper;

        public CurrenciesController(IUrlHelper urlHelper, IKeeperRepository rep) {
            _urlHelper = urlHelper;
            _rep = rep;
        }

        [HttpGet]
        public IActionResult GetCurrencies() {
            var currenciesFromRepo = _rep.GetCurrencies();
            var currencies = Mapper.Map<IEnumerable<CurrencyDto>>(currenciesFromRepo);

            var data = new {
                Default = currencies.FirstOrDefault(c => c.IsoCode == "RUB"),
                Currencies = currencies
            };

            return Ok(data);
        }

        [HttpGet("{id}", Name = "GetCurrency")]
        public IActionResult GetCurrency([FromRoute] Guid id) {
            var currencyFromRepo = _rep.GetCurrency(id);

            if (currencyFromRepo == null) {
                return NotFound();
            }

            var docType = Mapper.Map<CurrencyDto>(currencyFromRepo);
            return Ok(docType);
        }

        [HttpPost]
        public IActionResult CreateTypeForDocument([FromBody] CurrencyForCreationDto currency) {
            if (currency == null) {
                return BadRequest();
            }

            var currencyEntity = Mapper.Map<Currency>(currency);

            _rep.AddCurrency(currencyEntity);
            if (!_rep.Save()) {
                return StatusCode(500, "A problem happened with handling your request.");
            }

            var currencyToReturn = Mapper.Map<CurrencyDto>(currencyEntity);
            return CreatedAtRoute("GetCurrency",
                new { id = currencyToReturn.Id },
                currencyToReturn);
        }
    }
}