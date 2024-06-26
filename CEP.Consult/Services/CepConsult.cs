﻿using CEP.Consult.Controllers.Response;
using Newtonsoft.Json;

namespace CEP.Consult.Services
{
    public class CepConsult
    {
        public static async Task<ApiResponse<ResponseData>> Get(string CEP)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                CancellationTokenSource cts = new CancellationTokenSource();
                ApiResponse<ResponseData> result = new ApiResponse<ResponseData>();

                string openCepUrl = $"https://opencep.com/v1/{CEP}";
                string viaCepUrl = $"https://viacep.com.br/ws/{CEP}/json/";
                try
                {
                    List<Task<ResponseData?>> taskList = new List<Task<ResponseData?>>()
                    {
                        RequestCEPAsync(openCepUrl, httpClient, cts.Token),
                        RequestCEPAsync(viaCepUrl, httpClient, cts.Token)
                    };

                    var completedTask = await Task.WhenAny(taskList.FirstOrDefault(x => x.Result is not null));

                    cts.Cancel();

                    if (completedTask.Result is not null)
                    {
                        result.Status = Status.Success;
                        result.Data = completedTask.Result;
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    return new ApiResponse<ResponseData>
                    {
                        Status = Status.Error,
                    };
                }
            }
        }

        private static async Task<ResponseData?> RequestCEPAsync(string url, HttpClient client, CancellationToken token)
        {
            HttpResponseMessage response = await client.GetAsync(url, token);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<ResponseData>(await response.Content.ReadAsStringAsync());

            return null;
        }
    }
}
