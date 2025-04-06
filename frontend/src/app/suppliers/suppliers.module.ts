import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../material/material.module';
import { SupplierListComponent } from './supplier-list/supplier-list.component';

@NgModule({
  declarations: [
    SupplierListComponent,
  ],
  imports: [
    CommonModule,
    MaterialModule,
  ],
})
export class SuppliersModule { }