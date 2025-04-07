import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { SupplierResponse } from '../suppliers/interfaces/supplier.interface';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-add-edit-supplier-dialog',
  imports: [CommonModule, MatSelectModule, ReactiveFormsModule, MatFormFieldModule, MatInputModule],
  templateUrl: './add-edit-supplier-dialog.component.html',
  styleUrls: ['./add-edit-supplier-dialog.component.css']
})
export class AddEditSupplierDialogComponent {
  supplierForm: FormGroup;

  constructor(
    private dialogRef: MatDialogRef<AddEditSupplierDialogComponent>,
    private fb: FormBuilder,
    @Inject(MAT_DIALOG_DATA) public data: { supplier: SupplierResponse; isNew: boolean }
  ) {
    this.supplierForm = this.fb.group({
      businessName: [this.data?.supplier.businessName || '', [Validators.required]],
      tradeName: [this.data?.supplier.tradeName || '', [Validators.required]],
      taxId: [this.data?.supplier.taxId || '', [Validators.required, Validators.pattern(/^\d{11}$/)]],
      phone: [this.data?.supplier.phone || '', [Validators.required,]],
      email: [this.data?.supplier.email || '', [Validators.required, Validators.email]],
      website: [this.data?.supplier.website || '', [Validators.required, Validators.pattern(/^(https?:\/\/)?([\w\-]+\.)+[\w\-]+(\/[\w\-._~:/?#[\]@!$&'()*+,;=]*)?$/)]],
      address: [this.data?.supplier.address || '', [Validators.required]],
      country: [this.data?.supplier.country || '', [Validators.required]],
      annualBilling: [this.data?.supplier.annualBilling || '', [Validators.required,]]
    });
  }

  save() {
    if (this.supplierForm.valid) {
      const supplier = this.supplierForm.value;
      if(this.data.isNew == false) supplier.id = this.data.supplier.id;
      this.dialogRef.close(supplier);
    }
  }

  cancel() {
    this.dialogRef.close();
  }
}