using Project.Business.Notifications;

namespace Project.Business.Interfaces;

public interface INotifier
{
    bool HasNotification();
    List<Notification> GetNotificacoes();
    void Handle(Notification notification);
}
