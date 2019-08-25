using System;
using System.Collections.Generic;
using System.Linq;
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
using System.IO.Ports;
using System.Threading;
using System.Windows.Threading;

namespace CurrentSensor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static SerialPort _serialPort;

        public MainWindow()
        {
            InitializeComponent();
            //MainTest();
            _serialPort = new SerialPort();
            _serialPort.PortName = "COM3";
            _serialPort.BaudRate = 57600;
            _serialPort.Open();
            Thread.Sleep(1500);
        }


        public static void MainTest()
        {
            byte[] buffer = new byte[256];
            using (SerialPort sp = new SerialPort("COM3", 19200))
            {
                sp.Open();
                //read directly
                sp.Read(buffer, 0, (int)buffer.Length);
                //read using a Stream
                sp.BaseStream.Read(buffer, 0, (int)buffer.Length);
            }
        }
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            Task.Factory.StartNew(() => CalculateMyOperation());
        }

        private void CalculateMyOperation()
        {
            for (int i = 0; i <= 1000000000; i++)
            {
                _serialPort.DiscardInBuffer();
                Thread.Sleep(200);
                //byte[] buffer = new byte[256];
                //_serialPort.Read(buffer, 0, (int)buffer.Length);
                //_serialPort.BaseStream.Read(buffer, 0, (int)buffer.Length);
                //var serialPort = new SerialPortExample("COM1", "\r\n", 2500);
                //serialPort.Open();
                //var response = serialPort.Send("COMMAND", true);
                //serialPort.Close();
                string a = _serialPort.ReadExisting();
                char[] array1 = a.Take(5).ToArray();
                char[] array2 = a.Skip(4).Take(8).ToArray();
                string rawvoltage = string.Join("", array1);
                string rawcurrent = string.Join("", array2);
                int k1 = rawcurrent.IndexOf('.'); //5 в данном случае нужный тебе символ
                int k2 = rawvoltage.IndexOf('.'); //5 в данном случае нужный тебе символ
                string current = "";
                string voltage = "";
                if (k2 >= 2)
                    switch (k1)
                    {
                        case 0:
                            if (k2 >= 2)
                            {
                                current = "0";
                            }

                            break;
                        case 2:
                            if (k2 >= 2)
                            {
                                current = string.Join("", rawcurrent.Skip(1).Take(4));
                            }

                            break;
                        case 3:
                            if (k2 >= 2)
                            {
                                current = string.Join("", rawcurrent.Skip(1).Take(5));
                            }

                            break;
                        case 4:
                            if (k2 >= 2)
                            {
                                current = string.Join("", rawcurrent.Skip(1).Take(6));
                            }

                            break;
                        case 5:
                            if (k2 >= 2)
                            {
                                current = string.Join("", rawcurrent.Skip(1).Take(7));
                            }

                            break;
                    }


                else
                {
                    switch (k1)
                    {
                        case 0:
                            current = "0";
                            break;
                        case 1:
                            current = string.Join("", rawcurrent.Take(4));
                            break;
                        case 2:
                            current = string.Join("", rawcurrent.Take(5));
                            break;
                        case 3:
                            current = string.Join("", rawcurrent.Take(6));
                            break;
                        case 4:
                            current = string.Join("", rawcurrent.Take(7));
                            break;
                    }
                }

                switch (k2)
                {
                    case 1:
                        voltage = string.Join("", rawvoltage.Take(4));
                        break;
                    case 2:
                        voltage = string.Join("", rawvoltage.Take(5));
                        break;
                }

                float calcc = Convert.ToSingle(current);
                float calcv = Convert.ToSingle(voltage);
                double powercal = 0.001 * (calcc * calcv);
                string powerraw = powercal.ToString();
                string power = "0";
                int k3 = powerraw.IndexOf('.'); //5 в данном случае нужный тебе символ
                switch (k3)
                {
                    case 1:
                        power = string.Join("", powerraw.Take(4));
                        break;
                    case 2:
                        power = string.Join("", powerraw.Take(5));
                        break;
                }

                Dispatcher.BeginInvoke(new Action(() => { VoltageBlock.Text = voltage + "V"; }),
                    DispatcherPriority.Background);
                Dispatcher.BeginInvoke(new Action(() => { CurrentBlock.Text = current + "mA"; }),
                    DispatcherPriority.Background);
                Dispatcher.BeginInvoke(new Action(() => { PowerBlock.Text = power + "W"; }),
                    DispatcherPriority.Background);
                i--;
            }
        }
    }
}

