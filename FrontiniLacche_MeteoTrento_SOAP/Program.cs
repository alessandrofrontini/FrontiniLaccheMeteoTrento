using FrontiniLacche_MeteoTrento_SOAP.BusinessLogic;
using SoapCore;
using static SoapCore.DocumentationWriter.SoapDefinition;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSoapCore();
builder.Services.AddScoped<ServizioMeteoSOAPInterface, ServizioMeteoSOAP>();

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.UseSoapEndpoint<ServizioMeteoSOAPInterface>("/servizio.wsdl", new SoapEncoderOptions(), SoapSerializer.XmlSerializer);
});


app.Run();
