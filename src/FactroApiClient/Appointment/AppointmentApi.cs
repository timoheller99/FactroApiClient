namespace FactroApiClient.Appointment
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Net.Http;
    using System.Net.Mime;
    using System.Text;
    using System.Threading.Tasks;

    using FactroApiClient.Appointment.Contracts;

    using Microsoft.Extensions.Logging;

    using Newtonsoft.Json;

    public class AppointmentApi : IAppointmentApi
    {
        private const string BaseClientName = "BaseClient";

        private readonly ILogger<AppointmentApi> logger;

        private readonly IHttpClientFactory httpClientFactory;

        private readonly JsonSerializerSettings jsonSerializerSettings;

        public AppointmentApi(ILogger<AppointmentApi> logger, IHttpClientFactory httpClientFactory)
        {
            this.logger = logger;
            this.httpClientFactory = httpClientFactory;
            this.jsonSerializerSettings = SerializerSettings.JsonSerializerSettings;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException"><paramref name="createAppointmentRequest"/> is null or EmployeeId is null, empty or whitespace</exception>
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

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                const string requestRoute = ApiEndpoints.Appointment.Create;

                var requestString = JsonConvert.SerializeObject(createAppointmentRequest, this.jsonSerializerSettings);
                var requestContent = GetStringContent(requestString);

                var response = await client.PostAsync(requestRoute, requestContent);

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogWarning(
                        "Could not create appointment: '{RequestRoute}' {StatusCode} - '{ReasonPhrase}'}",
                        response.RequestMessage.RequestUri,
                        (int)response.StatusCode,
                        response.ReasonPhrase);

                    return null;
                }

                var responseContentString = await response.Content.ReadAsStringAsync();

                var result =
                    JsonConvert.DeserializeObject<CreateAppointmentResponse>(
                        responseContentString,
                        this.jsonSerializerSettings);

                return result;
            }
        }

        /// <inheritdoc />
        public async Task<IEnumerable<GetAppointmentPayload>> GetAppointmentsAsync()
        {
            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var response = await client.GetAsync(ApiEndpoints.Appointment.GetAll);

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogWarning(
                        "Could not fetch appointments: '{RequestRoute}' {StatusCode} - '{ReasonPhrase}'}",
                        response.RequestMessage.RequestUri,
                        (int)response.StatusCode,
                        response.ReasonPhrase);

                    return null;
                }

                var responseContentString = await response.Content.ReadAsStringAsync();

                var result =
                    JsonConvert.DeserializeObject<List<GetAppointmentPayload>>(
                        responseContentString,
                        this.jsonSerializerSettings);

                return result;
            }
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException"><paramref name="appointmentId"/> is null, empty or whitespace.</exception>
        public async Task<GetAppointmentByIdResponse> GetAppointmentAsync(string appointmentId)
        {
            if (string.IsNullOrWhiteSpace(appointmentId))
            {
                throw new ArgumentNullException(nameof(appointmentId), $"{nameof(appointmentId)} can not be null, empty or whitespace.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = string.Format(CultureInfo.InvariantCulture, ApiEndpoints.Appointment.GetById, appointmentId);

                var response = await client.GetAsync(requestRoute);

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogWarning(
                        "Could not fetch appointment with id '{AppointmentId}': '{RequestRoute}' {StatusCode} - '{ReasonPhrase}'}",
                        appointmentId,
                        response.RequestMessage.RequestUri,
                        (int)response.StatusCode,
                        response.ReasonPhrase);

                    return null;
                }

                var responseContentString = await response.Content.ReadAsStringAsync();

                var result =
                    JsonConvert.DeserializeObject<GetAppointmentByIdResponse>(
                        responseContentString,
                        this.jsonSerializerSettings);

                return result;
            }
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException"><paramref name="appointmentId"/> is null, empty or whitespace.</exception>
        public async Task<UpdateAppointmentResponse> UpdateAppointmentAsync(string appointmentId, UpdateAppointmentRequest updateAppointmentRequest)
        {
            if (string.IsNullOrWhiteSpace(appointmentId))
            {
                throw new ArgumentNullException(nameof(appointmentId), $"{nameof(appointmentId)} can not be null, empty or whitespace.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = string.Format(CultureInfo.InvariantCulture, ApiEndpoints.Appointment.Update, appointmentId);

                var requestString = JsonConvert.SerializeObject(updateAppointmentRequest, this.jsonSerializerSettings);
                var requestContent = GetStringContent(requestString);

                var response = await client.PutAsync(requestRoute, requestContent);

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogWarning(
                        "Could not fetch appointment with id '{AppointmentId}': '{RequestRoute}' {StatusCode} - '{ReasonPhrase}'}",
                        appointmentId,
                        response.RequestMessage.RequestUri,
                        (int)response.StatusCode,
                        response.ReasonPhrase);

                    return null;
                }

                var responseContentString = await response.Content.ReadAsStringAsync();

                var result =
                    JsonConvert.DeserializeObject<UpdateAppointmentResponse>(
                        responseContentString,
                        this.jsonSerializerSettings);

                return result;
            }
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException"><paramref name="appointmentId"/> is null, empty or whitespace.</exception>
        public async Task<DeleteAppointmentResponse> DeleteAppointmentAsync(string appointmentId)
        {
            if (string.IsNullOrWhiteSpace(appointmentId))
            {
                throw new ArgumentNullException(nameof(appointmentId), $"{nameof(appointmentId)} can not be null, empty or whitespace.");
            }

            using (var client = this.httpClientFactory.CreateClient(BaseClientName))
            {
                var requestRoute = string.Format(CultureInfo.InvariantCulture, ApiEndpoints.Appointment.Delete, appointmentId);

                var response = await client.DeleteAsync(requestRoute);

                if (!response.IsSuccessStatusCode)
                {
                    this.logger.LogWarning(
                        "Could not delete appointment with id '{AppointmentId}': '{RequestRoute}' {StatusCode} - '{ReasonPhrase}'}",
                        appointmentId,
                        response.RequestMessage.RequestUri,
                        (int)response.StatusCode,
                        response.ReasonPhrase);

                    return null;
                }

                var responseContentString = await response.Content.ReadAsStringAsync();

                var result =
                    JsonConvert.DeserializeObject<DeleteAppointmentResponse>(
                        responseContentString,
                        this.jsonSerializerSettings);

                return result;
            }
        }

        private static StringContent GetStringContent(string content)
        {
            return new StringContent(content, Encoding.UTF8, MediaTypeNames.Application.Json);
        }
    }
}
