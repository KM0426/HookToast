using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Notifications;
using Windows.UI.Notifications.Management;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;
// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x411 を参照してください

namespace HookToast
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            HookClass hookClass = new HookClass(this);
            hookClass.hookTitle = new string[] { "ラピッド・ストレージ・テクノロジー・エンタープライズ", "Rapid Storage Technology" };
            hookClass.hookBody = new string[] { "劣化", "不明", "削除", "詳細を参照"};
            hookClass.start();
            bool isFirstActivate = true;
            Windows.UI.Xaml.Window.Current.Activated += async (s, e) =>
            {
                if (e.WindowActivationState == CoreWindowActivationState.CodeActivated
                    && isFirstActivate)
                {
                    isFirstActivate = false;

                    // ウインドウが初めてアクティブになったとき、 CompactOverlay にする。
                    await ApplicationView.GetForCurrentView()
                      .TryEnterViewModeAsync(ApplicationViewMode.CompactOverlay);
                }
            };
        }
    }
}
