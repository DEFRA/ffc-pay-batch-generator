using FFCPayBatchGenerator.Factory;
using FFCPayBatchGenerator.Services;
using System;
using System.Collections.Generic;

namespace FFCPayBatchGenerator;
public class Program
{
    private static Request request = new("SFI");

    private readonly static List<string> batchTypes = new()
    {
        "BPS",
        "CS",
        "FDMR",
        "SFIP",
        "SFI",
        "SFI23",
        "LS"
    };

    static void Main()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Payment Batch Generator");
        Console.WriteLine("Future Farming and Countryside programme {0}", DateTime.Now.Year);
        Console.ResetColor();

        bool gatheringParams = true;

        while (gatheringParams)
        {
            Console.WriteLine("Enter payment batch to generate (BPS, FDMR, CS, SFIP, SFI, SFI23 or LS)");
            string batchType = Console.ReadLine() ?? string.Empty;
            while (string.IsNullOrEmpty(batchType) || !batchTypes.Contains(batchType.ToUpper()))
            {
                LogError("Invalid batch type");
                batchType = Console.ReadLine() ?? string.Empty;
            }

            request = new(batchType);
            CaptureSequence();
            CaptureBatchSize();
            CaptureInvoiceValue();
            CaptureRequestNumber();
            CaptureInvoiceNumber();
            CaptureFRN();
            CaptureDeliveryBody();
            CaptureSchemeYear();
            CaptureChecksum();
            CaptureControlFiles();
            CapturePendingRename();

            CreateBatchFile(request);
            gatheringParams = false;
        }
    }

    private static void CaptureSchemeYear()
    {
        Console.WriteLine("Enter scheme year or leave blank for default (current year). 2015 - 2099");
        string schemeYear = Console.ReadLine() ?? string.Empty;

        if (!string.IsNullOrEmpty(schemeYear))
        {
            int schemeYearValue;
            while (!int.TryParse(schemeYear, out schemeYearValue) || schemeYearValue < 2015 || schemeYearValue > 2099)
            {
                LogError("Invalid scheme year");
                schemeYear = Console.ReadLine() ?? string.Empty;
            }
            request.SchemeYear = schemeYearValue;
        }
    }

    private static void CaptureDeliveryBody()
    {
        Console.WriteLine("Enter Delivery Body or leave blank for default (RP00 or NE00)");
        string deliveryBody = Console.ReadLine() ?? string.Empty;
        if (!string.IsNullOrEmpty(deliveryBody))
        {
            request.DeliveryBody = deliveryBody;
        }
    }

    private static void CaptureFRN()
    {
        Console.WriteLine("Enter FRN for first request in batch or leave blank for default (1000000001). Maximum 9999999999");
        string frn = Console.ReadLine() ?? string.Empty;

        if (!string.IsNullOrEmpty(frn))
        {
            long frnValue;
            while (!long.TryParse(frn, out frnValue) || frnValue <= 0 || frnValue > 9999999999)
            {
                LogError("Invalid FRN");
                frn = Console.ReadLine() ?? string.Empty;
            }
            request.FRN = frnValue;
        }
    }

    private static void CaptureInvoiceNumber()
    {
        Console.WriteLine("Enter invoice number for first invoice in batch or leave blank for default (1). Maximum 9999999");
        string invoiceNumber = Console.ReadLine() ?? string.Empty;

        if (!string.IsNullOrEmpty(invoiceNumber))
        {
            int invoiceNumberValue;
            while (!int.TryParse(invoiceNumber, out invoiceNumberValue) || invoiceNumberValue <= 0 || invoiceNumberValue > 9999999)
            {
                LogError("Invalid invoice number");
                invoiceNumber = Console.ReadLine() ?? string.Empty;
            }
            request.InvoiceNumber = invoiceNumberValue;
        }
    }

    private static void CaptureRequestNumber()
    {
        Console.WriteLine("Enter payment request number or leave blank for default (1)");
        string requestNumber = Console.ReadLine() ?? string.Empty;

        if (!string.IsNullOrEmpty(requestNumber))
        {
            int requestNumberValue;
            while (!int.TryParse(requestNumber, out requestNumberValue) || requestNumberValue <= 0)
            {
                LogError("Invalid payment request number");
                requestNumber = Console.ReadLine() ?? string.Empty;
            }
            request.RequestNumber = requestNumberValue;
        }
    }

    private static void CaptureInvoiceValue()
    {
        Console.WriteLine("Enter request value or leave blank for default (100)");
        string invoiceValue = Console.ReadLine() ?? string.Empty;

        if (!string.IsNullOrEmpty(invoiceValue))
        {
            decimal invoiceValueValue;
            while (!decimal.TryParse(invoiceValue, out invoiceValueValue))
            {
                LogError("Invalid request value");
                invoiceValue = Console.ReadLine() ?? string.Empty;
            }
            request.InvoiceValue = invoiceValueValue;
        }
    }

    private static void CaptureBatchSize()
    {
        Console.WriteLine("Enter batch size or leave blank for default (1).  Maximum 10000");
        string batchSize = Console.ReadLine() ?? string.Empty;

        if (!string.IsNullOrEmpty(batchSize))
        {
            int batchSizeValue;
            while (!int.TryParse(batchSize, out batchSizeValue) || batchSizeValue <= 0 || batchSizeValue > 10000)
            {
                LogError("Invalid batch size");
                batchSize = Console.ReadLine() ?? string.Empty;
            }
            request.BatchSize = batchSizeValue;
        }
    }

    private static void CaptureSequence()
    {
        Console.WriteLine("Enter sequence for batch file or leave blank for default (1).  Maximum 9999");
        string sequence = Console.ReadLine() ?? string.Empty;

        if (!string.IsNullOrEmpty(sequence))
        {
            int sequenceValue;
            while (!int.TryParse(sequence, out sequenceValue) || sequenceValue <= 0 || sequenceValue > 9999)
            {
                LogError("Invalid sequence");
                sequence = Console.ReadLine() ?? string.Empty;
            }
            request.Sequence = sequenceValue;
        }
    }

    private static void CaptureChecksum()
    {
        Console.WriteLine("Create checksum file or leave blank for default? (no)");
        string checksum = Console.ReadLine() ?? string.Empty;

        request.CreateChecksum = IsYes(checksum);
    }

    private static void CaptureControlFiles()
    {
        Console.WriteLine("Create control files or leave blank for default? (no)");
        string control = Console.ReadLine() ?? string.Empty;

        request.CreateControl = IsYes(control);
    }

    private static void CapturePendingRename()
    {
        Console.WriteLine("Create file with pending file name? (no)");
        string rename = Console.ReadLine() ?? string.Empty;

        request.PendingRename = IsYes(rename);
    }

    private static void CreateBatchFile(Request request)
    {
        BaseBatchFactory batchFactory = BatchFactory.Create(request);
        IFileService fileService = new FileService();
        var batchPath = fileService.Generate(batchFactory.GetFileName(), batchFactory.GetContent());

        if (request.CreateChecksum)
        {
            fileService.Generate(Checksum.GetFileName(batchPath), Checksum.Generate(batchPath));
        }

        if (request.CreateControl)
        {
            fileService.Generate(Control.GetFileName(batchPath), string.Empty);

            if (request.CreateChecksum)
            {
                fileService.Generate(Control.GetFileName(Checksum.GetFileName(batchPath)), string.Empty);
            }
        }
    }

    private static bool IsYes(string input)
    {
        return !string.IsNullOrEmpty(input) || input.ToUpper() == "YES" || input.ToUpper() == "Y" || input.ToUpper() == "TRUE";
    }

    private static void LogError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(string.Format("{0} - please retry", message));
        Console.ResetColor();
    }
}
