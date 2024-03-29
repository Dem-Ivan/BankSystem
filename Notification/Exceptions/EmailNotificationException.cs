﻿
namespace Notification.Exceptions
{
    public class EmailNotificationException : Exception
    {
        public EmailNotificationException()
        {
        }

        public EmailNotificationException(string message) : base(message)
        {
        }

        public EmailNotificationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
