// I: Interface Segregation Principle - Only message sending capability is abstracted
namespace ShoppingCart.Core.Messaging
{
    public interface IMessageSender
    {
        void Send(string to, string content);
    }
}