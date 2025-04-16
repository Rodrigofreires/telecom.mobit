import { bootstrapApplication } from '@angular/platform-browser';
import { provideRouter } from '@angular/router';
import { AppComponent } from './app/app.component';
import { provideAnimations } from '@angular/platform-browser/animations';
import { routes } from './app/app.routes';
import { provideCharts } from 'ng2-charts';
import { provideHttpClient } from '@angular/common/http';
import { provideNgxMask } from 'ngx-mask'

bootstrapApplication(AppComponent, {
  providers: [
    provideRouter(routes),
    provideAnimations(),
    provideCharts(),
    provideHttpClient(),
    provideNgxMask()

  ]
}).catch(err => console.error(err));
