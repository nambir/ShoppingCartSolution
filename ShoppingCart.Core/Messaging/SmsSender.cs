// D: SMS sender also implements the same IMessageSender abstraction
using System;

namespace ShoppingCart.Core.Messaging
{
    public class SmsSender : IMessageSender
    {
        public void Send(string to, string content)
        {
            Console.WriteLine($"Sending SMS to {to}: {content}");
        }
    }
}