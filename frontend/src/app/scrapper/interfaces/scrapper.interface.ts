export interface ScrapperResponse {
    totalHint: number;
    resultados: ScrapperResult[];
}

export interface ScrapperResult {
    firmName: string;
    additionalInfo: string;
    address: string;
    country: string;
    fromDate: string;
    toDate: string;
    grounds: string;
}