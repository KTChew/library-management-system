import type {
  Book,
  CreateBookInput,
  UpdateBookInput
} from "../types/Book";

const API_URL = "https://localhost:7222/api/books";

export async function getBooks(): Promise<Book[]> {
  const response = await fetch(API_URL);

  if (!response.ok) {
    throw new Error("Failed to load books");
  }

  return (await response.json()) as Book[];
}

export async function addBook(
  book: CreateBookInput
): Promise<Book> {
  const response = await fetch(API_URL, {
    method: "POST",
    headers: {
      "Content-Type": "application/json"
    },
    body: JSON.stringify(book)
  });

  if (!response.ok) {
    throw new Error("Failed to add book");
  }

  return (await response.json()) as Book;
}

export async function updateBook(
  book: Book
): Promise<Book> {
  const updateRequest: UpdateBookInput = {
    title: book.title,
    author: book.author,
    status: book.status
  };

  const response = await fetch(`${API_URL}/${book.id}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json"
    },
    body: JSON.stringify(updateRequest)
  });

  if (!response.ok) {
    throw new Error("Failed to update book");
  }

  return (await response.json()) as Book;
}

export async function deleteBook(id: number): Promise<void> {
  const response = await fetch(`${API_URL}/${id}`, {
    method: "DELETE"
  });

  if (!response.ok) {
    throw new Error("Failed to delete book");
  }
}