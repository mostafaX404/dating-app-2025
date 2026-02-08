import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit, Signal, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-root',
  imports: [],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App  implements OnInit {
  
  private http = inject(HttpClient)
  protected readonly title = signal('Dating APP');
  protected members = signal<any>([]) ; 

  async ngOnInit(){
    this.members.set(await this.getMembers())
  }


 async getMembers (){
  try{
    return firstValueFrom(this.http.get("https://localhost:5001/api/members"))
  }catch(err){
    console.log(err)
    throw err;
  }
  }

}
