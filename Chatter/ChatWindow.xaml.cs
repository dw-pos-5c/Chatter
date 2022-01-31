using System;
using System.Windows;
using System.Windows.Interop;

namespace Chatter;
/// <summary>
/// Interaction logic for ChatWindow.xaml
/// </summary>
public partial class ChatWindow : Window, IObserver
{
    private readonly Subject subject;

    public ChatWindow(Subject subject,string clientName)
    {
        InitializeComponent();

        this.subject = subject;
        ClientName = clientName;
        this.subject.Attach(this);

        lblName.Content = clientName;
    }

    public string ClientName { get; }
    public void ClientAttached(string name) {
        lvMessages.Items.Add($"[{DateTime.Now.ToLongTimeString()}] {name}: joined chat");
    }

    public void ClientDetached(string name) {
        lvMessages.Items.Add($"[{DateTime.Now.ToLongTimeString()}] {name}: left chat");
    }

    public void Update(Message msg)
    {
        lvMessages.Items.Add(msg.ToString());
    }

    private void ChatWindow_OnClosed(object? sender, EventArgs e)
    {
        subject.Detach(this);
    }

    private void SendMessage(object sender, RoutedEventArgs e)
    {
        var msg = tbMessage.Text;
        if (msg.Length == 0) return;

        subject.Notify(new Message
        {
            Name = ClientName,
            Content = msg,
            TimeStamp = DateTime.Now,
        });

        tbMessage.Clear();
    }
}
