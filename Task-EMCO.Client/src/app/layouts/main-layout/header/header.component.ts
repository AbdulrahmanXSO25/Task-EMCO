import { Component } from '@angular/core';
import { AuthService } from '../../../core/services/auth.service';
import { Router } from '@angular/router';
import { SHARED_IMPORTS } from '../../../shared/shared-imports';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [SHARED_IMPORTS],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {
  constructor(public authService: AuthService, private router: Router) {}

  navigateHome() {
    this.router.navigate(['/']);
  }
}