using System;
using System.Text;

namespace FFCPayBatchGenerator.Factory;
public class FDMRFactory : BaseBatchFactory
{
    public FDMRFactory(Request request) : base(request)
    {
    }

    public override string GetFileName()
    {
        return string.Format("{0}FDMR_{1}_AP_{2}.dat",
            pendingRename ? pendingPrefix : string.Empty,
            sequence.ToString("D4"),
            DateTime.Now.ToString("yyyyMMddHHmmssFFF"));
    }

    public override string GetContent()
    {
        StringBuilder sb = new();
        sb.AppendLine(string.Format("B^{0}^{1}^{2}^{3}^FDMR^AP",
            DateTime.Now.ToString("yyyy-MM-dd"),
            batchSize,
            batchSize * invoiceValue,
            sequence));

        for (int i = 0; i < batchSize; i++)
        {
            sb.AppendLine(string.Format("H^FDMR{0}^{1}^C{2}^{3}^{4}^{5}^{6}^GBP",
                invoiceNumber.ToString("D7"),
                requestNumber.ToString("D3"),
                invoiceNumber.ToString("D7"),
                frn,
                requestNumber == 1 ? 1 : 2,
                invoiceValue,
                deliveryBody));

            sb.AppendLine(string.Format("L^FDMR{0}^{1}^{2}^{3}^EGF00^{4}^1^G01 - Gross value of claim^{5}",
                invoiceNumber.ToString("D7"),
                invoiceValue,
                schemeYear,
                GetSchemeCode(schemeYear),
                deliveryBody,
                new DateTime(schemeYear, 12, 1).ToString("yyyy-MM-dd")));

            invoiceNumber++;
            frn++;
        }

        return sb.ToString();
    }

    private static int GetSchemeCode(int schemeYear)
    {
        return schemeYear switch
        {
            2015 => 10575,
            2016 => 10576,
            2017 => 10577,
            2018 => 10578,
            2019 => 10579,
            2020 => 10580,
            _ => 10580,
        };
    }
}
