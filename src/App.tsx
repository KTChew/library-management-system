import { useEffect, useState } from "react";
import { Routes, Route, Link } from "react-router-dom";

import BookFilters from "./components/BookFilters";
import BookForm from "./components/BookForm";
import BookTable from "./components/BookTable";
import Home from "./pages/Home";
import About from "./pages/About";

import useBooks from "./hooks/useBooks";
import {
  deleteBook,
  updateBook
} from "./services/bookService";

import type {
    Book,
    BookStatus,
    BookFilter
} from "./types/Book";

import "./App.css";


function App() {
  const { books, setBooks } = useBooks();

  const [editBook, setEditBook] =
    useState<Book | null>(null);

  const [searchTerm, setSearchTerm] =
    useState<string>("");

  const [filter, setFilter] =
    useState<BookFilter>("ALL");

  const filteredBooks = books.filter((book) => {
    const matchesSearch = book.title
      .toLowerCase()
      .includes(searchTerm.toLowerCase());

    const matchesFilter =
      filter === "ALL" ||
      book.status.toUpperCase() === filter;

    return matchesSearch && matchesFilter;
  });

  function handleEdit(book: Book): void {
    setEditBook(book);
  }

  async function handleDelete(id: number): Promise<void> {
    try {
      await deleteBook(id);

      setBooks((currentBooks) =>
        currentBooks.filter((book) => book.id !== id)
      );
    } catch (error: unknown) {
      console.error("Failed to delete book:", error);
    }
  }

  async function toggleStatus(id: number): Promise<void> {
    const book = books.find(
      (currentBook) => currentBook.id === id
    );

    if (!book) {
      return;
    }

    const newStatus: BookStatus =
      book.status === "Available"
        ? "Borrowed"
        : "Available";

    const savedBook = await updateBook({
      ...book,
      status: newStatus
    });

    setBooks((currentBooks) =>
      currentBooks.map((currentBook) =>
        currentBook.id === id
          ? savedBook
          : currentBook
      )
    );
  }

  useEffect(() => {
    document.title = "Library Dashboard";
  }, []);

  return (
    <div style={{ padding: "20px" }}>
      <nav style={{ marginBottom: "20px" }}>
        <Link to="/" style={{ marginRight: "10px" }}>
          Home
        </Link>

        <Link
          to="/books"
          style={{ marginRight: "10px" }}
        >
          Books
        </Link>

        <Link to="/about">About</Link>
      </nav>

      <Routes>
        <Route path="/" element={<Home />} />

        <Route path="/about" element={<About />} />

        <Route
          path="/books"
          element={
            <div>
              <h2>Library Dashboard</h2>

              <div className="container">
                <h1 className="page-title">
                  📚 Library Dashboard
                </h1>

                <BookFilters
                  filter={filter}
                  setFilter={setFilter}
                />

                <input
                  type="text"
                  placeholder="Search books..."
                  value={searchTerm}
                  onChange={(event) =>
                    setSearchTerm(event.target.value)
                  }
                  className="search-box"
                />

                <p>Book List</p>

                <BookForm
                  books={books}
                  setBooks={setBooks}
                  editBook={editBook}
                  setEditBook={setEditBook}
                />

                <BookTable
                  books={filteredBooks}
                  onDelete={handleDelete}
                  onToggle={toggleStatus}
                  onEdit={handleEdit}
                />
              </div>
            </div>
          }
        />
      </Routes>
    </div>
  );
}

export default App;