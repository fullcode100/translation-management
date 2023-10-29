using System;
using System.Threading.Tasks;

namespace External.ThirdParty.Services
{
    // DO NOT MODIFY
    // this is an external service out of your hands
    
    public sealed class UnreliableNotificationService : INotificationService
    {
        /// <summary>
        /// Sends notifications. It can be unreliable, please make sure you retry for yourself.
        /// </summary>
        /// <param name="text">Text to send as a notification</param>
        /// <returns>True if notification was sent</returns>
        public async Task<bool> SendNotification(string text)
        {
            await Task.Delay(TimeSpan.FromSeconds(1));

            int response = new Random().Next(0, 3);
            return response switch
            {
                0 => throw new ApplicationException("Oops, properly unreliable service"),
                1 => true,
                _ => false,
            };
        }
    }

    public interface INotificationService
    {
        /// <summary>
        /// Sends notifications. It can be unreliable, please make sure you retry for yourself.
        /// </summary>
        /// <param name="text">Text to send as a notification</param>
        /// <returns>True if notification was sent</returns>
        Task<bool> SendNotification(string text);
    }
}
