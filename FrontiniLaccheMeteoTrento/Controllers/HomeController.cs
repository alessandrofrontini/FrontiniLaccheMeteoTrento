using ClassiUtility.Modelli;
using ClassiUtility.Utility;
using FrontiniLacche_MeteoTrento_SOAP.BusinessLogic;
using FrontiniLaccheMeteoTrento.Models;
using FrontiniLaccheMeteoTrento.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Xml;
using static SoapCore.DocumentationWriter.SoapDefinition;

namespace FrontiniLaccheMeteoTrento.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// all'apertura dell'app si estraggono tutti i dati e si carica la pagina principale
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            MeteoTrentoMainViewModel vm = new MeteoTrentoMainViewModel(Globals.EstraiSOAP());
            return View("Views/MeteoTrento/Index.cshtml", vm);
        }

        /// <summary>
        /// si estraggono le previsioni meteo di uno specifico giorno e si apre la pagina di visualizzazione dettagliata
        /// </summary>
        /// <param name="giorno"></param>
        /// <returns></returns>
        public IActionResult Giorno(string giorno)
        {
            MeteoTrentoGiornoViewModel vm = new MeteoTrentoGiornoViewModel(Globals.EstraiGiornoSOAP(giorno));
            return View("Views/MeteoTrento/meteoGiorno.cshtml", vm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
