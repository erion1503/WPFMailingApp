using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using WPFMailingApp.Mail;

namespace WPFMailingApp
{
    /// <summary>
    /// UserSettingWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class UserSettingWindow : Window
    {
        #region "初期化処理"
        public UserSettingWindow()
        {
            InitializeComponent();
        }
        #endregion

        #region "メール送信処理_OKボタン押下"
        /// <summary>
        /// メール送信処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(SendMail01());
            //MessageBox.Show(SendMail02());
        }
        #endregion

        #region "処理キャンセル"
        /// <summary>
        /// 処理キャンセル
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region "送信処理方法１"
        /// <summary>
        /// 送信処理方法１
        /// </summary>
        /// <returns>送信結果メッセージ</returns>
        private String SendMail01()
        {
            // SMTPサーバーを指定する
            var client = new SmtpClient(TextBoxSmtpServerName.Text, int.Parse(TextBoxSmtpServerPort.Text));
            // 送信元、宛先、件名、本文を指定する
            string fromAddress = TextBoxMailSendUser.Text;
            string toAddress = TextBoxMailResponceUser.Text;
            string subject = TextBoxMailHeader.Text;
            string body = TextBoxMailMessage.Text;
            //メールアカウント認証のユーザ名、パスワードを指定する
            string mailUser = TextBoxMailUser.Text;
            string mailPassword = TextBoxMailPassword.Text;
            try
            {
                client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                //ユーザー名とパスワードを設定する
                client.Credentials = new System.Net.NetworkCredential(mailUser, mailPassword);
                //SSLを使用する
                client.EnableSsl = true;

                // Send
                client.Send(fromAddress, toAddress, subject, body);
                //MessageBox.Show("送信しました");
                //後始末（.NET Framework 4.0以降）
                client.Dispose();
            }
            catch (SmtpException ex)
            {
                // SMTP Server connect miss.
                //MessageBox.Show(ex.Message);
                return ex.Message;
            }
            return "送信されました";
        }
        #endregion

        #region "送信処理方法２"
        /// <summary>
        /// 送信処理方法２
        /// </summary>
        /// <returns>送信結果メッセージ</returns>
        private String SendMail02()
        {
            try
            {
            //Hotmailでメールを送信する

            //MailMessageの作成
            MailMessage msg = new MailMessage(
                TextBoxMailSendUser.Text, TextBoxMailResponceUser.Text,
                TextBoxMailHeader.Text,TextBoxMailMessage.Text);

            SmtpClient sc = new SmtpClient();
            //SMTPサーバーなどを設定する
            sc.Host = TextBoxSmtpServerName.Text;
            sc.Port = int.Parse(TextBoxSmtpServerPort.Text);
            sc.DeliveryMethod = SmtpDeliveryMethod.Network;
            //ユーザー名とパスワードを設定する
            sc.Credentials = new System.Net.NetworkCredential(TextBoxMailUser.Text,TextBoxMailPassword.Text);
            //SSLを使用する
            sc.EnableSsl = true;
            //メッセージを送信する
            sc.Send(msg);

            //後始末
            msg.Dispose();
            //後始末（.NET Framework 4.0以降）
            sc.Dispose();
            }
            catch (SmtpException ex)
            {
                // SMTP Server connect miss.
                return ex.Message;
            }
            return "送信されました";
        }
        #endregion
    }
}
