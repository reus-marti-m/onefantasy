import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-public-leagues-list',
  standalone: true,
  imports: [CommonModule, MatIconModule],
  templateUrl: './public-list.component.html',
  styleUrls: ['../../common/list.component.scss']
})
export class PublicListComponent {
  
  constructor(private router: Router) { }

  onSelect(id: number) {
    this.router.navigate(['/app', 'leagues', id]);
  }

  officialLeagues = [
    {
      id: 1,
      name: 'Lliga oficial',
      positionLabel: '652è',
      competition: 'LaLiga',
      isFavorite: true,
      statusIcon: 'fiber_manual_record',
      statusClass: 'status-live',
      blink: true,
      points: 16,
      totalPoints: 60,
      matchdayLabel: '89è'
    },
    {
      id: 2,
      name: 'Lliga oficial',
      positionLabel: '1156è',
      competition: 'Champions',
      isFavorite: true,
      statusIcon: 'fiber_manual_record',
      statusClass: 'status-live',
      blink: true,
      points: 35,
      totalPoints: 60,
      matchdayLabel: '32è'
    },
    {
      id: 8,
      name: 'Lliga oficial',
      positionLabel: '45è',
      competition: 'Copa del Rei',
      isFavorite: true,
      statusIcon: 'fiber_manual_record',
      statusClass: 'status-live',
      blink: true,
      points: 60,
      totalPoints: 60,
      matchdayLabel: '1è'
    },
    {
      id: 9,
      name: 'Lliga oficial',
      positionLabel: '754è',
      competition: 'Premier',
      isFavorite: true,
      statusIcon: 'fiber_manual_record',
      statusClass: 'status-played',
      blink: false,
      points: 35,
      totalPoints: 60,
      matchdayLabel: '88è'
    },
    {
      id: 3,
      name: 'Katana del Rei',
      positionLabel: '2n',
      competition: 'Copa del Rei',
      isFavorite: false,
      statusIcon: 'fiber_manual_record',
      statusClass: 'status-played',
      blink: false,
      points: 80,
      totalPoints: 80,
      matchdayLabel: '1r'
    },
    {
      id: 10,
      name: 'FantasyOneFCB',
      positionLabel: '6è',
      competition: 'LaLiga',
      isFavorite: false,
      statusIcon: 'fiber_manual_record',
      statusClass: 'status-played',
      blink: false,
      points: 22,
      totalPoints: 60,
      matchdayLabel: '7è'
    }
  ];

  nonOfficialLeagues = [
    {
      id: 4,
      name: 'Fantasy Carrasco',
      positionLabel: '36è',
      competition: 'Multi',
      statusIcon: 'fiber_manual_record',
      statusClass: 'status-live',
      blink: true,
      points: 16,
      totalPoints: 60,
      matchdayLabel: '65è'
    },
    {
      id: 5,
      name: 'FutbolFantasy Esp',
      positionLabel: '87è',
      competition: 'LaLiga',
      statusIcon: 'fiber_manual_record',
      statusClass: 'status-live',
      blink: true,
      points: 16,
      totalPoints: 60,
      matchdayLabel: '154è'
    },
    {
      id: 6,
      name: 'EuroMallorca',
      positionLabel: '1r',
      competition: 'Europa',
      statusIcon: 'close',
      statusClass: 'status-not-sent',
      blink: false
    },
    {
      id: 7,
      name: 'Multi 07110',
      positionLabel: '89è',
      competition: 'Multi',
      statusIcon: 'fiber_manual_record',
      statusClass: 'status-played',
      blink: false,
      points: 80,
      totalPoints: 80,
      matchdayLabel: '1r'
    },
    {
      id: 11,
      name: 'Fantasy Carrasco',
      positionLabel: '36è',
      competition: 'Multi',
      statusIcon: 'fiber_manual_record',
      statusClass: 'status-live',
      blink: true,
      points: 16,
      totalPoints: 60,
      matchdayLabel: '65è'
    },
    {
      id: 12,
      name: 'FutbolFantasy Esp',
      positionLabel: '87è',
      competition: 'LaLiga',
      statusIcon: 'fiber_manual_record',
      statusClass: 'status-live',
      blink: true,
      points: 16,
      totalPoints: 60,
      matchdayLabel: '154è'
    },
    {
      id: 13,
      name: 'EuroMallorca',
      positionLabel: '1r',
      competition: 'Europa',
      statusIcon: 'close',
      statusClass: 'status-not-sent',
      blink: false
    },
    {
      id: 14,
      name: 'Multi 07110',
      positionLabel: '89è',
      competition: 'Multi',
      statusIcon: 'fiber_manual_record',
      statusClass: 'status-played',
      blink: false,
      points: 80,
      totalPoints: 80,
      matchdayLabel: '1r'
    }
  ];
}
