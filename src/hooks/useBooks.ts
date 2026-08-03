import { useEffect, useState } from "react";
import { getBooks } from "../services/bookService";
import type { Book } from "../types/Book";

export default function useBooks() {
  const [books, setBooks] = useState<Book[]>([]);

  useEffect(() => {
    async function loadBooks(): Promise<void> {
      try {
        const data = await getBooks();
        setBooks(data);
      } catch (error: unknown) {
        console.error("Failed to load books:", error);
      }
    }

    loadBooks();
  }, []);

  return {
    books,
    setBooks
  };
}