using ClassiUtility.Modelli;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml;

namespace ClassiUtility.Utility
{
    public static class Globals
    {
        #region URI
        //Uri del servizio REST del meteo di Trento
        public static readonly string uri = "https://www.meteotrentino.it/protcivtn-meteo/api/front/previsioneOpenDataLocalita?localita=TRENTO";

        //Uri dei servizi SOAP implementati
        public static readonly string uriOttieniTuttoSOAP = "http://frontinilacche_meteotrento_soap:80/servizio.wsdl/estraiTutto";
        public static readonly string uriOttieniGiornoSOAP = "http://frontinilacche_meteotrento_soap:80/servizio.wsdl/estraiMeteoGiorno?giorno=";
        #endregion

        #region metodiSOA
        /// <summary>
        /// Metodo globale che ottiene tutte le informazioni del file JSON e le serializza in un oggetto di tipo JSONToModel
        /// </summary>
        /// <param name="uri">Uri dell' API</param>
        /// <returns></returns>
        public static JSONToModel Estrai()
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = client.GetAsync(Globals.uri).Result)
                {
                    using (HttpContent content = response.Content)
                    {
                        //Ottenimento del JSON dal servizio REST
                        string result = content.ReadAsStringAsync().Result;

                        //Deserializing del JSON in un oggetto (modello)
                        JSONToModel jsontomodel = JsonConvert.DeserializeObject<JSONToModel>(result);

                        return jsontomodel;
                    }
                }
            }
        }


        /// <summary>
        /// Metodo globale che permette di ottenere, a partire dal metodo precedente, il meteo di un giorno specifico
        /// </summary>
        /// <param name="giorno">Giorno desiderato</param>
        /// <returns></returns>
        public static Giorni EstraiGiorno(string giorno)
        {
            //Ottenimento di tutte le informazioni dal servizio REST e deserializzate in un oggetto
            JSONToModel raw = Globals.Estrai();

            foreach (var previsione in raw.previsione)
            {
                //si ottengono le informazioni del giorno desiderato
                return previsione.giorni.First(giorni => giorni.giorno.Equals(giorno));
            }

            //Giorno non trovato
            return null;
        }
        #endregion

        #region metodiAppMVC
        /// <summary>
        /// Metodo globale che permette di ottenere tutte le informazioni dal servizio SOAP
        /// </summary>
        /// <returns></returns>
        public static JSONToModel EstraiSOAP()
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = client.GetAsync(uriOttieniTuttoSOAP).Result)
                {
                    using (HttpContent content = response.Content)
                    {
                        //Si ottiene il risultato in XML
                        string result = content.ReadAsStringAsync().Result;

                        //una volta tolti i namespace, che darebbero fastidio nel processo di deserializzazione, si ritorna un oggetto di tipo JSONToModel
                        XElement xmlDocumentWithoutNs = RemoveAllNamespaces(XElement.Parse(result));
                        return Globals.serializzaXMLToJSONToModel(xmlDocumentWithoutNs.ToString());

                    }
                }
            }
        }

        /// <summary>
        /// Metodo globale che permette di ottenere tutte le previsioni di un giorno dal servizio SOAP
        /// </summary>
        /// <param name="giorno">il giorno desiderato</param>
        /// <returns></returns>
        public static Giorni EstraiGiornoSOAP(string giorno)
        {

            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = client.GetAsync(uriOttieniGiornoSOAP + giorno).Result)
                {
                    using (HttpContent content = response.Content)
                    {
                        //si ottiene il risultato in XML
                        string result = content.ReadAsStringAsync().Result;

                        //una volta tolti i namespace, che darebbero fastidio nel processo di deserializzazione, si ritorna un oggetto di tipo Giorni
                        XElement xmlDocumentWithoutNs = RemoveAllNamespaces(XElement.Parse(result));
                        return Globals.serializzaXMLToGiorni(xmlDocumentWithoutNs.ToString());

                    }
                }
            }
        }
        #endregion

        #region metodiPrivati
        /// <summary>
        /// il metodo serializza le informazioni di un documento XML in un oggetto di tipo JSONToModel
        /// </summary>
        /// <param name="xml">xml in formato stringa</param>
        /// <returns></returns>
        private static JSONToModel serializzaXMLToJSONToModel(string xml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(JSONToModel));
            using (StringReader stringReader = new StringReader(xml))
            {
                JSONToModel modello = (JSONToModel)xmlSerializer.Deserialize(stringReader);
                return modello;
            }
        }

        /// <summary>
        /// il metodo serializza le informazioni di un documento XML in un oggetto di tipo Giorni
        /// </summary>
        /// <param name="xml">xml in formato stringa</param>
        /// <returns></returns>
        private static Giorni serializzaXMLToGiorni(string xml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Giorni));
            using (StringReader stringReader = new StringReader(xml))
            {
                Giorni modello = (Giorni)xmlSerializer.Deserialize(stringReader);
                return modello;
            }
        }


        /// <summary>
        /// Il metodo rimuove i namespace in un xml.
        /// </summary>
        /// <param name="xmlDocument"></param>
        /// <returns></returns>
        private static XElement RemoveAllNamespaces(XElement xmlDocument)
        {
            if (!xmlDocument.HasElements)
            {
                XElement xElement = new XElement(xmlDocument.Name.LocalName);
                xElement.Value = xmlDocument.Value;

                foreach (XAttribute attribute in xmlDocument.Attributes())
                    xElement.Add(attribute);

                return xElement;
            }
            return new XElement(xmlDocument.Name.LocalName, xmlDocument.Elements().Select(el => RemoveAllNamespaces(el)));
        }

    }
    #endregion

}

