﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFCPayBatchGenerator.Factory;
public class BPSFactory : BaseBatchFactory
{
    public BPSFactory(Request request) : base(request)
    {
    }

    public override string GetFileName()
    {
        return string.Format("{0}SITI_{1}_AP_{2}.dat",
            pendingRename ? pendingPrefix : string.Empty,
            sequence.ToString("D4"),
            DateTime.Now.ToString("yyyyMMddHHmmssFFF"));
    }

    public override string GetContent()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(string.Format("B^{0}^{1}^{2}^{3}^SITI AGRI SYS^AP",
            DateTime.Now.ToString("yyyy-MM-dd"),
            batchSize,
            batchSize * invoiceValue,
            sequence));

        for (int i = 0; i < batchSize; i++)
        {
            sb.AppendLine(string.Format("H^SITI{0}^{1}^C{2}^{3}^{4}^{5}^{6}^GBP",
                invoiceNumber.ToString("D7"),
                requestNumber.ToString("D3"),
                invoiceNumber.ToString("D7"),
                frn,
                requestNumber == 1 ? 1 : 2,
                invoiceValue,
                deliveryBody));

            sb.AppendLine(string.Format("L^SITI{0}^{1}^{2}^10501^EGF00^{3}^1^G00 - Gross value of claim^{4}",
                invoiceNumber.ToString("D7"),
                invoiceValue,
                schemeYear,
                deliveryBody,
                new DateTime(schemeYear, 12, 1).ToString("yyyy-MM-dd")));

            invoiceNumber++;
            frn++;
        }

        return sb.ToString();
    }
}
