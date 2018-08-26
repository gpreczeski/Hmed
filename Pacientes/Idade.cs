using System;

namespace Hmed.Pacientes
{
    public class Idade
    {
        public int Anos;
        public int Meses;
        public int Dias;

        public Idade(DateTime dataNascimento)
        {
            this.Count(dataNascimento);
        }

        private Idade Count(DateTime datNascimento)
        {
            return this.Count(datNascimento, DateTime.Today);
        }

        private Idade Count(DateTime datNascimento, DateTime dataComparacao)
        {
            if ((dataComparacao.Year - datNascimento.Year) > 0 ||
                (((dataComparacao.Year - datNascimento.Year) == 0) && ((datNascimento.Month < dataComparacao.Month) ||
                                                    ((datNascimento.Month == dataComparacao.Month) && (datNascimento.Day <= dataComparacao.Day)))))
            {
                int DaysInBdayMonth = DateTime.DaysInMonth(datNascimento.Year, datNascimento.Month);
                int DaysRemain = dataComparacao.Day + (DaysInBdayMonth - datNascimento.Day);

                if (dataComparacao.Month > datNascimento.Month)
                {
                    this.Anos = dataComparacao.Year - datNascimento.Year;
                    this.Meses = dataComparacao.Month - (datNascimento.Month + 1) + Math.Abs(DaysRemain / DaysInBdayMonth);
                    this.Dias = (DaysRemain % DaysInBdayMonth + DaysInBdayMonth) % DaysInBdayMonth;
                }
                else if (dataComparacao.Month == datNascimento.Month)
                {
                    if (dataComparacao.Day >= datNascimento.Day)
                    {
                        this.Anos = dataComparacao.Year - datNascimento.Year;
                        this.Meses = 0;
                        this.Dias = dataComparacao.Day - datNascimento.Day;
                    }
                    else
                    {
                        this.Anos = (dataComparacao.Year - 1) - datNascimento.Year;
                        this.Meses = 11;
                        this.Dias = DateTime.DaysInMonth(datNascimento.Year, datNascimento.Month) - (datNascimento.Day - dataComparacao.Day);
                    }
                }
                else
                {
                    this.Anos = (dataComparacao.Year - 1) - datNascimento.Year;
                    this.Meses = dataComparacao.Month + (11 - datNascimento.Month) + Math.Abs(DaysRemain / DaysInBdayMonth);
                    this.Dias = (DaysRemain % DaysInBdayMonth + DaysInBdayMonth) % DaysInBdayMonth;
                }
            }
            else
            {
                throw new ArgumentException("Birthday date must be earlier than current date");
            }
            return this;
        }
    }
}