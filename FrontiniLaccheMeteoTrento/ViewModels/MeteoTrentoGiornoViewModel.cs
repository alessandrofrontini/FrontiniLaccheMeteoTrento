using ClassiUtility.Modelli;

namespace FrontiniLaccheMeteoTrento.ViewModels
{
    public class MeteoTrentoGiornoViewModel
    {
        public Giorni giorno { get; set; }

        public MeteoTrentoGiornoViewModel(Giorni g)
        {
            this.giorno = g;
        }
    }
}
