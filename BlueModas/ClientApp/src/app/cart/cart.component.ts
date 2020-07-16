import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Globals } from '../app.globals';

@Component({
  selector: 'app-cart-component',
  templateUrl: './cart.component.html'
})
export class CartComponent {
  public order: Order;
  private baseUrl: string;
  public orderConfirmed: boolean = false;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string, public globals: Globals) {
    http.get<Order>(baseUrl + 'api/order/get-cart').subscribe(result => {
      this.order = result;
      if (result)
        this.globals.cartQtde = result.quantity;
    }, error => console.error(error));
    this.baseUrl = baseUrl;
  }

  changeQuantity(item: OrderItem) {
    if (item.quantity > 0) {
      this.http.post<Result>(this.baseUrl + 'api/order/update-cart-item/', item).subscribe(result => {
        console.log(result.success, result.quantityInCart);
        if (result.success) {
          this.globals.cartQtde = result.quantityInCart;
        }
      }, error => console.error(error));
    }
    else {
      this.removeItem(item);
    }
  }

  removeItem(item: OrderItem) {
    if (item.quantity > 0) {
      this.http.delete<Result>(this.baseUrl + 'api/order/remove-item-cart/' + item.orderItemId).subscribe(result => {
        console.log(result.success, result.quantityInCart);
        if (result.success) {
          this.globals.cartQtde = result.quantityInCart;
          const index = this.order.orderItems.indexOf(item);
          if (index !== -1) {
            this.order.orderItems.splice(index, 1);
          }
        }
      }, error => console.error(error));
    }
  }

  confirmOrder() {
    this.http.post<Result>(this.baseUrl + 'api/order/confirm-order/', this.order).subscribe(result => {
      console.log(result.success, result.quantityInCart);
      if (result.success) {
        this.orderConfirmed = true;
        this.globals.cartQtde = result.quantityInCart;
      }
    }, error => console.error(error));
    console.log(this.order);
    
  }

}

interface Order {
  orderId: string;
  orderItems: OrderItem[];
  total: number;
  inProgress: boolean;
  customerData: Customer;
  quantity: number;
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

interface Result {
  success: boolean;
  quantityInCart: number;
}