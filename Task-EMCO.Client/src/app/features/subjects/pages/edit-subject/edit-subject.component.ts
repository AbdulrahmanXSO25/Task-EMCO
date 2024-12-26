import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UpdateSubjectDto } from '../../../../core/models/subject.models';
import { SubjectService } from '../../services/subject.service';
import { SHARED_IMPORTS } from '../../../../shared/shared-imports';

@Component({
  selector: 'app-edit-subject',
  standalone: true,
  imports: [SHARED_IMPORTS],
  templateUrl: './edit-subject.component.html',
  styleUrl: './edit-subject.component.css'
})
export class EditSubjectComponent implements OnInit {
  subjectForm: FormGroup;
  subjectId: number;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private subjectService: SubjectService
  ) {
    this.subjectForm = this.fb.group({
      code: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(8), Validators.pattern(/^[A-Z]{2,5}\d{3}$/)]],
      name: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      description: ['', Validators.maxLength(500)]
    });
    this.subjectId = 0;
  }

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.subjectId = +id;
      this.loadSubject(this.subjectId);
    }
  }

  loadSubject(id: number) {
    this.subjectService.getById(id).subscribe(
      result => {
        if (result.success) {
          this.subjectForm.patchValue(result.data);
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
    if (this.subjectForm.valid) {
      const updatedSubject: UpdateSubjectDto = {
        id: this.subjectId,
        ...this.subjectForm.value
      };
      this.subjectService.update(updatedSubject).subscribe(
        result => {
          if (result.success) {
            this.router.navigate(['/subjects']);
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
