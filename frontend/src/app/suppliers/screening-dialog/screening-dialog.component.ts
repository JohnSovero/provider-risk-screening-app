import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { MatTableModule } from '@angular/material/table';
import { CommonModule } from '@angular/common';
import { SupplierResponse } from '../interfaces/supplier.interface';

@Component({
  selector: 'app-screening-dialog',
  templateUrl: './screening-dialog.component.html',
  imports: [CommonModule, MatTableModule, MatDialogModule],
  styleUrls: ['./screening-dialog.component.css'],
  standalone: true
})

export class ScreeningDialogComponent implements OnInit {
  screeningResults: SupplierResponse[] = [];
  totalHits: number = 0;
  displayedColumns: string[] = ['firmName', 'address', 'country', 'fromDate', 'toDate', 'grounds'];

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: { totalHits: number, resultados: SupplierResponse[] }
  ) { }

  ngOnInit(): void {
    this.screeningResults = this.data.resultados;
    this.totalHits = this.data.totalHits;
    console.log(this.totalHits);
  }
}
