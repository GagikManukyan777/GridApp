using GridApp.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GridApp.DataService
{
    public class ProviderDataService
    {
        private static readonly HttpClient client = new HttpClient();
        private readonly string _basePath;

        public ProviderDataService(string basePath)
        {
            _basePath = basePath;
        }

        public async Task<ProviderGridItem[]> GetProviderListAsync(string filter = null)
        {
            var result = new ProviderGridItem[] { };

            HttpResponseMessage response = await client.GetAsync(QuerySelectClause()).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<ProviderGridItem[]>();
            }
            return result;
        }

        public async Task<PagedResult<ProviderGridItem>> SearchProviderAsync(ProviderSearchRequest request)
        {
            var items = new ProviderGridItem[] { };
            var pagedResult = new PagedResult<ProviderGridItem>();

            HttpResponseMessage response = await client.GetAsync(QueryWithWhereClause(request)).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                items = await response.Content.ReadAsAsync<ProviderGridItem[]>();
            }

            pagedResult.PageSize = request.PageSize.GetValueOrDefault(100);
            pagedResult.PageNumber = request.PageNumber.GetValueOrDefault(1);
            pagedResult.TotalRowCount = items.Count();
            pagedResult.Items = items.Skip((pagedResult.PageNumber - 1) * pagedResult.PageSize).Take(pagedResult.PageSize).ToArray();

            return pagedResult;
        }

        private string QuerySelectClause(string filter = null)
        {
            return $"{_basePath}{MetaDataHelper.QuerySelect} {MetaDataHelper.GetColumnList()}";
        }

        private string QueryWithWhereClause(ProviderSearchRequest request)
        {
            var selectClause = QuerySelectClause();
            var whereClause = new StringBuilder();

            whereClause.Append(QuerySelectClause());

            if (!string.IsNullOrWhiteSpace(request.SearchTerm ))
            {
                whereClause.Append(" where");
                whereClause.Append($" provider_name like %27%25{request.SearchTerm}%25%27");
                whereClause.Append(" OR ");
                whereClause.Append($"federal_provider_number like %27%25{request.SearchTerm}%25%27");
                whereClause.Append(" OR ");
                whereClause.Append($"provider_zip_code like %27%25{request.SearchTerm}%25%27");
            }

            return whereClause.ToString();
        }
    }
}
