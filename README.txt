The application is an ASP.NET Core Web API that fetches any crypto currency price from multiple sources and presents it to the user.

Prerequisites:
1) .NET 6 installed on the machine

The application exposes three endpoints which are described in great detail on the Swagger page that should open on app run.

App is configured using the appsettings.json file in XM.Assignment.API folder. It has two app domain sections: "Sources" and "DatastoreExpirationTimeHours". 
The latter specifies the amount of hours the price history entity will be stored at the datastore. If no such section is specified, the app will delete a value from the datastore after 24 hours.

To add a new price source to the application the developer has to:

1) Add a new source to the appsettings.json file under "Sources" section

2) Write a IDeserializer implementation for the new source (located in XM.Assignment.Domain.Deserialiazation.Deserializers namespace). 
The name of the class of the implementation must contain the name of the source which was added to config on the previous step 
(e.g. "Bitfinex" => BitfinexDeserializer (case-insensitive)). If the implementation is not provided, the app will use DefaultDeserializer 
implementation, which will try to map the response from the source to { "Price" : decimal, "TimeStamp" : uint } model

3) Write an IUriProducer for the new source (located in XM.Assignment.Domain.UriProducers namespace).
The name of the class of the implementation must contain the name of the source which was added to config on the previous step 
(e.g. "Bitfinex" => BitfinexUrlProducer (case-insensitive)). If the implementation is not provided, the app will use DefaultUrlProducer
implementation, which will create the result URI to the price source in the following way: {source-address-from-the-appsettings}/{provided-currency}

