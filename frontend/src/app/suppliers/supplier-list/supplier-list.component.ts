import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { SupplierService } from '../services/supplier.service';
import { SupplierResponse } from '../interfaces/supplier.interface';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormsModule } from '@angular/forms';
import { MatSelectModule } from '@angular/material/select';  // Importa MatSelectModule
import { ScrapperService } from '../../scrapper/services/scrapper.service';
import { AddEditSupplierDialogComponent } from '../../add-edit-provider-dialog/add-edit-supplier-dialog.component';
import { ScreeningDialogComponent } from '../screening-dialog/screening-dialog.component';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@Component({
  imports: [CommonModule, MatTableModule, MatDialogModule, MatFormFieldModule, MatInputModule, FormsModule, MatSelectModule, MatProgressSpinnerModule],
  templateUrl: './supplier-list.component.html',
  styleUrls: ['./supplier-list.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class SupplierListComponent implements OnInit {
  displayedColumns: string[] = ['businessName', 'tradeName', 'taxId', 'phone', 'email', 'website', 'address', 'country', 'annualBilling', 'lastEdited', 'actions'];
  dataSource = new MatTableDataSource<SupplierResponse>([]);
  currentSupplier: SupplierResponse = { id: '', businessName: '', tradeName: '', taxId: '', phone: '', email: '', website: '', address: '', country: '', annualBilling: 0, lastEdited: '' };
  selectedEntities: { [supplierId: string]: string[] } = {};
  isLoading: boolean = false;

  constructor(
    private supplierService: SupplierService,
    private scrapperService: ScrapperService,
    private dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.supplierService.getAllSuppliers().subscribe({
      next: (suppliers: SupplierResponse[]) => {  
        suppliers.sort((a, b) => new Date(b.lastEdited).getTime() - new Date(a.lastEdited).getTime());
        //put lastedited to format fecha y hora
        suppliers.forEach(supplier => {
          const date = new Date(supplier.lastEdited);
          supplier.lastEdited = `${date.toLocaleDateString()} ${date.toLocaleTimeString()}`;
        });
        this.dataSource.data = suppliers;

      },
      error: (error) => {
        console.error('Error al obtener proveedores:', error);
      }
    });
  }

  openAddSupplierDialog(): void {
    this.currentSupplier = { id: '', businessName: '', tradeName: '', taxId: '', phone: '', email: '', website: '', address: '', country: '', annualBilling: 0, lastEdited: '' };

    const dialogRef = this.dialog.open(AddEditSupplierDialogComponent, {
      maxWidth: '95%',
      minWidth: '95%',
      data: { supplier: this.currentSupplier, isNew: true },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.addSupplier(result);
      }
    });
  }

  addSupplier(supplier: SupplierResponse) {
    console.log('Adding supplier:', supplier);
    this.supplierService.createSupplier(supplier).subscribe({
      next: (supplier) => {
        this.dataSource.data = [...this.dataSource.data, supplier];
      },
      error: (error) => {
        console.error('Error al crear proveedor:', error);
      }
    });
  }

  editSupplier(supplier: SupplierResponse): void {
    const dialogRef = this.dialog.open(AddEditSupplierDialogComponent, {
      maxWidth: '95%',
      minWidth: '95%',
      data: { supplier: supplier, isNew: false },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.saveSupplier(result);
      }
    });
  }

  saveSupplier(supplier: SupplierResponse): void {
    console.log('Saving supplier:', supplier);
    this.supplierService.updateSupplier(supplier).subscribe({
      next: (updatedSupplier) => {
        const index = this.dataSource.data.findIndex((item) => item.id === updatedSupplier.id);
        if (index !== -1) {
          this.dataSource.data[index] = updatedSupplier;
          this.dataSource.data = [...this.dataSource.data]; // Refresh the data source
        }
      },
      error: (error) => {
        console.error('Error al actualizar proveedor:', error);
      }
    });
  }

  deleteSupplier(supplier: SupplierResponse): void {
    this.supplierService.deleteSupplier(supplier.id).subscribe({
      next: () => {
        this.dataSource.data = this.dataSource.data.filter((item) => item.id !== supplier.id);
      },
      error: (error) => {
        console.error('Error al eliminar proveedor:', error);
      }
    });
  }

  scrappedSupplier(supplier: SupplierResponse, entidades: string[]): void {
    if (entidades.length === 0) {
      alert('No se seleccionaron entidades para el scrapping');
      return;
    }

    this.isLoading = true;

    this.scrapperService.getScrappedByName(supplier.businessName, entidades).subscribe({
      next: (response) => {
        this.isLoading = false;
        this.dialog.open(ScreeningDialogComponent, {
          maxWidth: '95%',
          minWidth: '90%',
          data: {
            totalHits: response.totalHits,
            results: response.results || []
          }
        });
      },
      error: (error) => {
        this.isLoading = false;
        console.error('Error al realizar screening:', error);
      }
    });
  }
}
