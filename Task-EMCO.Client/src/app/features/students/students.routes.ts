import { Routes } from '@angular/router';

export const STUDENT_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () => import('./pages/students-list/students-list.component')
      .then(m => m.StudentsListComponent)
  },
  {
    path: 'new',
    loadComponent: () => import('./pages/add-student/add-student.component')
      .then(m => m.AddStudentComponent)
  },
  {
    path: ':id',
    loadComponent: () => import('./pages/student-detail/student-detail.component')
      .then(m => m.StudentDetailComponent)
  },
  {
    path: ':id/edit',
    loadComponent: () => import('./pages/edit-student/edit-student.component')
      .then(m => m.EditStudentComponent)
  }
];