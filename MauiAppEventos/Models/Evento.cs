using System;

namespace MauiAppEventos.Models
{
    public class Evento
    {
        public string Nome { get; set; }
        public DateTime DataInicio { get; set; } = DateTime.Today;
        public DateTime DataTermino { get; set; } = DateTime.Today;
        public string NumeroParticipantes { get; set; }  // Alterado de int para string
        public string Local { get; set; }
        public string CustoPorParticipante { get; set; } // Alterado de double para string

        public int DuracaoDias => (DataTermino - DataInicio).Days + 1;

        public string SufixoDuracao => DuracaoDias == 1 ? "dia" : "dias";

        public string DuracaoFormatada
        {
            get
            {
                return $"Duração: {DuracaoDias} {SufixoDuracao}";
            }
        }

        public double CustoTotal
        {
            get
            {
                int participantes = int.TryParse(NumeroParticipantes, out var n) ? n : 0;
                double custo = double.TryParse(CustoPorParticipante, out var c) ? c : 0;
                return participantes * custo;
            }
        }
    }
}