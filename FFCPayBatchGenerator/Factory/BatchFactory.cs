
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFCPayBatchGenerator.Factory
{
    public static class BatchFactory
    {
        public static BaseBatchFactory Create(Request request)
        {
            return request.BatchType switch
            {
                "BPS" => new BPSFactory(request),
                "FDMR" => new FDMRFactory(request),
                "CS" => new CSFactory(request),
                "SFI" => new SFIFactory(request),
                _ => throw new ArgumentException("Invalid batch type")
            };
        }
    }
}
