import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';

@Component({
  selector: 'app-main-layout',
  standalone: true,
  imports: [RouterOutlet, HeaderComponent, FooterComponent],
  template: `
    <div class="flex flex-col min-h-screen">
      <app-header />
      <main class="flex-grow mt-20 p-5">
        <router-outlet />
      </main>
      <app-footer />
    </div>
  `
})
export class MainLayoutComponent {}