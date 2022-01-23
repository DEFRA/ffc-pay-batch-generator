﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFCPayBatchGenerator.Factory;
public abstract class BaseBatchFactory
{
    protected int sequence;

    protected int batchSize;

    protected decimal invoiceValue;

    protected int requestNumber;

    protected int invoiceNumber;

    protected long frn;

    protected string deliveryBody;

    protected int schemeYear;

    public BaseBatchFactory(Request request)
    {
        sequence = request.Sequence;
        batchSize = request.BatchSize;
        invoiceValue = request.InvoiceValue;
        requestNumber = request.RequestNumber;
        invoiceNumber = request.InvoiceNumber;
        frn = request.FRN;
        deliveryBody = request.DeliveryBody;
        schemeYear = request.SchemeYear;
    }

    public abstract string GetFileName();

    public abstract string GetContent();
}
