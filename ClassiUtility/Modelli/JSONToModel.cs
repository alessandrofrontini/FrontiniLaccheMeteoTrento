using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ClassiUtility.Modelli
{
    [Serializable]
    public class JSONToModel
    {
        public string fonte_da_citare { get; set; }
        public string codice_ipa_titolare { get; set; }
        public string nome_titolare { get; set; }
        public string codice_ipa_editore { get; set; }
        public string nome_editore { get; set; }
        public string dataPubblicazione { get; set; }//
        public int idPrevisione { get; set; }//
        public string evoluzione { get; set; }//
        public string evoluzioneBreve { get; set; }//
        public object[] AllerteList { get; set; }//
        public Previsione[] previsione { get; set; }
    }
}
