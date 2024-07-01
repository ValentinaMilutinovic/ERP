import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-dialog',
  template: `
    <h2 mat-dialog-title>Success</h2>
    <div mat-dialog-content>
      <p>{{ data }}</p>
    </div>
    <div mat-dialog-actions>
      <button mat-button [mat-dialog-close]="true">OK</button>
    </div>
  `,
  styleUrls: ['./dialog.component.css']
})
export class DialogComponent {
  constructor(@Inject(MAT_DIALOG_DATA) public data: string) {}
}
