using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace EMBC.DFA.Services.CHEFS
{
    public interface ICHEFSAPIService
    {
        Task<IEnumerable<SmbForm>> GetSmbSubmissions();
        Task<IEnumerable<IndForm>> GetIndSubmissions();
        Task<IEnumerable<GovForm>> GetGovSubmissions();
    }

    public class CHEFSAPIService : ICHEFSAPIService
    {
        private HttpClient _client;
        private IConfiguration _configuration;
        private INotificationManager _notification;

        public CHEFSAPIService(IConfiguration configuration, INotificationManager notification, HttpClient httpClient)
        {
            _configuration = configuration;
            _notification = notification;
            _client = httpClient;
        }

        public async Task<IEnumerable<SmbForm>> GetSmbSubmissions()
        {
            try
            {
                var responseContent = await GetSubmissionsForType(FormType.SMB);
                if (string.IsNullOrEmpty(responseContent)) return Enumerable.Empty<SmbForm>();
                var options = new JsonSerializerOptions { Converters = { new JsonInt32Converter() } };
                var result = JsonSerializer.Deserialize<IEnumerable<CHEFSmbResponse>>(responseContent, options);

                if (result != null)
                {
                    foreach (var res in result)
                    {
                        res.submission.SubmissionId = res.id;
                        res.submission.ConfirmationId = res.confirmationId;
                    }
                }

                var submissions = result
                    .Where(response => !response.deleted && !response.draft)
                    .Select(response => response.submission)
                    .ToList();

                var submissionLimit = _configuration.GetValue("CHEFS_SUBMISSION_LIMIT", 2500);

                if (submissions.Count > submissionLimit)
                {
                    Log.Information("SMB - Over submission limit");
                    await _notification.SendSubmissionLimitWarning(Resources.Submissions.FormType.SMB);
                }

                return submissions;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error retrieving/mapping Smb Submissions from CHEFS");
                throw;
            }
        }

        public async Task<IEnumerable<IndForm>> GetIndSubmissions()
        {
            try
            {
                var responseContent = await GetSubmissionsForType(FormType.IND);
                if (string.IsNullOrEmpty(responseContent)) return Enumerable.Empty<IndForm>();
                var options = new JsonSerializerOptions { Converters = { new JsonInt32Converter() } };
                var result = JsonSerializer.Deserialize<IEnumerable<CHEFIndResponse>>(responseContent, options);

                if (result != null)
                {
                    foreach (var res in result)
                    {
                        res.submission.SubmissionId = res.id;
                        res.submission.ConfirmationId = res.confirmationId;
                    }
                }

                var submissions = result
                    .Where(response => !response.deleted && !response.draft)
                    .Select(response => response.submission)
                    .ToList();

                var submissionLimit = _configuration.GetValue("CHEFS_SUBMISSION_LIMIT", 2500);

                if (submissions.Count > submissionLimit)
                {
                    Log.Information("IND - Over submission limit");
                    await _notification.SendSubmissionLimitWarning(Resources.Submissions.FormType.IND);
                }

                return submissions;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error retrieving/mapping Ind Submissions from CHEFS");
                throw;
            }
        }

        public async Task<IEnumerable<GovForm>> GetGovSubmissions()
        {
            try
            {
                var responseContent = await GetSubmissionsForType(FormType.GOV);
                if (string.IsNullOrEmpty(responseContent)) return Enumerable.Empty<GovForm>();
                var options = new JsonSerializerOptions { Converters = { new JsonInt32Converter() } };
                var result = JsonSerializer.Deserialize<IEnumerable<CHEFGovResponse>>(responseContent, options);

                if (result != null)
                {
                    foreach (var res in result)
                    {
                        res.submission.SubmissionId = res.id;
                        res.submission.ConfirmationId = res.confirmationId;
                    }
                }

                var submissions = result
                    .Where(response => !response.deleted && !response.draft)
                    .Select(response => response.submission)
                    .ToList();

                var submissionLimit = _configuration.GetValue("CHEFS_SUBMISSION_LIMIT", 2500);

                if (submissions.Count > submissionLimit)
                {
                    Log.Information("GOV - Over submission limit");
                    await _notification.SendSubmissionLimitWarning(Resources.Submissions.FormType.GOV);
                }

                return submissions;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error retrieving/mapping Gov Submissions from CHEFS");
                throw;
            }
        }

        private async Task<string> GetSubmissionsForType(FormType type)
        {
            var formId = GetFormIdForType(type);
            var versionId = await GetVersionId(type, formId);
            if (string.IsNullOrEmpty(versionId)) return string.Empty;
            string endpointUrl = $"{_configuration["CHEFS_API_BASE_URI"]}/app/api/v1/forms/{formId}/versions/{versionId}/submissions";
            return await Get(endpointUrl, type);
        }

        private async Task<string> GetVersionId(FormType type, string formId)
        {
            var ret = string.Empty;
            string endpointUrl = $"{_configuration["CHEFS_API_BASE_URI"]}/app/api/v1/forms/{formId}/version";
            string _responseContent = await Get(endpointUrl, type);
            var result = JsonSerializer.Deserialize<CHEFVersionResponse>(_responseContent);
            if (result != null)
            {
                var versionInfo = result.versions.SingleOrDefault(v => v.published);
                if (versionInfo != null) ret = versionInfo.id;
            }
            return ret;
        }

        private string GetFormIdForType(FormType type)
        {
            switch (type)
            {
                case FormType.SMB:
                    return _configuration["SMB_FORM_ID"];
                case FormType.IND:
                    return _configuration["IND_FORM_ID"];
                case FormType.GOV:
                    return _configuration["GOV_FORM_ID"];
                default:
                    return _configuration["SMB_FORM_ID"];
            }
        }

        private async Task<string> Get(string endpointUrl, FormType type)
        {
            try
            {
                var _httpRequest = new HttpRequestMessage(HttpMethod.Get, endpointUrl);
                _httpRequest.Headers.Authorization = GetAuthHeaderForForm(type);

                var _httpResponse = await _client.SendAsync(_httpRequest);
                var _statusCode = _httpResponse.StatusCode;

                if (!new HttpResponseMessage(_statusCode).IsSuccessStatusCode)
                {
                    var ex = new Exception(string.Format("{0} - {1}", _httpResponse.Content, _statusCode));
                    ex.Data.Add(_statusCode, _httpResponse.Content);
                    throw ex;
                }

                return await _httpResponse.Content.ReadAsStringAsync();
            }
            catch
            {
                throw;
            }
        }

        private AuthenticationHeaderValue GetAuthHeaderForForm(FormType type)
        {
            string formId = string.Empty;
            string apiKey = string.Empty;

            switch (type)
            {
                case FormType.SMB:
                    {
                        formId = _configuration["SMB_FORM_ID"];
                        apiKey = _configuration["SMB_KEY"];
                        break;
                    }
                case FormType.IND:
                    {
                        formId = _configuration["IND_FORM_ID"];
                        apiKey = _configuration["IND_KEY"];
                        break;
                    }
                case FormType.GOV:
                    {
                        formId = _configuration["GOV_FORM_ID"];
                        apiKey = _configuration["GOV_KEY"];
                        break;
                    }
            }

            string basicAuth = Convert.ToBase64String(
                        System.Text.ASCIIEncoding.ASCII.GetBytes(
                        $"{formId}:{apiKey}"));
            return new AuthenticationHeaderValue("Basic", basicAuth);
        }

        private enum FormType
        {
            SMB,
            IND,
            GOV
        }
    }


    //CHEFS defaults no value in a number field to an empty string
    //Handle converting an empty string to an int field
    public class JsonInt32Converter : JsonConverter<int>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert == typeof(int);
        }

        public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            try
            {
                var value = reader.GetInt32();
                return value;
            }
            catch
            {
                return 0;
            }
        }

        public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
