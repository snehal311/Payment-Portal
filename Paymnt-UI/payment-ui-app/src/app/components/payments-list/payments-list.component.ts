import { Component } from '@angular/core';
import { PaymentService } from '../../services/payment.service';
import { Router, RouterModule } from '@angular/router';
import { CommonModule, DatePipe } from '@angular/common';
import { Payment } from '../../model/payment.model';

@Component({
  selector: 'app-payments-list',
  standalone: true,
  imports: [CommonModule, RouterModule, DatePipe],
  templateUrl: './payments-list.component.html',
  styleUrls: ['./payments-list.component.css']
})
export class PaymentsListComponent {

  constructor(private paymentService: PaymentService, private router: Router) { }
  payments: Payment[] = [];
  
  ngOnInit() {
    this.loadPayments();
  }

  loadPayments() {
    this.paymentService.getPayments().subscribe(payments => {
      this.payments = payments;});
  }

  editPayment(paymentId: string) {
    this.router.navigate(['/edit', paymentId]);
  }

  deletePayment(paymentId: string) {
    this.paymentService.deletePayment(paymentId).subscribe(() => {
      this.loadPayments();
    });
  }
}
