import { HttpClient } from "@angular/common/http";
import { inject, Injectable} from "@angular/core";
import { Observable, map } from "rxjs";
import { environment } from "../../../environments/environment";
import { SupplierResponse } from "../interfaces/supplier.interface";
import { Console } from "node:console";

@Injectable({
    providedIn: 'root'
})
export class SupplierService {

    private http = inject(HttpClient);

    // Method to get all suppliers from the backend
    getAllSuppliers(): Observable<SupplierResponse[]> {
      return this.http.get<SupplierResponse[]>(`${environment.apiURL}/supplier`)
        .pipe(
            map(suppliers => suppliers.map(supplier => {
                return {
                    ...supplier,
                };
            })),
      )
    }
    createSupplier(supplier: SupplierResponse): Observable<SupplierResponse> {
        console.log("SupplierService: createSupplier called with supplier:", supplier);
        return this.http.post<SupplierResponse>(`${environment.apiURL}/supplier`, supplier)
            .pipe(
                map((createdSupplier: SupplierResponse) => {
                    return createdSupplier;
                })
            );
    }
    
    // Method to update a supplier in the backend
    updateSupplier(supplier: SupplierResponse): Observable<SupplierResponse> {
        return this.http.put<SupplierResponse>(`${environment.apiURL}/supplier/${supplier.id}`, supplier)
            .pipe(
                map((updatedSupplier: SupplierResponse) => {
                    return updatedSupplier;
                })
            );
    }

    // Method to delete a supplier in the backend
    deleteSupplier(supplierId: string): Observable<void> {
        return this.http.delete<void>(`${environment.apiURL}/supplier/${supplierId}`)
            .pipe(
                map(() => {
                    return;
                })
            );
    }
}
