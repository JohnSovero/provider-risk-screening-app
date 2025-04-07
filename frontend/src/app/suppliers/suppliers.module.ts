import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../material/material.module';
import { SupplierListComponent } from './supplier-list/supplier-list.component';
import { ScreeningDialogComponent } from './screening-dialog/screening-dialog.component';

@NgModule({
  declarations: [
    SupplierListComponent,
    ScreeningDialogComponent,
  ],
  imports: [
    CommonModule,
    MaterialModule,
  ],
})
export class SuppliersModule { }