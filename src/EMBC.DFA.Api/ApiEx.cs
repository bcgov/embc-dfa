using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace EMBC.DFA.Api
{
    public static class ApiEx
    {
        public static async Task<Model<T>> ReadJsonModelAsync<T>(this HttpRequest request)
        {
            if (!request.HasJsonContentType()) return Model<T>.Invalid;
            var payload = await request.ReadFromJsonAsync<T>();
            if (payload == null) return Model<T>.Invalid;
            return Model<T>.Valid(payload);
        }

        public static Task ClientError(this HttpResponse response, Exception exception)
        {
            return BadRequest(response, "client error", exception.Message);
        }

        public static Task ServerError(this HttpResponse response, Exception exception)
        {
            return ProblemDetails(response, HttpStatusCode.InternalServerError, "server error", exception.Message);
        }

        public static Task ValidationError(this HttpResponse response, string reason)
        {
            return BadRequest(response, "validation error", reason);
        }

        public static Task BadRequest(this HttpResponse response, string title, string detail)
        {
            return ProblemDetails(response, HttpStatusCode.BadRequest, title, detail);
        }

        private static async Task ProblemDetails(this HttpResponse response, HttpStatusCode httpStatusCode, string title, string detail)
        {
            response.StatusCode = (int)httpStatusCode;
            await response.WriteAsJsonAsync(new ProblemDetails
            {
                Status = (int)httpStatusCode,
                Title = title,
                Detail = detail,
            });
            await response.CompleteAsync();
        }

        public static async Task Created(this HttpResponse response, string reference)
        {
            response.StatusCode = (int)HttpStatusCode.Created;
            await response.WriteAsJsonAsync(new { reference = reference });
            await response.CompleteAsync();
        }
    }

    public struct Model<T>
    {
        public static Model<T> Invalid => new Model<T>(false);

        public static Model<T> Valid(T payload) => new Model<T>(payload);

        private Model(bool isValid)
        {
            IsValid = isValid;
            Payload = default!;
        }

        private Model(T payload)
        {
            Payload = payload;
            IsValid = true;
        }

        public readonly T Payload;
        public readonly bool IsValid;
    }
}
