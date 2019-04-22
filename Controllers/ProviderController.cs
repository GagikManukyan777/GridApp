using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GridApp.DataService;
using GridApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace GridApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _basePath;
        private readonly ProviderDataService _providerService;

        public ProviderController(IConfiguration configuration)
        {
            _configuration = configuration;
            _basePath = configuration["ProviderApi"];
            _providerService = new ProviderDataService(_basePath);
        }

        [HttpGet("[action]")]
        [ProducesResponseType(200, Type = typeof(ProviderGridItem[]))]
        public IActionResult ProviderList()
        {
            return Ok(_providerService.GetProviderListAsync().Result);
        }

        [HttpGet("[action]")]
        [ProducesResponseType(200, Type = typeof(ProviderGridItem))]
        public IActionResult ProviderSearch([FromQuery]string filter, [FromQuery]int pageNumber, [FromQuery]int pageSize)
        {
            var request = new ProviderSearchRequest()
            {
                 PageNumber = pageNumber,
                 PageSize = pageSize,
                 SearchTerm = filter
            };
            return Ok(_providerService.SearchProviderAsync(request).Result);
        }
    }
}