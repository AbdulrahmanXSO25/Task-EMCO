import { Routes } from '@angular/router';

export const SUBJECT_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () => import('./pages/subjects-list/subjects-list.component')
      .then(m => m.SubjectsListComponent)
  },
  {
    path: 'new',
    loadComponent: () => import('./pages/add-subject/add-subject.component')
      .then(m => m.AddSubjectComponent)
  },
  {
    path: ':id',
    loadComponent: () => import('./pages/subject-detail/subject-detail.component')
      .then(m => m.SubjectDetailComponent)
  },
  {
    path: ':id/edit',
    loadComponent: () => import('./pages/edit-subject/edit-subject.component')
      .then(m => m.EditSubjectComponent)
  }
];