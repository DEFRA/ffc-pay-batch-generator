﻿using System;

namespace FFCPayBatchGenerator;
public class Request
{
    public string BatchType { get; }
    public int Sequence { get; set; } = 1;
    public int BatchSize { get; set; } = 1;
    public decimal InvoiceValue { get; set; } = 100;
    public int RequestNumber { get; set; } = 1;
    public int InvoiceNumber { get; set; } = 1;
    public long FRN { get; set; } = 1000000001;
    public string DeliveryBody { get; set; }
    public int SchemeYear { get; set; }
    public bool CreateChecksum { get; set; } = false;
    public bool CreateControl { get; set; } = false;
    public bool PendingRename { get; set; } = false;
    public Request(string batchType)
    {
        BatchType = batchType.ToUpper();
        SchemeYear = DateTime.Now.Year;
        DeliveryBody = batchType == "CS" ? "NE00" : "RP00";
    }
}
