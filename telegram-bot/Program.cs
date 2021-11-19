using System;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace telegram_bot
{
    class Program
    {
        private static string token { get; set; } = "your_token";
        private static TelegramBotClient client;
        static void Main(string[] args)
        {
            client = new TelegramBotClient(token);
            client.StartReceiving();

            var me = client.GetMeAsync().Result;
            Console.WriteLine($"Bot id:{me.Id}.Bot Name: {me.FirstName}");

            client.OnMessage += OnMessgaeHandler;
            Console.ReadLine();
            client.StartReceiving();
        }

        private static async void OnMessgaeHandler(object sender, MessageEventArgs e)
        {
            var msg = e.Message;
            if (msg.Text != null)
            {
                Console.WriteLine($"i have message with text: {msg.Text}");
                switch (msg.Text)
                {
                    case "Sticker":
                        var stic = await client.SendStickerAsync(chatId: msg.Chat.Id,
                                                                 sticker: "https://cdn.tlgrm.ru/stickers/432/81c/43281cdc-b5f0-47fa-804e-8434da128d2b/192/4.webp",
                                                                 replyToMessageId: msg.MessageId,
                                                                 replyMarkup: GetButtons());
                        break;
                    case "Picture mountain":
                        var pic = await client.SendPhotoAsync(chatId: msg.Chat.Id,
                                                              photo: "https://media.wired.com/photos/598e35fb99d76447c4eb1f28/master/pass/phonepicutres-TA.jpg",
                                                              replyMarkup: GetButtons());
                        break;
                    case "Picture ford":
                        var picture = await client.SendPhotoAsync(chatId: msg.Chat.Id,
                                                                  photo: "https://s1.1zoom.ru/b5050/450/Ford_Shelby_Mustang_480389_3840x2400.jpg",
                                                                  replyMarkup: GetButtons());
                        break;
                    case "video":
                        var video = await client.SendVideoAsync(chatId: msg.Chat.Id,
                                                                video: "https://raw.githubusercontent.com/TelegramBots/book/master/src/docs/video-countdown.mp4",
                                                                thumb: "https://raw.githubusercontent.com/TelegramBots/book/master/src/2/docs/thumb-clock.jpg",
                                                                supportsStreaming: true);
                        break;

                    default:
                        await client.SendTextMessageAsync(msg.Chat.Id, "Choise comands: ", replyMarkup: GetButtons());
                        break;
                }
            }
        }

        private static IReplyMarkup GetButtons()
        {
            return new ReplyKeyboardMarkup()
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton>{new KeyboardButton { Text = "Sticker"}, new KeyboardButton { Text= "Picture mountain" } },
                    new List<KeyboardButton>{new KeyboardButton { Text= "Picture ford" }, new KeyboardButton {Text ="video"} }
                }
            };
        }
    }
}
