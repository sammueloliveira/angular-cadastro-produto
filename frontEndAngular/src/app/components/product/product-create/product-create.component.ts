import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../../services/product.service';
import { Router, RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { Product } from '../../../interfaces/product.module';
import { FormsModule, NgForm } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatSelectModule } from '@angular/material/select';
import { TipoProdutoEnum } from '../../../enums/tipo.produto.enum';
import { CommonModule } from '@angular/common'; 

@Component({
  selector: 'app-product-create',
  standalone: true,
  imports: [
    RouterModule,
    MatButtonModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatCardModule,
    MatSelectModule,
    CommonModule, 
  ],
  templateUrl: './product-create.component.html',
  styleUrls: ['./product-create.component.css'],
})
export class ProductCreateComponent implements OnInit {
  product: Product = {
    id: undefined,
    nome: '',
    precoDeVenda: null,
    descricao: '',
    quantidade: 0,
    tipo: TipoProdutoEnum.Organico,
    dataDeCadastro: '',
  };

  constructor(private productService: ProductService, private router: Router) {}

  ngOnInit(): void {}

  createProduct(form: NgForm): void {
    if (form.valid) {
      const dateParts = this.product.dataDeCadastro.split('-');

      if (dateParts.length === 3) {
        const [year, month, day] = dateParts.map((part) => parseInt(part, 10));

        const formattedDate = `${day.toString().padStart(2, '0')}/${month
          .toString()
          .padStart(2, '0')}/${year}`;

        const productToCreate: Product = {
          ...this.product,
          dataDeCadastro: formattedDate,
        };

        this.productService.create(productToCreate).subscribe({
          next: (createdProduct) => {
            this.productService.showMessage('Produto cadastrado com sucesso!');
            this.router.navigate(['/products']);
          },
          error: (e) => {
            this.productService.showMessage('Erro ao cadastrar produto!');
          },
        });
      } else {
        this.productService.showMessage('Formato de data inválido!');
      }
    } else {
      form.form.markAllAsTouched(); 
      this.productService.showMessage('Por favor, preencha todos os campos obrigatórios.');
    }
  }

  cancel(): void {
    this.router.navigate(['/products']);
  }

  onFocus() {
    if (this.product.precoDeVenda === 0) {
      this.product.precoDeVenda = null;
    }
  }

  onBlur() {
    if (this.product.precoDeVenda === null) {
      this.product.precoDeVenda = 0;
    }
  }

  onQuantidadeFocus() {
    if (this.product.quantidade === 0) {
      this.product.quantidade = null;
    }
  }

  onQuantidadeBlur() {
    if (this.product.quantidade === null) {
      this.product.quantidade = 0;
    }
  }
}
