import { transition, trigger, useAnimation } from '@angular/animations';
import { Component } from '@angular/core';
import { bounce, shakeX, tada } from 'ng-animate';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    animations: [
      trigger("shake", [transition(":increment", useAnimation(shakeX, {
        params: {timing : 2.0}
      }))]),
      trigger("bounce", [transition(":increment", useAnimation(bounce, {
        params: {timing : 4.0}
      }))]),
      trigger("tada", [transition(":increment", useAnimation(tada, {
        params: {timing : 3.0}
      }))]),
    ],
    styleUrls: ['./app.component.css'],
    standalone: true
})
export class AppComponent {
  title = 'ngAnimations';
  
  tourner = false
  rouge = 0;
  vert = 0;
  bleu = 0

  constructor() {
  }

  animate(boucle : boolean){
    this.rouge++;
    setTimeout(()=>{
      this.vert++;
      setTimeout(()=>{
        this.bleu++
        setTimeout(()=>{
          if(boucle){
            this.animate(boucle);
          }
        },3000)
      },3000)
    },2000)
  }

  faireTourner(){
    
      this.tourner = true
        setTimeout(()=>{
          this.tourner=false
        },2000)
    
  }

 

}
