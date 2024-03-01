import { NgModule } from '@angular/core';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatCardModule } from "@angular/material/card";
import { MatRadioModule } from "@angular/material/radio";

import { MatTableModule } from "@angular/material/table"
import { MatPaginatorModule } from "@angular/material/paginator"
import { MatSortModule } from "@angular/material/sort"
import { MatDialogModule } from "@angular/material/dialog"
import { MatSelectModule } from "@angular/material/select"
import { MatCheckboxModule } from "@angular/material/checkbox"
import { MatDatepickerModule } from "@angular/material/datepicker"
import { MatNativeDateModule } from "@angular/material/core";
import { MatIconModule } from "@angular/material/icon";
import { MatExpansionModule } from "@angular/material/expansion";

@NgModule({
    exports: [
      MatInputModule,
      MatButtonModule,
      FormsModule,
      ReactiveFormsModule,
      MatSnackBarModule,
      MatCardModule,
      MatRadioModule,
      MatTableModule,
      MatPaginatorModule,
      MatSortModule,
      MatDialogModule,
      MatSelectModule,
      MatCheckboxModule,
      MatDatepickerModule,
      MatNativeDateModule,
      MatIconModule,
      MatExpansionModule,
    ]
  })
  export class MaterialModule { }
