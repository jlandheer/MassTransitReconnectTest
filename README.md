# MassTransitReconnectTest

Belongs to Issue https://github.com/MassTransit/MassTransit/issues/1307

* Start all three projects.
* Use the sender to send some messages
* The messages will be received by both Clients
* Stop the RabbitMq service from an administrator prompt: Net stop RabbitMq
* You'll see some `RabbitMQ Connect Failed: Broker unreachable: guest@localhost:5672/` messages in all three windows
* Restart the RabbitMq service from the administrator prompt: Net start RabbitMq
* Wait a couple of seconds to let the Sender and CLients to reconnect
* Use the sender to send some messages
* The **Sync** client will receive the messages
* The **Async** client will **NOT** receive the messages

