using System;
using System.Windows;

namespace Chatter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IObserver
    {
        private readonly Subject subject;

        public MainWindow()
        {
            InitializeComponent();

            subject = new Subject();
            subject.Attach(this);
            SetClientCountLabel(0);
        }

        public string ClientName { get; } = "Server";
        public void ClientAttached(string name)
        {
            SetClientCountLabel(subject.NrObservers - 1);
            lvLog.Items.Add($"[{DateTime.Now.ToLongTimeString()}] {name}: attached");
        }

        public void ClientDetached(string name)
        {
            SetClientCountLabel( subject.NrObservers + 1);
            lvLog.Items.Add($"[{DateTime.Now.ToLongTimeString()}] {name}: detached");
        }

        public void Update(Message msg)
        {
            lvMessages.Items.Add(msg.ToString());
        }

        private void StartClient(object sender, RoutedEventArgs e)
        {
            if (tbClientName.Text.Length == 0) return;

            new ChatWindow(subject, tbClientName.Text).Show();
            tbClientName.Clear();
        }

        private void SetClientCountLabel(int count)
        {
            lblClientCount.Content = $"Nr Clients: {count}";
        }
    }
}
