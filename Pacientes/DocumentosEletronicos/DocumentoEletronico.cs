using System;

namespace Hmed.Pacientes.DocumentosEletronicos
{
    public class DocumentoEletronico
    {
        public int Id { get; set; }
        public string Documento { get; set; }
        public DateTime Data
        {
            get;
            set;
        }


        //public string DocumentoUrl
        //{
        //    get
        //    {
        //        return Apis.ApiDocumentoViewer
        //            .Replace("[idHospital]", Configuration.Instancia.IdHospital.ToString())
        //            .Replace("[id]", Id.ToString());
        //    }
        //}
    }
}