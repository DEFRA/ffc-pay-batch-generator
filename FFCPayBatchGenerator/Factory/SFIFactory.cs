﻿using System;
using System.Text;

namespace FFCPayBatchGenerator.Factory;
public class SFIFactory : BaseBatchFactory
{
    public SFIFactory(Request request) : base(request)
    {
    }

    public override string GetFileName()
    {
        return string.Format("{0}SITISFI{1}_AP_{2}.dat",
            pendingRename ? pendingPrefix : string.Empty,
            sequence.ToString("D4"),
            DateTime.Now.ToString("yyyyMMddHHmmssFFF"));
    }

    public override string GetContent()
    {
        StringBuilder sb = new();
        sb.AppendLine(string.Format("B^{0}^{1}^{2}^{3}^SFI^AP",
            DateTime.Now.ToString("yyyy-MM-dd"),
            batchSize,
            batchSize * invoiceValue,
            sequence.ToString("D4")));

        for (int i = 0; i < batchSize; i++)
        {
            sb.AppendLine(string.Format("H^SFI{0}^{1}^{2}^{3}^{4}^GBP^{5}^{6}^GBP^SFI^Q4",
                invoiceNumber.ToString("D8"),
                requestNumber.ToString("D2"),
                frn.ToString("D8")[^8..],
                requestNumber == 1 ? 1 : 2,
                frn,
                invoiceValue,
                deliveryBody));

            sb.AppendLine(string.Format("L^SFI{0}^{1}^{2}^80101^DRD10^{3}^{4}^N^1^G00 - Gross value of claim^{5}^{5}^SOS270",
                invoiceNumber.ToString("D8"),
                invoiceValue,
                schemeYear,
                frn.ToString("D8")[^8..],
                deliveryBody,
                new DateTime(schemeYear, 12, 1).ToString("yyyy-MM-dd")));

            invoiceNumber++;
            frn++;
        }

        return sb.ToString();
    }
}
