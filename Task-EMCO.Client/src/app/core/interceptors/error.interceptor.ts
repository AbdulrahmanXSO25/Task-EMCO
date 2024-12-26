import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';
import Swal from 'sweetalert2';
import { AuthService } from '../services/auth.service';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  return next(req).pipe(
    catchError(error => {
      if (error.status === 401) {
        authService.logout();
        router.navigate(['/login']);
      }
      
      Swal.fire({
        title: 'Error',
        text: error.error?.message || 'An unexpected error occurred',
        icon: 'error',
        confirmButtonText: 'OK'
      });
      
      return throwError(() => error);
    })
  );
};