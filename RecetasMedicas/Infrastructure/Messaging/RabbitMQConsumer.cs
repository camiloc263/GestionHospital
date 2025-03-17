using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RecetasMedicas.Application.Commands;
using RecetasMedicas.Application.DTO;
using System;
using System.Text;
using System.Threading.Tasks;

namespace RecetasMedicas.Infrastructure.Messaging
{
    public class RabbitMQConsumer
    {
        private const string QueueName = "recetas_queue";
        private const string ExchangeName = "recetas_exchange";
        private readonly IMediator _mediator;


        public RabbitMQConsumer(IMediator mediator)
        {
           _mediator = mediator;
        }

        public async Task Escuchar()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = await factory.CreateConnectionAsync())
            using (var channel = await connection.CreateChannelAsync())
            {
                await channel.ExchangeDeclareAsync(ExchangeName, ExchangeType.Fanout, durable: true);
                await channel.QueueDeclareAsync(QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
                await channel.QueueBindAsync(QueueName, ExchangeName, "");

                var consumer = new AsyncEventingBasicConsumer(channel);
                consumer.ReceivedAsync += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($"[✔] Mensaje recibido: {message}");

                    try
                    {
                        // Deserializa el mensaje
                        var recetaDTO = JsonConvert.DeserializeObject<FormulaMedicaDTO>(message);

                        var command = new AddFormulaCommand(recetaDTO);
                        var id = await _mediator.Send(command);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[❌] Error al procesar el mensaje: {ex.Message}");
                    }
                };

                await channel.BasicConsumeAsync(queue: QueueName, autoAck: true, consumer: consumer);


                await Task.Delay(-1);
            }
        }


    }
}











