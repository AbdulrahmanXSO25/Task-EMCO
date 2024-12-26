import { Component, OnInit } from '@angular/core';
import { SHARED_IMPORTS } from '../../../../shared/shared-imports';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { SubjectToReturnDto } from '../../../../core/models/subject.models';
import { ConfirmationModalComponent, ConfirmationData } from '../../../../shared/components/confirmation-modal/confirmation-modal.component';
import { SubjectService } from '../../services/subject.service';

@Component({
  selector: 'app-subjects-list',
  standalone: true,
  imports: [SHARED_IMPORTS],
  templateUrl: './subjects-list.component.html',
  styleUrl: './subjects-list.component.css'
})
export class SubjectsListComponent implements OnInit {
  subjects: SubjectToReturnDto[] = [];
  totalCount = 0;
  pageSize = 6;
  pageIndex = 0;
  displayedColumns: string[] = ['code', 'name', 'studentsCount', 'actions'];
  searchTerm: string = '';

  constructor(
    private subjectService: SubjectService,
    private dialog: MatDialog,
    private router: Router
  ) {}

  ngOnInit() {
    this.loadSubjects();
  }

  loadSubjects() {
    this.subjectService.getAll(this.pageIndex, this.pageSize, this.searchTerm).subscribe(
      result => {
        if (result.success) {
          this.subjects = result.data.data;
          this.totalCount = result.data.totalCount;
        } else {
          console.error(result.message);
        }
      },
      error => {
        console.error('An error occurred:', error);
      }
    );
  }

  onPageChange(event: any) {
    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;
    this.loadSubjects();
  }

  viewDetails(id: number) {
    this.router.navigate(['/subjects', id]);
  }

  editSubject(id: number) {
    this.router.navigate(['/subjects', id, 'edit']);
  }

  deleteSubject(id: number) {
    const dialogRef = this.dialog.open(ConfirmationModalComponent, {
      data: {
        title: 'Delete Subject',
        message: 'Are you sure you want to delete this subject?',
        confirmText: 'Delete',
        cancelText: 'Cancel'
      } as ConfirmationData
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.subjectService.delete(id).subscribe(
          result => {
            if (result.success) {
              this.loadSubjects();
            } else {
              console.error(result.message);
            }
          },
          error => {
            console.error('An error occurred:', error);
          }
        );
      }
    });
  }

  onSearch() {
    this.pageIndex = 0;
    this.loadSubjects();
  }
}