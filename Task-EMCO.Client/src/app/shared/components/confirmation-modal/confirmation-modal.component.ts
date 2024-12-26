import { Component, inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { SHARED_IMPORTS } from '../../shared-imports';

export interface ConfirmationData {
  title: string;
  message: string;
  confirmText?: string;
  cancelText?: string;
}

@Component({
  selector: 'app-confirmation-modal',
  templateUrl: './confirmation-modal.component.html',
  styleUrls: ['./confirmation-modal.component.scss'],
  standalone: true,
  imports: [SHARED_IMPORTS]
})
export class ConfirmationModalComponent {
  dialogRef = inject(MatDialogRef<ConfirmationModalComponent>);
  data: ConfirmationData = inject(MAT_DIALOG_DATA);
}