using MauiAppEventos.Models;

namespace MauiAppEventos.Views
{
    public partial class ResumoEvento : ContentPage
    {
        public ResumoEvento(Evento evento)
        {
            InitializeComponent();
            BindingContext = evento;
        }

        private async void OnVoltarClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void OnConfirmarClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Evento Confirmado", "O evento foi confirmado e enviado para a agenda de eventos.", "OK");
        }
    }
}