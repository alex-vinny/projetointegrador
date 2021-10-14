import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})
export class AuthComponent implements OnInit {

  constructor(
    private router: Router,
  ) { }

  ngOnInit() {
    this.avalAuth();
  }

  avalAuth(){
    if(sessionStorage.getItem('usuario') == null || sessionStorage.getItem('usuario') == ''){
      this.router.navigate(['login']);
    }
    else{
      this.router.navigate(['home']);
    }
  }
}
