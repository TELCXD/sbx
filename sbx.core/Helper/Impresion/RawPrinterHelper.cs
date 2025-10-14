
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;

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

        public static bool SendStringToPrinter2(string impresora, string tirilla, int lineasAbajo)
        {
            // Comandos ESC/POS
            byte[] initPrinter = new byte[] { 0x1B, 0x40 }; // ESC @ – inicializar
            byte[] feedBottom = new byte[] { 0x1B, 0x64, (byte)lineasAbajo }; // ESC d n – avanzar líneas
            byte[] cutPaper = new byte[] { 0x1D, 0x56, 0x00 }; // GS V 0 – cortar

            // Convertir texto a bytes ANSI
            byte[] textoBytes = Encoding.GetEncoding(1252).GetBytes(tirilla);

            // Combinar todo
            byte[] finalBytes = initPrinter
                .Concat(textoBytes)
                .Concat(feedBottom)
                .Concat(cutPaper)
                .ToArray();

            // Enviar a impresora
            IntPtr pBytes = Marshal.AllocCoTaskMem(finalBytes.Length);
            Marshal.Copy(finalBytes, 0, pBytes, finalBytes.Length);
            bool result = SendBytesToPrinter(impresora, pBytes, finalBytes.Length);
            Marshal.FreeCoTaskMem(pBytes);

            return result;
        }

        public static bool SendStringToPrinter(string Impresora, string tirilla, int LineasAbajo)
        {
            byte[] initPrinter = new byte[] { 0x1B, 0x40 }; // ESC @ – inicializar
            byte[] feedBottom = new byte[] { 0x1B, 0x64, (byte)LineasAbajo }; // ESC d n – avanzar líneas
            byte[] cutPaper = new byte[] { 0x1D, 0x56, 0x00 }; // GS V 0 – cortar

            // Convertir texto a bytes ANSI
            byte[] textoBytes = Encoding.GetEncoding(1252).GetBytes(tirilla);

            // Combinar todo
            byte[] finalBytes = initPrinter
                .Concat(textoBytes)
                .Concat(feedBottom)
                .Concat(cutPaper)
                .ToArray();

            /// Construye tirilla final
            string tirillaFinal = initPrinter + tirilla + feedBottom;

            // Enviar a impresora
            IntPtr pBytes = Marshal.AllocCoTaskMem(finalBytes.Length);
            Marshal.Copy(finalBytes, 0, pBytes, finalBytes.Length);
            bool result = SendBytesToPrinter(Impresora, pBytes, finalBytes.Length);
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
            byte[] qrBytes = ConvertBase64QrToEscPosManual(qrBase64);

            // 3. Agregar feed final
            byte[] feedBytes = Encoding.GetEncoding(1252).GetBytes(feedBottom);

            byte[] alignCenter = new byte[] { 0x1B, 0x61, 0x01 }; // ESC a 1 (centrar)
            byte[] alignLeft = new byte[] { 0x1B, 0x61, 0x00 };   // ESC a 0 (alinear a la izquierda, si quieres restaurarlo después)

            string saltoLinea = "\n"; // o varios: "\n\n"
            byte[] saltoLineaBytes = Encoding.GetEncoding(1252).GetBytes(saltoLinea);

            byte[] cutPaper = new byte[] { 0x1D, 0x56, 0x00 };

            // 4. Unir todo
            byte[] finalBytes = textoBytes
             .Concat(saltoLineaBytes)
             .Concat(alignCenter)  // Centrar
             .Concat(qrBytes)      // Imprimir QR
             .Concat(alignLeft)    // (opcional) volver a alinear a la izquierda
             .Concat(feedBytes)    // Salto final
             .Concat(cutPaper)
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


        #region  ESC/POS Manualmente

        //Convertir Base64 a ESC/POS Manualmente
        public static byte[] ConvertBase64QrToEscPosManual(string base64Image)
        {
            // 1. Limpiar base64
            string base64 = base64Image.Substring(base64Image.IndexOf(",") + 1);
            byte[] imageBytes = Convert.FromBase64String(base64);

            using var ms = new MemoryStream(imageBytes);
            using var original = new Bitmap(ms);

            //Bitmap resized = ResizeBitmap(original, 384);

            // 2. Convertir a blanco y negro (1bpp)
            Bitmap monochrome = ConvertToMonochrome(original);

            // 3. Obtener bytes ESC/POS
            return ConvertBitmapToEscPosRaster(monochrome);
        }

        public static Bitmap ResizeBitmap(Bitmap original, int width)
        {
            int originalWidth = original.Width;
            int originalHeight = original.Height;
            float scale = (float)width / originalWidth;
            int newHeight = (int)(originalHeight * scale);

            Bitmap resized = new Bitmap(width, newHeight);
            using (Graphics g = Graphics.FromImage(resized))
            {
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.DrawImage(original, 0, 0, width, newHeight);
            }
            return resized;
        }

        //Convertir a Blanco y Negro
        private static Bitmap ConvertToMonochrome(Bitmap original)
        {
            int width = original.Width;
            int height = original.Height;

            // Crear nuevo Bitmap en formato 1bpp
            Bitmap monoBitmap = new Bitmap(width, height, PixelFormat.Format1bppIndexed);

            // Bloqueo de bits para acceso rápido
            BitmapData bmpData = monoBitmap.LockBits(
                new Rectangle(0, 0, width, height),
                ImageLockMode.WriteOnly,
                PixelFormat.Format1bppIndexed);

            int stride = bmpData.Stride;
            int bytes = stride * height;
            byte[] pixelData = new byte[bytes];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color pixel = original.GetPixel(x, y);
                    int luminance = (int)(pixel.R * 0.3 + pixel.G * 0.59 + pixel.B * 0.11);
                    bool isBlack = luminance < 80;

                    if (isBlack)
                    {
                        int index = y * stride + (x >> 3);
                        pixelData[index] |= (byte)(0x80 >> (x & 0x7));
                    }
                }
            }

            // Copiar datos al bitmap
            Marshal.Copy(pixelData, 0, bmpData.Scan0, bytes);
            monoBitmap.UnlockBits(bmpData);

            return monoBitmap;
        }

        //Convertir Bitmap a ESC/POS Raster (GS v 0)
        private static byte[] ConvertBitmapToEscPosRaster(Bitmap bmp)
        {
            List<byte> escPosBytes = new();

            int width = bmp.Width;
            int height = bmp.Height;

            int widthBytes = (width + 7) / 8;

            // ESC * m nL nH d1...dk (GS v 0 raster format)
            escPosBytes.Add(0x1D); // GS
            escPosBytes.Add(0x76); // 'v'
            escPosBytes.Add(0x30); // '0'
            escPosBytes.Add(0x00); // mode: normal

            escPosBytes.Add((byte)(widthBytes % 256)); // xL
            escPosBytes.Add((byte)(widthBytes / 256)); // xH
            escPosBytes.Add((byte)(height % 256));     // yL
            escPosBytes.Add((byte)(height / 256));     // yH

            // Leer datos directamente del Bitmap 1bpp
            BitmapData bmpData = bmp.LockBits(
                new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadOnly,
                PixelFormat.Format1bppIndexed);

            int stride = bmpData.Stride;
            int bytes = stride * bmp.Height;
            byte[] rawData = new byte[bytes];
            Marshal.Copy(bmpData.Scan0, rawData, 0, bytes);
            bmp.UnlockBits(bmpData);

            // Copiar directamente línea por línea
            for (int y = 0; y < height; y++)
            {
                int offset = y * stride;
                for (int i = 0; i < widthBytes; i++)
                {
                    escPosBytes.Add(rawData[offset + i]);
                }
            }

            return escPosBytes.ToArray();
        }
        #endregion
    }
}
