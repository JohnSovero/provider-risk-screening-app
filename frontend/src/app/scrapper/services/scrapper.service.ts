import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { Observable, map } from "rxjs";
import { environment } from "../../../environments/environment";
import { ScrapperResponse } from "../interfaces/scrapper.interface";

@Injectable({
    providedIn: 'root'
})
export class ScrapperService {

    private http = inject(HttpClient);

    // Method to get scrapped by name
    getScrappedByName(name: string): Observable<any> {
        console.log("ScrapperService: getScrappedByName called with name:", name);
        return this.http.get<ScrapperResponse>(`${environment.apiURL}/Firma?nombre=${name}`)
            .pipe(
                map((response: ScrapperResponse) => {
                    return response;
                })
            );
    }
}
