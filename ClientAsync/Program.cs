using System;
using System.Threading.Tasks;
using MassTransit;

namespace ClientAsync
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri("rabbitmq://localhost/"),
                    h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                sbc.ReceiveEndpoint(host, "async",
                    e => e.Handler<Sender.SimpleEvent>(context => Console.Out.WriteLineAsync($"Async received: {context.Message.Value}")));
            });

            await bus.StartAsync();

            Console.WriteLine("ASYNC: Listening, press any key to exit");
            Console.ReadKey();

            await bus.StopAsync();
        }
    }
}
