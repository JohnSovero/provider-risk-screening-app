import { HttpClient } from "@angular/common/http";
import { inject, Injectable} from "@angular/core";
import { Observable } from "rxjs/internal/Observable";
import { environment } from "../../../environments/environment";
import { map } from "rxjs";
import { SupplierResponse } from "../interfaces/supplier.interface";

@Injectable({
    providedIn: 'root'
})
export class SupplierService {
    private http = inject(HttpClient);

    getAllSuppliers(): Observable<SupplierResponse[]> {
        return this.http.get<SupplierResponse[]>(`${environment.apiURL}/suppliers`).pipe(
            map(response => {
                return response
            })
        )
    }
}
