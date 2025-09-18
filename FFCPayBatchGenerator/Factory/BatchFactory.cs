
using System;

namespace FFCPayBatchGenerator.Factory;
public static class BatchFactory
{
    public static BaseBatchFactory Create(Request request)
    {
        return request.BatchType switch
        {
            "BPS" => new BPSFactory(request),
            "FDMR" => new FDMRFactory(request),
            "CS" => new CSFactory(request),
            "SFIP" => new SFIPFactory(request),
            "SFI" => new SFIFactory(request),
            "SFI23" => new SFI23Factory(request),
            "ESFIO" => new ESFIOFactory(request),
            "LS" => new LSFactory(request),
            "Delinked" => new DelinkedFactory(request),
            "SITICOHTC" => new SITICOHTCFactory(request),
            "SITICOHTR" => new SITICOHTRFactory(request),
            _ => throw new ArgumentException("Invalid batch type")
        };
    }
}
