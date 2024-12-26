import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';
import { AuthLayoutComponent } from './layouts/auth-layout/auth-layout.component';
import { MainLayoutComponent } from './layouts/main-layout/main-layout.component';

export const routes: Routes = [
  {
    path: '',
    component: MainLayoutComponent,
    canActivate: [authGuard],
    children: [
        {
            path: 'home',
            loadComponent: () => import('./features/home/pages/home/home.component')
                .then(m => m.HomeComponent),
            canActivate: [authGuard]
        },
        {
            path: 'subjects',
            loadChildren: () => import('./features/subjects/subjects.routes')
            .then(m => m.SUBJECT_ROUTES)
        },
        {
            path: 'students',
            loadChildren: () => import('./features/students/students.routes')
            .then(m => m.STUDENT_ROUTES)
        },
        {
            path: '',
            redirectTo: 'home',
            pathMatch: 'full'
        }
    ]
  },
  {
    path: '',
    component: AuthLayoutComponent,
    children: [
        {
            path: 'login',
            loadComponent: () => import('./features/auth/pages/login/login.component')
            .then(m => m.LoginComponent)
        },
        {
            path: '404',
            loadComponent: () => import('./features/auth/pages/not-found/not-found.component')
            .then(m => m.NotFoundComponent)
        }
    ]
  },
  {
    path: '**',
    redirectTo: '/404'
  }
];