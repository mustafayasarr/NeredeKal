# NeredeKal
NeredeKal Assessment

Proje docker-compose ile rabbitmq, elasticsearch ve kibana imageleri alınır. Appsettings.development.json dosyasından konfigürasyonlar yapılır.

NeredeKal.HotelServices.API üzerinden datalar eklendikten sonra NeredeKal.ReportServices.API üzerinden rapor çekme işlemleri yapılır.

Projede rabbitmq için CAP kullanılmıştır. Raporları statik excel olarak kaydedip hazır olduğunda listeleme endpointinden tamamlandı olarak path gösterilmektedir.

apiden rapor servisine Httpclient ile haberleşme sağlanmaktadır. Rapor servisi apiye rabbitmq ile haberleşmektedir.
