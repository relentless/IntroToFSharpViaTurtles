// F# Language Demo - Advanced
// ===========================

// ** units of measure **
[<Measure>] type mile
[<Measure>] type hour

let distanceTravelled = 200<mile>
let timeTaken = 3<hour>

let speed (distance:int<mile>) (time:int<hour>) =
    distance / time

speed 2 120
//speed 120<mile> 2<hour>

let area = 5<mile> * 3<mile>


// ** Type Providers **

#r @".\packages\FSharp.Data.2.2.2\lib\net40\FSharp.Data.dll"
#load "packages/FSharp.Charting.0.82/FSharp.Charting.fsx"
open FSharp.Charting
open FSharp.Data

// JSON Type Provider

type MapInfo = JsonProvider<"gmaps.json">
let locations = MapInfo.Load("gmaps.json")

// WorldBank Type Provider

// http://data.worldbank.org/developers/data-catalog-api

let wb = WorldBankData.GetDataContext()


// ** interactive charting **

wb.Countries.``United Kingdom``
    .Indicators.``School enrollment, tertiary (% gross)``
|> Chart.Line

let countries = 
 [| wb.Countries.Australia
    wb.Countries.Albania
    wb.Countries.``United Kingdom``
    wb.Countries.``United States`` |]

[ for c in countries ->
    c.Indicators.``School enrollment, tertiary (% gross)`` ]
|> List.map Chart.Line
|> Chart.Combine


// Backup charting

Chart.Pie( [(10,5);(7,3);(19,6)], Labels=["Europe";"USA";"Asia"])

// set up some data
let futureDate numDays = System.DateTime.Today.AddDays(float numDays)
let rnd = System.Random()
let rand() = rnd.NextDouble()

let expectedIncome = [ for x in 1 .. 100 -> (futureDate x, 1000.0 + rand() * 100.0 * exp (float x / 40.0) ) ]
let expectedExpenses = [ for x in 1 .. 100 -> (futureDate x, rand() * 500.0 * sin (float x / 50.0) ) ]
let computedProfit = (expectedIncome, expectedExpenses) ||> List.map2 (fun (d1,i) (d2,e) -> (d1, i - e))

// show it!
Chart.Combine(
   [ Chart.Line(expectedIncome,Name="Income")
     Chart.Line(expectedExpenses,Name="Expenses") 
     Chart.Line(computedProfit,Name="Profit") ])
