import type { BookFilter } from "../types/Book";

interface BookFiltersProps {
    filter: BookFilter;
    setFilter: React.Dispatch<
        React.SetStateAction<BookFilter>
    >;
}

export default function BookFilters({
    filter,
    setFilter
}: BookFiltersProps) {

    return (
        <div style={{ marginBottom: "15px" }}>

            <button
                style={{
                    marginRight: "10px",
                    padding: "8px 12px",
                    cursor: "pointer"
                }}
                onClick={() => setFilter("ALL")}
            >
                All
            </button>

            <button
                style={{
                    marginRight: "10px",
                    padding: "8px 12px",
                    cursor: "pointer"
                }}
                onClick={() => setFilter("AVAILABLE")}
            >
                Available
            </button>

            <button
                style={{
                    marginRight: "10px",
                    padding: "8px 12px",
                    cursor: "pointer"
                }}
                onClick={() => setFilter("BORROWED")}
            >
                Borrowed
            </button>

        </div>
    );
}