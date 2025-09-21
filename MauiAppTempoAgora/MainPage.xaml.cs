using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Services;
using System.Globalization;
using System.Threading.Tasks;

namespace MauiAppTempoAgora
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txt_cidade.Text))
                {
                    Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);

                    if (t != null)
                    {
                        string dados_previsão = "";

                        dados_previsão = $"Latitude: {t.lat} \n" +
                                         $"Longitude: {t.lon} \n" +
                                         $"Nascer do Sol: {t.sunrise} \n" +
                                         $"Por do Sol: {t.sunset} \n" +
                                         $"Temp Max: {t.temp_max} \n" +
                                         $"Temp Min: {t.temp_min} \n" +
                                         $"Descrição do texto: {t.description} \n" +
                                         $"Velocidade do vento: {t.speed} \n" +
                                         $"Visibilidade: {t.visibility} \n";
                        lbl_res.Text = dados_previsão;
                    } else
                    {
                        lbl_res.Text = "Cidade não encontrada";
                    }
                    
                        
                }
                else
                {
                    lbl_res.Text = "Preencha a cidade";
                }
            }
            catch (HttpRequestException httpEx)
            {
                // Aqui capturamos problemas de conexão
                await DisplayAlert("Erro de Conexão", "Não foi possível acessar a internet. Verifique sua conexão.", "Ok");
            }
            catch (Exception ex) 
            {
                await DisplayAlert("Ops", ex.Message, "Ok");
            }

        }
    }
}
