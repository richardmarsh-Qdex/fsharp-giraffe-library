open System
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.DependencyInjection
open Giraffe
open Giraffe.Endpoints
open LibraryManagement.Handlers

let webApp =
    choose [
        route "/" >=> text "Library Management API"
        subRoute "/api" (
            choose [
                subRoute "/books" (
                    choose [
                        GET >=> BooksHandlers.getAllBooks
                        POST >=> BooksHandlers.createBook
                        routef "/%i" BooksHandlers.getBook
                        routef "/%i" (fun id -> PUT >=> BooksHandlers.updateBook id)
                        routef "/%i" (fun id -> DELETE >=> BooksHandlers.deleteBook id)
                    ])
                subRoute "/members" (
                    choose [
                        GET >=> MembersHandlers.getAllMembers
                        POST >=> MembersHandlers.createMember
                        routef "/%i" MembersHandlers.getMember
                    ])
                subRoute "/loans" (
                    choose [
                        GET >=> LoansHandlers.getAllLoans
                        POST >=> LoansHandlers.createLoan
                        routef "/%i/return" (fun id -> POST >=> LoansHandlers.returnLoan id)
                    ])
            ])
        setStatusCode 404 >=> text "Not Found"
    ]

let configureApp (app: IApplicationBuilder) =
    app.UseGiraffe webApp

let configureServices (services: IServiceCollection) =
    services.AddGiraffe() |> ignore
    services.AddSingleton<LibraryManagement.Data.Database>() |> ignore

[<EntryPoint>]
let main _ =
    WebHostBuilder()
        .UseKestrel()
        .Configure(configureApp)
        .ConfigureServices(configureServices)
        .Build()
        .Run()
    0
