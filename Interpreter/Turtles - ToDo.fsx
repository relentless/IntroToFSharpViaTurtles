#r @"packages\FParsec.1.0.1\lib\net40-client\FParsecCS.dll"
#r @"packages\FParsec.1.0.1\lib\net40-client\FParsec.dll"

#r "System.Drawing.dll"
#r "System.Windows.Forms.dll"

open FParsec
open System.Windows.Forms
open System.Drawing

// Examples

"forward 50"
"forward 50 right 90 forward 30 left 45 forward 40"
"repeat 4 [forward 100 right 90]"
"repeat 10 [right 36 repeat 5 [forward 54 right 72]]"

// AST
type Command =
    | Forward of float
    // TODO: more commands

// PARSER
let pForward = pstring "forward" >>. spaces1 >>. pfloat |>> (fun x -> Forward(x))
// TODO: more parsers

// TODO: list of commands

/// Converts a string into an AST
let parse text =
    match run pForward text with
    | Success(result, _, _) -> result
    | Failure(errorMsg, _, _) -> failwith ("Parse error: " + errorMsg)

parse "forward 90"

// INTERPRETER
type Turtle = { X: float; Y: float }

/// Converts an AST into a list of lines [((int * int) * (int * int))]
let execute startTurtle ast =

    let newPosition x y angle distance =
        let radians = angle * System.Math.PI / 180.0
        (x + distance * cos radians, y + distance * sin radians)
    
    let rec exec codeToExec turtle =
        match codeToExec with
        | _ -> ((0.0,0.0),(0.0,0.0))
        // TODO: match AST types

    exec ast startTurtle

// DISPLAY

let width,height = 800,600

/// Displays a list of lines on-screen
let display lines =
    let form = new Form (Text="Turtles", Width=width, Height=height)
    let image = new Bitmap(width, height)
    let picture = new PictureBox(Dock=DockStyle.Fill, Image=image)
    do  form.Controls.Add(picture)
    let pen = new Pen(Color.Red)

    let drawLine ((x1,y1),(x2,y2)) =
        use graphics = Graphics.FromImage(image)
        graphics.DrawLine(pen,int x1,int y1,int x2, int y2)

    // TODO: Draw lines

    form.ShowDialog() |> ignore

// TODO: parse, execute, display

