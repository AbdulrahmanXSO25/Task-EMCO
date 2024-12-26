import { Component, OnInit } from '@angular/core';
import { SHARED_IMPORTS } from '../../../../shared/shared-imports';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { ConfirmationModalComponent, ConfirmationData } from '../../../../shared/components/confirmation-modal/confirmation-modal.component';
import { StudentService } from '../../services/student.service';
import { StudentToReturnDto } from '../../../../core/models/student.model';

@Component({
  selector: 'app-students-list',
  standalone: true,
  imports: [SHARED_IMPORTS],
  templateUrl: './students-list.component.html',
  styleUrls: ['./students-list.component.css']
})
export class StudentsListComponent implements OnInit {
  students: StudentToReturnDto[] = [];
  totalCount = 0;
  pageSize = 6;
  pageIndex = 0;
  displayedColumns: string[] = ['firstName', 'secondName', 'email', 'phoneNumber', 'actions'];
  searchTerm: string = '';

  constructor(
    private studentService: StudentService,
    private dialog: MatDialog,
    private router: Router
  ) {}

  ngOnInit() {
    this.loadStudents();
  }

  loadStudents() {
    this.studentService.getAll(this.pageIndex, this.pageSize, this.searchTerm).subscribe(
      result => {
        if (result.success) {
          this.students = result.data.data;
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
    this.loadStudents();
  }

  viewDetails(id: number) {
    this.router.navigate(['/students', id]);
  }

  editStudent(id: number) {
    this.router.navigate(['/students', id, 'edit']);
  }

  deleteStudent(id: number) {
    const dialogRef = this.dialog.open(ConfirmationModalComponent, {
      data: {
        title: 'Delete Student',
        message: 'Are you sure you want to delete this student?',
        confirmText: 'Delete',
        cancelText: 'Cancel'
      } as ConfirmationData
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.studentService.delete(id).subscribe(
          result => {
            if (result.success) {
              this.loadStudents();
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
    this.loadStudents();
  }
}
