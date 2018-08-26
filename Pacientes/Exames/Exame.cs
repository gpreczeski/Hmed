
using System;

namespace Hmed.Pacientes.Exames
{
    public class Exame
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime Data { get; set; }
        public string DataFormatada => Data.ToString("dd/MM/yyyy");

        public bool Carregando
        {
            get;
            set;
        }

    }
}