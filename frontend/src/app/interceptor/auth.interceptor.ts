import { HttpInterceptorFn } from '@angular/common/http';

export const AuthInterceptor: HttpInterceptorFn = (req, next) => {
    const username = 'admin';
    const password = 'admin';
    const authHeader = `Basic ${btoa(`${username}:${password}`)}`;

    const clonedRequest = req.clone({
        setHeaders: {
            Authorization: authHeader
        }
    });

    return next(clonedRequest);
};