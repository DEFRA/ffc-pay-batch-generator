# ffc-pay-batch-generator
Generate SITI Agri batch files to support testing of FFC payment services

## Prerequisites
- .NET 6 SDK to build
- .NET 6 Runtime to run

## Build
To run the application first build the application using .NET 6 SDK.
The application can then be run as a console application.

```
# build application
dotnet build

# run application in debug configuration
dotnet run --project ./FFCPayBatchGenerator/FFCPayBatchGenerator.csproj

# publish application
# default release path is bin/FFCPayBatchGenerator/Release/net6.0/
dotnet publish ./FFCPayBatchGenerator/ -c Release

# run published application
dotnet <RELEASE PATH>/FFCPayBatchGenerator.dll
```
## Using the console
The console will ask a series of question to determine the type of batch to generate and it's content.

1. Type of batch 
   Valid - `BPS`, `FDMR`, `CS`, `SFIP`

2. Batch sequence
   Valid - any integer from `1` to `9999`, default `1`

3. Batch size - total number of invoices to add to batch file
   Valid - any integer from `1` to `10000`, default `1`

4. Invoice value - the value attached to every invoice gross line
   Valid - any decimal, default `100`, default `100`

5. Invoice request number, ie is first payment or post payment adjustment
   Valid - any integer, default `1`

6. Invoice number for first invoice in batch.  All subseqent invoices will have this number incremented
   Valid - any integer up to `9999999`, default `1`

7. FRN for first invoice in batch.  All subseqent invoices will have this number incremented
   Valid - any large integer up to `9999999999`, default `1000000001`

8. Delivery body
   Valid - any string, default `RP00` for all batches except CS which is `NE00`

9. Marketing year
   Valid - any integer from `2015` to `2099`, default is current year

## File output
Generated batch files will be written to a `Files` directory created in the location of the executing assembly.  

## Patterns
The application uses the factory pattern to orchestrate building of different batch files.
