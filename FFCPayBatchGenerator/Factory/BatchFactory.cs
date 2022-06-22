
using System;

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
                "LSES" => new LSESFactory(request),
                _ => throw new ArgumentException("Invalid batch type")
            };
        }
    }
}
