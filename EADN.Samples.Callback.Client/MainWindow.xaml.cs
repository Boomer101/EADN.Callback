using EADN.Samples.Callback.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EADN.Samples.Callback.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private IClockSingleton Proxy;

        private TimerCallback TimerCallback;

        public event PropertyChangedEventHandler PropertyChanged;

        public string CurrentTime { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            // WPF bereit:
            Loaded += MainWindow_Loaded;
        }

        // Dieses Event für eigenen Aufbau verwenden
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            TimerCallback = new TimerCallback();
            TimerCallback.Tick += TimerCallback_Tick;

            Proxy = DuplexChannelFactory<IClockSingleton>.CreateChannel(
                TimerCallback,
                new NetTcpBinding(),
                new EndpointAddress("net.tcp://localhost:4715/Clock"));

            Proxy.RegisterClient();
        }

        private void TimerCallback_Tick(object sender, TimeServerEventArgument e)
        {
            CurrentTime = e.CurrentTime.ToLongTimeString();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentTime)));
        }
    }
}
