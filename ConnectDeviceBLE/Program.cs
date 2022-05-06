using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Enumeration;
using Windows.Storage.Streams;

namespace ConnectDeviceBLE
{
    class Program
    {
        static DeviceInformation device = null;

        public static string HEART_RATE_SERVICE_ID = "0000ffb0-0000-1000-8000-00805f9b34fb";
        public static string HEART_RATE_CHARACTERISTIC_ID = "0000ffb3-0000-1000-8000-00805f9b34fb";

        // Leer encabezado de envío
        public static int HEADER_READ_SEND = 0xB0;
        // Leer encabezado de recepción
        public static int HEADER_READ_GET = 0xB1;
        // Establecer encabezado de envío
        public static int HEADER_WRITE_SEND = 0xB2;
        // Establecer encabezado de recepción
        public static int HEADER_WRITE_GET = 0xB3;
        // Encabezado de envío de paso
        public static int HEADER_SETP_SEND = 0xB4;
        // Encabezado de recepción del paso
        public static int HEADER_STEP_GET = 0xB5;
        // Transmisor de frecuencia cardiaca
        public static int HEADER_HEARTRATE_SEND = 0xB6;
        // Receptor de frecuencia cardiaca
        public static int HEADER_HEARTRATE_GET = 0xB7;

        static async Task Main(string[] args)
        {
            // Query for extra properties you want returned
            string[] requestedProperties = { "System.Devices.Aep.DeviceAddress", "System.Devices.Aep.IsConnected" };

            DeviceWatcher deviceWatcher =
                        DeviceInformation.CreateWatcher(
                                BluetoothLEDevice.GetDeviceSelectorFromPairingState(true),
                                requestedProperties,
                                DeviceInformationKind.AssociationEndpoint);

            // Register event handlers before starting the watcher.
            // Added, Updated and Removed are required to get all nearby devices
            deviceWatcher.Added += DeviceWatcher_Added;
            deviceWatcher.Updated += DeviceWatcher_Updated;
            deviceWatcher.Removed += DeviceWatcher_Removed;

            // EnumerationCompleted and Stopped are optional to implement.
            deviceWatcher.EnumerationCompleted += DeviceWatcher_EnumerationCompleted; 
            deviceWatcher.Stopped += DeviceWatcher_Stopped;

            // Start the watcher.
            deviceWatcher.Start();
            while (true)
            {
                if (device == null)
                {
                    Thread.Sleep(200);
                }
                else
                {
                    Console.WriteLine("Presiona cualquier para emparejar con el Brazalete");
                    Console.ReadKey();
                    //Environment.Exit(0);

                    BluetoothLEDevice bluetoothLeDevice = await BluetoothLEDevice.FromIdAsync(device.Id);
                    Console.WriteLine("Intentando emparejar con el dispositivo");

                    GattDeviceServicesResult result = await bluetoothLeDevice.GetGattServicesAsync();

                    if (result.Status == GattCommunicationStatus.Success)
                    {
                        Console.WriteLine("Emparejamiento exitoso");
                        var services = result.Services;
                        foreach (var service in services)
                        {
                            Console.WriteLine("Servicio Uuid:" + service.Uuid);
                            GattCharacteristicsResult resultC = await service.GetCharacteristicsAsync();

                            //foreach (var characteristic in characteristics)
                            var characteristics = resultC.Characteristics;

                            Console.WriteLine("\tSe intenta el acceso a las caracteristicas de este servicio");

                            foreach (var characteristic in characteristics)
                            {
                                GattCharacteristicProperties properties = characteristic.CharacteristicProperties;

                                Console.WriteLine("\t\tCaracteristica Uuid:" + characteristic.Uuid.ToString());


                                GattReadResult resultcharacteristicValue = await characteristic.ReadValueAsync();

                                if (resultcharacteristicValue.Status == GattCommunicationStatus.Success)
                                {


                                    if (properties.HasFlag(GattCharacteristicProperties.Read))
                                    {
                                        // This characteristic supports reading from it.
                                        Console.WriteLine("\t\t\tEsta caracteristica admite la lectura");
                                        //GattReadResult resultcharacteristicValue = await characteristic.ReadValueAsync();

                                        //if (resultcharacteristicValue.Status == GattCommunicationStatus.Success)
                                        //{
                                        var reader = DataReader.FromBuffer(resultcharacteristicValue.Value);
                                        var receivedStrings = "";

                                        // Keep reading until we consume the complete stream.
                                        while (reader.UnconsumedBufferLength > 0)
                                        {
                                            // Note that the call to readString requires a length of "code units" 
                                            // to read. This is the reason each string is preceded by its length 
                                            // when "on the wire".
                                            uint bytesToRead = reader.UnconsumedBufferLength;
                                            try
                                            {
                                                receivedStrings += reader.ReadString(bytesToRead);
                                                Console.WriteLine("\t\t\t\tLeyendo el valor de la caracteristica: " + receivedStrings);
                                            }
                                            catch (Exception)
                                            {
                                                byte[] input = new byte[reader.UnconsumedBufferLength];
                                                reader.ReadBytes(input);

                                                //reader.ReadBytes(input);
                                                for (int iValor = 0; iValor < input.Length; iValor++)
                                                {
                                                    string valor = input.GetValue(iValor).ToString();
                                                    Console.WriteLine("\t\t\t\tLeyendo el valor de la caracteristica: " + valor);
                                                }
                                            }
                                        }
                                        //}


                                    }
                                    if (properties.HasFlag(GattCharacteristicProperties.Write))
                                    {
                                        // This characteristic supports writing to it.
                                        Console.WriteLine("\t\t\tEsta caracteristica admite la escritura");
                                    }

                                    string characteristicUuid = "" + characteristic.Uuid.ToString();
                                    //if (characteristicUuid.Equals(HEART_RATE_CHARACTERISTIC_ID))
                                    //{

                                        if (properties.HasFlag(GattCharacteristicProperties.Notify))
                                        {
                                            // This characteristic supports subscribing to notifications.
                                            Console.WriteLine("\t\t\tEsta caracteristica soporta las notificaciones");
                                            GattCommunicationStatus status = await characteristic.WriteClientCharacteristicConfigurationDescriptorAsync(
                                                GattClientCharacteristicConfigurationDescriptorValue.Notify);
                                            if (status == GattCommunicationStatus.Success)
                                            {
                                                // Server has been informed of clients interest.
                                                characteristic.ValueChanged += Characteristic_ValueChanged;

                                                var reader = DataReader.FromBuffer(resultcharacteristicValue.Value);

                                                //var flags = reader.ReadByte();
                                                //var value = reader.ReadByte();
                                                //Console.WriteLine($"{flags} - {value}");

                                                var receivedStrings = "";

                                                ////Keep reading until we consume the complete stream.
                                                while (reader.UnconsumedBufferLength > 0)
                                                {
                                                    //Note that the call to readString requires a length of "code units"
                                                    // to read. This is the reason each string is preceded by its length
                                                    // when "on the wire".

                                                    uint bytesToRead = reader.UnconsumedBufferLength;
                                                    try
                                                    {
                                                        receivedStrings += reader.ReadString(bytesToRead);
                                                        Console.WriteLine("\t\t\t\tLeyendo el valor de la caracteristica Modificado: " + receivedStrings);
                                                    }
                                                    catch (Exception)
                                                    {
                                                        byte[] input = new byte[reader.UnconsumedBufferLength];
                                                        reader.ReadBytes(input);

                                                        //reader.ReadBytes(input);
                                                        for (int iValor = 0; iValor < input.Length ; iValor++)
                                                        {
                                                            string valor = input.GetValue(iValor).ToString();
                                                            Console.WriteLine("\t\t\t\tLeyendo el valor de la caracteristica Modificado: " + valor);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        //Console.WriteLine("\t\tEncontrada la caracteristica Heart Rate");
                                    //}
                                }
                            }
                        }
                    }

                    Console.WriteLine("Presiona cualquier para terminar la aplicación");
                    Console.ReadKey();
                    break;
                }
            }
            deviceWatcher.Stop();
        }

        private static void Characteristic_ValueChanged(GattCharacteristic sender, GattValueChangedEventArgs args)
        {
            var reader = DataReader.FromBuffer(args.CharacteristicValue);
        }

        private static void DeviceWatcher_Stopped(DeviceWatcher sender, object args)
        {
            //throw new NotImplementedException();
        }

        private static void DeviceWatcher_EnumerationCompleted(DeviceWatcher sender, object args)
        {
            Console.WriteLine(sender.GetType().ToString());
            //throw new NotImplementedException();
        }

        private static void DeviceWatcher_Removed(DeviceWatcher sender, DeviceInformationUpdate args)
        {
            //Console.WriteLine(args.Id);
            
            //throw new NotImplementedException();
        }

        private static void DeviceWatcher_Updated(DeviceWatcher sender, DeviceInformationUpdate args)
        {
            //throw new NotImplementedException();
        }

        private static void DeviceWatcher_Added(DeviceWatcher sender, DeviceInformation args)
        {
            
            //byte[] data = FromHex(args.Id[23..]);
            //string s = Encoding.ASCII.GetString(data);
            //byte[] ba = Encoding.Default.GetBytes(args.Id[24..]);
            //var hexString = BitConverter.ToString(ba);
            
            string NameDevice = args.Name;
            Console.WriteLine(NameDevice + " id:" + args.Id);

            if (NameDevice.Length > 0)
            {

                if (NameDevice == "H709")
                {
                    //Console.WriteLine(args.Properties.Values);
                    device = args;
                }
            }
            //throw new NotImplementedException();
        }

        private static byte[] FromHex(string hex)
        {
            hex = hex.Replace(":", "");
            hex = hex.Replace("-", "");
            byte[] raw = new byte[hex.Length / 2];
            for (int i = 0; i < raw.Length; i++)
            {
                raw[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return raw;
        }
    }
}
