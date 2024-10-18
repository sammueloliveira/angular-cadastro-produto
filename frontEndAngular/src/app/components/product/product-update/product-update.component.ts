import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { ProductService } from '../../../services/product.service';
import { Product } from '../../../interfaces/product.module';
import { MatButtonModule } from '@angular/material/button';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';
import { TipoProdutoEnum } from '../../../enums/tipo.produto.enum';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-product-update',
  standalone: true,
  imports: [
    RouterModule,
    MatButtonModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatCardModule,
    MatSelectModule,
    MatTableModule,
    CommonModule
  ],
  templateUrl: './product-update.component.html',
  styleUrls: ['./product-update.component.css']
})
export class ProductUpdateComponent implements OnInit {
  product: Product = { 
    id: undefined, 
    nome: '', 
    precoDeVenda: null, 
    descricao: '', 
    quantidade: 0, 
    tipo: TipoProdutoEnum.Organico, 
    dataDeCadastro: '' 
  }; 
  
  tipos = Object.values(TipoProdutoEnum).filter(value => typeof value === 'number') as TipoProdutoEnum[];
  displayedColumns: string[] = ['nome', 'precoDeVenda', 'descricao', 'quantidade', 'tipo', 'dataCadastro'];


  constructor(
    private route: ActivatedRoute,
    private productService: ProductService,
    private router: Router
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.productService.readById(id).subscribe(product => {
        this.product = product;
      });
    }
  }

  editProduct(): void {
    this.productService.update(this.product).subscribe(() => {
      this.router.navigate(['/products']);
      this.productService.showMessage("Produto atualizado com sucesso!")
    });
  }

  cancel(): void {
    this.router.navigate(['/products']);
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
