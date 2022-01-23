using FFCPayBatchGenerator.Factory;
using FFCPayBatchGenerator.Services;
using System;

namespace FFCPayBatchGenerator;
class Program
{
    private static Request request;

    static void Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Payment Batch Generator");
        Console.WriteLine("Future Farming and Countryside programme");
        Console.WriteLine("Development Team {0}", DateTime.Now.Year);
        Console.ResetColor();

        while (true)
        {
            Console.WriteLine("Enter payment batch to generate (BPS, FDMR, CS or SFI)");
            string batchType = Console.ReadLine();
            while (batchType.ToUpper() != "BPS" && batchType.ToUpper() != "FDMR" && batchType.ToUpper() != "CS" && batchType.ToUpper() != "SFI")
            {
                LogError("Invalid batch type");
                batchType = Console.ReadLine();
            }

            request = new Request(batchType);
            CaptureSequence();
            CaptureBatchSize();
            CaptureInvoiceValue();
            CaptureRequestNumber();
            CaptureInvoiceNumber();
            CaptureFRN();
            CaptureDeliveryBody();
            CaptureSchemeYear();

            CreateBatchFile(request);
        }
    }

    private static void CaptureSchemeYear()
    {
        Console.WriteLine("Enter scheme year or leave blank for default (current year). 2015 - 2099");
        string SchemeYear = Console.ReadLine();
        int SchemeYearValue = 0;

        if (!string.IsNullOrEmpty(SchemeYear))
        {
            while (!int.TryParse(SchemeYear, out SchemeYearValue) || (SchemeYearValue < 2015 || SchemeYearValue > 2099))
            {
                LogError("Invalid scheme year");
                SchemeYear = Console.ReadLine();
            }
            request.SchemeYear = SchemeYearValue;
        }
    }

    private static void CaptureDeliveryBody()
    {
        Console.WriteLine("Enter Delivery Body or leave blank for default (RP00 or NE00)");
        string deliveryBody = Console.ReadLine();
        if (!string.IsNullOrEmpty(deliveryBody))
        {
            request.DeliveryBody = deliveryBody;
        }
    }

    private static void CaptureFRN()
    {
        Console.WriteLine("Enter FRN for first invoice in batch or leave blank for default (1000000001). Maximum 9999999999");
        string frn = Console.ReadLine();
        long frnValue = 0;

        if (!string.IsNullOrEmpty(frn))
        {
            while (!long.TryParse(frn, out frnValue) || (frnValue <= 0 || frnValue > 9999999999))
            {
                LogError("Invalid FRN");
                frn = Console.ReadLine();
            }
            request.FRN = frnValue;
        }
    }

    private static void CaptureInvoiceNumber()
    {
        Console.WriteLine("Enter invoice number for first invoice in batch or leave blank for default (1). Maximum 9999999");
        string invoiceNumber = Console.ReadLine();
        int invoiceNumberValue = 0;

        if (!string.IsNullOrEmpty(invoiceNumber))
        {
            while (!int.TryParse(invoiceNumber, out invoiceNumberValue) || (invoiceNumberValue <= 0 || invoiceNumberValue > 9999999))
            {
                LogError("Invalid invoice number");
                invoiceNumber = Console.ReadLine();
            }
            request.InvoiceNumber = invoiceNumberValue;
        }
    }

    private static void CaptureRequestNumber()
    {
        Console.WriteLine("Enter request invoice number or leave blank for default (1)");
        string requestNumber = Console.ReadLine();
        int requestNumberValue = 0;

        if (!string.IsNullOrEmpty(requestNumber))
        {
            while (!int.TryParse(requestNumber, out requestNumberValue) || requestNumberValue <= 0)
            {
                LogError("Invalid request invoice number");
                requestNumber = Console.ReadLine();
            }
            request.RequestNumber = requestNumberValue;
        }
    }

    private static void CaptureInvoiceValue()
    {
        Console.WriteLine("Enter invoice value or leave blank for default (100)");
        string invoiceValue = Console.ReadLine();
        decimal invoiceValueValue = 0;

        if (!string.IsNullOrEmpty(invoiceValue))
        {
            while (!decimal.TryParse(invoiceValue, out invoiceValueValue))
            {
                LogError("Invalid invoice value");
                invoiceValue = Console.ReadLine().ToUpper();
            }
            request.InvoiceValue = invoiceValueValue;
        }
    }

    private static void CaptureBatchSize()
    {
        Console.WriteLine("Enter batch size or leave blank for default (1).  Maximum 10000");
        string batchSize = Console.ReadLine();
        int batchSizeValue = 0;

        if (!string.IsNullOrEmpty(batchSize))
        {
            while (!int.TryParse(batchSize, out batchSizeValue) || (batchSizeValue <= 0 || batchSizeValue > 10000))
            {
                LogError("Invalid batch size");
                batchSize = Console.ReadLine();
            }
            request.BatchSize = batchSizeValue;
        }
    }

    private static void CaptureSequence()
    {
        Console.WriteLine("Enter sequence for batch file or leave blank for default (1).  Maximum 9999");
        string sequence = Console.ReadLine();
        int sequenceValue = 0;

        if (!string.IsNullOrEmpty(sequence))
        {
            while (!int.TryParse(sequence, out sequenceValue) || (sequenceValue <= 0 || sequenceValue > 9999))
            {
                LogError("Invalid sequence");
                sequence = Console.ReadLine();
            }
            request.Sequence = sequenceValue;
        }
    }

    private static void CreateBatchFile(Request request)
    {
        BaseBatchFactory batchFactory = new BatchFactory().Create(request);
        IFileService fileService = new FileService();
        fileService.Generate(batchFactory.GetFileName(), batchFactory.GetContent());
    }

    private static void LogError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(string.Format("{0} - please retry", message));
        Console.ResetColor();
    }
}
