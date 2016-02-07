open FSharp.Data
open System


[<Literal>]
let connectionString = 
    @"Data Source=localhost;Initial Catalog=AdventureWorks;Integrated Security=True"

do
    use cmd = new SqlCommandProvider<"
        SELECT TOP(@topN) FirstName, LastName, SalesYTD 
        FROM Sales.vSalesPerson
        WHERE CountryRegionName = @regionName AND SalesYTD > @salesMoreThan 
        ORDER BY SalesYTD
        " , connectionString>()

    cmd.Execute(topN = 3L, regionName = "United States", salesMoreThan = 1000000M) |> printfn "%A"


(* 
Section 2.
*)
printfn("\n\n")

type ShipMethod = SqlEnumProvider<"
    SELECT Name, ShipMethodID FROM Purchasing.ShipMethod ORDER BY ShipMethodID", connectionString>

//Combine with SqlCommandProvider
do 
    use cmd = new SqlCommandProvider<"
        SELECT COUNT(*) 
        FROM Purchasing.PurchaseOrderHeader 
        WHERE ShipDate > @shippedLaterThan AND ShipMethodID = @shipMethodId
    ", connectionString>() 
    //overnight orders shipped since Jan 1, 2008 
    cmd.Execute( System.DateTime( 2008, 1, 1), ShipMethod.``OVERNIGHT J-FAST``) |> printfn "%A"

Console.ReadLine() |> ignore