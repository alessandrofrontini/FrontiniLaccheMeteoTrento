using ClassiUtility.Modelli;
using ClassiUtility.Utility;
using Newtonsoft.Json;
using SoapCore;
using System.ServiceModel;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace FrontiniLacche_MeteoTrento_SOAP.BusinessLogic
{
    [ServiceContract]
    public interface ServizioMeteoSOAPInterface
    {
        [OperationContract]
        public JSONToModel estraiTutto();

        [OperationContract]
        public Giorni estraiMeteoGiorno(string giorno);
    }
    public class ServizioMeteoSOAP : ServizioMeteoSOAPInterface
    {

        /// <summary>
        /// Metodo che richiede al servizio REST pubblico del meteo il JSON contenente tutte le informazioni.
        /// </summary>
        /// <returns></returns>
        public JSONToModel estraiTutto()
        {
            return Globals.Estrai();
        }


        /// <summary>
        /// /// Metodo che richiede al servizio REST pubblico del meteo il JSON contenente le informazioni riguardo uno specifico giorno.
        /// </summary>
        /// <param name="giorno"></param>
        /// <returns></returns>
        public Giorni estraiMeteoGiorno(string giorno)
        {
            return Globals.EstraiGiorno(giorno);
        }
    }
}
