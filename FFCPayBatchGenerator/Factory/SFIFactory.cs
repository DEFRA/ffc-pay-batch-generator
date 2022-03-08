using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFCPayBatchGenerator.Factory;
public class SFIFactory : BaseBatchFactory
{
    public SFIFactory(Request request) : base(request)
    {
    }

    public override string GetFileName()
    {
        return string.Format("{0}SITIELM{1}_AP_{2}.dat",
            pendingRename ? pendingPrefix : string.Empty,
            sequence.ToString("D4"),
            DateTime.Now.ToString("yyyyMMddHHmmssFFF"));
    }

    public override string GetContent()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(string.Format("B^{0}^{1}^{2}^{3}^SFIP^AP",
            DateTime.Now.ToString("yyyy-MM-dd"),
            batchSize,
            batchSize * invoiceValue,
            sequence.ToString("D4")));

        for (int i = 0; i < batchSize; i++)
        {
            sb.AppendLine(string.Format("H^SFI{0}^{1}^SFIP{2}^{3}^{4}^GBP^{5}^{6}^GBP^SFIP^Q4",
                invoiceNumber.ToString("D8"),
                requestNumber.ToString("D2"),
                invoiceNumber.ToString("D6"),
                requestNumber == 1 ? 1 : 2,
                frn,
                invoiceValue,
                deliveryBody));

            sb.AppendLine(string.Format("L^SFI{0}^{1}^{2}^80001^DRD10^SIP{3}^{4}^N^1^G00 - Gross value of claim^{5}^{5}^SOS273",
                invoiceNumber.ToString("D8"),
                invoiceValue,
                schemeYear,
                invoiceNumber.ToString("D12"),
                deliveryBody,
                new DateTime(schemeYear, 12, 1).ToString("yyyy-MM-dd")));

            invoiceNumber++;
            frn++;
        }

        return sb.ToString();
    }
}
