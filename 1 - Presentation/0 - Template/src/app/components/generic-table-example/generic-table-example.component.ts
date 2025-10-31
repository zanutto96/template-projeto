import { Component, OnInit } from '@angular/core';
import { TableColumn, TableConfig, TableAction } from '../../common/components/generic-table/generic-table.component';

@Component({
  selector: 'app-generic-table-example',
  templateUrl: './generic-table-example.component.html',
  styleUrls: ['./generic-table-example.component.scss'],
  standalone: false
})
export class GenericTableExampleComponent implements OnInit {
  
  // Example 1: Basic Table
  basicTableConfig: TableConfig;
  basicTableData: any[] = [];

  // Example 2: Advanced Table with all types
  advancedTableConfig: TableConfig;
  advancedTableData: any[] = [];

  // Example 3: Selectable Table
  selectableTableConfig: TableConfig;
  selectableTableData: any[] = [];

  selectedRows: any[] = [];

  ngOnInit() {
    this.initializeBasicTable();
    this.initializeAdvancedTable();
    this.initializeSelectableTable();
  }

  private initializeBasicTable() {
    this.basicTableConfig = {
      columns: [
        { field: 'id', header: 'ID', type: 'numeric', sortable: true, width: '80px' },
        { field: 'name', header: 'Name', type: 'text', sortable: true },
        { field: 'email', header: 'Email', type: 'text', sortable: true },
        { field: 'role', header: 'Role', type: 'text', sortable: true }
      ],
      paginate: true,
      pageSize: 5,
      pageSizeOptions: [5, 10, 20],
      showFilters: true,
      filtersVisible: false
    };

    this.basicTableData = [
      { id: 1, name: 'John Doe', email: 'john@example.com', role: 'Admin' },
      { id: 2, name: 'Jane Smith', email: 'jane@example.com', role: 'User' },
      { id: 3, name: 'Bob Johnson', email: 'bob@example.com', role: 'Editor' },
      { id: 4, name: 'Alice Brown', email: 'alice@example.com', role: 'User' },
      { id: 5, name: 'Charlie Wilson', email: 'charlie@example.com', role: 'Admin' },
      { id: 6, name: 'Diana Martinez', email: 'diana@example.com', role: 'User' },
      { id: 7, name: 'Eve Anderson', email: 'eve@example.com', role: 'Editor' }
    ];
  }

  private initializeAdvancedTable() {
    const actions: TableAction[] = [
      {
        id: 'edit',
        icon: 'fas fa-edit',
        tooltip: 'Edit',
        cssClass: 'btn-outline-primary'
      },
      {
        id: 'delete',
        icon: 'fas fa-trash',
        tooltip: 'Delete',
        cssClass: 'btn-outline-danger',
        condition: (row) => row.status !== 'completed'
      },
      {
        id: 'view',
        icon: 'fas fa-eye',
        tooltip: 'View Details',
        cssClass: 'btn-outline-info'
      }
    ];

    this.advancedTableConfig = {
      columns: [
        { field: 'id', header: 'Order ID', type: 'text', sortable: true, width: '100px' },
        { 
          field: 'thumbnail', 
          header: 'Image', 
          type: 'thumbnail', 
          sortable: false, 
          filterable: false,
          config: {
            aspectRatio: { w: 1, h: 1 }
          }
        },
        { field: 'product', header: 'Product', type: 'text', sortable: true },
        { 
          field: 'price', 
          header: 'Price', 
          type: 'currency', 
          sortable: true,
          config: {
            currencySymbol: '$',
            currencyCode: 'USD'
          }
        },
        { field: 'quantity', header: 'Qty', type: 'numeric', sortable: true, width: '80px' },
        { 
          field: 'date', 
          header: 'Order Date', 
          type: 'date', 
          sortable: true,
          config: {
            dateFormat: 'MM/dd/yyyy'
          }
        },
        { 
          field: 'progress', 
          header: 'Progress', 
          type: 'progress', 
          sortable: true,
          config: {
            showLabel: true,
            progressColor: '#035388'
          }
        },
        { field: 'active', header: 'Active', type: 'boolean', sortable: true },
        { 
          field: 'actions', 
          header: 'Actions', 
          type: 'actions', 
          sortable: false, 
          filterable: false,
          config: {
            actions: actions
          }
        }
      ],
      paginate: true,
      pageSize: 10,
      pageSizeOptions: [5, 10, 25],
      showFilters: true,
      filtersVisible: true
    };

    this.advancedTableData = [
      {
        id: 'ORD-001',
        thumbnail: 'assets/imgs/products/product-1.jpg',
        product: 'Wireless Headphones',
        price: 89.99,
        quantity: 2,
        date: '2025-10-15',
        progress: 75,
        active: true,
        status: 'processing'
      },
      {
        id: 'ORD-002',
        thumbnail: 'assets/imgs/products/product-2.jpg',
        product: 'Smart Watch',
        price: 299.99,
        quantity: 1,
        date: '2025-10-20',
        progress: 100,
        active: true,
        status: 'completed'
      },
      {
        id: 'ORD-003',
        thumbnail: 'assets/imgs/products/product-3.jpg',
        product: 'Laptop Stand',
        price: 45.50,
        quantity: 3,
        date: '2025-10-22',
        progress: 30,
        active: false,
        status: 'pending'
      },
      {
        id: 'ORD-004',
        thumbnail: 'assets/imgs/products/product-4.jpg',
        product: 'USB-C Cable',
        price: 12.99,
        quantity: 5,
        date: '2025-10-24',
        progress: 50,
        active: true,
        status: 'processing'
      },
      {
        id: 'ORD-005',
        thumbnail: 'assets/imgs/products/product-5.jpg',
        product: 'Mechanical Keyboard',
        price: 149.99,
        quantity: 1,
        date: '2025-10-25',
        progress: 10,
        active: true,
        status: 'pending'
      }
    ];
  }

  private initializeSelectableTable() {
    this.selectableTableConfig = {
      columns: [
        { field: 'id', header: 'Task ID', type: 'numeric', sortable: true, width: '100px' },
        { field: 'title', header: 'Task Title', type: 'text', sortable: true },
        { field: 'assignee', header: 'Assignee', type: 'text', sortable: true },
        { field: 'dueDate', header: 'Due Date', type: 'date', sortable: true },
        { field: 'completed', header: 'Completed', type: 'checkbox', sortable: true },
        { 
          field: 'priority', 
          header: 'Priority', 
          type: 'text', 
          sortable: true,
          cssClass: 'priority-cell'
        }
      ],
      paginate: true,
      pageSize: 5,
      pageSizeOptions: [5, 10, 20],
      showFilters: true,
      filtersVisible: false,
      selectable: true
    };

    this.selectableTableData = [
      { id: 1, title: 'Fix login bug', assignee: 'John Doe', dueDate: '2025-10-28', completed: false, priority: 'High' },
      { id: 2, title: 'Update documentation', assignee: 'Jane Smith', dueDate: '2025-10-30', completed: true, priority: 'Low' },
      { id: 3, title: 'Implement new feature', assignee: 'Bob Johnson', dueDate: '2025-11-05', completed: false, priority: 'Medium' },
      { id: 4, title: 'Code review', assignee: 'Alice Brown', dueDate: '2025-10-27', completed: true, priority: 'High' },
      { id: 5, title: 'Deploy to production', assignee: 'Charlie Wilson', dueDate: '2025-11-01', completed: false, priority: 'Critical' }
    ];
  }

  onActionClicked(event: { action: string; row: any }) {
    console.log('Action clicked:', event.action, 'Row:', event.row);
    
    switch (event.action) {
      case 'edit':
        alert(`Edit order: ${event.row.id}`);
        break;
      case 'delete':
        alert(`Delete order: ${event.row.id}`);
        break;
      case 'view':
        alert(`View details for order: ${event.row.id}`);
        break;
    }
  }

  onRowClicked(row: any) {
    console.log('Row clicked:', row);
  }

  onSelectionChanged(selectedRows: any[]) {
    this.selectedRows = selectedRows;
    console.log('Selected rows:', selectedRows);
  }

  onPageChanged(event: any) {
    console.log('Page changed:', event);
  }
}
