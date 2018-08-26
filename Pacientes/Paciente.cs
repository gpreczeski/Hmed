using System;

namespace Hmed.Pacientes
{
    public class Paciente
    {
        public Paciente()
        {
        }

        public int Id { get; set; }
        
        public string Nome { get; set; }
        public string Leito { get; set; }
        public string Sexo { get; set; }
        public string TipoSanguineo { get; set; }
        public string UnidadeInternacao { get; set; }
        public DateTime? DataNascimento { get; set; }
        
        public string Telefone { get; set; }
        public string Convenio { get; set; }
        public string Atendimento { get; set; }
        public string Prontuario { get; set; }

        public string SexoIdadeFormatado => Sexo + " | " + Idade;
        public string LeitoComLabel => "Leito\n" + Leito;


        public string Idade
        {
            get
            {
                if (!DataNascimento.HasValue) return "Não Informado";
                var idade = new Idade(DataNascimento.Value);
                var resposta = "";
                if (idade.Anos > 0)
                {
                    resposta += idade.Anos + " ano";
                    if (idade.Anos > 1) resposta += "s";
                    if (idade.Meses > 0) resposta += " e ";
                }

                if (idade.Meses > 0)
                {
                    if (idade.Meses == 1)
                        resposta += "1 mês";
                    else
                        resposta += idade.Meses + " meses";
                }

                return resposta;
            }
        }

        public string Stringfyed => this.Nome + " " + this.Leito + " " + this.Sexo + " " + Telefone;
    }
}