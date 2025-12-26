import { Routes } from '@angular/router';
import { PaymentFormComponent } from './components/payment-form/payment-form.component';
import { PaymentsListComponent } from './components/payments-list/payments-list.component';

export const routes: Routes = [
    { path: '', redirectTo: '/payments', pathMatch: 'full' },
    { path: 'payments', component: PaymentsListComponent },
    { path: 'payments/add', component: PaymentFormComponent },
    {path: 'edit/:id', component: PaymentFormComponent }
];
