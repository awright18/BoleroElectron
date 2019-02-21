namespace BoleroElectron.Server

open System
open Microsoft.AspNetCore
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Http
open Microsoft.Extensions.DependencyInjection
open Bolero.Templating.Server
open BoleroElectron
open ElectronNET.API
open System.Threading

type Startup() =

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    member this.ConfigureServices(services: IServiceCollection) =
        services
#if DEBUG
         //   .AddHotReload(templateDir = "../BoleroElectron.Client/wwwroot")
#endif
        |> ignore

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    member this.Configure(app: IApplicationBuilder, env: IHostingEnvironment) =
        app
#if DEBUG
           // .UseHotReload()
#endif
            .UseBlazor<Client.Startup>()
        |> ignore
        
        Electron.WindowManager.CreateWindowAsync()|> Async.AwaitTask |> ignore
            

module Program =

    [<EntryPoint>]
    let main args =
        WebHost
            .CreateDefaultBuilder(args)          
            .UseStartup<Startup>()
            .UseElectron(args)
            .Build()
            .Run()
        0
