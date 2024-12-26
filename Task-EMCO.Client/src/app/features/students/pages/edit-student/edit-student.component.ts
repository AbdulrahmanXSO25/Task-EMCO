import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SHARED_IMPORTS } from '../../../../shared/shared-imports';
import { StudentService } from '../../services/student.service';
import { SubjectService } from '../../../subjects/services/subject.service';
import { SubjectToReturnDto } from '../../../../core/models/subject.models';
import { UpdateStudentDto } from '../../../../core/models/student.model';

@Component({
  selector: 'app-edit-student',
  standalone: true,
  imports: [SHARED_IMPORTS],
  templateUrl: './edit-student.component.html',
  styleUrls: ['./edit-student.component.css']
})
export class EditStudentComponent implements OnInit {
  studentForm: FormGroup;
  studentId: number;
  availableSubjects: SubjectToReturnDto[] = [];

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private studentService: StudentService,
    private subjectService: SubjectService
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
    this.studentId = 0;
  }

  ngOnInit() {
    this.loadSubjects();
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.studentId = +id;
      this.loadStudent(this.studentId);
    }
  }

  loadStudent(id: number) {
    this.studentService.getById(id).subscribe(
      result => {
        if (result.success) {
          const studentData = {
            ...result.data,
            subjectIds: result.data.subjects.map(s => s.id)
          };
          this.studentForm.patchValue(studentData);
        } else {
          console.error(result.message);
        }
      },
      error => {
        console.error('An error occurred:', error);
      }
    );
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
      const updatedStudent: UpdateStudentDto = {
        id: this.studentId,
        ...this.studentForm.value
      };
      this.studentService.update(updatedStudent).subscribe(
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