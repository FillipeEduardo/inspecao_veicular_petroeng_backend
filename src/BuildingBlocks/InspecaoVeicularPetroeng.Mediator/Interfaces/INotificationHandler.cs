namespace InspecaoVeicularPetroeng.Mediator.Interfaces;

public interface INotificationHandler<TNotification> where TNotification : INotification
{
    Task Handler(TNotification notification, CancellationToken cancellationToken);
}