using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Notifications;
using Windows.UI.Notifications.Management;
namespace HookToast
{
    class HookClass
    {
        private static MainPage mainPage;
        public int interval { get; set; } = 10;
        public string[] hookTitle { get; set; } = { };
        public string[] hookBody { get; set; } = { };
        public HookClass(MainPage mp)
        {
            mainPage = mp;
        }
        public async void start()
        {

            UserNotificationListener listener = UserNotificationListener.Current;
            while (true)
            {
                bool playFlg = false; string titleText = ""; string bodyText = "";int toastn = 0;
                IReadOnlyList<UserNotification> notifs = await listener.GetNotificationsAsync(NotificationKinds.Toast);
                foreach (var n in notifs)
                {
                    NotificationBinding toastBinding = n.Notification.Visual.GetBinding(KnownNotificationBindings.ToastGeneric);

                    if (toastBinding != null)
                    {
                        IReadOnlyList<AdaptiveNotificationText> textElements = toastBinding.GetTextElements();
                        titleText = textElements.FirstOrDefault()?.Text;
                        bodyText = string.Join("\n", textElements.Skip(1).Select(t => t.Text));
                        playFlg = checkTitle(titleText, bodyText);
                        if (playFlg) {
                            toastn++;
                        }
                    }
                }
                if (playFlg)
                {
                    
                    mainPage.AlertSountd.Volume = 1;
                    mainPage.AlertSountd.Play();
                    mainPage.textbox1.Text = titleText;
                    mainPage.textbox2.Text = bodyText;
                    await Task.Delay((int)((interval * 1000) / (toastn * 0.8)));
                }
                await Task.Delay(4 * 1000);
            }
        }
        private bool checkTitle(string title,string body)
        {
            foreach (string str in hookTitle)
            {
                if (title.IndexOf(str) >= 0)
                {
                    return true;
                }
            }
            foreach (string str in hookBody)
            {
                if (body.IndexOf(str) >= 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
