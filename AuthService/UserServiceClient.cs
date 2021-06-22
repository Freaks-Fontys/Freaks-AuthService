using AuthService.Config;
using AuthService.Models;
using Microsoft.Extensions.Options;
using Nancy.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AuthService
{
    public class UserServiceClient
    {
        HttpClient httpClient;
        UserServiceHostConfig config;
        public UserServiceClient(IOptions<UserServiceHostConfig> options)
        {
            config = options.Value;
            httpClient = new HttpClient();
        }


        public async Task<User> SendUserCreated(User user)
        {
            var jsonobject = new JavaScriptSerializer().Serialize(user);
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(config.URI),
                Content = new StringContent(jsonobject,
                                    Encoding.UTF8,
                                    "application/json")
            };

            HttpResponseMessage response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var body = await response.Content.ReadAsStringAsync();

            User returnedUser = new JavaScriptSerializer().Deserialize<User>(body);

            return returnedUser;
        }
    }
}
