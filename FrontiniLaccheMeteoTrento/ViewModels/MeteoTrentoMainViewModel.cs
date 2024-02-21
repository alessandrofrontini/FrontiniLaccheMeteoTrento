using ClassiUtility.Modelli;

namespace FrontiniLaccheMeteoTrento.ViewModels
{
    public class MeteoTrentoMainViewModel
    {
        public JSONToModel jsontomodel { get; set; }

        public MeteoTrentoMainViewModel(JSONToModel model)
        {
            this.jsontomodel = model;
        }


    }
}
