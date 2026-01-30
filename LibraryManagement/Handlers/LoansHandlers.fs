module LibraryManagement.Handlers.LoansHandlers

open Giraffe
open Microsoft.AspNetCore.Http
open LibraryManagement.Models
open LibraryManagement.Data

let getAllLoans: HttpHandler =
    fun (next: HttpFunc) (ctx: HttpContext) ->
        let db = ctx.GetService<Database>()
        let loans = db.GetAllLoans()
        json loans next ctx

let createLoan: HttpHandler =
    fun (next: HttpFunc) (ctx: HttpContext) ->
        task {
            let! loan = ctx.BindJsonAsync<Loan>()
            let db = ctx.GetService<Database>()
            let newLoan = db.CreateLoan(loan)
            return! json newLoan next ctx
        }

let returnLoan (id: int): HttpHandler =
    fun (next: HttpFunc) (ctx: HttpContext) ->
        let db = ctx.GetService<Database>()
        match db.ReturnLoan(id) with
        | true -> json {| message = "Loan returned" |} next ctx
        | false -> setStatusCode 404 >=> text "Loan not found" next ctx
