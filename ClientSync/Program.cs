using System;
using MassTransit;

namespace ClientSync
{
    class Program
    {
        static void Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
                {
                    var host = sbc.Host(new Uri("rabbitmq://localhost/"),
                        h =>
                        {
                            h.Username("guest");
                            h.Password("guest");
                        });

                    sbc.ReceiveEndpoint(host, "sync",
                        e => e.Handler<Sender.SimpleEvent>(context => Console.Out.WriteLineAsync($"Sync received: {context.Message.Value}")));
                });

            bus.Start();

            Console.WriteLine("SYNC: Listening, press any key to exit");
            Console.ReadKey();

            bus.Stop();
        }
    }
}
