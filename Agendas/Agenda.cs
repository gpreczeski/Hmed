using System;
using Hmed.Pacientes;

namespace Hmed.Agendas
{
    public class Agenda
    {
        #region Propriedades Públicas

        public DateTime Horario { get; set; }
        public Paciente Paciente { get; set; }
        public string Tipo { get; set; }
        public string HorarioFormatado => Horario.ToString("dd/MM") + "\n" + Horario.ToString("HH:mm");
        #endregion

        #region Construtores

        public Agenda()
        {

        }

        #endregion                        
    }
}