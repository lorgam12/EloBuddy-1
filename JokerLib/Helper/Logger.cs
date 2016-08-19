using System;
using EloBuddy;
using EloBuddy.SDK.Notifications;

namespace JokerLib.Helper
{
    public static class Logger
    {
        public static void Notification(string Header, string Message, int Time = 10000)
        {
            Notifications.Show(new SimpleNotification(Header, Message), Time);
        }

        public static void WriteChat(string Message)
        {
            Chat.Print(Message);
        }

        public static void WriteConsole(string Message)
        {
            Console.WriteLine(Message);
        }
    }
}
