using System;
using System.IO.Ports;
using System.Threading.Tasks;
using System.IO.Ports.;

namespace ExtensionMethods.SerialPort
{
    public static class SerialPortExtensions
    {

        public async static Task ReadAsync(this System.IO.Ports.SerialPort serialPort, byte[] buffer, int offset, int count)
        {
            int quedanPorLeer = count;
            var temp = new byte[count];

            while(quedanPorLeer > 0)
            {
                int leidos = await serialPort.BaseStream.ReadAsync(temp, 0, quedanPorLeer);
                Array.Copy(temp, 0, buffer, offset + count - quedanPorLeer, leidos);
                quedanPorLeer -= leidos;
            }
        }

        public async static Task<byte[]> ReadAsync(this System.IO.Ports.SerialPort serialPort, int count)
        {
            var datos = new byte[count];
            await serialPort.ReadAsync(datos, 0, count);
            return datos;
        }
    }
}