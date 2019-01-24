using System;
using MassTransit;

namespace Sender
{
    public class SimpleEvent
    {
        public string Value { get; set; }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                sbc.Host(new Uri("rabbitmq://localhost/"),
                    h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
            });

            bus.Start();
            Console.WriteLine("Press any key to send message, 'q' to exit");
            var counter = 1;

            while (Console.ReadKey(true).KeyChar != 'q')
            {
                var ev = new SimpleEvent { Value = $"Message: {counter++}" };
                Console.WriteLine($"Publish: {ev.Value}");
                bus.Publish(ev);
            }

            bus.Stop();
        }
    }
}
