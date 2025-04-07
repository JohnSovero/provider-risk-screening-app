export interface ScrappedResponse {
    totalHits: number;
    resultados: (WorldBankResult | OFACResult)[];
}

export interface WorldBankResult {
    web: 'WorldBank';
    firmName: string;
    address: string;
    country: string;
    fromDate: string;
    toDate: string;
    grounds: string;
}
    
export interface OFACResult {
    web: 'OFAC';
    name: string;
    address: string;
    type: string;
    program: string | null;
    list: string;
    score: string;
}

export interface UnifiedResult {
    web: string;
    firmName?: string;
    address: string;
    country?: string;
    fromDate?: string;
    toDate?: string;
    grounds?: string;
    name?: string;
    type?: string;
    program?: string | null;
    list?: string;
    score?: string;
}