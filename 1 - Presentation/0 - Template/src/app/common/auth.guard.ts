
import { map } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { ApiService } from './services/api.service';
declare var verificarAcesso: any;
@Injectable()
export class AuthGuard implements CanActivate {
  user: any = {};
  acessos = [''];
  ;
  constructor(private router: Router, private api: ApiService) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | boolean {

    let user = localStorage.getItem("userInfo")?.toString();

    //verifica a data de login

    setTimeout(() => {
      verificarAcesso = false;
    }, 2000);
    if (user == undefined || user == '') {
      this.router.navigateByUrl('/login/sign-in');
      return false;
    }
    else {
      this.user = JSON.parse(user);
      this.api.get('Membro', 'GetMemberUser', {}).subscribe(ret => {
        if (!ret || !ret.data) {
          localStorage.removeItem("userInfo");
          this.router.navigateByUrl('/login/sign-in');
        }

        if (ret.data.membroStatusId == 1) {
          if (route.url.length == 0 || (route.url[0].path != 'membro-pendente')) {
            this.router.navigateByUrl('/membro-pendente');
          }
        }
        else if (ret.data.membroStatusId == 4 || ret.data.membroStatusId == 5 || ret.data.membroStatusId == 3) {
          if (route.url.length == 0 || (route.url[0].path != 'ficha-membro' && route.url[0].path != 'reset-password')) {
            this.router.navigateByUrl('/ficha-membro/' + ret.data.usuarioId);
          }
          else if (ret.data.membroStatusId == 3) {

          }
        }
        else if (ret.data.usuario.mudarSenhaProximoLogin && (route.url.length == 0 || (route.url[0].path != 'ficha-membro' && route.url[0].path != 'reset-password'))) {
          this.router.navigateByUrl('/reset-password');
        } else if (route.url[0].path == 'membro-pendente') {
          this.router.navigateByUrl('/');
        }
      });

      console.log(route.url[0].path);

      // if (this.user.permissoes == 'AcessoAdminFull')
      //   return true;
      // else {

      //   let acessos = this.user.acessos;
      //   if (!acessos)
      //     return false;

      //   let achei: boolean = false;
      //   for (var i = 0; i < acessos.length; i++) { 

      //     if (acessos[i] == rota) {
      //       achei = true;
      //     }
      //   }

      //   if (achei)
      //     return true;
      //   else
      //     this.router.navigateByUrl('/unauthorized');
      // } 
    }

    return true;
  }





}
