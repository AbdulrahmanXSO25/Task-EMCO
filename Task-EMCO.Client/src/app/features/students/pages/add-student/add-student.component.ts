import { Component, OnInit } from '@angular/core';
import { SHARED_IMPORTS } from '../../../../shared/shared-imports';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { StudentService } from '../../services/student.service';
import { CreateStudentDto } from '../../../../core/models/student.model';
import { SubjectToReturnDto } from '../../../../core/models/subject.models';
import { SubjectService } from '../../../subjects/services/subject.service';

@Component({
  selector: 'app-add-student',
  standalone: true,
  imports: [SHARED_IMPORTS],
  templateUrl: './add-student.component.html',
  styleUrls: ['./add-student.component.css']
})
export class AddStudentComponent implements OnInit {
  studentForm: FormGroup;
  availableSubjects: SubjectToReturnDto[] = [];

  constructor(
    private fb: FormBuilder,
    private studentService: StudentService,
    private subjectService: SubjectService,
    private router: Router
  ) {
    this.studentForm = this.fb.group({
      firstName: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(50)]],
      secondName: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(50)]],
      lastName: ['', Validators.maxLength(50)],
      dateOfBirth: ['', [Validators.required]],
      isMale: [true],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', [Validators.required, Validators.maxLength(20)]],
      subjectIds: [[]]
    });
  }

  ngOnInit(): void {
    this.loadSubjects();
  }

  loadSubjects() {
    this.subjectService.getAllNoPagination().subscribe(
      result => {
        if (result.success) {
          this.availableSubjects = result.data;
        } else {
          console.error(result.message);
        }
      },
      error => {
        console.error('An error occurred:', error);
      }
    );
  }

  onSubmit() {
    if (this.studentForm.valid) {
      const newStudent: CreateStudentDto = this.studentForm.value;
      this.studentService.create(newStudent).subscribe(
        result => {
          if (result.success) {
            this.router.navigate(['/students']);
          } else {
            console.error(result.message);
          }
        },
        error => {
          console.error('An error occurred:', error);
        }
      );
    }
  }
}
