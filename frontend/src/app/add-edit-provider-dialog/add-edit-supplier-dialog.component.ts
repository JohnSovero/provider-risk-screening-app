import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { SupplierResponse } from '../suppliers/interfaces/supplier.interface';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { CommonModule } from '@angular/common';
import { Country, CountrySelectComponent } from '@wlucha/ng-country-select';
@Component({
  selector: 'app-add-edit-supplier-dialog',
  imports: [CommonModule, MatSelectModule, ReactiveFormsModule, MatFormFieldModule, MatInputModule, CountrySelectComponent],
  templateUrl: './add-edit-supplier-dialog.component.html',
  styleUrls: ['./add-edit-supplier-dialog.component.css']
})
export class AddEditSupplierDialogComponent {
  supplierForm: FormGroup;
  countryControl: FormControl<Country | null>;

  constructor(
    private dialogRef: MatDialogRef<AddEditSupplierDialogComponent>,
    private fb: FormBuilder,
    @Inject(MAT_DIALOG_DATA) public data: { supplier: SupplierResponse; isNew: boolean }
  ) {

    this.countryControl = new FormControl<Country | null>(
      this.data?.supplier.country
        ? { translations: { en: this.data.supplier.country } } as Country
        : null
    );

    this.supplierForm = this.fb.group({
      businessName: [
        this.data?.supplier.businessName || '',
        [Validators.required], // Solo letras y espacios
      ],
      tradeName: [
        this.data?.supplier.tradeName || '',
        [Validators.required], // Solo letras y espacios
      ],
      taxId: [
        this.data?.supplier.taxId || '',
        [Validators.required, Validators.pattern(/^\d{11}$/)], // Numérico, 11 dígitos
      ],
      phone: [
        this.data?.supplier.phone || '',
        [Validators.required, Validators.pattern(/^\+?[0-9\s\-()]{7,15}$/)], // Teléfono
      ],
      email: [
        this.data?.supplier.email || '',
        [Validators.required, Validators.email], // Correo electrónico válido
      ],
      website: [
        this.data?.supplier.website || '',
        [
          Validators.required,
          Validators.pattern(
            /^(https:\/\/|http:\/\/)([\w\-]+\.)+[\w\-]+(\/[\w\-._~:/?#[\]@!$&'()*+,;=]*)?$/
          ),
        ],
      ],
      address: [
        this.data?.supplier.address || '',
        [Validators.required],  
      ],
      country: [
        this.data?.supplier.country || '',
        [Validators.required], 
      ],
      annualBilling: [
        this.data?.supplier.annualBilling || '',
        [Validators.required, Validators.pattern(/^\$?(\d{1,3})(,\d{3})*(\.\d{2})?$/)], // Numérico con formato contable
      ],
    })
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

  //handle selection of country
  handleSelection(event: any) {
    this.supplierForm.patchValue({ country: event.translations.en });
  }
}