import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { ScreeningDialogComponent } from '../screening-dialog/screening-dialog.component';
import { MatPaginator } from '@angular/material/paginator';
import { SupplierService } from '../services/supplier.service';

@Component({
  selector: 'app-supplier-list',
  imports: [],
  templateUrl: './supplier-list.component.html',
  styleUrl: './supplier-list.component.css',
})
export class SupplierListComponent implements OnInit, AfterViewInit {
  displayedColumns: string[] = ['name', 'updatedAt', 'actions'];
  dataSource = new MatTableDataSource<any>([]);

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
    private service: SupplierService, 
    private dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.service.getAllSuppliers().subscribe((suppliers: any[]) => {
      suppliers.sort((a, b) => new Date(b.updatedAt).getTime() - new Date(a.updatedAt).getTime());
      this.dataSource.data = suppliers;
    });
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
  }

  view(s: any) { /* ... */ }
  edit(s: any) { /* ... */ }
  delete(s: any) { /* ... */ }
  openScreening(supplier: any): void {
    this.dialog.open(ScreeningDialogComponent, {
      width: '600px',
      data: { supplier },
    });
  }
}