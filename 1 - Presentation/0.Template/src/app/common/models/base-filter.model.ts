export interface BaseFilter {
  // Pagination and filtering properties
  pageIndex?: number;
  pageSize?: number;
  isPagination?: boolean;
  isOrderByDomain?: boolean;
  orderFields?: string[];
  orderBy?: string;
  orderByType?: 'OrderBy' | 'OrderByDescending';
}