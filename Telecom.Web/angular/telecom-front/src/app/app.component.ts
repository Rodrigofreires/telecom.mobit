import { Component } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';
import { ContainerComponent } from '../shared/container/container.component';
import { FooterComponent } from '../shared/footer/footer.component';
import { HeaderComponent } from '../shared/header/header.component';

@Component({
  standalone: true,
  selector: 'app-root',
  imports: [
    RouterModule,
    RouterOutlet,
    HeaderComponent,
    FooterComponent,
 
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'telecom-front';
}
