module LibraryManagement.Models

open System

type Book = {
    Id: int
    Title: string
    Author: string
    ISBN: string
    PublishedYear: int
    Available: bool
}

type Member = {
    Id: int
    Name: string
    Email: string
    Phone: string
    JoinDate: DateTime
}

type Loan = {
    Id: int
    BookId: int
    MemberId: int
    LoanDate: DateTime
    ReturnDate: DateTime option
    DueDate: DateTime
}
