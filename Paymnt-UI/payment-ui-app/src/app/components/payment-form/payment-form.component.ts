import { Component } from '@angular/core';
import { PaymentService } from '../../services/payment.service';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { v4 as uuidv4 } from 'uuid';

@Component({
  selector: 'app-payment-form',
  standalone: true,
  imports: [CommonModule, RouterModule, ReactiveFormsModule],
  templateUrl: './payment-form.component.html',
  styleUrls: ['./payment-form.component.css']
})
export class PaymentFormComponent {

  isEdit=false;
  paymentId?:string;

  form!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private paymentService: PaymentService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.form = this.fb.group({
      amount: [0, [Validators.required, Validators.min(1)]],
      currency: ['USD', Validators.required]
    });
  }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id) {
        this.isEdit = true;
        this.paymentId = id;
        // load payment data and populate form
        this.paymentService.getPaymentById(id).subscribe({
          next: payment => {
            const patch: any = {};
            if (payment && typeof payment.amount !== 'undefined') patch.amount = payment.amount;
            if (payment && typeof payment.currency !== 'undefined') patch.currency = payment.currency;
            this.form.patchValue(patch);
          },
          error:(() => {
            alert('Failed to load payment');
          })
        });
      } else {
        this.isEdit = false;
        this.paymentId = undefined;
      }
    });
  }

  onSubmit() {
    if (this.form.invalid) {
      return;
    }
    const paymentData = {
      ...this.form.value,
      clientRequestId: uuidv4()
    };

    if (this.isEdit && this.paymentId) {
      this.paymentService.updatePayment(this.paymentId, paymentData).subscribe({
        next: () => {
          this.router.navigate(['/payments']);
        },
        error: () => {
          alert('Error updating payment');
        }
      });
    } else {
      this.paymentService.createPayment(paymentData).subscribe({
        next: () => {
          this.router.navigate(['/payments']);
        },
        error: () => {
          alert('Error creating payment');
        }
      });
    }
  }

  onCancel() {
    this.router.navigate(['/payments']);
  }
}
