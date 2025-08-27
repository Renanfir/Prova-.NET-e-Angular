import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { FormsModule } from '@angular/forms';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { ToastModule } from 'primeng/toast';

@Component({
  selector: 'app-endereco',
  imports: [RouterOutlet, ButtonModule, FormsModule, HttpClientModule, ToastModule],
  templateUrl: './endereco.html',
  styleUrl: './endereco.css'
})



export class Endereco {
  idusuario = '';
  cep = '';
  estado = 'AC';
  rua = '';
  bairro = '';
  numero = '';
  complemento = '';

  

  constructor(private http: HttpClient) {}

limpaEndereco() {
  this.idusuario = '',
  this.cep = '',
  this.estado = 'AC',
  this.rua = '',
  this.bairro = '',
  this.numero = '',
  this.complemento = ''
}

buscarCep() {
  const cep = this.cep.replace(/\D/g, '');

  if (cep !== '') {
    const validacep = /^[0-9]{8}$/;

    if (validacep.test(cep)) {
      this.http.get<any>(`https://viacep.com.br/ws/${cep}/json/`)
        .subscribe({
          next: (data) => {
            if (!data.erro) {
              this.rua = data.logradouro;
              this.bairro = data.bairro;
              this.estado = data.uf;
            } else {
              alert('CEP não encontrado.');
              this.limpaEndereco();
            }
          },
          error: () => {
            alert('Erro ao consultar o CEP.');
            this.limpaEndereco();
          }
        });
    } else {
      alert('Formato de CEP inválido.');
      this.limpaEndereco();
    }
  }
}

  onSubmit() {
    if(Number(this.idusuario) === 0 || this.cep.trim() === '' || this.estado.trim() === '' || this.rua.trim() === '' || this.bairro.trim() === '' || Number(this.numero) === 0) {
      alert('Por favor preencha todos os campos.');
      return;
    }

    if(Number(this.numero) < 1 || Number(this.numero) % 1 !== 0) {
      alert('Por favor insira um número válido (maior que 0 e par')
      return;
    }

    const endereco = {
      idusuario: Number(this.idusuario),
      cep: this.cep,
      estado: this.estado,
      rua: this.rua,
      bairro: this.bairro,
      numero: Number(this.numero),
      complemento: this.complemento
    };

    this.http.post('https://localhost:7233/endereco', endereco)
      .subscribe({
        next: (res) => {
          console.log(res);   
          this.limpaEndereco();
        },
        error: (err) => {
          if (err.status === 400) {
            const msg = err.error as string;
              if(msg.includes('O id do usuario não existe')) {
              alert('O id do usuário não existe');
              return;
            }  
          }  
          if (err.status === 500) {
            alert('Erro no servidor. Tente novamente mais tarde.');
            return;
          }
        }
      });
  }
}
