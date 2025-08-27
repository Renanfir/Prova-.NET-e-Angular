import { Component } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { FormsModule } from '@angular/forms';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { ToastModule } from 'primeng/toast';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterOutlet, ButtonModule, FormsModule, HttpClientModule, ToastModule],
  templateUrl: './home.html',
  styleUrl: './home.css'
})
export class Home {
  // protected readonly title = signal('front');

  nome = '';
  sobrenome = '';
  email = '';
  genero = '2';
  datanascimento = '';
  idusuario = 0;

  constructor(private router: Router, private http: HttpClient) {}

  excluirUsuario(){

    const id = Number(this.idusuario);

    this.http.delete(`https://localhost:7233/usuario/${id}`)
    .subscribe({
        next: (res) => {
          this.idusuario = 0;
        },
        error: (err) => {
          if (err.status === 400) {
            const msg = err.error as string;

            if(msg.includes('Usuário com ID')) {
              alert('Usuário não encontrado.');
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

  onSubmit() {
    const usuario = {
      nome: this.nome,
      sobrenome: this.sobrenome,
      email: this.email,
      genero: Number(this.genero),
      datanascimento: this.datanascimento
    };

    if(usuario.nome.trim() === '' || usuario.sobrenome.trim() === '' || usuario.email.trim() === '' || usuario.datanascimento.trim() === '') {
      alert('Por favor preencha todos os campos.');
      return;
    }
    

    this.http.post('https://localhost:7233/usuario', usuario)
      .subscribe({
        next: (res) => {
          console.log('Sucesso:', res)
          usuario.nome = '';
          usuario.sobrenome = '';
          usuario.email = '';
          usuario.genero = 0;
          usuario.datanascimento = '';
          this.router.navigate(['/endereco']);
        },

        error: (err) => {
          if (err.status === 400) {
            const msg = err.error as string;

            if(msg.includes('O email já está em uso.')) {
              alert('O email já está em uso.');
              return;
            }
            if(msg.includes('A data de nascimento não pode ser no futuro.')) {
              alert('A data de nascimento não pode ser no futuro.');
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
