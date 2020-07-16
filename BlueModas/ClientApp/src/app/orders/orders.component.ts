import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html'
})
export class OrdersComponent {
  public orders: Order[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Order[]>(baseUrl + 'api/order/last-orders').subscribe(result => {
      this.orders = result;
      console.log(this.orders);
    }, error => console.error(error));
  }
}

interface Order {
  orderId: string;
  orderItems: OrderItem[];
  total: number;
  inProgress: boolean;
  customerData: Customer;
  quantity: number;
  dateTimeOrder: Date;
}

interface OrderItem {
  orderItemId: string;
  product: Product;
  quantity: number;
}

interface Customer {
  customerId: string;
  name: string;
  email: string;
  phone: string;
}

interface Product {
  productId: string;
  description: string;
  price: number;
  image: string;
}