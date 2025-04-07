import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { MatTableModule } from '@angular/material/table';
import { CommonModule } from '@angular/common';
import { OFACResult, ScrappedResponse, UnifiedResult, WorldBankResult } from '../../scrapper/interfaces/scrapper.interface';

@Component({
  selector: 'app-screening-dialog',
  templateUrl: './screening-dialog.component.html',
  imports: [CommonModule, MatTableModule, MatDialogModule],
  styleUrls: ['./screening-dialog.component.css'],
  standalone: true
})

export class ScreeningDialogComponent implements OnInit {
  screeningResults: UnifiedResult[] = [];
  totalHits: number = 0;
  displayedColumns: string[] = [];

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: { totalHits: number, resultados: ScrappedResponse[] }
  ) { }

  ngOnInit(): void {
    const rawResults = this.data.resultados;

    this.screeningResults = rawResults.map((item: any) => {
      if (item.web === 'WorldBank') {
        return {
          web: item.web,
          firmName: item.firmName,
          address: item.address,
          country: item.country,
          fromDate: item.fromDate,
          toDate: item.toDate,
          grounds: item.grounds
        };
      } else if (item.web === 'OFAC') {
        return {
          web: item.web,
          firmName: item.name,
          address: item.address,
          type: item.type,
          program: item.program,
          list: item.list,
          score: item.score
        };
      }
      return item;
    });

    this.totalHits = this.data.totalHits;
    console.log(this.screeningResults);
    // Columnas dinámicas
    const hasWorldBank = this.screeningResults.some(r => r.web === 'WorldBank');
    const hasOFAC = this.screeningResults.some(r => r.web === 'OFAC');

    this.displayedColumns = ['web'];

    if (hasWorldBank) {
      this.displayedColumns.push('firmName', 'address', 'country', 'fromDate', 'toDate', 'grounds');
    }

    if (hasOFAC) {
      this.displayedColumns.push('firmName', 'address', 'type', 'program', 'list', 'score');
    }

    console.log(this.totalHits, this.screeningResults);
  }

  hasSource(source: string): boolean {
    return this.screeningResults.some(r => r.web === source);
  }
  getFilteredResults(source: string): UnifiedResult[] {
    return this.screeningResults.filter(r => r.web === source);
  }
}
