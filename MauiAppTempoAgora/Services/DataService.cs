using MauiAppTempoAgora.Models;
using Newtonsoft.Json.Linq;


namespace MauiAppTempoAgora.Services
{
    public class DataService
    {
        public static async Task<Tempo?> GetPrevisao(String cidade)
        {
            Tempo? t = null;

            string chave = "95bafda087f628e8be0fcba4b5f5fc6b";
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={cidade}&units=metric&appid={chave}";

            using (HttpClient Client = new HttpClient())
            {
                HttpResponseMessage resp = await Client.GetAsync(url);

                if (resp.IsSuccessStatusCode)
                {
                    string json = await resp.Content.ReadAsStringAsync();

                    var rascunho = JObject.Parse(json);

                    var sunriseUnix = (long)rascunho["sys"]["sunrise"];
                    var sunsetUnix = (long)rascunho["sys"]["sunset"];

                    DateTime sunrise = DateTimeOffset.FromUnixTimeSeconds(sunriseUnix).ToLocalTime().DateTime;
                    DateTime sunset = DateTimeOffset.FromUnixTimeSeconds(sunsetUnix).ToLocalTime().DateTime;

                    t = new()
                    {
                        lat = (double)rascunho["coord"]["lat"],
                        lon = (double)rascunho["coord"]["lon"],

                        description = (string)rascunho["weather"][0]["description"],
                        main = (string)rascunho["weather"][0]["main"],

                        temp_min = (double)rascunho["main"]["temp_min"],
                        temp_max = (double)rascunho["main"]["temp_max"],

                        speed = (double)rascunho["wind"]["speed"],
                        visibility = (int)rascunho["visibility"],

                        sunrise = sunrise.ToString("HH:mm"),
                        sunset = sunset.ToString("HH:mm"),
                    }; // Fecha obj do tempo
                }// Fecha if se o estutos do servidor foi de sucesso
            }// fecha 
            return t;
        }
    }
}
