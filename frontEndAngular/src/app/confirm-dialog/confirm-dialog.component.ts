import { Component } from '@angular/core';
import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-confirm-dialog',
  standalone: true,
  imports: [CommonModule, MatButtonModule, MatDialogModule],
  template: `
    <h2 mat-dialog-title>Confirmação</h2>
    <mat-dialog-content>
      <p>Você tem certeza que deseja excluir este produto?</p>
    </mat-dialog-content>
    <mat-dialog-actions>
      <button mat-button class="cancel-button" (click)="onCancel()">Cancelar</button>
      <button mat-button color="warn" class="delete-button" (click)="onConfirm()">Excluir</button>
    </mat-dialog-actions>
  `,
  styles: [`
    .delete-button {
      background-color: #f44336; 
      color: white;
      width: 100px; 
      font-weight: bold; 
    }
    .delete-button:hover {
      background-color: #d32f2f; 
    }
    .cancel-button {
      background-color: white; 
      color: black; 
      width: 100px; 
    }
    .cancel-button:hover {
      background-color: #f1f1f1; 
    }
  `]
})
export class ConfirmDialogComponent {
  constructor(public dialogRef: MatDialogRef<ConfirmDialogComponent>) {}

  onConfirm(): void {
    this.dialogRef.close(true);
  }

  onCancel(): void {
    this.dialogRef.close(false);
  }
}
