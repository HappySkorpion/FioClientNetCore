using HappySkorpion.FioClient.Internal;
using HappySkorpion.FioClient.Internal.Dtos;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using HappySkorpion.FioClient.Internal.Validations;
using System.Web;

namespace HappySkorpion.FioClient
{
    /// <summary>
    /// Client for communication with FIO API.
    /// </summary>
    public class ApiClient
        : IDisposable
    {
        private const string BaseUrl = "https://www.fio.cz/ib_api";
        private const string DateFormat = "yyyy-MM-dd";

        private readonly string _authToken;
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Api client contructor.
        /// </summary>
        /// <param name="authToken">API token.</param>
        /// <remarks>Token can be created in FIO internet banking on page https://ib.fio.sk/ib/wicket/page/NastaveniPage?4 .</remarks>
        public ApiClient(string authToken)
        {
            _authToken = authToken;
            _httpClient = new HttpClient();
        }

        /// <summary>
        /// List account transactions in specified time period.
        /// </summary>
        /// <param name="from">Date from.</param>
        /// <param name="to">Date to.</param>
        /// <param name="ctx">Cancellation token.</param>
        /// <returns>List of transactions.</returns>
        public async Task<AccountTransactionsResult> ListTransactionsAsync(
            DateTime from, 
            DateTime to, 
            CancellationToken ctx = default)
        {
            var url = new Uri($"{BaseUrl}/rest/periods/{HttpUtility.UrlEncode(_authToken)}/{FormatParam(from)}/{FormatParam(to)}/transactions.xml");

            var statement = await GetAccountStatementAsync(url, ctx)
                .ConfigureAwait(false);

            return MapperHelper.MapToAccountTransactionsResult(statement);
        }

        /// <summary>
        /// List account transactions for specified year and extraction id.
        /// </summary>
        /// <param name="year">Year.</param>
        /// <param name="extractionId">Extraction id.</param>
        /// <param name="ctx">Cancellation token.</param>
        /// <returns>List of transactions.</returns>
        public async Task<AccountTransactionsResult> ListTransactionsAsync(
            int year,
            int extractionId,
            CancellationToken ctx = default)
        {
            var url = new Uri($"{BaseUrl}/rest/by-id/{HttpUtility.UrlEncode(_authToken)}/{year}/{extractionId}/transactions.xml");

            var statement = await GetAccountStatementAsync(url, ctx)
                .ConfigureAwait(false);

            return MapperHelper.MapToAccountTransactionsResult(statement);
        }

        /// <summary>
        /// List all transactions since last transaction listing.
        /// </summary>
        /// <param name="ctx">Cancellation token.</param>
        /// <returns>List of transactions.</returns>
        public async Task<AccountTransactionsResult> ListLastTransactionsAsync(
            CancellationToken ctx = default)
        {
            var url = new Uri($"{BaseUrl}/rest/last/{HttpUtility.UrlEncode(_authToken)}/transactions.xml");

            var statement = await GetAccountStatementAsync(url, ctx)
                .ConfigureAwait(false);

            return MapperHelper.MapToAccountTransactionsResult(statement);
        }

        /// <summary>
        /// Set the backstop for next call to last transactions listing.
        /// </summary>
        /// <param name="date">Date to which set the backstop.</param>
        /// <param name="ctx">Cancellation token.</param>
        public async Task SetLastTransactionAsync(
            DateTime date,
            CancellationToken ctx = default)
        {
            var url = new Uri($"{BaseUrl}/rest/set-last-date/{HttpUtility.UrlEncode(_authToken)}/{FormatParam(date)}/");

            using var response = await _httpClient.GetAsync(url, ctx)
                .ConfigureAwait(false);

            using var successfulResponse = response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Set the backstop for next call to last transactions listing.
        /// </summary>
        /// <param name="transactionId">Last transaction id to which set the backstop.</param>
        /// <param name="ctx">Cancellation token.</param>
        public async Task SetLastTransactionAsync(
            int transactionId,
            CancellationToken ctx = default)
        {
            var url = new Uri($"{BaseUrl}/rest/set-last-id/{HttpUtility.UrlEncode(_authToken)}/{transactionId}/");

            using var response = await _httpClient.GetAsync(url, ctx)
                .ConfigureAwait(false);

            using var successfulResponse = response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Send domestic transactions order.
        /// </summary>
        /// <param name="transactionOrders">Domestic transaction orders.</param>
        /// <param name="ctx">Cancellation token.</param>
        public async Task SendTransactionOrdersAsync(
            IEnumerable<DomesticTransactionOrder> transactionOrders,
            CancellationToken ctx = default)
        {
            await new CollectionValidation<DomesticTransactionOrder, DomesticTransactionOrderValidation>()
                .ValidateAndThrowAsync(transactionOrders, ctx)
                .ConfigureAwait(false);

            var import = MapperHelper.MapToDomesticTransactionsImport(transactionOrders);

            await SendTransactionOrderAsync(import, ctx)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Send domestic transaction order.
        /// </summary>
        /// <param name="transactionOrder">Domestic transaction order.</param>
        /// <param name="ctx">Cancellation token.</param>
        public async Task SendTransactionOrderAsync(
            DomesticTransactionOrder transactionOrder,
            CancellationToken ctx = default)
        {
            await new DomesticTransactionOrderValidation()
                .ValidateAndThrowAsync(transactionOrder, ctx)
                .ConfigureAwait(false);

            var import = MapperHelper.MapToDomesticTransactionsImport(new[] { transactionOrder });

            await SendTransactionOrderAsync(import, ctx)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Send euro transactions order.
        /// </summary>
        /// <param name="transactionOrders">Euro transactions order.</param>
        /// <param name="ctx">Cancellation token.</param>
        public async Task SendTransactionOrdersAsync(
            IEnumerable<EuroTransactionOrder> transactionOrders,
            CancellationToken ctx = default)
        {
            await new CollectionValidation<EuroTransactionOrder, EuroTransactionOrderValidation>()
                .ValidateAndThrowAsync(transactionOrders, ctx)
                .ConfigureAwait(false);

            var import = MapperHelper.MapToT2TransactionsImport(transactionOrders);

            await SendTransactionOrderAsync(import, ctx)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Send euro transaction order.
        /// </summary>
        /// <param name="transactionOrder">Euro transaction order.</param>
        /// <param name="ctx">Cancellation token.</param>
        public async Task SendTransactionOrderAsync(
            EuroTransactionOrder transactionOrder,
            CancellationToken ctx = default)
        {
            await new EuroTransactionOrderValidation()
                .ValidateAndThrowAsync(transactionOrder, ctx)
                .ConfigureAwait(false);

            var import = MapperHelper.MapToT2TransactionsImport(new[] { transactionOrder });

            await SendTransactionOrderAsync(import, ctx)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Send foreign transactions order.
        /// </summary>
        /// <param name="transactionOrders">Foreign transactions order.</param>
        /// <param name="ctx">Cancellation token.</param>
        public async Task SendTransactionOrdersAsync(
            IEnumerable<ForeignTransactionOrder> transactionOrders,
            CancellationToken ctx = default)
        {
            await new CollectionValidation<ForeignTransactionOrder, ForeignTransactionOrderValidation>()
                .ValidateAndThrowAsync(transactionOrders, ctx)
                .ConfigureAwait(false);

            var import = MapperHelper.MapToForeignTransactionsImport(transactionOrders);

            await SendTransactionOrderAsync(import, ctx)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Send foreign transaction order.
        /// </summary>
        /// <param name="transactionOrder">Foreign transaction order.</param>
        /// <param name="ctx">Cancellation token.</param>
        public async Task SendTransactionOrderAsync(
            ForeignTransactionOrder transactionOrder,
            CancellationToken ctx = default)
        {
            await new ForeignTransactionOrderValidation()
                .ValidateAndThrowAsync(transactionOrder, ctx)
                .ConfigureAwait(false);

            var import = MapperHelper.MapToForeignTransactionsImport(new[] { transactionOrder });

            await SendTransactionOrderAsync(import, ctx)
                .ConfigureAwait(false);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0028:Simplify collection initialization")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "Not needed")]
        private async Task SendTransactionOrderAsync<TTransaction>(
            Import<TTransaction> import,
            CancellationToken ctx)
        {
            var url = new Uri($"{BaseUrl}/rest/import/");

            using var content = new MultipartFormDataContent();
            content.Add(new StringContent(_authToken), "token");
            content.Add(new StringContent("xml"), "type");
            content.Add(new StringContent("lng"), "en");
            content.Add(new StringContent(XmlSerializerHelper.Serialize(import)), "file");

            using var response = await _httpClient.PostAsync(url, content, ctx)
                .ConfigureAwait(false);

            using var successfulResponse = response.EnsureSuccessStatusCode();
        }

        private async Task<AccountStatement> GetAccountStatementAsync(
            Uri url, 
            CancellationToken ctx)
        {
            using var response = await _httpClient.GetAsync(url, ctx)
                .ConfigureAwait(false);

            using var successResponse = response.EnsureSuccessStatusCode();

            using var content = await successResponse.Content.ReadAsStreamAsync()
                .ConfigureAwait(false);

            return XmlSerializerHelper.Deserialize<AccountStatement>(content);
        }

        private static string FormatParam(DateTime datetime)
        {
            return datetime.ToString(DateFormat, CultureInfo.InvariantCulture);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _httpClient.Dispose();
            }
        }
    }
}