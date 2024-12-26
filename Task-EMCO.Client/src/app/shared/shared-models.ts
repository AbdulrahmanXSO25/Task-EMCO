export interface Result<T> {
    success: boolean;
    data: T;
    message: string;
}

export interface PaginatedResult<T> {
    data: T[];
    totalCount: number;
    pageNumber: number;
    pageSize: number;
}  