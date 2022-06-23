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
    protected bool createChecksum;
    protected bool createControl;
    protected bool pendingRename;
    protected string pendingPrefix = "PENDING_";

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
        createChecksum = request.CreateChecksum;
        createControl = request.CreateControl;
        pendingRename = request.PendingRename;
    }

    public abstract string GetFileName();

    public abstract string GetContent();
}
