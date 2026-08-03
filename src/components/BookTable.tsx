import type { CSSProperties } from "react";
import type { Book } from "../types/Book";

interface BookTableProps {
  books: Book[];
  onDelete: (id: number) => void;
  onToggle: (id: number) => void;
  onEdit: (book: Book) => void;
}

export default function BookTable({
  books,
  onDelete,
  onToggle,
  onEdit
}: BookTableProps) {
  const thStyle: CSSProperties = {
    textAlign: "left",
    borderBottom: "2px solid #ccc",
    padding: "8px"
  };

  const tdStyle: CSSProperties = {
    padding: "8px",
    borderBottom: "1px solid #eee"
  };

  return (
    <table
      style={{
        width: "100%",
        borderCollapse: "collapse",
        marginTop: "20px"
      }}
    >
      <thead>
        <tr>
          <th style={thStyle}>Title</th>
          <th style={thStyle}>Author</th>
          <th style={thStyle}>Status</th>
          <th style={thStyle}>Actions</th>
          <th style={thStyle}>Status Action</th>
        </tr>
      </thead>

      <tbody>
        {books.length === 0 ? (
          <tr>
            <td
              colSpan={5}
              style={{
                textAlign: "center",
                padding: "20px"
              }}
            >
              No books found.
            </td>
          </tr>
        ) : (
          books.map((book) => (
            <tr key={book.id}>
              <td style={tdStyle} className="table-cell">
                {book.title}
              </td>

              <td style={tdStyle} className="table-cell">
                {book.author}
              </td>

              <td style={tdStyle} className="table-cell">
                <span
                  style={{
                    padding: "4px 8px",
                    borderRadius: "6px",
                    color: "white",
                    backgroundColor:
                      book.status === "Available"
                        ? "green"
                        : "red"
                  }}
                >
                  {book.status}
                </span>
              </td>

              <td style={tdStyle} className="table-cell">
                <button
                  onClick={() => onEdit(book)}
                  style={{ marginRight: "10px" }}
                >
                  Edit
                </button>

                <button onClick={() => onDelete(book.id)}>
                  Delete
                </button>
              </td>

              <td style={tdStyle} className="table-cell">
                <button onClick={() => onToggle(book.id)}>
                  Toggle Status
                </button>
              </td>
            </tr>
          ))
        )}
      </tbody>
    </table>
  );
}