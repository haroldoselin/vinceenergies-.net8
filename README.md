# OrderSerice-vinceenergy

## Getting started

Aplicação contrtuida no .NET Core 8

Para rodar a aplicação é necesário executar o codigo abaixo para criar o docker do RabbitMQ.

docker pull rabbitmq:3-management
docker run --rm  -it -p 15672:15672 -p 5672:5672 rabbitmq:3-management

Após rodar os comando acima, substituir o IP do arquivo appsettings.json (AppSettings/HostName) pelo IP da rede atual.

Rodar projeto OrderService.WebApi (Visual Studio)
Rodar projeto OrderServiceApp (react)

endpoint-api
https://localhost:7077

frontend
http://localhost:5173/


