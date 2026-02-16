module LibraryManagement.Handlers.MembersHandlers

open Giraffe
open Microsoft.AspNetCore.Http
open LibraryManagement.Models
open LibraryManagement.Data

let getAllMembers: HttpHandler =
    fun (next: HttpFunc) (ctx: HttpContext) ->
        let db = ctx.GetService<Database>()
        let members = db.GetAllMembers()
        json members next ctx

let createMember: HttpHandler =
    fun (next: HttpFunc) (ctx: HttpContext) ->
        task {
            let! member = ctx.BindJsonAsync<Member>()
            let db = ctx.GetService<Database>()
            let newMember = db.CreateMember(member)
            return! json newMember next ctx
        }

let getMember (id: int): HttpHandler =
    fun (next: HttpFunc) (ctx: HttpContext) ->
        let db = ctx.GetService<Database>()
        match db.GetMemberById(id) with
        | Some member -> json member next ctx
        | None -> setStatusCode 404 >=> text "Member not found" next ctx
