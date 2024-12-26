import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { StudentService } from '../../services/student.service';
import { SHARED_IMPORTS } from '../../../../shared/shared-imports';
import { StudentToReturnDto } from '../../../../core/models/student.model';

@Component({
  selector: 'app-student-detail',
  standalone: true,
  imports: [SHARED_IMPORTS],
  templateUrl: './student-detail.component.html',
  styleUrls: ['./student-detail.component.css']
})
export class StudentDetailComponent implements OnInit {
  student: StudentToReturnDto | null = null;
  subjectColors: string[] = [
    'bg-blue-50', 'bg-purple-50', 'bg-pink-50', 
    'bg-indigo-50', 'bg-green-50', 'bg-yellow-50',
    'bg-red-50', 'bg-orange-50', 'bg-teal-50'
  ];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private studentService: StudentService
  ) {}

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.loadStudent(+id);
    }
  }

  loadStudent(id: number) {
    this.studentService.getById(id).subscribe(
      result => {
        if (result.success) {
          this.student = result.data;
        } else {
          console.error(result.message);
        }
      },
      error => {
        console.error('An error occurred:', error);
      }
    );
  }

  getRandomColor(index: number): string {
    return this.subjectColors[index % this.subjectColors.length];
  }
  
  goBack() {
    this.router.navigate(['/students']);
  }

  editStudent() {
    if (this.student) {
      this.router.navigate(['/students', this.student.id, 'edit']);
    }
  }
}