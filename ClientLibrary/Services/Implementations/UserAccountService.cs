using ServerLibrary.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientLibrary.Services.Contracts;
using BaseLibrar.Responses;
using BaseLibrar.DTOs;
using ClientLibrary.Helpers;
using System.Net.Http.Json;

namespace ClientLibrary.Services.Implementations
{
    public class UserAccountService(GetHttpClient getHttpClient) : IUserAccountService
    {
        public const string AuthUrl = "api/authentication";
        public async Task<GeneralResponse> CreateAsync(Register user)
        {
            var httpClient = getHttpClient.GetPublicHttpClient();
            var result = await httpClient.PostAsJsonAsync($"{AuthUrl}/register", user);
            if (!result.IsSuccessStatusCode) return new GeneralResponse(false, " Error ocured");

            return await result.Content.ReadFromJsonAsync<GeneralResponse>()!;
        }
        public async Task<LoginResponse> SignInAsync(Login user)
        {
            var httpClient = getHttpClient.GetPublicHttpClient();
            var result = await httpClient.PostAsJsonAsync($"{AuthUrl}/login", user);
            if (!result.IsSuccessStatusCode) return new LoginResponse(false, " Error ocured");

            return await result.Content.ReadFromJsonAsync<LoginResponse>()!;
        }

        public Task<LoginResponse> RefreshTokenAsync(RefreshToken token)
        {
            throw new NotImplementedException();
        }

        public async Task<WeatherForecast[]> GetWeatherForecast()
        {
            var httpClien = await getHttpClient.GetPrivateHttpClient();
            var result = await httpClien.GetFromJsonAsync<WeatherForecast[]>("api/weatherforecast");
            return result!;
        }
    }
}
