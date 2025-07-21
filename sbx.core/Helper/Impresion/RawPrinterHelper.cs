using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using ESCPOS_NET.Emitters;

namespace sbx.core.Helper.Impresion
{
    public class RawPrinterHelper
    {
        // Structure and API declarions:
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public class DOCINFOA
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDocName;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pOutputFile;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDataType;
        }
        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);

        [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level, [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

        [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, out Int32 dwWritten);

        // SendBytesToPrinter()
        // When the function is given a printer name and an unmanaged array
        // of bytes, the function sends those bytes to the print queue.
        // Returns true on success, false on failure.
        public static bool SendBytesToPrinter(string szPrinterName, IntPtr pBytes, Int32 dwCount)
        {
            Int32 dwError = 0, dwWritten = 0;
            IntPtr hPrinter = new IntPtr(0);
            DOCINFOA di = new DOCINFOA();
            bool bSuccess = false; // Assume failure unless you specifically succeed.

            di.pDocName = "Ticket de Venta";//Este es el nombre con el que guarda el archivo en caso de no imprimir a la impresora fisica.
            di.pDataType = "RAW";//de tipo texto plano

            // Open the printer.
            if (OpenPrinter(szPrinterName.Normalize(), out hPrinter, IntPtr.Zero))
            {
                // Start a document.
                if (StartDocPrinter(hPrinter, 1, di))
                {
                    // Start a page.
                    if (StartPagePrinter(hPrinter))
                    {
                        // Write your bytes.
                        bSuccess = WritePrinter(hPrinter, pBytes, dwCount, out dwWritten);
                        EndPagePrinter(hPrinter);
                    }
                    EndDocPrinter(hPrinter);
                }
                ClosePrinter(hPrinter);
            }
            // If you did not succeed, GetLastError may give more information
            // about why not.
            if (bSuccess == false)
            {
                dwError = Marshal.GetLastWin32Error();
            }
            return bSuccess;
        }

        public static bool SendStringToPrinter(string Impresora, string tirilla, int LineasAbajo)
        {
            string initPrinter = "\x1B\x40";
            string feedBottom = $"\x1B\x64{(char)LineasAbajo}";

            /// Construye tirilla final
            string tirillaFinal = initPrinter + tirilla + feedBottom;

            IntPtr pBytes;
            Int32 dwCount;
            dwCount = tirillaFinal.Length;
            pBytes = Marshal.StringToCoTaskMemAnsi(tirillaFinal);
            SendBytesToPrinter(Impresora, pBytes, dwCount);
            Marshal.FreeCoTaskMem(pBytes);
            return true;
        }

        public static bool SendStringToPrinterConQr(string Impresora, string tirilla, int LineasAbajo, string qrBase64)
        {
            string initPrinter = "\x1B\x40"; // Reset
            string feedBottom = $"\x1B\x64{(char)LineasAbajo}";

            // 1. Convertir tirilla de texto a bytes ANSI
            byte[] textoBytes = Encoding.GetEncoding(1252).GetBytes(initPrinter + tirilla);

            // 2. Convertir imagen base64 a ESC/POS bytes
            byte[] qrBytes = ConvertBase64QrToEscPos(qrBase64);

            // 3. Agregar feed final
            byte[] feedBytes = Encoding.GetEncoding(1252).GetBytes(feedBottom);

            // 4. Unir todo
            byte[] finalBytes = textoBytes
                .Concat(qrBytes)
                .Concat(feedBytes)
                .ToArray();

            // 5. Enviar a impresora
            IntPtr pBytes = Marshal.AllocCoTaskMem(finalBytes.Length);
            Marshal.Copy(finalBytes, 0, pBytes, finalBytes.Length);
            bool result = SendBytesToPrinter(Impresora, pBytes, finalBytes.Length);
            Marshal.FreeCoTaskMem(pBytes);

            return result;
        }

        public static byte[] ConvertBase64QrToEscPos(string base64Image)
        {
            var e = new ESCPOS_NET.Emitters.EPSON();

            // Quitar encabezado base64
            string base64 = base64Image.Substring(base64Image.IndexOf(",") + 1);
            byte[] imageBytes = Convert.FromBase64String(base64);

            using var ms = new MemoryStream(imageBytes);
            #pragma warning disable CA1416 // Solo Windows
            using var bmp = new Bitmap(ms);
            #pragma warning restore CA1416

            // Convertir el Bitmap a un nuevo MemoryStream en formato PNG
            using var imgStream = new MemoryStream();
            #pragma warning disable CA1416
            bmp.Save(imgStream, System.Drawing.Imaging.ImageFormat.Png);
            #pragma warning restore CA1416
            byte[] pngBytes = imgStream.ToArray();

            // Ahora sí: pasar los bytes PNG a PrintImage
            return e.PrintImage(pngBytes,false);
        }
    }
}
