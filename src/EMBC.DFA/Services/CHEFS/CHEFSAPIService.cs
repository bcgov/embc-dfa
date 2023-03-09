using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

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

        public CHEFSAPIService(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _client = httpClient;
        }

        public async Task<IEnumerable<SmbForm>> GetSmbSubmissions()
        {
            var _responseContent = await GetSubmissionsForType(FormType.SMB);
            var options = new JsonSerializerOptions();
            options.Converters.Add(new JsonInt32Converter());
            var result = JsonSerializer.Deserialize<IEnumerable<CHEFSmbResponse>>(_responseContent, options);
            if (result != null)
            {
                foreach (var res in result)
                {
                    res.submission.SubmissionId = res.id;
                    res.submission.ConfirmationId = res.confirmationId;
                }
            }
            var ret = result.Select(s => s.submission);
            return ret;
        }

        public async Task<IEnumerable<IndForm>> GetIndSubmissions()
        {
            var _responseContent = await GetSubmissionsForType(FormType.IND);
            var options = new JsonSerializerOptions();
            options.Converters.Add(new JsonInt32Converter());
            var result = JsonSerializer.Deserialize<IEnumerable<CHEFIndResponse>>(_responseContent, options);
            if (result != null)
            {
                foreach (var res in result)
                {
                    res.submission.SubmissionId = res.id;
                    res.submission.ConfirmationId = res.confirmationId;
                }
            }
            var ret = result.Select(s => s.submission);
            return ret;
        }

        public async Task<IEnumerable<GovForm>> GetGovSubmissions()
        {
            var _responseContent = await GetSubmissionsForType(FormType.GOV);
            var options = new JsonSerializerOptions();
            options.Converters.Add(new JsonInt32Converter());
            var result = JsonSerializer.Deserialize<IEnumerable<CHEFGovResponse>>(_responseContent, options);
            if (result != null)
            {
                foreach (var res in result)
                {
                    res.submission.SubmissionId = res.id;
                    res.submission.ConfirmationId = res.confirmationId;
                }
            }
            var ret = result.Select(s => s.submission);
            return ret;
        }

        private async Task<string> GetSubmissionsForType(FormType type)
        {
            var formId = GetFormIdForType(type);
            var versionId = await GetVersionId(type, formId);
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
                var versionInfo = result.versions.FirstOrDefault();
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
