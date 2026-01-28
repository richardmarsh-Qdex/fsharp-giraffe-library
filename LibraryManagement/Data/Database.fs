module LibraryManagement.Data

open System
open System.Collections.Generic
open LibraryManagement.Models

type Database() =
    let mutable books = List<Book>()
    let mutable members = List<Member>()
    let mutable loans = List<Loan>()
    let mutable nextBookId = 1
    let mutable nextMemberId = 1
    let mutable nextLoanId = 1

    member this.GetAllBooks() = books |> Seq.toList

    member this.GetBookById(id: int) =
        books |> Seq.tryFind (fun b -> b.Id = id)

    member this.CreateBook(book: Book) =
        let newBook = { book with Id = nextBookId }
        nextBookId <- nextBookId + 1
        books.Add(newBook)
        newBook

    member this.UpdateBook(id: int, book: Book) =
        match books |> Seq.tryFindIndex (fun b -> b.Id = id) with
        | Some index ->
            books.[index] <- { book with Id = id }
            true
        | None -> false

    member this.DeleteBook(id: int) =
        match books |> Seq.tryFindIndex (fun b -> b.Id = id) with
        | Some index ->
            books.RemoveAt(index)
            true
        | None -> false

    member this.GetAllMembers() = members |> Seq.toList

    member this.GetMemberById(id: int) =
        members |> Seq.tryFind (fun m -> m.Id = id)

    member this.CreateMember(member: Member) =
        let newMember = { member with Id = nextMemberId }
        nextMemberId <- nextMemberId + 1
        members.Add(newMember)
        newMember

    member this.GetAllLoans() = loans |> Seq.toList

    member this.CreateLoan(loan: Loan) =
        let newLoan = { loan with Id = nextLoanId }
        nextLoanId <- nextLoanId + 1
        loans.Add(newLoan)
        newLoan

    member this.ReturnLoan(id: int) =
        match loans |> Seq.tryFindIndex (fun l -> l.Id = id) with
        | Some index ->
            let loan = loans.[index]
            loans.[index] <- { loan with ReturnDate = Some DateTime.Now }
            true
        | None -> false
