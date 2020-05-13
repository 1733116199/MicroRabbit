using MicroRabbit.Banking.Domain.Commands;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Banking.Domain.Events;

namespace MicroRabbit.Banking.Domain.CommandHandlers
{
    public class TransferCommandHandler : IRequestHandler<CreateTransferCommand, bool>
    {
        private readonly IEventBus _bus;

        public TransferCommandHandler(IEventBus bus)
        {
            this._bus = bus;
        }
        public Task<bool> Handle(CreateTransferCommand request, CancellationToken cancellationToken)
        {
            //publish event to RabbitMQ
            _bus.Publish(new TransferCreatedEvent(
                request.From,
                request.To,
                request.Amount
            ));

            return Task.FromResult(true);
        }
    }
}