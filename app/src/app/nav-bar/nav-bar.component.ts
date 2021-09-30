import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {
  

  constructor(
    private alerts: ToastrService,
  ) { }

  ngOnInit() {
    this.alerts.success('Teste msg','Titulo', 
    {
      positionClass: 'toast-top-full-width',
      timeOut: 2000
    })
  }

}
