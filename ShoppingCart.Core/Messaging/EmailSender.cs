// D: EmailSender implements abstraction IMessageSender
using System;

namespace ShoppingCart.Core.Messaging
{
    public class EmailSender : IMessageSender
    {
        public void Send(string to, string content)
        {
            Console.WriteLine($"Sending Email to {to}: {content}");
        }
    }
}