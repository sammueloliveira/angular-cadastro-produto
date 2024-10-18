import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductService } from '../../../services/product.service';
import { Product } from '../../../interfaces/product.module';
import { CommonModule } from '@angular/common'; 
import { RouterModule } from '@angular/router'; 
import { MatButtonModule } from '@angular/material/button';
import { TipoProdutoEnum } from '../../../enums/tipo.produto.enum';

@Component({
  selector: 'app-product-detail',
  standalone: true, 
  imports: [CommonModule, RouterModule, MatButtonModule],
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.css']
})
export class ProductDetailComponent implements OnInit {
  product!: Product;

  constructor(
    private route: ActivatedRoute,
    private productService: ProductService
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.productService.readById(id).subscribe(product => {
        this.product = product;
        this.productService.showMessage("Informações do produto!")
      });
    }
  }

  loadProduct(id: string): void {
    this.productService.readById(id).subscribe((product: Product) => {
      this.product = product;
      console.log('Produto carregado:', this.product); 
    });
  }

  getTipoProduto(tipo: TipoProdutoEnum): string {
    switch (tipo) {
      case TipoProdutoEnum.Organico:
        return 'Orgânico';
      case TipoProdutoEnum.NaoOrganico:
        return 'Não Orgânico';
      default:
        return 'Tipo Desconhecido';
    }
  }
}
