module LibraryManagement.Handlers.BooksHandlers

open Giraffe
open Microsoft.AspNetCore.Http
open LibraryManagement.Models
open LibraryManagement.Data

let getAllBooks: HttpHandler =
    fun (next: HttpFunc) (ctx: HttpContext) ->
        let db = ctx.GetService<Database>()
        let books = db.GetAllBooks()
        json books next ctx

let createBook: HttpHandler =
    fun (next: HttpFunc) (ctx: HttpContext) ->
        task {
            let! book = ctx.BindJsonAsync<Book>()
            let db = ctx.GetService<Database>()
            let newBook = db.CreateBook(book)
            return! json newBook next ctx
        }

let getBook (id: int): HttpHandler =
    fun (next: HttpFunc) (ctx: HttpContext) ->
        let db = ctx.GetService<Database>()
        match db.GetBookById(id) with
        | Some book -> json book next ctx
        | None -> setStatusCode 404 >=> text "Book not found" next ctx

let updateBook (id: int): HttpHandler =
    fun (next: HttpFunc) (ctx: HttpContext) ->
        task {
            let! book = ctx.BindJsonAsync<Book>()
            let db = ctx.GetService<Database>()
            match db.UpdateBook(id, book) with
            | true -> return! json {| message = "Book updated" |} next ctx
            | false -> return! setStatusCode 404 >=> text "Book not found" next ctx
        }

let deleteBook (id: int): HttpHandler =
    fun (next: HttpFunc) (ctx: HttpContext) ->
        let db = ctx.GetService<Database>()
        match db.DeleteBook(id) with
        | true -> json {| message = "Book deleted" |} next ctx
        | false -> setStatusCode 404 >=> text "Book not found" next ctx
