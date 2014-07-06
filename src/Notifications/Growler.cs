using System;
using Growl.Connector;
using Growl.CoreLibrary;

namespace s7.cmDo.Notifications
{
    public static class Growler
    {
        private static Application m_App = new Application("cmDo");
        private static GrowlConnector m_Connector = new GrowlConnector();

        public static NotificationType SuccessNotification = new NotificationType("ADDED", "Task Added");
        public static NotificationType ErrorNotification = new NotificationType("ERROR", "Error");
        public static NotificationType GeneralNotification = new NotificationType("GENERAL", "General");

        public static void Initialize() {
            m_App.Icon = Resources.icon.ToBitmap();
            m_Connector.Register(m_App, new NotificationType[] { SuccessNotification, ErrorNotification, GeneralNotification });
        }

        public static bool Growl(NotificationType nt, string message)
        {
            return Growl(nt, nt.DisplayName, message);
        }

        public static bool Growl(NotificationType nt, string title, string message)
        {
            if (m_Connector == null || ( m_Connector != null && !m_Connector.IsGrowlRunning() ) )
                return false;
            Notification notification = new Notification(m_App.Name, nt.Name, Guid.NewGuid().ToString(), title, message);
            m_Connector.Notify(notification);
            return true;
        }
    }
}
