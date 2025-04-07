import { Routes } from '@angular/router';
import { SupplierListComponent } from './suppliers/supplier-list/supplier-list.component';

export const routes: Routes = [
    { path: '', redirectTo: 'suppliers', pathMatch: 'full' },
    { path: 'suppliers', component: SupplierListComponent }
];
