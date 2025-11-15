using MauiAppEventos.Models;

namespace MauiAppEventos.Views
{
    public partial class CadastroEvento : ContentPage
    {
        Evento evento = new Evento();

        public CadastroEvento()
        {
            InitializeComponent();
            BindingContext = evento;

            // Limites para Data de Início
            dtpck_inicio.MinimumDate = DateTime.Today;
            dtpck_inicio.MaximumDate = DateTime.Today.AddDays(179);

            // Limites para Data de Término (baseado na data inicial)
            dtpck_termino.MinimumDate = dtpck_inicio.Date;
            dtpck_termino.MaximumDate = dtpck_inicio.Date.AddDays(179);

            // Atualiza limites da data de término quando a data inicial muda
            dtpck_inicio.DateSelected += (s, e) =>
            {
                dtpck_termino.MinimumDate = e.NewDate;
                dtpck_termino.MaximumDate = e.NewDate.AddDays(179);
            };
        }

        private async void OnCadastrarClicked(object sender, EventArgs e)
        {
            // Validação campo a campo
            if (string.IsNullOrWhiteSpace(evento.Nome))
            {
                await DisplayAlert("Campo obrigatório", "Por favor, preencha o nome do evento.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(evento.NumeroParticipantes))
            {
                await DisplayAlert("Campo obrigatório", "Por favor, informe o número de participantes.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(evento.Local))
            {
                await DisplayAlert("Campo obrigatório", "Por favor, informe o local do evento.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(evento.CustoPorParticipante))
            {
                await DisplayAlert("Campo obrigatório", "Por favor, informe o valor por participante em R$.", "OK");
                return;
            }

            // Conversão segura dos campos numéricos
            if (!int.TryParse(evento.NumeroParticipantes, out var numero) || numero <= 0)
            {
                await DisplayAlert("Valor inválido", "O número de participantes deve ser maior que zero.", "OK");
                return;
            }

            if (!double.TryParse(evento.CustoPorParticipante, out var custo) || custo < 0)
            {
                await DisplayAlert("Valor inválido", "O custo por participante não pode ser negativo.", "OK");
                return;
            }

            // Validação de datas
            if (evento.DataTermino < evento.DataInicio)
            {
                await DisplayAlert("Data inválida", "A data final não pode ser anterior à data inicial.", "OK");
                return;
            }

            // Calcula duração do evento
            await DisplayAlert("Duração do Evento", $"O evento terá duração de {evento.DuracaoDias} {evento.SufixoDuracao}.", "OK");

            // Atualiza os valores convertidos no objeto
            evento.NumeroParticipantes = numero.ToString();
            evento.CustoPorParticipante = custo.ToString("F2");

            // Navega para a tela de resumo
            await Navigation.PushAsync(new ResumoEvento(evento));
        }

        private async void OnCancelarClicked(object sender, EventArgs e)
        {
            bool confirmar = await DisplayAlert("Cancelar", "Deseja realmente cancelar o cadastro?", "Sim", "Não");
            if (confirmar)
            {
                await Navigation.PopAsync();
            }
        }
    }
}