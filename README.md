Il progetto è diviso in 3 sezioni: una libreria di classi di utilizzo comune, un servizio SOAP e un'app web MVC che consuma il servizio.
All'interno dell'applicazione MVC sarà possibile visualizzare tutte le previsioni del meteo, oppure scegliendo un giorno dalla tabella sarà possibile leggere il meteo del giorno in questione.
Per l'avvio del progetto selezionare "Docker Compose" come progetto d'avvio su Visual Studio, in caso non lo facesse da solo.

L'endpoint del web service, utile ad eventuali test con SOAPUI, è raggiungibile all'indirizzo http://localhost:1111/servizio.wsdl.
L'app MVC è disponibile all'indirizzo http://localhost:2222/.
