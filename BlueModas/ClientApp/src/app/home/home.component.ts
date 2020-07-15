import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Globals } from '../app.globals';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public products: Product[];
  private baseUrl

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string, public globals: Globals) {
    http.get<Product[]>(baseUrl + 'api/product').subscribe(result => {
      this.products = result;
    }, error => console.error(error));
    this.baseUrl = baseUrl;
  }

  addProduct(productId) {
    console.log(this.baseUrl);
    this.http.post(this.baseUrl + 'api/order/add-product/' + productId, {}).subscribe(result => {
      console.log("done");
    }, error => console.error(error));
  }

}

interface Product {
  productId: string;
  description: string;
  price: number;
  image: string;
}
