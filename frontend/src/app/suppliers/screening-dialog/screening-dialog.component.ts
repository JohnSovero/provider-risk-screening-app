import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MaterialModule } from '../../material/material.module';
import { MatListModule } from '@angular/material/list';
import { FormsModule } from '@angular/forms';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-screening-dialog',
  imports: [
    CommonModule,
    MaterialModule,
    MatListModule,
    FormsModule,
    MatProgressSpinnerModule,
  ],
  templateUrl: './screening-dialog.component.html',
  styleUrl: './screening-dialog.component.css'
})
export class ScreeningDialogComponent implements OnInit {
  sources = ['SUNAT', 'OSCE', 'INTERPOL'];
  selectedSources: string[] = [];
  results: any[] = [];
  isLoading = false;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: { supplier: any },
    private http: HttpClient
  ) { }

  ngOnInit(): void { }

  onSourceChange() {
    if (this.selectedSources.length >= 1 && this.selectedSources.length <= 3) {
      this.isLoading = true;
      this.http.post('/firma', {
        supplier: this.data.supplier,
        sources: this.selectedSources,
      }).subscribe((res: any) => {
        this.results = res;
        this.isLoading = false;
      }, () => {
        this.isLoading = false;
      });
    }
  }
}