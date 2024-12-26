import { HttpInterceptorFn } from '@angular/common/http';

export const jwtInterceptor: HttpInterceptorFn = (req, next) => {

    if (req.url.includes('login')) {
        return next(req);
    }
    const token = localStorage.getItem('token');
    
    if (token) {
        req = req.clone({
        setHeaders: {
            Authorization: `Bearer ${token}`
        }
        });
    }
    
    return next(req);
};