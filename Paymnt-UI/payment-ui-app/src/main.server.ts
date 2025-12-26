import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { config } from './app/app.config.server';

// The server renderer calls the default export with a bootstrap context
// (contains `document` and `url`). Forward that context to
// `bootstrapApplication` so server providers (e.g. provideServerRendering)
// can read it and avoid NG0401.
const bootstrap = (context?: unknown) => bootstrapApplication(AppComponent, config, context as any);

export default bootstrap;
