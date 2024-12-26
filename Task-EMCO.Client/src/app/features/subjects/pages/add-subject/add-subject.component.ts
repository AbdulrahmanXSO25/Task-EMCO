import { Component } from '@angular/core';
import { SHARED_IMPORTS } from '../../../../shared/shared-imports';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CreateSubjectDto } from '../../../../core/models/subject.models';
import { SubjectService } from '../../services/subject.service';

@Component({
  selector: 'app-add-subject',
  standalone: true,
  imports: [SHARED_IMPORTS],
  templateUrl: './add-subject.component.html',
  styleUrl: './add-subject.component.css'
})
export class AddSubjectComponent {
  subjectForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private subjectService: SubjectService,
    private router: Router
  ) {
    this.subjectForm = this.fb.group({
      code: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(8), Validators.pattern(/^[A-Z]{2,5}\d{3}$/)]],
      name: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      description: ['', Validators.maxLength(500)]
    });
  }

  onSubmit() {
    if (this.subjectForm.valid) {
      const newSubject: CreateSubjectDto = this.subjectForm.value;
      this.subjectService.create(newSubject).subscribe(
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