export type BookStatus = "Available" | "Borrowed";

export interface Book {
  id: number;
  title: string;
  author: string;
  status: BookStatus;
}

export interface CreateBookInput {
  title: string;
  author: string;
}

export interface UpdateBookInput {
  title: string;
  author: string;
  status: BookStatus;
}