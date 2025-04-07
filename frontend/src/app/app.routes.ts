import { Routes } from '@angular/router';
import { SupplierListComponent } from './suppliers/supplier-list/supplier-list.component';
import { ScreeningDialogComponent } from './suppliers/screening-dialog/screening-dialog.component';

export const routes: Routes = [
    { path: 'supplier', component: SupplierListComponent },
    { path: 'screening', component: ScreeningDialogComponent }
];
