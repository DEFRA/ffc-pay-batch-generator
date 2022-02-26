using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFCPayBatchGenerator.Factory;
public class CSFactory : BaseBatchFactory
{
    public CSFactory(Request request) : base(request)
    {
    }

    public override string GetFileName()
    {
        return string.Format("{0}SITICS{1}_AP_{2}.dat",
            pendingRename ? pendingPrefix : string.Empty,
            sequence.ToString("D4"),
            DateTime.Now.ToString("yyyyMMddHHmmssFFF"));
    }

    public override string GetContent()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(string.Format("B^{0}^{1}^{2}^{3}^SITI AGRI CS SYS^AP",
            DateTime.Now.ToString("yyyy-MM-dd"),
            batchSize,
            batchSize * invoiceValue,
            sequence));

        for (int i = 0; i < batchSize; i++)
        {
            sb.AppendLine(string.Format("H^CS{0}^{1}^A{2}^1^{3}^GBP^{4}^{5}^GBP",
                invoiceNumber.ToString("D9"),
                requestNumber.ToString("D3"),
                invoiceNumber.ToString("D7"),
                frn,
                invoiceValue,
                deliveryBody));

            sb.AppendLine(string.Format("L^CS{0}^{1}^{2}^5704A^ERD14^A{3}/MT^{4}^1^G00 - Gross value of claim^{5}^SOS273",
                invoiceNumber.ToString("D9"),
                invoiceValue,
                schemeYear,
                invoiceNumber.ToString("D11"),
                deliveryBody,
                new DateTime(schemeYear, 12, 1).ToString("yyyy-MM-dd")));

            invoiceNumber++;
            frn++;
        }

        return sb.ToString();
    }
}
