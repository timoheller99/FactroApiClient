namespace FactroApiClient.Appointment
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    using FactroApiClient.Appointment.Contracts;
    using FactroApiClient.Appointment.Endpoints;
    using FactroApiClient.SharedContracts;

    using Newtonsoft.Json;

    public class AppointmentApi : IAppointmentApi
    {
        private const string BaseClientName = "BaseClient";

        private readonly IHttpClientFactory httpClientFactory;

        private readonly JsonSerializerSettings jsonSerializerSettings;

        public AppointmentApi(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
            this.jsonSerializerSettings = SerializerSettings.JsonSerializerSettings;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException"><paramref name="createAppointmentRequest"/> is null or EmployeeId or Subject is null, empty or whitespace</exception>
        public async Task<CreateAppointmentResponse> CreateAppointmentAsync(CreateAppointmentRequest createAppointmentRequest)
        {
            if (createAppointmentRequest == null)
            {
                throw new ArgumentNullException(nameof(createAppointmentRequest), $"{nameof(createAppointmentRequest)} can not be null.");
            }

            if (string.IsNullOrWhiteSpace(createAppointmentRequest.EmployeeId))
            {
                throw new ArgumentNullException(nameof(createAppointmentRequest), $"{nameof(createAppointmentRequest.EmployeeId)} can not be null, empty or whitespace.");
            }

            if (createAppointmentRequest.Subject == null)
            {
                throw new ArgumentNullException(nameof(createAppointmentRequest), $"{nameof(createAppointmentRequest.Subject)} can not be null.");
            }

            using var client = this.httpClientFactory.CreateClient(BaseClientName);
            var requestRoute = AppointmentApiEndpoints.Base.Create();

            var requestString = JsonConvert.SerializeObject(createAppointmentRequest, this.jsonSerializerSettings);
            var requestContent = ApiHelpers.GetStringContent(requestString);

            var response = await client.PostAsync(requestRoute, requestContent);

            if (!response.IsSuccessStatusCode)
            {
                throw new FactroApiException(
                    "Could not create appointment.",
                    response.RequestMessage.RequestUri.ToString(),
                    response.StatusCode,
                    response.Content == null ? null : await response.Content.ReadAsStringAsync());
            }

            var responseContentString = await response.Content.ReadAsStringAsync();

            var result =
                JsonConvert.DeserializeObject<CreateAppointmentResponse>(
                    responseContentString,
                    this.jsonSerializerSettings);

            client.Dispose();
            return result;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<GetAppointmentPayload>> GetAppointmentsAsync()
        {
            using var client = this.httpClientFactory.CreateClient(BaseClientName);
            var requestRoute = AppointmentApiEndpoints.Base.GetAll();

            var response = await client.GetAsync(requestRoute);

            if (!response.IsSuccessStatusCode)
            {
                throw new FactroApiException(
                    "Could not fetch appointments.",
                    response.RequestMessage.RequestUri.ToString(),
                    response.StatusCode,
                    response.Content == null ? null : await response.Content.ReadAsStringAsync());
            }

            var responseContentString = await response.Content.ReadAsStringAsync();

            var result =
                JsonConvert.DeserializeObject<List<GetAppointmentPayload>>(
                    responseContentString,
                    this.jsonSerializerSettings);

            client.Dispose();
            return result;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException"><paramref name="appointmentId"/> is null, empty or whitespace.</exception>
        public async Task<GetAppointmentByIdResponse> GetAppointmentByIdAsync(string appointmentId)
        {
            if (string.IsNullOrWhiteSpace(appointmentId))
            {
                throw new ArgumentNullException(nameof(appointmentId), $"{nameof(appointmentId)} can not be null, empty or whitespace.");
            }

            using var client = this.httpClientFactory.CreateClient(BaseClientName);
            var requestRoute = AppointmentApiEndpoints.Base.GetById(appointmentId);

            var response = await client.GetAsync(requestRoute);

            if (!response.IsSuccessStatusCode)
            {
                throw new FactroApiException(
                    $"Could not fetch appointment with id '{appointmentId}'.",
                    response.RequestMessage.RequestUri.ToString(),
                    response.StatusCode,
                    response.Content == null ? null : await response.Content.ReadAsStringAsync());
            }

            var responseContentString = await response.Content.ReadAsStringAsync();

            var result =
                JsonConvert.DeserializeObject<GetAppointmentByIdResponse>(
                    responseContentString,
                    this.jsonSerializerSettings);

            client.Dispose();
            return result;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException"><paramref name="appointmentId"/> is null, empty or whitespace.</exception>
        public async Task<UpdateAppointmentResponse> UpdateAppointmentAsync(string appointmentId, UpdateAppointmentRequest updateAppointmentRequest)
        {
            if (string.IsNullOrWhiteSpace(appointmentId))
            {
                throw new ArgumentNullException(nameof(appointmentId), $"{nameof(appointmentId)} can not be null, empty or whitespace.");
            }

            if (updateAppointmentRequest == null)
            {
                throw new ArgumentNullException(nameof(updateAppointmentRequest), $"{nameof(updateAppointmentRequest)} can not be null.");
            }

            using var client = this.httpClientFactory.CreateClient(BaseClientName);
            var requestRoute = AppointmentApiEndpoints.Base.Update(appointmentId);

            var requestString = JsonConvert.SerializeObject(updateAppointmentRequest, this.jsonSerializerSettings);
            var requestContent = ApiHelpers.GetStringContent(requestString);

            var response = await client.PutAsync(requestRoute, requestContent);

            if (!response.IsSuccessStatusCode)
            {
                throw new FactroApiException(
                    $"Could not update appointment with id '{appointmentId}'.",
                    response.RequestMessage.RequestUri.ToString(),
                    response.StatusCode,
                    response.Content == null ? null : await response.Content.ReadAsStringAsync());
            }

            var responseContentString = await response.Content.ReadAsStringAsync();

            var result =
                JsonConvert.DeserializeObject<UpdateAppointmentResponse>(
                    responseContentString,
                    this.jsonSerializerSettings);

            client.Dispose();
            return result;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException"><paramref name="appointmentId"/> is null, empty or whitespace.</exception>
        public async Task<DeleteAppointmentResponse> DeleteAppointmentAsync(string appointmentId)
        {
            if (string.IsNullOrWhiteSpace(appointmentId))
            {
                throw new ArgumentNullException(nameof(appointmentId), $"{nameof(appointmentId)} can not be null, empty or whitespace.");
            }

            using var client = this.httpClientFactory.CreateClient(BaseClientName);
            var requestRoute = AppointmentApiEndpoints.Base.Delete(appointmentId);

            var response = await client.DeleteAsync(requestRoute);

            if (!response.IsSuccessStatusCode)
            {
                throw new FactroApiException(
                    $"Could not delete appointment with id '{appointmentId}'.",
                    response.RequestMessage.RequestUri.ToString(),
                    response.StatusCode,
                    response.Content == null ? null : await response.Content.ReadAsStringAsync());
            }

            var responseContentString = await response.Content.ReadAsStringAsync();

            var result =
                JsonConvert.DeserializeObject<DeleteAppointmentResponse>(
                    responseContentString,
                    this.jsonSerializerSettings);

            client.Dispose();
            return result;
        }
    }
}
