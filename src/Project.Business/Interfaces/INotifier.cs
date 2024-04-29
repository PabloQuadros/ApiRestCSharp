using Project.Business.Notifications;

namespace Project.Business.Interfaces;

public interface INotifier
{
    bool HasNotification();
    List<Notification> GetNotifications();
    void Handle(Notification notification);
}
