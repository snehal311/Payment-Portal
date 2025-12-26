import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Payment } from '../model/payment.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {

  private apiUrl = 'https://localhost:7152/api/payments';
  constructor(private http: HttpClient) { }

  getPayments() {
    return this.http.get<Payment[]>(this.apiUrl);
  }

  createPayment(data: any): Observable<Payment> {
    return this.http.post<Payment>(this.apiUrl, data);
  }

  updatePayment(id: string, data: any): Observable<Payment> {
    return this.http.put<Payment>(`${this.apiUrl}/${id}`, data);
  }

  getPaymentById(id: string): Observable<Payment> {
    return this.http.get<Payment>(`${this.apiUrl}/${id}`);
  }

  deletePayment(id: string) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

}
