import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SubjectToReturnDto } from '../../../../core/models/subject.models';
import { SubjectService } from '../../services/subject.service';
import { SHARED_IMPORTS } from '../../../../shared/shared-imports';

@Component({
  selector: 'app-subject-detail',
  standalone: true,
  imports: [SHARED_IMPORTS],
  templateUrl: './subject-detail.component.html',
  styleUrl: './subject-detail.component.css'
})
export class SubjectDetailComponent implements OnInit {
  subject: SubjectToReturnDto | null = null;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private subjectService: SubjectService
  ) {}

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.loadSubject(+id);
    }
  }

  loadSubject(id: number) {
    this.subjectService.getById(id).subscribe(
      result => {
        if (result.success) {
          this.subject = result.data;
        } else {
          console.error(result.message);
        }
      },
      error => {
        console.error('An error occurred:', error);
      }
    );
  }

  goBack() {
    this.router.navigate(['/subjects']);
  }

  editSubject() {
    if (this.subject) {
      this.router.navigate(['/subjects', this.subject.id, 'edit']);
    }
  }
}
