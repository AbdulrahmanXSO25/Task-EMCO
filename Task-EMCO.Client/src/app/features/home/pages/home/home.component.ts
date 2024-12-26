import { Component, OnInit } from '@angular/core';
import { UserDto } from '../../../../core/models/auth.models';
import { AuthService } from '../../../../core/services/auth.service';
import { SHARED_IMPORTS } from '../../../../shared/shared-imports';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  standalone: true,
  imports: [SHARED_IMPORTS]
})
export class HomeComponent implements OnInit {
  currentUser: UserDto | null = null;

  constructor(
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.authService.currentUser$.subscribe(
      user => this.currentUser = user
    );
  }
}