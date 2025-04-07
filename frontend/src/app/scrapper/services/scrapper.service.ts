import { HttpClient, HttpParams } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { Observable, map } from "rxjs";
import { environment } from "../../../environments/environment";
import { ScrappedResponse } from "../interfaces/scrapper.interface";

@Injectable({
    providedIn: 'root'
})
export class ScrapperService {

    private http = inject(HttpClient);

    // Method to get scrapped by name
    getScrappedByName(name: string, paginas: string[]): Observable<ScrappedResponse> {
        console.log("ScrapperService: getScrappedByName called with name:", name, "and pages:", paginas);

        let params = new HttpParams()
            .set('nombre', name);

        paginas.forEach(pagina => {
            params = params.append('paginas', pagina); // agrega múltiples 'paginas' al query
        });

        return this.http.get<ScrappedResponse>(`${environment.apiURL}/scrapper`, { params })
            .pipe(
                map((response: ScrappedResponse) => {
                    return response;
                })
            );
    }
}
