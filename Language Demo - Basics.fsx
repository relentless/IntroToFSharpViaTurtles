// F# Language Demo - Basics
// =========================

open System.Drawing;


// ** run code interactively in the REPL **
let isEven num = 
    num % 2 = 0

// ^ not much syntax!
// expression-based


// ** first class functions **
List.filter isEven [1..10]


// ** immutable by default**
let x = 10
x <- 20

// much recursion / declarative


// ** type inference **
let add x y =
    x + y

add 5 7


// ** currying & partial application **
let add10 = add 10

add10 5

List.map (add 10) [1..5]


// ** automatic generalisation **
let print thing =
    sprintf "%A" thing


// ** pipe operator **

// old way
List.sum (List.map add10 (List.filter isEven [1..10]))

// better way
[1..10]
|> List.filter isEven
|> List.map add10
|> List.sum


// ** Record types **
type Person = { Name: string; Age: int; Eyes:Color }

let jim = {Name = "Jim"; Age = 34; Eyes = Color.Blue}

let oldJim = {jim with Age = 87}


// ** Discriminated Union types **
type Shape =
    | Circle
    | Rectangle of int * int
    | Square of int

let myShape = Square(10)


// ** Pattern Matching **
let printShape shape =
    match shape with
    | Circle -> printfn "I'm a circle"
    | Rectangle(width, height) -> printfn "I'm a rectangle of size %ix%i" width height
    | Square(0) -> printfn "I'm a really small square"
    | Square(x) when x > 100 -> printfn "I'm a really big square"
    | Square(_) -> printfn "I'm some other square"

// ^ checks all patterns covered

let myTuple = (1, 2, 3)

let x, y, z = myTuple