module BoleroElectron.Client.Main

open Elmish
open Bolero
open Bolero.Html
open Bolero.Templating.Client

type Model =
    {
        value: int
    }

let initModel =
    {
        value = 0
    }

type Message =
    | Increment
    | Decrement

let update message model =
    match message with
    | Increment -> { model with value = model.value + 1 }
    | Decrement -> { model with value = model.value - 1 }

type Button = Template<"wwwroot/button.html">

let view model dispatch =
    concat [
        Button().Text("-").Click(fun _ -> dispatch Decrement).Elt()
        span [] [textf " %i " model.value]
        Button().Text("+").Click(fun _ -> dispatch Increment).Elt()
    ]

type MyApp() =
    inherit ProgramComponent<Model, Message>()

    override this.Program =
        Program.mkSimple (fun _ -> initModel) update view
#if DEBUG
        |> Program.withHotReloading
#endif
