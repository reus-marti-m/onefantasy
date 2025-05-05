import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';

@Component({
  selector: 'app-notifications',
  imports: [
    CommonModule,
    MatListModule,
    MatIconModule,
    MatButtonModule
  ],
  templateUrl: './notifications.component.html',
  styleUrl: './notifications.component.scss'
})
export class NotificationsComponent {

  notifications = [
    { icon: 'schedule', text: 'Només queden dues hores per participar a l’Europa League.' },
    { icon: 'check', text: 'Resultats minijoc de LaLiga disponibles' },
    { icon: 'campaign', text: 'Nova participació oberta per Copa del Rei.' },
    { icon: 'check', text: 'Participació de LaLiga finalitzada: 18/60 punts.' }
  ];

  remove(index: number) {
    this.notifications.splice(index, 1);
  }

  clearAll() {
    this.notifications = [];
  }

}
