import { useEffect, useState } from "react";
import { addBook, updateBook } from "../services/bookService";
import type { Book, CreateBookInput } from "../types/Book";

interface BookFormProps {
  books: Book[];
  setBooks: React.Dispatch<React.SetStateAction<Book[]>>;
  editBook: Book | null;
  setEditBook: React.Dispatch<React.SetStateAction<Book | null>>;
}

export default function BookForm({
  books,
  setBooks,
  editBook,
  setEditBook
}: BookFormProps) {
  const [title, setTitle] = useState<string>("");
  const [author, setAuthor] = useState<string>("");

  useEffect(() => {
    if (editBook) {
      setTitle(editBook.title);
      setAuthor(editBook.author);
    }
  }, [editBook]);

  async function handleSubmit(): Promise<void> {
    if (!title.trim() || !author.trim()) {
      return;
    }

    if (editBook) {
      const updatedBook = await updateBook({
        ...editBook,
        title: title.trim(),
        author: author.trim()
      });

      setBooks((currentBooks) =>
        currentBooks.map((book) =>
          book.id === updatedBook.id ? updatedBook : book
        )
      );

      setEditBook(null);
    } else {
      const newBook: CreateBookInput = {
        title: title.trim(),
        author: author.trim()
      };

      const savedBook = await addBook(newBook);

      setBooks((currentBooks) => [
        ...currentBooks,
        savedBook
      ]);
    }

    setTitle("");
    setAuthor("");
  }

  return (
    <div style={{ marginTop: "20px", marginBottom: "20px" }}>
      <input
        placeholder="Title"
        value={title}
        onChange={(event: React.ChangeEvent<HTMLInputElement>) =>
          setTitle(event.target.value)
        }
        style={{
          marginRight: "10px",
          padding: "8px"
        }}
      />

      <input
        placeholder="Author"
        value={author}
        onChange={(event: React.ChangeEvent<HTMLInputElement>) =>
          setAuthor(event.target.value)
        }
        style={{
          marginRight: "10px",
          padding: "8px"
        }}
      />

      <button onClick={handleSubmit}>
        {editBook ? "Update Book" : "Add Book"}
      </button>
    </div>
  );
}