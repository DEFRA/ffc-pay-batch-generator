
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFCPayBatchGenerator.Factory;
public class BatchFactory
{
    public BaseBatchFactory Create(Request request)
    {
        switch (request.BatchType)
        {
            case "BPS":
                return new BPSFactory(request);
            case "FDMR":
                return new FDMRFactory(request);
            case "CS":
                return new CSFactory(request);
            case "SFI":
                return new SFIFactory(request);
            default:
                throw new ArgumentException("Invalid batch type");
        }
    }
}
